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
        static void Main(string[] args)
        {
            // Создаём объект для работы с файлами
            SaveData dataManager = new SaveData();

            // Загружаем информацию об игроке
            PlayerInfo player = dataManager.LoadSettings();

            Console.WriteLine("Игра «Угадай число»");
            Console.WriteLine();

            // Изменение имени игрока
            Console.WriteLine("Текущее имя: " + player.PlayerName);
            Console.Write("Введите новое имя или нажмите Enter: ");

            string name = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(name))
            {
                player.PlayerName = name;
            }

            // Выбор уровня сложности
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

            // Сохраняем настройки игрока
            dataManager.SaveSettings(player);

            Console.WriteLine("\nПривет, " + player.PlayerName + "!");
            Console.WriteLine("Текущий уровень: " + player.Difficulty);

            // Загружаем статистику
            List<GameResult> results = dataManager.LoadResults();

            // Создаём объект статистики
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

                        // Подписываемся на события игры
                        game.GameEvent += (sender, message) =>
                        {
                            Console.WriteLine(message.Message);
                        };

                        // Запускаем игру
                        GameResult result = game.Play(player.PlayerName);

                        // Добавляем результат в список
                        results.Add(result);

                        // Сохраняем статистику
                        dataManager.SaveResults(results);

                        Console.WriteLine("\nРезультат игры сохранён!");
                        break;

                    case "2":
                        // Показываем статистику
                        statistics.ShowStatistics(results);
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
