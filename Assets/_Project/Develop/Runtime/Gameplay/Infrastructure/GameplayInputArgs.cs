using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameModes pickedGameMode, int countOfGeneratedSymbols)
        {
            PickedGameMode = pickedGameMode;
            CountOfGeneratedSymbols = countOfGeneratedSymbols;
        }

        public GameModes PickedGameMode { get; }
        public int CountOfGeneratedSymbols { get; }
    }
}
