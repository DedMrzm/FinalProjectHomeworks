using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");
            container.RegisterAsSingle(CreateSymbolsGenerator);
        }

        public static SymbolsGenerator CreateSymbolsGenerator(DIContainer c)
            => new SymbolsGenerator(c.Resolve<ConfigsProviderService>());
    }
}
