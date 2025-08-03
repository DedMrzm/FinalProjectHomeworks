using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class UpdateService : MonoBehaviour
    {
        private List<IUpdatable> _updatables = new();

        public void Initialize(params IUpdatable[] updatables)
        {
            _updatables = updatables.ToList();
        }

        private void Update()
        {
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update(Time.deltaTime);
            }
        }
    }

    public interface IUpdatable
    {
        void Update(float deltaTime);
    }
}
