using System;
using System.Collections;
using System.Collections.Generic;
using Snake.Consumables;
using Snake.Input;
using UnityEngine;

namespace Snake {
    public class Snake : MonoBehaviour {
        public static EventHandler OnChangeDirection;
        public static EventHandler<OnGrowEventHandlerArgs> OnGrow;

        public class OnGrowEventHandlerArgs {
            public int Size;
        }

        [SerializeField] private Transform tailPrefab;

        /// <summary>
        /// Laymask for collitions
        /// </summary>
        [SerializeField] private LayerMask hitLayerMask;

        /// <summary>
        /// Max interval for moving snake
        /// </summary>
        [SerializeField] private float movementTimerMax = .7f;

        /// <summary>
        /// Timer for moving the snake
        /// </summary>
        private float _movementTimer;


        /// <summary>
        /// Timer for changing the movement speed
        /// </summary>
        private float _periodicMovementIncreaseTimer;

        /// <summary>
        /// Max interval for changing the movement speed
        /// </summary>
        private const float PeriodicMovementIncreaseTimerMax = 1f;

        private const float MaxMovementSpeed = 0.1f;

        private int _bodyLength;
        private MoveDirection _moveDirection;
        private List<Vector3> snakeBodyPositionsList;

        /// <summary>
        /// Protection for fast direction change resulting in inversion of movement
        /// </summary>
        private bool _movedRecently = true;

        private void Awake() {
            _movementTimer = movementTimerMax;
            _moveDirection = MoveDirection.Up;
            snakeBodyPositionsList = new List<Vector3>();
            _bodyLength = 0;
        }


        // Start is called before the first frame update
        void Start() {
            GameInput.Instance.OnMoveLeft += OnMoveLeft;
            GameInput.Instance.OnMoveUp += OnMoveUp;
            GameInput.Instance.OnMoveDown += OnMoveDown;
            GameInput.Instance.OnMoveRight += OnMoveRight;
        }

        private void OnMoveUp(object sender, EventArgs e) {
            HandleDirectionChange(MoveDirection.Up, MoveDirection.Down);
        }

        private void OnMoveDown(object sender, EventArgs e) {
            HandleDirectionChange(MoveDirection.Down, MoveDirection.Up);
        }

        private void OnMoveRight(object sender, EventArgs e) {
            HandleDirectionChange(MoveDirection.Right, MoveDirection.Left);
        }

        private void OnMoveLeft(object sender, EventArgs e) {
            HandleDirectionChange(MoveDirection.Left, MoveDirection.Right);
        }

        private void HandleDirectionChange(MoveDirection newDirection, MoveDirection invalidReverseDirection) {
            if (_moveDirection == newDirection) {
                return;
            }

            if (_moveDirection == invalidReverseDirection) {
                return;
            }

            if (!_movedRecently) {
                return;
            }

            _movedRecently = false;
            _moveDirection = newDirection;
            OnChangeDirection?.Invoke(this, EventArgs.Empty);
        }

        // Update is called once per frame
        void Update() {
            HandlePeriodicMovementIncrease();
            HandleMovement();
        }

        private void HandlePeriodicMovementIncrease() {
            if (movementTimerMax <= MaxMovementSpeed) {
                return;
            }

            _periodicMovementIncreaseTimer -= Time.deltaTime;
            if (_periodicMovementIncreaseTimer <= 0) {
                _periodicMovementIncreaseTimer = PeriodicMovementIncreaseTimerMax;
                movementTimerMax -= Time.deltaTime;
            }
        }

        private void HandleMovement() {
            _movementTimer -= Time.deltaTime;
            if (_movementTimer <= 0) {
                _movementTimer = movementTimerMax;

                snakeBodyPositionsList.Insert(0, transform.position);


                var movementRotation = GetDirectionRotation(_moveDirection);

                transform.eulerAngles = movementRotation;


                Vector3 newMovementPosition;

                switch (_moveDirection) {
                    default:
                    case MoveDirection.Right:
                        newMovementPosition = new Vector3(1, 0f, 0);
                        break;
                    case MoveDirection.Left:
                        newMovementPosition = new Vector3(-1, 0f, 0);
                        break;
                    case MoveDirection.Up:
                        newMovementPosition = new Vector3(0f, 0f, 1);
                        break;
                    case MoveDirection.Down:
                        newMovementPosition = new Vector3(0f, 0f, -1);
                        break;
                }


                transform.position += newMovementPosition;

                _movedRecently = true;

                if (snakeBodyPositionsList.Count >= _bodyLength + 1) {
                    snakeBodyPositionsList.RemoveAt(snakeBodyPositionsList.Count - 1);
                }

                for (int i = 0; i < snakeBodyPositionsList.Count; i++) {
                    var snakeBody = snakeBodyPositionsList[i];
                    var newBody = Instantiate(tailPrefab, snakeBody, tailPrefab.rotation);
                    StartCoroutine(DestroyBody(newBody));
                }
            }
        }

        private IEnumerator DestroyBody(Transform newBody) {
            yield return new WaitForSeconds(movementTimerMax);
            Destroy(newBody.gameObject);
        }

        private Vector3 GetDirectionRotation(MoveDirection moveDirection) {
            Vector3 movementRotation;
            switch (moveDirection) {
                default:
                case MoveDirection.Right:
                    movementRotation = new Vector3(0, 90, 0);
                    break;
                case MoveDirection.Left:
                    movementRotation = new Vector3(0, -90, 0);
                    break;
                case MoveDirection.Up:
                    movementRotation = new Vector3(0, 0, 0);
                    break;
                case MoveDirection.Down:
                    movementRotation = new Vector3(0, 180, 0);
                    break;
            }

            return movementRotation;
        }


        private void OnTriggerEnter(Collider other) {
            var consumable = other.GetComponent<ConsumableController>();
            if (consumable != null) {
                consumable.Consume(this);
            }

            HandleSnakePartsCollisions(other);
        }

        private void HandleSnakePartsCollisions(Collider other) {
            var gameObjectLayer = other.gameObject.layer;
            var isOnHitLayerMask = hitLayerMask == (hitLayerMask | 1 << gameObjectLayer);
            if (!isOnHitLayerMask) {
                return;
            }

            Die();
        }

        private void Die() {
            GameManager.Instance.GameOver();
        }

        public void Grow(int amount) {
            _bodyLength += amount;
            OnGrow?.Invoke(this, new OnGrowEventHandlerArgs() {
                Size = _bodyLength
            });
        }

        public List<Vector3> GetFullSnakePositions() {
            List<Vector3> snakePositions = new List<Vector3>() {
                transform.position
            };
            snakePositions.AddRange(snakeBodyPositionsList);
            return snakePositions;
        }
    }

    public enum MoveDirection {
        Right,
        Left,
        Up,
        Down
    }
}