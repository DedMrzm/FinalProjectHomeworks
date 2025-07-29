using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public class GameplayInputArgsConfig : ScriptableObject
    {
        [field: SerializeField] public GameModes GameMode { get; private set; }
    }
}
