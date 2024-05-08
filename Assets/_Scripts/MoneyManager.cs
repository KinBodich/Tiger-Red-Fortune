using Common.DataPersistence;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class MoneyManager : BaseSingleton<MoneyManager>, IDataPersistence
    {
        [field: SerializeField] public int MoneyCount { get; private set; }

        public event Action OnMoneyChange;

        public void ChangeMoney(int amount)
        {
            MoneyCount += amount;
            OnMoneyChange?.Invoke();
        }

        public void LoadData(GameData data)
        {
            MoneyCount = data.MoneyCount;
        }

        public void SaveData(GameData data)
        {
            data.MoneyCount = MoneyCount;
        }
    }
}