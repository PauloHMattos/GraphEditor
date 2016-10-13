using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Decomposer", "Bounds")]
    [AddComponentMenu("Klak/Wiring/Decomposer/Bounds")]
    public class BoundsDecomposer : DecomposerNode
    {
        #region Private members

        [SerializeField]
        public Bounds _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Bounds input
        {
            set
            {
                if (!enabled)
                    return;
                _inputValue = value;
                _sizeEvent.Invoke(_inputValue.size);
            }
        }

        [SerializeField, Outlet]
        Vector3Event _sizeEvent = new Vector3Event();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}