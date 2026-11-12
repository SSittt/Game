
using GameGuessNumber.GuessNumber.Events;
using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.Data;
using System;
using System.Diagnostics;

namespace GuessNumber.GameModes
{
    /// <summary>
    /// Третий уровень сложности. Игрок должен угадать число
    /// от 1 до 100 за 60 секунд.
    /// За каждый неправильный ответ отнимается 5 секунд.
    /// </summary>
    public class TimeMode : IGameLevel
    {
        public string Name => "Режим на время";

        public event EventHandler<GameMessage>? GameEvent;

        private void SendMessage(string message)
        {
            GameEvent?.Invoke(this, new GameMessage(message));
        }

        public GameResult Play(string playerName)
        {
            Random random = new Random();

            // Загадываем число от 1 до 100
            int secretNumber = random.Next(1, 101);

            int attempts = 0;
            int penalty = 0;
            bool isWin = false;

            // Запускаем таймер на 60 секунд
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SendMessage("Угадай число от 1 до 100!");
            SendMessage("У тебя есть 60 секунд.");
            SendMessage("За каждый неправильный ответ отнимается 5 секунд.");

            while (stopwatch.Elapsed.TotalSeconds + penalty < 60)
            {
                // Вычисляем оставшееся время
                int remainingTime = 60 - (int)stopwatch.Elapsed.TotalSeconds - penalty;

                SendMessage("Осталось времени: " + remainingTime + " секунд.");
                SendMessage("Введи число от 1 до 100:");

                string? input = Console.ReadLine();

                // Проверяем, не закончилось ли время
                if (stopwatch.Elapsed.TotalSeconds + penalty >= 60)
                {
                    break;
                }

                // Проверяем введённое значение
                if (!int.TryParse(input, out int guess) || guess < 1 || guess > 100)
                {
                    SendMessage("Пожалуйста, введи число от 1 до 100.");
                    continue;
                }

                attempts++;

                // Проверяем, угадал ли игрок
                if (guess == secretNumber)
                {
                    isWin = true;
                    SendMessage("Поздравляем! Ты угадал число!");
                    break;
                }

                // Вычисляем расстояние до загаданного числа
                int difference = Math.Abs(secretNumber - guess);

                if (difference <= 10)
                {
                    SendMessage("Очень близко!");
                }
                else if (difference <= 30)
                {
                    SendMessage("Близко!");
                }
                else
                {
                    SendMessage("Далеко!");
                }

                // За неправильный ответ отнимаем 5 секунд
                penalty += 5;

                SendMessage("Неправильно! Минус 5 секунд.");
            }

            stopwatch.Stop();

            // Если игрок не успел угадать число
            if (!isWin)
            {
                SendMessage("Время вышло!");
                SendMessage("Загаданное число было: " + secretNumber);
                Loser();
            }

            // Возвращаем результат игры
            return new GameResult
            {
                PlayerName = playerName,
                Difficulty = Name,
                IsWin = isWin,
                Attempts = attempts,
                Date = DateTime.Now
            };
        }

        private void Loser()
        {
            SendMessage(@"

                ((((################
                ((((#((((((((((((((#
            ////##((#((((#((((#((((#
            ////#####((((#((((#((((#
              ((((###((##((((((##((#
                ((((#(((########(((#
                ((((#((((((((((((((#
                ((((################

                       L O S E R
                    H A H A H A !!!
            ");
        }
    }
}
