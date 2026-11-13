using GuessNumber.Data;
using GameGuessNumber.GuessNumber.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameGuessNumber.GuessNumber.DataStorage
{
    /// <summary>
    /// Класс для сохранения и загрузки данных игры
    /// </summary>
    public class SaveData : IDataManager
    {
        /// <summary>
        /// Путь к папке, где будут храниться файлы с данными
        /// </summary>
        private string folderPath;
        /// <summary>
        /// Имя файла для сохранения информации об игроке
        /// </summary>
        private string playerInfoPath;
        /// <summary>
        /// Путь к файлу, в котором будут храниться результаты игр
        /// </summary>
        private string statisticsPath;

        /// <summary>
        /// Создаёт экземпляр класса SaveData и инициализирует пути к файлам для сохранения данных.
        /// </summary>
        public SaveData()
        {
         
            folderPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "DataFiles"
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            playerInfoPath = Path.Combine(folderPath, "playerInfo.json");
            statisticsPath = Path.Combine(folderPath, "statistics.json");

            if (!File.Exists(playerInfoPath))
            {
                File.WriteAllText(playerInfoPath, "{}");
            }

            if (!File.Exists(statisticsPath))
            {
                File.WriteAllText(statisticsPath, "[]");
            }
        }

        /// <summary>
        /// Сохранение информации об игроке
        /// </summary>
        public void SaveSettings(PlayerInfo player)
        {
            string json = JsonSerializer.Serialize(
                player,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(playerInfoPath, json);
        }

        /// <summary>
        /// Загрузка информации об игроке
        /// </summary>
        public PlayerInfo LoadSettings()
        {
            string json = File.ReadAllText(playerInfoPath);

            PlayerInfo player = JsonSerializer.Deserialize<PlayerInfo>(json);

            return player ?? new PlayerInfo();
        }

        /// <summary>
        /// Сохранение результатов игр
        /// </summary>
        public void SaveResults(List<GameResult> results)
        {
            string json = JsonSerializer.Serialize(
                results,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(statisticsPath, json);
        }

        /// <summary>
        /// Загрузка результатов игр
        /// </summary>
        public List<GameResult> LoadResults()
        {
            string json = File.ReadAllText(statisticsPath);

            List<GameResult> results =
                JsonSerializer.Deserialize<List<GameResult>>(json);

            return results ?? new List<GameResult>();
        }
    }
}
