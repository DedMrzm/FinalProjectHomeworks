using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Gameplay")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameModes GameMode { get; private set; }

        public void SetGameMode(GameModes gameMode)
        {
            GameMode = gameMode;
        }
    }
}
