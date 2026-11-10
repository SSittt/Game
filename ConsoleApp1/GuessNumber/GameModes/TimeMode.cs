using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.Data;
using GuessNumber.Events;
using System.Diagnostics;

namespace GuessNumber.GameModes
{
    /// <summary>
    /// Третий уровень сложности. Игрок должен угадать число
    /// за 60 секунд. За каждую ошибку отнимается 5 секунд.
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
        /// Отправляет сообщение игроку.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        private void SendMessage(string message)
        {
            GameEvent?.Invoke(this, new GameMessage(message));
        }

        /// <summary>
        /// Показывает рисунок при проигрыше.
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

        /// <summary>
        /// Запускает третий уровень сложности.
        /// </summary>
        /// <param name="playerName">Имя игрока.</param>
        /// <returns>Результат игры.</returns>
        public GameResult Play(string playerName)
        {
            Random random = new Random();

            int secretNumber = random.Next(1, 1001);
            int attempts = 0;
            int penalty = 0;
            bool isWin = false;

            Stopwatch stopwatch = new Stopwatch();

            SendMessage("Угадай число от 1 до 1000.");
            SendMessage("У тебя есть 60 секунд на игру.");
            SendMessage("За каждую ошибку отнимается 5 секунд.");

            stopwatch.Start();

            while (60 - stopwatch.Elapsed.TotalSeconds - penalty > 0)
            {
                double remainingTime = 60 - stopwatch.Elapsed.TotalSeconds - penalty;

                SendMessage($"Осталось времени: {Math.Ceiling(remainingTime)} секунд");
                SendMessage("Введи число:");

                string? input = Console.ReadLine();

                // Проверяем, не закончилось ли время во время ввода
                remainingTime = 60 - stopwatch.Elapsed.TotalSeconds - penalty;

                if (remainingTime <= 0)
                {
                    break;
                }

                if (!int.TryParse(input, out int guess) || guess < 1 || guess > 1000)
                {
                    SendMessage("Пожалуйста, введи число от 1 до 1000.");
                    continue;
                }

                attempts++;

                if (guess == secretNumber)
                {
                    isWin = true;
                    break;
                }
                else
                {
                    penalty += 5;
                    SendMessage("Неправильно! Минус 5 секунд.");
                }
            }

            stopwatch.Stop();

            if (isWin)
            {
                SendMessage($"Поздравляем, {playerName}! Ты угадал число {secretNumber} за {attempts} попыток!");
            }
            else
            {
                SendMessage($"К сожалению, {playerName}, время закончилось!");
                SendMessage($"Загаданное число было {secretNumber}.");
                Loser();
            }

            return new GameResult
            {
                PlayerName = playerName,
                Difficulty = Name,
                IsWin = isWin,
                Attempts = attempts,
                Date = DateTime.Now
            };
        }
    }
}
