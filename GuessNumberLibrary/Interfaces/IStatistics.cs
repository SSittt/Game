using GuessNumber.Data;

namespace GameGuessNumber.GuessNumber.Interfaces
{
    public interface IStatistics
    {
        /// <summary>
        /// показать статистику игрока по результатам игр
        /// </summary>
        /// <param name="results">список результатов игр</param>
        /// <param name="playerName">имя игрока</param>
        public void ShowStatistics(List<GameResult> results, string playerName);
    }
}
