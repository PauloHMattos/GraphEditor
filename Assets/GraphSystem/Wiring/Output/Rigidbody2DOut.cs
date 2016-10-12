using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Output", "Components", "Physics", "2D")]
    [AddComponentMenu("Klak/Wiring/Output/Component/Rigidbody2D Out")]
    public class Rigidbody2DOut : OutputNode
    {
        #region Editable properties

        [SerializeField]
        Rigidbody2D _targetRigidbody;

        [SerializeField]
        bool _addToOriginal = true;

        #endregion

        #region Node I/O

        [Inlet]
        public Vector3 velocity
        {
            set
            {
                if (!isNodeActive || _targetRigidbody == null)
                    return;
                _targetRigidbody.velocity = value;
            }
        }

        [Inlet]
        public Vector3 addForce
        {
            set
            {
                if (!isNodeActive || _targetRigidbody == null)
                    return;
                _targetRigidbody.AddForce(value);
            }
        }

        [Inlet]
        public float addTorque
        {
            set
            {
                if (!isNodeActive || _targetRigidbody == null)
                    return;
                _targetRigidbody.AddTorque(value);
            }
        }

        #endregion

        #region Private members

        #endregion
    }
}