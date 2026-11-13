using GameGuessNumber.GuessNumber.Interfaces;

namespace GameGuessNumber.GuessNumber.Interfaces
{
    /// <summary>
    /// Интерфейс для уровней сложности.
    /// </summary>
    public interface IDifficulty
    {
        /// <summary>
        /// Название уровня сложности
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Создание игрового режима
        /// </summary>
        /// <returns>Созданный игровой режим</returns>
        IGameLevel CreateGame();
    }
}