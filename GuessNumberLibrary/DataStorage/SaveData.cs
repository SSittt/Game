using GuessNumber.Data;
using GameGuessNumber.GuessNumber.Interfaces;

namespace GameGuessNumber.GuessNumber.DataStorage
{
    /// <summary>
    /// Класс для сохранения и загрузки данных игры в текстовые файлы.
    /// </summary>
    public class SaveData : IDataManager
    {
        /// <summary>
        /// Путь к папке с файлами игры.
        /// </summary>
        private string folderPath;

        /// <summary>
        /// Путь к файлу с информацией об игроке.
        /// </summary>
        private string playerInfoPath;

        /// <summary>
        /// Путь к файлу со статистикой игр.
        /// </summary>
        private string statisticsPath;

        /// <summary>
        /// Создаёт папку и текстовые файлы, если их ещё нет.
        /// </summary>
        public SaveData()
        {
            folderPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "DataFiles"
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            playerInfoPath = Path.Combine(folderPath, "playerInfo.txt");
            statisticsPath = Path.Combine(folderPath, "statistics.txt");

            if (!File.Exists(playerInfoPath))
            {
                File.WriteAllText(playerInfoPath, "");
            }

            if (!File.Exists(statisticsPath))
            {
                File.WriteAllText(statisticsPath, "");
            }
        }

        /// <summary>
        /// Сохраняет информацию об игроке.
        /// </summary>
        /// <param name="player">Информация об игроке.</param>
        public void SaveSettings(PlayerInfo player)
        {
            // Записываем имя и уровень сложности в две строки.
            string[] lines =
            {
                player.PlayerName,
                player.Difficulty
            };

            File.WriteAllLines(playerInfoPath, lines);
        }

        /// <summary>
        /// Загружает информацию об игроке.
        /// </summary>
        /// <returns>Настройки игрока.</returns>
        public PlayerInfo LoadSettings()
        {
            string[] lines = File.ReadAllLines(playerInfoPath);

            // Если данных нет, создаём настройки по умолчанию.
            if (lines.Length < 2)
            {
                return new PlayerInfo();
            }

            PlayerInfo player = new PlayerInfo();

            player.PlayerName = lines[0];
            player.Difficulty = lines[1];

            return player;
        }

        /// <summary>
        /// Сохраняет результаты всех игр.
        /// </summary>
        /// <param name="results">Список результатов игр.</param>
        public void SaveResults(List<GameResult> results)
        {
            List<string> lines = new List<string>();

            foreach (GameResult result in results)
            {
                // Собираем данные одной игры в одну строку.
                string line =
                    result.PlayerName + ";" +
                    result.Difficulty + ";" +
                    result.IsWin + ";" +
                    result.Attempts + ";" +
                    result.Date.ToString("O");

                lines.Add(line);
            }

            File.WriteAllLines(statisticsPath, lines);
        }

        /// <summary>
        /// Загружает результаты всех игр.
        /// </summary>
        /// <returns>Список результатов игр.</returns>
        public List<GameResult> LoadResults()
        {
            List<GameResult> results = new List<GameResult>();

            string[] lines = File.ReadAllLines(statisticsPath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                // Проверяем, что в строке есть все пять значений.
                if (parts.Length == 5)
                {
                    GameResult result = new GameResult();

                    result.PlayerName = parts[0];
                    result.Difficulty = parts[1];
                    result.IsWin = bool.Parse(parts[2]);
                    result.Attempts = int.Parse(parts[3]);
                    result.Date = DateTime.Parse(parts[4]);

                    results.Add(result);
                }
            }

            return results;
        }
    }
}
