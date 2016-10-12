using Klak.Wiring;
using UnityEngine;

namespace Klak.SpecificNodes
{
    [NodeType("Game Specific Logic")]
    [AddComponentMenu("Klak/Wiring/SpecificNodes/Pong Hit Vector")]
    public class PongHitVector : NodeBase
    {
        [SerializeField] private Collision2D _inputValue;
        public Rigidbody2D _racketRight;
        public Rigidbody2D _racketLeft;

        [Inlet]
        public Collision2D Input
        {
            set
            {
                if (!enabled)
                    return;

                _inputValue = value;
                var ball = _inputValue.contacts[0].otherCollider;
                var racket = _inputValue.contacts[0].collider;
                if (racket.attachedRigidbody != _racketLeft && racket.attachedRigidbody != _racketRight)
                    return;

                var vector = new Vector2
                {
                    x = racket.attachedRigidbody == _racketLeft ? 1 : -1,
                    y = (ball.transform.position.y - racket.transform.position.y) / racket.bounds.size.y
                };
                _hitVectorEvent.Invoke(vector * 30);
            }
        }

        [Inlet]
        public Rigidbody2D racketRight
        {
            set
            {
                if (!enabled)
                    return;
                _racketRight = value;
            }
        }

        [Inlet]
        public Rigidbody2D racketLeft
        {
            set
            {
                if (!enabled)
                    return;
                _racketLeft = value;
            }
        }

        [SerializeField, Outlet]
        Vector3Event _hitVectorEvent = new Vector3Event();
    }
}