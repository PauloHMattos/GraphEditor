using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Decomposer", "Physics", "2D", "Collider")]
    [AddComponentMenu("Klak/Wiring/Decomposer/Collider2D")]
    public class Collider2DDecomposer : DecomposerNode
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
                _gameObject.Invoke(_inputValue.gameObject);
                _boundsEvent.Invoke(_inputValue.bounds);
            }
        }


        [SerializeField, Outlet]
        GameObjectEvent _gameObject = new GameObjectEvent();
        [SerializeField, Outlet]
        BoundsEvent _boundsEvent = new BoundsEvent();

        #endregion


        #region MonoBehaviour functions
        #endregion
    }
}