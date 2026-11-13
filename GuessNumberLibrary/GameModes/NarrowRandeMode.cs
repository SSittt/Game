using GameGuessNumber.GuessNumber.Events;
using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.Data;

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
        /// Запускает первый уровень сложности.
        /// </summary>
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
                if (max - min + 1 == 2)
                {
                    SendMessage("Ты уже очень близко к загаданному числу!");
                    SendMessage($"Попытка {attempts + 1}: осталось два варианта. Выбирай: {min} или {max}.");
                }
                else if (max - min + 1 == 1)
                {
                    SendMessage($"Попытка {attempts + 1}: осталось последнее число. Попробуй угадать!");
                }
                else
                {
                    SendMessage($"Попытка {attempts + 1}: введи число от {min} до {max}:");
                }

                string? input = Console.ReadLine();

                // Проверяем правильность введённого значения.
                if (!int.TryParse(input, out int guess) || guess < min || guess > max)
                {
                    if (max - min + 1 == 2)
                    {
                        SendMessage($"Выбери одно из двух чисел: {min} или {max}.");
                    }
                    else if (max - min + 1 == 1)
                    {
                        SendMessage("Попробуй ещё раз!");
                    }
                    else
                    {
                        SendMessage($"Пожалуйста, введи число от {min} до {max}.");
                    }

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
                else if (guess < secretNumber)
                {
                    min = guess + 1;
                }
                else
                {
                    max = guess - 1;
                }

                SendMessage("Внимание! Диапазон возможных чисел сузился.");

                if (max - min + 1 <= 2)
                {
                    SendMessage("Ты уже очень близко к загаданному числу!");
                }
                else
                {
                    SendMessage($"Новый диапазон: от {min} до {max}");
                }
            }

            // Если игрок не угадал число за 10 попыток.
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
