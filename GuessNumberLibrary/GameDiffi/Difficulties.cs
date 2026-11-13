using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.GameModes;

namespace GameGuessNumber.GuessNumber.Difficulties
{
    /// <summary>
    /// Класс для хранения уровня сложности
    /// и создания соответствующего игрового режима.
    /// </summary>
    public class GameDifficulty : IDifficulty
    {
        // Название сложности
        public string Name { get; private set; }

        // Функция создания игрового режима
        private Func<IGameLevel> createGame;

        public GameDifficulty(string name, Func<IGameLevel> game)
        {
            Name = name;
            createGame = game;
        }

        // Создаём игровой режим
        public IGameLevel CreateGame()
        {
            return createGame();
        }
    }
}