using GuessNumber.Data;
using GameGuessNumber.GuessNumber.Interfaces;

namespace GameGuessNumber.GuessNumber.DataStorage
{
    public class Statistics : IStatistics
    {
        private IUserInterface userInterface;

        public Statistics(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        /// <summary>
        /// Показывает статистику игрока по его играм.
        /// </summary>
        /// <param name="results">Список результатов всех игр.</param>
        /// <param name="playerName">Имя игрока, для которого отображается статистика.</param>
        public void ShowStatistics(List<GameResult> results, string playerName)
        {
            userInterface.ShowMessage("\n===== СТАТИСТИКА ИГРОКА =====");
            userInterface.ShowMessage       ("Игрок: " + playerName);

            List<GameResult> playerResults = results.FindAll(
                result => result.PlayerName == playerName
            );

            if (playerResults.Count == 0)
            {
                userInterface.ShowMessage("У тебя пока нет сыгранных игр.");
                return;
            }

            int wins = 0;
            int losses = 0;

            foreach (GameResult result in playerResults)
            {
                if (result.IsWin)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }
            }

            int winPercent = wins * 100 / playerResults.Count;

            userInterface.ShowMessage("Всего игр: " + playerResults.Count);
            userInterface.ShowMessage("Побед: " + wins);
            userInterface.ShowMessage("Поражений: " + losses);
            userInterface.ShowMessage   ("Процент побед: " + winPercent + "%");

            userInterface.ShowMessage("\n===== ИСТОРИЯ ИГР =====");

            foreach (GameResult result in playerResults)
            {
                userInterface.ShowMessage("-------------------------");
                userInterface.ShowMessage("Уровень: " + result.Difficulty);
                userInterface.ShowMessage("Результат: " + (result.IsWin ? "Победа" : "Поражение"));
                userInterface.ShowMessage("Попытки: " + result.Attempts);
                userInterface.ShowMessage("Дата: " + result.Date);
            }
        }
    }
}
