using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.Data;
using GuessNumber.Events;

namespace GuessNumber.GameModes
{
    /// <summary>
    /// Первый уровень сложности. После каждой неправильной
    /// попытки диапазон возможных чисел сужается.
    /// </summary>
    public class NarrowRangeMode : IGameLevel
    {
        /// <summary>
        /// Название уровня сложности.
        /// </summary>
        public string Name => "Сужение диапазона";

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
        /// Запускает первый уровень сложности.
        /// </summary>
        /// <param name="playerName">Имя игрока.</param>
        /// <returns>Результат игры.</returns>
        public GameResult Play(string playerName)
        {
            Random random = new Random();

            int secretNumber = random.Next(1, 101);
            int min = 1;
            int max = 100;
            int attempts = 0;
            bool isWin = false;

            SendMessage("Угадай число от 1 до 100.");
            SendMessage("У тебя есть 10 попыток, чтобы угадать число.");

            while (attempts < 10)
            {
                SendMessage($"Попытка {attempts + 1}: введи число от {min} до {max}:");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int guess) || guess < min || guess > max)
                {
                    SendMessage($"Пожалуйста, введи число от {min} до {max}.");
                    continue;
                }

                attempts++;

                if (guess == secretNumber)
                {
                    isWin = true;
                    SendMessage("Поздравляем! Ты угадал число!");
                    break;
                }
                else if (guess < secretNumber)
                {
                    min = guess + 1;
                }
                else
                {
                    max = guess - 1;
                }

                SendMessage("Внимание! Диапазон возможных чисел сузился.");
                SendMessage($"Новый диапазон: от {min} до {max}");
            }

            if (!isWin)
            {
                SendMessage($"Попытки закончились. Загаданное число: {secretNumber}");
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

