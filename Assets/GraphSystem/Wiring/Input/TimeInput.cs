using Klak.Math;
using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Input/Time Input")]
    public class TimeInput : NodeBase
    {
        public enum TimeType
        {
            DeltaTime,
            FixedDeltaTime,
            FixedTime,
            MaximumDeltaTime,
            RealtimeSinceStartup,
            CaptureFramerate,
            FrameCount,
            RenderedFramecount,
            SmoothDeltaTime,
            Time,
            TimeScale,
            TimeSinceLevelLoad,
            UnscaledDeltaTime,
            UnscaledTime
        }
        #region Editable properties

        [SerializeField] private TimeType _timeType = TimeType.DeltaTime;
        
        [SerializeField]
        FloatInterpolator.Config _interpolator;

        #endregion

        #region Node I/O

        [SerializeField, Outlet]
        FloatEvent _valueEvent = new FloatEvent();
        
        #endregion

        #region MonoBehaviour functions

        FloatInterpolator _timeValue;

        void Start()
        {
            _timeValue = new FloatInterpolator(0, _interpolator);
        }

        void Update()
        {
            switch (_timeType)
            {
                case TimeType.DeltaTime:
                    _timeValue.targetValue = Time.deltaTime;
                    break;
                case TimeType.Time:
                    _timeValue.targetValue = Time.time;
                    break;
                case TimeType.FixedDeltaTime:
                    _timeValue.targetValue = Time.fixedDeltaTime;
                    break;
                case TimeType.CaptureFramerate:
                    _timeValue.targetValue = Time.captureFramerate;
                    break;
                case TimeType.FixedTime:
                    _timeValue.targetValue = Time.fixedTime;
                    break;
                case TimeType.FrameCount:
                    _timeValue.targetValue = Time.frameCount;
                    break;
                case TimeType.MaximumDeltaTime:
                    _timeValue.targetValue = Time.maximumDeltaTime;
                    break;
                case TimeType.RealtimeSinceStartup:
                    _timeValue.targetValue = Time.realtimeSinceStartup;
                    break;
                case TimeType.RenderedFramecount:
                    _timeValue.targetValue = Time.renderedFrameCount;
                    break;
                case TimeType.SmoothDeltaTime:
                    _timeValue.targetValue = Time.smoothDeltaTime;
                    break;
                case TimeType.TimeSinceLevelLoad:
                    _timeValue.targetValue = Time.timeSinceLevelLoad;
                    break;
                case TimeType.UnscaledDeltaTime:
                    _timeValue.targetValue = Time.unscaledDeltaTime;
                    break;
                case TimeType.UnscaledTime:
                    _timeValue.targetValue = Time.unscaledTime;
                    break;
            }
            _valueEvent.Invoke(_timeValue.Step());
        }

        #endregion
    }
}