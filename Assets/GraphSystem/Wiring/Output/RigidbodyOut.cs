using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Output", "Components", "Physics", "3D")]
    [AddComponentMenu("Klak/Wiring/Output/Component/Rigidbody Out")]
    public class RigidbodyOut : NodeBase
    {
        #region Editable properties

        [SerializeField]
        Rigidbody _targetRigidbody;

        [SerializeField]
        bool _addToOriginal = true;

        #endregion

        #region Node I/O

        [Inlet]
        public Vector3 velocity
        {
            set
            {
                if (!enabled || _targetRigidbody == null) return;
                _targetRigidbody.velocity = value;
            }
        }

        #endregion

        #region Private members

        #endregion
    }
}