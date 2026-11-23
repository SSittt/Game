using GuessNumber.Data;
using GuessNumber.GameModes;
using GameGuessNumber.GuessNumber.DataStorage;
using GameGuessNumber.GuessNumber.Interfaces;
using GameGuessNumber.GuessNumber.Difficulties;

namespace GameGuessNumber
{
     public class Program
    {
        static void Main(string[] args)
        {
            // Инициализация пользовательского интерфейса и менеджера данных
            IUserInterface userInterface = new ConsoleInterface();

            // Инициализация менеджера данных для сохранения и загрузки настроек и результатов
            IDataManager dataManager = new SaveData();

            // Загрузка настроек игрока
            PlayerInfo player = dataManager.LoadSettings();

            // Создаём список уровней сложности
            List<IDifficulty> difficulties = new List<IDifficulty>
            {
                new GameDifficulty("Сужение диапазона",
                    () => new NarrowRangeMode(userInterface)),
             
                new GameDifficulty("Горячо-холодно",
                    () => new HotColdMode(userInterface)),

                new GameDifficulty("Режим на время",
                    () => new TimeMode(userInterface))
            };

            // Ищем сохранённый уровень игрока
            IDifficulty selectedDifficulty = difficulties[0];

            foreach (IDifficulty difficulty in difficulties)
            {
                if (difficulty.Name == player.Difficulty)
                {
                    selectedDifficulty = difficulty;
                    break;
                }
            }

            userInterface.ShowMessage("Игра <<Угадай число>>");
            userInterface.ShowMessage("");

            userInterface.ShowMessage("Текущее имя:" + player.PlayerName);
            userInterface.ShowMessage("Введите новое имя (или нажмите Enter, чтобы оставить прежнее):");

            string name = userInterface.GetInput();

            if (!string.IsNullOrWhiteSpace(name))
            {
                player.PlayerName = name;
            }

            userInterface.ShowMessage("\nВыбранный уровень: " + selectedDifficulty.Name);
            userInterface.ShowMessage("Выберите уровень сложности:");

            // Выводим все доступные уровни
            for (int i = 0; i < difficulties.Count; i++)
            {
                userInterface.ShowMessage ((i + 1) + ". " + difficulties[i].Name);
            }

            userInterface.ShowMessage("Нажмите Enter, чтобы оставить прежний уровень.");
            userInterface.ShowMessage("Ваш выбор: ");

            string choice = userInterface.GetInput();

            if (choice != "")
            {
                if (int.TryParse(choice, out int number) &&
                    number >= 1 && number <= difficulties.Count)
                {
                    selectedDifficulty = difficulties[number - 1];
                }
                else
                {
                    userInterface.ShowMessage("Неверный выбор. Оставлен прежний уровень.");
                }
            }

            player.Difficulty = selectedDifficulty.Name;

            dataManager.SaveSettings(player);

            userInterface.ShowMessage("\nПривет, " + player.PlayerName + "!");
            userInterface.ShowMessage("Текущий уровень: " + player.Difficulty);

            List<GameResult> results = dataManager.LoadResults();

            IStatistics statistics = new Statistics(userInterface);

            bool exit = false;

            while (!exit)
            {
                userInterface.ShowMessage("\n===== МЕНЮ =====");
                userInterface.ShowMessage("1. Начать игру");
                userInterface.ShowMessage("2. Посмотреть статистику");
                userInterface.ShowMessage("0. Выход");
                userInterface.ShowMessage("Выбери действие: ");
                choice = userInterface.GetInput();

                switch (choice)
                {
                    case "1":

                        // Создаём выбранный игровой режим
                        IGameLevel game = selectedDifficulty.CreateGame();

                        // Подписываемся на события игры
                        game.GameEvent += (sender, message) =>
                        {
                            userInterface.ShowMessage(message.Message);
                        };

                        GameResult result = game.Play(player.PlayerName);

                        results.Add(result);

                        dataManager.SaveResults(results);

                        userInterface.ShowMessage("\nРезультат игры сохранён!");
                        break;

                    case "2":

                        statistics.ShowStatistics(results, player.PlayerName);
                        break;

                    case "0":

                        exit = true;
                        userInterface.ShowMessage("До свидания!");
                        break;

                    default:

                        userInterface.ShowMessage("Неверный пункт меню.");
                        break;
                }
            }
        }
    }
}
