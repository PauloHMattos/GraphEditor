using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Decomposer/Vector3")]
    public class Vector3Decomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        public Vector3 _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Vector3 input
        {
            set
            {
                if (!enabled)
                    return;
                _inputValue = value;
                _x.Invoke(_inputValue.x);
                _y.Invoke(_inputValue.y);
                _z.Invoke(_inputValue.z);
            }
        }

        [SerializeField, Outlet]
        FloatEvent _x = new FloatEvent();

        [SerializeField, Outlet]
        FloatEvent _y = new FloatEvent();

        [SerializeField, Outlet]
        FloatEvent _z = new FloatEvent();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}