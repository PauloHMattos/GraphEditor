//using UnityEditor;
//using UnityEngine;
//using VNKit;

//public class ZoomTestWindow : EditorWindow
//{
//    [MenuItem("Window/Zoom Test")]
//    private static void Init()
//    {
//        EditorWindow.GetWindow<ZoomTestWindow>("Zoom Test").Show();
//    }

//    private const float kZoomMin = 0.1f;
//    private const float kZoomMax = 10.0f;

//    private readonly Rect _zoomArea = new Rect(0.0f, 75.0f, 600.0f, 300.0f - 100.0f);
//    private float _zoom = 1.0f;
//    private Vector2 _zoomCoordsOrigin = Vector2.zero;

//    private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
//    {
//        return (screenCoords - _zoomArea.TopLeft()) / _zoom + _zoomCoordsOrigin;
//    }

//    private void DrawZoomArea()
//    {
//        // Within the zoom area all coordinates are relative to the top left corner of the zoom area
//        // with the width and height being scaled versions of the original/unzoomed area's width and height.
//        EditorZoomArea.Begin(_zoom, _zoomArea);

//        GUI.Box(new Rect(0.0f - _zoomCoordsOrigin.x, 0.0f - _zoomCoordsOrigin.y, 100.0f, 25.0f), "Zoomed Box");

//        // You can also use GUILayout inside the zoomed area.
//        GUILayout.BeginArea(new Rect(300.0f - _zoomCoordsOrigin.x, 70.0f - _zoomCoordsOrigin.y, 130.0f, 50.0f));
//        GUILayout.Button("Zoomed Button 1");
//        GUILayout.Button("Zoomed Button 2");
//        GUILayout.EndArea();

//        EditorZoomArea.End();
//    }

//    private void DrawNonZoomArea()
//    {
//        GUI.Box(new Rect(0.0f, 0.0f, 600.0f, 50.0f), "Adjust zoom of middle box with slider or mouse wheel.\nMove zoom area dragging with middle mouse button or Alt+left mouse button.");
//        _zoom = EditorGUI.Slider(new Rect(0.0f, 50.0f, 600.0f, 25.0f), _zoom, kZoomMin, kZoomMax);
//        GUI.Box(new Rect(0.0f, 300.0f - 25.0f, 600.0f, 25.0f), "Unzoomed Box");
//    }

//    private void HandleEvents()
//    {
//        // Allow adjusting the zoom with the mouse wheel as well. In this case, use the mouse coordinates
//        // as the zoom center instead of the top left corner of the zoom area. This is achieved by
//        // maintaining an origin that is used as offset when drawing any GUI elements in the zoom area.
//        if (Event.current.type == EventType.ScrollWheel)
//        {
//            Vector2 screenCoordsMousePos = Event.current.mousePosition;
//            Vector2 delta = Event.current.delta;
//            Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
//            float zoomDelta = -delta.y / 150.0f;
//            float oldZoom = _zoom;
//            _zoom += zoomDelta;
//            _zoom = Mathf.Clamp(_zoom, kZoomMin, kZoomMax);
//            _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) - (oldZoom / _zoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);

//            Event.current.Use();
//        }

//        // Allow moving the zoom area's origin by dragging with the middle mouse button or dragging
//        // with the left mouse button with Alt pressed.
//        if (Event.current.type == EventType.MouseDrag &&
//            (Event.current.button == 0 && Event.current.modifiers == EventModifiers.Alt) ||
//            Event.current.button == 2)
//        {
//            Vector2 delta = Event.current.delta;
//            delta /= _zoom;
//            _zoomCoordsOrigin += delta;

//            Event.current.Use();
//        }
//    }

//    public void OnGUI()
//    {
//        HandleEvents();

//        // The zoom area clipping is sometimes not fully confined to the passed in rectangle. At certain
//        // zoom levels you will get a line of pixels rendered outside of the passed in area because of
//        // floating point imprecision in the scaling. Therefore, it is recommended to draw the zoom
//        // area first and then draw everything else so that there is no undesired overlap.
//        DrawZoomArea();
//        DrawNonZoomArea();
//    }
//}