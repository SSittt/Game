using GameGuessNumber.GuessNumber.Interfaces;
using GuessNumber.GameModes;

namespace GameGuessNumber.GuessNumber.Difficulties
{
    /// <summary>
    ///  ласс дл€ хранени€ уровн€ сложности
    /// и создани€ соответствующего игрового режима.
    /// </summary>
    public class GameDifficulty : IDifficulty
    {
        /// <summary>
        /// Ќазвание уровн€ сложности
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// ‘ункци€ дл€ создани€ игрового режима на основе выбранного уровн€ сложности.
        /// </summary>
        private Func<IGameLevel> createGame;

        /// <summary>
        /// —оздает новый уровень сложности с указанным названием и функцией создани€ игрового режима.
        /// </summary>
        /// <param name="name">Ќазвание уровн€ сложности</param>
        /// <param name="game">‘ункци€ дл€ создани€ игрового режима</param>
        public GameDifficulty(string name, Func<IGameLevel> game)
        {
            Name = name;
            createGame = game;
        }

        /// <summary>
        /// создает игровой режим на основе выбранного уровн€ сложности.
        /// </summary>
        /// <returns>созданный игровой режим</returns>
        public IGameLevel CreateGame()
        {
            return createGame();
        }
    }
}