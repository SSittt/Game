using System;

namespace GameGuessNumber.GuessNumber.Interfaces
{
    /// <summary>
    /// реализация интерфейса IUserInterface для взаимодействия с игроком через консоль
    /// </summary>
    public class ConsoleInterface : IUserInterface
    {
        /// <summary>
        /// Показать сообщение игроку
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// получает ввод от игрока
        /// </summary>
        /// <returns>введеный текст</returns>
        public string GetInput()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
