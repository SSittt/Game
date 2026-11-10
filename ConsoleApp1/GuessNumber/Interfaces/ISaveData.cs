using GuessNumber.Data;

namespace GameGuessNumber.GuessNumber.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с данными игры
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Сохранение информации об игроке
        /// </summary>
        /// <param name="player">имя игрока</param>
        void SaveSettings(PlayerInfo player);

        /// <summary>
        /// Загрузка информации об игроке
        /// </summary>
        /// <returns></returns>
        PlayerInfo LoadSettings();

        /// <summary>
        /// Сохранение результатов игр
        /// </summary>
        /// <param name="results"> результаты </param>
        void SaveResults(List<GameResult> results);

        /// <summary>
        /// Загрузка результатов игр
        /// </summary>
        /// <returns></returns>
        List<GameResult> LoadResults();
    }
}