namespace Snake.Consumables {
    public class BigApple :IConsumable{
        public void Consume(Snake snake) {
            snake.Grow(2);
        }
    }
}