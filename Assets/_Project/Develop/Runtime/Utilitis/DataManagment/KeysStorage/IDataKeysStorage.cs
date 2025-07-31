using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilitis.DataManagment.KeysStorage
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }

    public class MapDataKeyStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
            {typeof(PlayerData), "PlayerData" },

        };

        public string GetKeyFor<TData>() where TData : ISaveData //Реализовать этот интерфейс и добавить в Dictionary<Type, string> Keys
            => Keys[typeof(TData)];
    }
}
