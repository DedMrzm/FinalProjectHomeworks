using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment
{
    public class CoroutinesPerformer : MonoBehaviour, ICoroutinesPerfomer
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutineFunction)
            => StartCoroutine(coroutineFunction);

        public void StopPerform(Coroutine coroutine)
            => StopCoroutine(coroutine);
    }
}