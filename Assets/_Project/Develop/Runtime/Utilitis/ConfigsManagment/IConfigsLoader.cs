using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}