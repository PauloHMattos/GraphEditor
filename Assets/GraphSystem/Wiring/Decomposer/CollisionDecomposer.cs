using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/Collision")]
    public class CollisionDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        public Collision _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Collision input
        {
            set
            {
                if (!enabled)
                    return;
                _inputValue = value;
                _colliderEvent.Invoke(_inputValue.collider);
                _gameObjectEvent.Invoke(_inputValue.gameObject);
                _transformEvent.Invoke(_inputValue.transform);
                _rigidbodyEvent.Invoke(_inputValue.rigidbody);
                _relativeVelocityEvent.Invoke(_inputValue.relativeVelocity);
                _impulseEvent.Invoke(_inputValue.impulse);
            }
        }

        [SerializeField, Outlet]
        ColliderEvent _colliderEvent = new ColliderEvent();

        [SerializeField, Outlet]
        GameObjectEvent _gameObjectEvent = new GameObjectEvent();

        [SerializeField, Outlet]
        TransformEvent _transformEvent = new TransformEvent();

        [SerializeField, Outlet]
        RigidbodyEvent _rigidbodyEvent = new RigidbodyEvent();

        [SerializeField, Outlet]
        Vector3Event _relativeVelocityEvent = new Vector3Event();

        [SerializeField, Outlet]
        Vector3Event _impulseEvent = new Vector3Event();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}