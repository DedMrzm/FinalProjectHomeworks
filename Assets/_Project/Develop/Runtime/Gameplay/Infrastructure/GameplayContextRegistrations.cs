using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private GameplayInputArgsConfig _mainConfig;

        public static void Process(DIContainer container, GameplayInputArgsConfig args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");

            SymbolsGenerator symbolsGenerator = new SymbolsGenerator(container, GameModes.Chars);

            container.RegisterAsSingle(CreateSymbolsGenerator);
        }

        private static SymbolsGenerator CreateSymbolsGenerator(DIContainer c)
           => new SymbolsGenerator(c, _mainConfig.GameMode);
    }
}
