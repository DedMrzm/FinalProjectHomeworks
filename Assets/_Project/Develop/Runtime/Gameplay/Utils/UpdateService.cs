using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Utils
{
    public class UpdateService : MonoBehaviour
    {
        private List<IUpdatable> _updatables = new();
        private List<IUpdatable> _toAdd = new();
        private List<IUpdatable> _toRemove = new();

        public void Initialize(params IUpdatable[] updatables)
        {
            _updatables = updatables.ToList();
        }

        public void AddUpdatableService(IUpdatable updatable)
        {
            _toAdd.Add(updatable);
        }

        public void RemoveUpdatableService(IUpdatable updatable)
        {
            _toRemove.Add(updatable);
        }

        private void Update()
        {
            if(_toAdd.Count > 0)
            {
                _updatables.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if(_toRemove.Count > 0)
            {
                foreach(IUpdatable updatable in _toRemove)
                    _updatables.Remove(updatable);

                _toRemove.Clear();
            }

            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update(Time.deltaTime);
            }
        }
    }
}
