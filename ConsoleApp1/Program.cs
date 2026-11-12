using System;
using System.Collections.Generic;
using GuessNumber.Data;
using GuessNumber.GameModes;
using GameGuessNumber.GuessNumber.DataStorage;
using GameGuessNumber.GuessNumber.Interfaces;

namespace GameGuessNumber
{
    internal class Program
    {
        /// <summary>
        /// программа "Угадай число" с сохранением настроек и статистики игрока.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            SaveData dataManager = new SaveData();

            PlayerInfo player = dataManager.LoadSettings();

            Console.WriteLine("Игра «Угадай число»");
            Console.WriteLine();

            Console.WriteLine("Текущее имя: " + player.PlayerName);
            Console.Write("Введите своё имя: ");

            string name = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(name))
            {
                player.PlayerName = name;
            }

            Console.WriteLine("\nВыбранный уровень: " + player.Difficulty);
            Console.WriteLine("Выберите уровень сложности:");
            Console.WriteLine("1. Сужение диапазона");
            Console.WriteLine("2. Горячо-холодно");
            Console.WriteLine("3. Режим на время");
            Console.WriteLine("Нажмите Enter, чтобы оставить прежний уровень.");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    player.Difficulty = "Сужение диапазона";
                    break;

                case "2":
                    player.Difficulty = "Горячо-холодно";
                    break;

                case "3":
                    player.Difficulty = "Режим на время";
                    break;

                case "":
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Оставлен прежний уровень.");
                    break;
            }


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
                        IGameLevel game;

                        if (player.Difficulty == "Сужение диапазона")
                        {
                            game = new NarrowRangeMode();
                        }
                        else if (player.Difficulty == "Горячо-холодно")
                        {
                            game = new HotColdMode();
                        }
                        else if (player.Difficulty == "Режим на время")
                        {
                            game = new TimeMode();
                        }
                        else
                        {
                            game = new NarrowRangeMode();
                        }

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
