using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgsConfig args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");

            SymbolsGenerator symbolsGenerator = new SymbolsGenerator(container, args.);
        }
    }
}
