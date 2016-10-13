using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/ContactPoint2D")]
    public class ContactPoint2DDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        private ContactPoint2D _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public ContactPoint2D input
        {
            set
            {
                if (!enabled)
                    return;

                _inputValue = value;
                _colliderEvent.Invoke(_inputValue.collider);
                _otherColliderEvent.Invoke(_inputValue.otherCollider);
                _pointEvent.Invoke(_inputValue.point);
                _normalEvent.Invoke(_inputValue.normal);
            }
        }

        [SerializeField, Outlet]
        Collider2DEvent _colliderEvent = new Collider2DEvent();

        [SerializeField, Outlet]
        Collider2DEvent _otherColliderEvent = new Collider2DEvent();

        [SerializeField, Outlet]
        Vector3Event _pointEvent = new Vector3Event();

        [SerializeField, Outlet]
        Vector3Event _normalEvent = new Vector3Event();

        #endregion
    }
}