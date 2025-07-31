using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilitis.DataManagment
{
    public class PlayerData : ISaveData
    {
        public int CountOfWins;
        public int CountOfDefeats;
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}
