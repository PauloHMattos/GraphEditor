using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Decomposer", "Physics", "2D", "Collider")]
    [AddComponentMenu("Klak/Wiring/Decomposer/Collider2D")]
    public class Collider2DDecomposer : NodeBase
    {
        #region Private members

        [SerializeField]
        public Collider2D _inputValue;

        #endregion


        #region Node I/O

        [Inlet]
        public Collider2D input
        {
            set
            {
                if (!enabled)
                    return;
                _inputValue = value;
                _boundsEvent.Invoke(_inputValue.bounds);
            }
        }

        [SerializeField, Outlet]
        BoundsEvent _boundsEvent = new BoundsEvent();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}