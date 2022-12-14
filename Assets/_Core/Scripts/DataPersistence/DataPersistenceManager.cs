using System;
using _Core.Scripts.DataPersistence.Data;
using UnityEngine;

namespace _Core.Scripts.DataPersistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")] [SerializeField]
        private string _fileName;

        private GameData _gameData;
        private FileDataHandler _dataHandler;
        public static DataPersistenceManager instance { get; private set; }
        public GameData GetGameData() => _gameData;
        public static event Action OnUpdateGameData;

        public void UpdateGameData()
        {
            OnUpdateGameData?.Invoke();
        }

        private void Awake()
        {
            instance = this;
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private void NewGame()
        {
            _gameData = new GameData();
        }

        private void LoadGame()
        {
            _gameData = _dataHandler.Load();
            if (_gameData == null)
                NewGame();
        }

        private void SaveGame()
        {
            _dataHandler.Save(_gameData);
        }
    }
}