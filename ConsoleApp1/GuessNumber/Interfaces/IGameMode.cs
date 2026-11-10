using GuessNumber.Events;
using GuessNumber.Data;

namespace GameGuessNumber.GuessNumber.Interfaces
{
    /// <summary>
    /// »нтерфейс дл€ игровых уровней
    /// </summary>
    public interface IGameLevel
    {
        /// <summary>
        /// название уровн€
        /// </summary>
        string Name { get; }

        /// <summary>
        /// событие дл€ передачи сообщений во врем€ игры
        /// </summary>
        event EventHandler<GameMessage> GameEvent;

        /// <summary>
        /// запуск выбранного уровн€
        /// </summary>
        /// <param name="playerName"></им€ игрока>
        /// <returns> результат игрока </returns>
        GameResult Play(string playerName);
    }
}