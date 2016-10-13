using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/Rigidbody2D")]
    public class Rigidbody2DDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        public Rigidbody2D _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Rigidbody2D input
        {
            set
            {
                if (!enabled)
                    return;
                _inputValue = value;
                _velocityEvent.Invoke(_inputValue.velocity);
                _positionEvent.Invoke(_inputValue.position);
            }
        }

        [SerializeField, Outlet]
        Vector3Event _velocityEvent = new Vector3Event();
        [SerializeField, Outlet]
        Vector3Event _positionEvent = new Vector3Event();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}