using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Decomposer", "GameObject")]
    [AddComponentMenu("Klak/Wiring/Decomposer/GameObject")]
    public class GameObjectDecomposer : DecomposerNode
    {
        #region Node I/O

        [Inlet]
        public GameObject input
        {
            set
            {
                if (!enabled)
                    return;
                _transformEvent.Invoke(value.transform);
                _rigidbodyEvent.Invoke(value.GetComponent<Rigidbody>());
                _rigidbody2DEvent.Invoke(value.GetComponent<Rigidbody2D>());
            }
        }

        [SerializeField, Outlet]
        TransformEvent _transformEvent = new TransformEvent();
        [SerializeField, Outlet]
        RigidbodyEvent _rigidbodyEvent = new RigidbodyEvent();
        [SerializeField, Outlet]
        Rigidbody2DEvent _rigidbody2DEvent = new Rigidbody2DEvent();

        #endregion
    }
}