using Klak.Wiring;
using UnityEngine;

namespace Assets.GraphSystem.Wiring.Action
{
    [NodeType("Action", "Instantiate")]
    [AddComponentMenu("Klak/Wiring/Action/Instantiate")]
    public class Instantiate : ActionNode
    {
        public GameObject prefab;
        [SerializeField] private Vector3 _position;

        [SerializeField, Outlet]
        private GameObjectEvent _instantiatedObjectEvent;
        [SerializeField, Outlet]
        private TransformEvent _instantiatedTransformEvent;

        [Inlet]
        public Vector3 Position
        {
            set
            {
                _position = value;
            }
        }

        protected override void InvokeEvents()
        {
            base.InvokeEvents();
            var obj = (GameObject)Instantiate(prefab, _position, Quaternion.identity);
            _instantiatedObjectEvent.Invoke(obj);
            _instantiatedTransformEvent.Invoke(obj.transform);
        }
    }
}