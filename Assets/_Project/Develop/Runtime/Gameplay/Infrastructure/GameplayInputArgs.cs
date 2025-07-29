using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameModes gameMode)
        {
            GameMode = gameMode;
        }

        public GameModes GameMode { get; }
    }
}
