using System;
using Klak.Wiring;
using UnityEngine;

namespace Assets.GraphSystem.Wiring.Flow
{
    [AddComponentMenu("Klak/Wiring/Comparison/Float Comparison")]
    public class FloatComparison : ComparisonNode<float>
    {
        [SerializeField]
        private float _threshold;
        
        [Inlet]
        public float Threshold
        {
            set
            {
                _threshold = value;
            }
        }

        protected override bool Equal(float value1, float value2)
        {
            return Math.Abs(_value1 - _value2) <= _threshold;
        }
    }
}