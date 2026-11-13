using System;

namespace GameGuessNumber.GuessNumber.Events
{
    /// <summary>
    /// класс для хранения сообщения игры
    /// </summary>
    public class GameMessage : EventArgs
    {
        /// <summary>
        /// текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// конструктор для создания сообщения
        /// </summary>
        /// <param name="message">сообщение</param>
        public GameMessage(string message)
        {
            Message = message;
        }
    }
}