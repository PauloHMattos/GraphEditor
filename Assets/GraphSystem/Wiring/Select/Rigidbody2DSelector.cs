using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Selector")]
    [AddComponentMenu("Klak/Wiring/Selector/Rigidbody2D")]
    public class Rigidbody2DSelector : NodeBase
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField, Outlet]
        Rigidbody2DEvent _valueEvent = new Rigidbody2DEvent();

        void Start()
        {
            if(_rigidbody != null)
                _valueEvent.Invoke(_rigidbody);
        }
    }
}
