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
        /// <summary>
        /// Название уровня сложности.
        /// </summary>
        public string Name => "Режим на время";

        /// <summary>
        /// Событие для отправки сообщений игроку.
        /// </summary>
        public event EventHandler<GameMessage>? GameEvent;

        /// <summary>
        /// Отправляет сообщение игроку через событие GameEvent.
        /// </summary>
        /// <param name="message">Сообщение для отправки игроку.</param>
        private void SendMessage(string message)
        {
            GameEvent?.Invoke(this, new GameMessage(message));
        }

        /// <summary>
        /// Показывает рисунок при победе.
        /// </summary>
        private void Winner()
        {
            SendMessage(@"

                    .-=========-.
                    \'-=======-'/
                    _|   WIN   |_
                   ((|   YOU   |))
                    \|   WON!  |/
                     \_________/
                      _|_____|_
                     /_________\

                     You're cool!
            ");
        }

        /// <summary>
        /// Запускает игру в режиме на время.
        /// Игрок должен угадать число от 1 до 100 за 60 секунд.
        /// За каждый неправильный ответ отнимается 5 секунд.
        /// </summary>
        /// <param name="playerName">Имя игрока.</param>
        /// <returns>Результат игры.</returns>
        public GameResult Play(string playerName)
        {
            Random random = new Random();

            // Загадываем число от 1 до 100.
            int secretNumber = random.Next(1, 101);

            int attempts = 0;
            int penalty = 0;
            bool isWin = false;

            // Запускаем таймер на 60 секунд.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SendMessage("Угадай число от 1 до 100!");
            SendMessage("У тебя есть 60 секунд.");
            SendMessage("За каждый неправильный ответ отнимается 5 секунд.");

            while (stopwatch.Elapsed.TotalSeconds + penalty < 60)
            {
                // Вычисляем оставшееся время.
                int remainingTime = 60 - (int)stopwatch.Elapsed.TotalSeconds - penalty;

                SendMessage("Осталось времени: " + remainingTime + " секунд.");
                SendMessage("Введи число от 1 до 100:");

                string? input = Console.ReadLine();

                // Проверяем, не закончилось ли время.
                if (stopwatch.Elapsed.TotalSeconds + penalty >= 60)
                {
                    break;
                }

                // Проверяем правильность введённого числа.
                if (!int.TryParse(input, out int guess) || guess < 1 || guess > 100)
                {
                    SendMessage("Пожалуйста, введи число от 1 до 100.");
                    continue;
                }

                attempts++;

                // Проверяем, угадал ли игрок число.
                if (guess == secretNumber)
                {
                    isWin = true;
                    SendMessage("Поздравляем! Ты угадал число!");
                    Winner();
                    break;
                }

                // Определяем, насколько близко игрок к загаданному числу.
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

                // За неправильный ответ отнимаем 5 секунд.
                penalty += 5;

                SendMessage("Неправильно! Минус 5 секунд.");
            }

            // Останавливаем таймер.
            stopwatch.Stop();

            // Если игрок не угадал число за отведённое время.
            if (!isWin)
            {
                SendMessage("Время вышло!");
                SendMessage("Загаданное число было: " + secretNumber);
                Loser();
            }

            // Возвращаем результат игры для сохранения статистики.
            return new GameResult
            {
                PlayerName = playerName,
                Difficulty = Name,
                IsWin = isWin,
                Attempts = attempts,
                Date = DateTime.Now
            };
        }

        /// <summary>
        /// Показывает рисунок при проигрыше с надписью "LOSER".
        /// </summary>
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
