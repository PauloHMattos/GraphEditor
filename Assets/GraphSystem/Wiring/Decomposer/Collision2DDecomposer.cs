using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/Collision2D")]
    public class Collision2DDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        public Collision2D _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Collision2D input
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
                _firstContactPoint2DEvent.Invoke(_inputValue.contacts[0]);
            }
        }

        [SerializeField, Outlet]
        Collider2DEvent _colliderEvent = new Collider2DEvent();

        [SerializeField, Outlet]
        GameObjectEvent _gameObjectEvent = new GameObjectEvent();

        [SerializeField, Outlet]
        TransformEvent _transformEvent = new TransformEvent();

        [SerializeField, Outlet]
        Rigidbody2DEvent _rigidbodyEvent = new Rigidbody2DEvent();

        [SerializeField, Outlet]
        Vector3Event _relativeVelocityEvent = new Vector3Event();


        [SerializeField, Outlet]
        ContactPoint2DEvent _firstContactPoint2DEvent = new ContactPoint2DEvent();


        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}