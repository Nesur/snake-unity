namespace Snake.Consumables {
    public class SmallApple : IConsumable {
        public void Consume(Snake snake) {
            snake.Grow(1);
        }
    }
}