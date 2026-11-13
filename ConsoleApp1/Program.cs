using System;
using System.Collections.Generic;
using GuessNumber.Data;
using GuessNumber.GameModes;
using GameGuessNumber.GuessNumber.DataStorage;
using GameGuessNumber.GuessNumber.Interfaces;
using GameGuessNumber.GuessNumber.Difficulties;

namespace GameGuessNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SaveData dataManager = new SaveData();

            PlayerInfo player = dataManager.LoadSettings();

            // Создаём список уровней сложности
            List<IDifficulty> difficulties = new List<IDifficulty>
            {
                new GameDifficulty("Сужение диапазона",
                    () => new NarrowRangeMode()),
             
                new GameDifficulty("Горячо-холодно",
                    () => new HotColdMode()),

                new GameDifficulty("Режим на время",
                    () => new TimeMode())
            };

            // Ищем сохранённый уровень игрока
            IDifficulty selectedDifficulty = difficulties[0];

            foreach (IDifficulty difficulty in difficulties)
            {
                if (difficulty.Name == player.Difficulty)
                {
                    selectedDifficulty = difficulty;
                    break;
                }
            }

            Console.WriteLine("Игра «Угадай число»");
            Console.WriteLine();

            Console.WriteLine("Текущее имя: " + player.PlayerName);
            Console.Write("Введите своё имя: ");

            string name = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(name))
            {
                player.PlayerName = name;
            }

            Console.WriteLine("\nВыбранный уровень: " + selectedDifficulty.Name);
            Console.WriteLine("Выберите уровень сложности:");

            // Выводим все доступные уровни
            for (int i = 0; i < difficulties.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + difficulties[i].Name);
            }

            Console.WriteLine("Нажмите Enter, чтобы оставить прежний уровень.");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine() ?? "";

            if (choice != "")
            {
                if (int.TryParse(choice, out int number) &&
                    number >= 1 && number <= difficulties.Count)
                {
                    selectedDifficulty = difficulties[number - 1];
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Оставлен прежний уровень.");
                }
            }

            player.Difficulty = selectedDifficulty.Name;

            dataManager.SaveSettings(player);

            Console.WriteLine("\nПривет, " + player.PlayerName + "!");
            Console.WriteLine("Текущий уровень: " + player.Difficulty);

            List<GameResult> results = dataManager.LoadResults();

            Statistics statistics = new Statistics();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== МЕНЮ =====");
                Console.WriteLine("1. Начать игру");
                Console.WriteLine("2. Посмотреть статистику");
                Console.WriteLine("0. Выход");
                Console.Write("Выбери действие: ");

                choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":

                        // Создаём выбранный игровой режим
                        IGameLevel game = selectedDifficulty.CreateGame();

                        // Подписываемся на события игры
                        game.GameEvent += (sender, message) =>
                        {
                            Console.WriteLine(message.Message);
                        };

                        GameResult result = game.Play(player.PlayerName);

                        results.Add(result);

                        dataManager.SaveResults(results);

                        Console.WriteLine("\nРезультат игры сохранён!");
                        break;

                    case "2":

                        statistics.ShowStatistics(results, player.PlayerName);
                        break;

                    case "0":

                        exit = true;
                        Console.WriteLine("До свидания!");
                        break;

                    default:

                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
        }
    }
}
