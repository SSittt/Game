using GameGuessNumber.GuessNumber.Events;
using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.Data;

namespace GuessNumber.GameModes
{
    /// <summary>
    /// Второй уровень сложности. Игрок должен угадать число,
    /// используя подсказки "горячо", "тепло" и "холодно".
    /// </summary>
    public class HotColdMode : IGameLevel
    {
        /// <summary>
        /// Название уровня сложности.
        /// </summary>
        public string Name => "Горячо-холодно";

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
        /// Запускает второй уровень сложности.
        /// </summary>
        /// <param name="playerName">Имя игрока.</param>
        /// <returns>Результат игры.</returns>
        public GameResult Play(string playerName)
        {
            Random random = new Random();

            int secretNumber = random.Next(1, 101);
            int attempts = 0;
            bool isWin = false;

            SendMessage("Угадай число от 1 до 100.");
            SendMessage("У тебя есть 7 попыток, чтобы угадать число.");

            while (attempts < 7)
            {
                SendMessage($"Попытка {attempts + 1}: введи число от 1 до 100:");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int guess) || guess < 1 || guess > 100)
                {
                    SendMessage("Пожалуйста, введи число от 1 до 100.");
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
                    int difference = Math.Abs(secretNumber - guess);

                    if (difference <= 5)
                    {
                        SendMessage("Горячо!");
                    }
                    else if (difference <= 15)
                    {
                        SendMessage("Тепло.");
                    }
                    else
                    {
                        SendMessage("Холодно.");
                    }
                }
            }

            if (isWin)
            {
                SendMessage($"Поздравляем, {playerName}! Ты угадал число {secretNumber} за {attempts} попыток.");
            }
            else
            {
                SendMessage($"К сожалению, {playerName}, ты не угадал число.");
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
