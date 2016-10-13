using Klak.Wiring;
using UnityEngine;

namespace Assets.GraphSystem.Wiring.Action
{
    [NodeType("Action", "Destroy")]
    [AddComponentMenu("Klak/Wiring/Action/Destroy")]
    public class Destroy : ActionNode
    {
        [SerializeField] private GameObject _objectInstance;

        [Inlet]
        public GameObject ObjectInstance
        {
            set
            {
                _objectInstance = value;
            }
        }

        protected override void InvokeEvents()
        {
            base.InvokeEvents();
            if(_objectInstance != null)
                Destroy(_objectInstance);
        }
    }
}