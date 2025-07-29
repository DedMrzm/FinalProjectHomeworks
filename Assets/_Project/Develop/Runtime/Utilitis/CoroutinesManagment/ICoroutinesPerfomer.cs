using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment
{
    public interface ICoroutinesPerfomer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}