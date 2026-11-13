namespace GuessNumber.Data
{
    /// <summary>
    /// класс дл€ хранение резульатат игры
    /// </summary>
    public class GameResult
    {
        /// <summary>
        ///  им€ игрока
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// выбранный уровень сложности
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// результат игры
        /// </summary>
        public bool IsWin { get; set; }
        /// <summary>
        /// кол-во использованыз попыток
        /// </summary>
        public int Attempts { get; set; }
        /// <summary>
        /// ƒата и врем€ заврешени€
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public GameResult()
        {
            PlayerName = "";
            Difficulty = "";
            IsWin = false;
            Attempts = 0;
            Date = DateTime.Now;
        }
    }
}