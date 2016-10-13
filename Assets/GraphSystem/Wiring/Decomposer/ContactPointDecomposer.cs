using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/ContactPoint")]
    public class ContactPointDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        private ContactPoint _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public ContactPoint input
        {
            set
            {
                if (!enabled)
                    return;

                _inputValue = value;
                _colliderEvent.Invoke(_inputValue.thisCollider);
                _otherColliderEvent.Invoke(_inputValue.otherCollider);
                _pointEvent.Invoke(_inputValue.point);
                _normalEvent.Invoke(_inputValue.normal);
            }
        }

        [SerializeField, Outlet]
        ColliderEvent _colliderEvent = new ColliderEvent();

        [SerializeField, Outlet]
        ColliderEvent _otherColliderEvent = new ColliderEvent();

        [SerializeField, Outlet]
        Vector3Event _pointEvent = new Vector3Event();

        [SerializeField, Outlet]
        Vector3Event _normalEvent = new Vector3Event();

        #endregion
    }
}