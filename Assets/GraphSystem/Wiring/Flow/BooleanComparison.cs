using UnityEngine;

namespace Assets.GraphSystem.Wiring.Flow
{
    [AddComponentMenu("Klak/Wiring/Comparison/Bool Comparison")]
    public class BooleanComparison : ComparisonNode<bool>
    {
        protected override bool Compare()
        {
            Debug.Log(_value1 + " " + _value2);
            return _value1 == _value2;
        }
    }
}