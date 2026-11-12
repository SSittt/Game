using GuessNumber.Data;
using System;
using System.Collections.Generic;

namespace GameGuessNumber.GuessNumber.DataStorage
{
    public class Statistics
    {
        /// <summary>
        /// Показывает статистику игрока по его играм.
        /// </summary>
        /// <param name="results">Список результатов всех игр.</param>
        /// <param name="playerName">Имя игрока, для которого отображается статистика.</param>
        public void ShowStatistics(List<GameResult> results, string playerName)
        {
            Console.WriteLine("\n===== СТАТИСТИКА ИГРОКА =====");
            Console.WriteLine("Игрок: " + playerName);

            List<GameResult> playerResults = results.FindAll(
                result => result.PlayerName == playerName
            );

            if (playerResults.Count == 0)
            {
                Console.WriteLine("У тебя пока нет сыгранных игр.");
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

            Console.WriteLine("Всего игр: " + playerResults.Count);
            Console.WriteLine("Побед: " + wins);
            Console.WriteLine("Поражений: " + losses);
            Console.WriteLine("Процент побед: " + winPercent + "%");

            Console.WriteLine("\n===== ИСТОРИЯ ИГР =====");

            foreach (GameResult result in playerResults)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Уровень: " + result.Difficulty);
                Console.WriteLine("Результат: " + (result.IsWin ? "Победа" : "Поражение"));
                Console.WriteLine("Попытки: " + result.Attempts);
                Console.WriteLine("Дата: " + result.Date);
            }
        }
    }
}
