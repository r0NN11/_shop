using UnityEngine;
using System;
using System.IO;
using _Core.Scripts.DataPersistence.Data;

namespace _Core.Scripts.DataPersistence
{
    public class FileDataHandler
    {
        private string _dataDirectoryPath = "";
        private string _dataFileName = "";

        public FileDataHandler(string dataDirectoryPath, string dataFileName)
        {
            _dataDirectoryPath = dataDirectoryPath;
            _dataFileName = dataFileName;
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirectoryPath, _dataFileName);
            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to load data from file" + fullPath + "\n" + e);
                }
            }

            return loadedData;
        }

        public void Save(GameData data)
        {
            string fullPath = Path.Combine(_dataDirectoryPath, _dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(data, true);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file" + fullPath + "\n" + e);
            }
        }
    }
}