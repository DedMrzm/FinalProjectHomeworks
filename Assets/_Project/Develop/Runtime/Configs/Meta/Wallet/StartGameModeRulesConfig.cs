using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewGameModeRulesConfig", fileName = "StartGameModeRulesConfig")]
    internal class StartGameModeRulesConfig : ScriptableObject
    {
        [field: SerializeField] public int CostOfReset { get; private set; }
        [field: SerializeField] public int PrizeForWin { get; private set; }
        [field: SerializeField] public int CostOfLose { get; private set; }

    }
}
