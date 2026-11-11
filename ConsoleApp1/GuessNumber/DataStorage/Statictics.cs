using GuessNumber.Data;
using System;
using System.Collections.Generic;

namespace GameGuessNumber.GuessNumber.DataStorage
{
    /// <summary>
    /// Класс для отображения статистики игр
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// Вывод результатов всех игр
        /// </summary>
        public void ShowStatistics(List<GameResult> results)
        {
            Console.WriteLine("\n СТАТИСТИКА ИГР ");

            if (results.Count == 0)
            {
                Console.WriteLine("Пока игр нет.");
                return;
            }

            foreach (GameResult result in results)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Игрок: " + result.PlayerName);
                Console.WriteLine("Уровень: " + result.Difficulty);
                Console.WriteLine("Результат: " + (result.IsWin ? "Победа" : "Поражение"));
                Console.WriteLine("Попытки: " + result.Attempts);
                Console.WriteLine("Дата: " + result.Date);
            }
        }
    }
}
