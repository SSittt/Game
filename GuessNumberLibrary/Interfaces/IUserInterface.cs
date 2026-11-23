namespace GameGuessNumber.GuessNumber.Interfaces
{
    /// <summary>
    /// интерфейс для взаимодействие с игроком 
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Показать сообщение игроку
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void ShowMessage(string message);

        /// <summary>
        /// получает ввод от игрока
        /// </summary>
        /// <returns>введеный текст</returns>
        string GetInput();
    }

    public interface Interface1
    {
    }
}