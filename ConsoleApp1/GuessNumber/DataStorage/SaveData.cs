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
        private string folderPath;
        private string playerInfoPath;
        private string statisticsPath;

        public SaveData()
        {
            // Путь к папке с файлами
            folderPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "DataFiles"
            );

            // Создаём папку, если её нет
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Пути к JSON-файлам
            playerInfoPath = Path.Combine(folderPath, "playerInfo.json");
            statisticsPath = Path.Combine(folderPath, "statistics.json");

            // Создаём файлы, если их нет
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
