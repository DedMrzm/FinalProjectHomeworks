using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    [CreateAssetMenu(fileName = "GameModeConfig", menuName = "Gameplay")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameModes GameMode { get; private set; }
    }
}
