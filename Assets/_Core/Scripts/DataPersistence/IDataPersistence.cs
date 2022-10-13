using _Core.Scripts.DataPersistence.Data;
using UnityEngine;

namespace _Core.Scripts.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(GameData data);
    }
}