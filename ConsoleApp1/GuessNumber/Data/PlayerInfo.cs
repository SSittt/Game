namespace GuessNumber.Data
{
    /// <summary>
    /// Класс для хранения информации об игроке
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Имя игрока по умолчанию
        /// </summary>
        public string PlayerName { get; set; } = "Игрок";

        /// <summary>
        /// Сохранённый уровень сложности
        /// </summary>
        public string Difficulty { get; set; } = "Сужение диапазона";
    }
}
