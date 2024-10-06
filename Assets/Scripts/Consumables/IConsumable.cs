namespace Snake.Consumables {
    public interface IConsumable {
        void Consume(Snake snake);

        public static IConsumable CreateDefault() {
            return new SmallApple();
        }
    }
}