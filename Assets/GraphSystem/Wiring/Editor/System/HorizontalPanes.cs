using UnityEditor;
using UnityEngine;

namespace VNKit
{
    // Helper Rect extension methods
    public static class RectExtensions
    {
        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }

        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
    }


    public class EditorZoomArea
    {
        private const float kEditorWindowTabHeight = 21.0f;
        private static Matrix4x4 _prevGuiMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea, Vector2 maxSize)
        {
            GUI.EndGroup();
            // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(zoomScale, screenCoordsArea.TopLeft());
            clippedArea.y += kEditorWindowTabHeight;

            GUI.BeginGroup(clippedArea, EditorStyles.helpBox);
            
            _prevGuiMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            GUI.matrix = translation*scale*translation.inverse;
            return clippedArea;
        }

        public static void End()
        {
            GUI.matrix = _prevGuiMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }
    }
}

public static class GUIHelper
{
    // Don't create new instances every time we need an option -- just pre-create the permutations:
    public static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                  NoExpandWidth = GUILayout.ExpandWidth(false),
                                  ExpandHeight = GUILayout.ExpandHeight(true),
                                  NoExpandHeight = GUILayout.ExpandHeight(false);

    // Provided for consistency of interface, but not actually a savings/win:
    public static GUILayoutOption Width(float w) { return GUILayout.Width(w); }

    // Again, don't create instances when we don't need to:
    public static GUIStyle NoStyle = GUIStyle.none;
    public static GUIContent NoContent = GUIContent.none;
}

public static class GUIStyleExtensions
{
    public static GUIStyle NoBackgroundImages(this GUIStyle style)
    {
        style.normal.background =
          style.active.background =
          style.hover.background =
          style.focused.background =
          style.onNormal.background =
          style.onActive.background =
          style.onHover.background =
          style.onFocused.background =
          null;
        return style;
    }

    public static GUIStyle BaseTextColor(this GUIStyle style, Color c)
    {
        // *INDENT-OFF*
        style.normal.textColor =
          style.active.textColor =
          style.hover.textColor =
          style.focused.textColor =
          style.onNormal.textColor =
          style.onActive.textColor =
          style.onHover.textColor =
          style.onFocused.textColor =
            c;
        // *INDENT-ON*
        return style;
    }

    public static GUIStyle ResetBoxModel(this GUIStyle style)
    {
        style.border = new RectOffset();
        style.margin = new RectOffset();
        style.padding = new RectOffset();
        style.overflow = new RectOffset();
        style.contentOffset = Vector2.zero;

        return style;
    }

    public static GUIStyle Padding(this GUIStyle style, int left, int right, int top, int bottom)
    {
        style.padding = new RectOffset(left, right, top, bottom);

        return style;
    }

    public static GUIStyle Margin(this GUIStyle style, int left, int right, int top, int bottom)
    {
        style.margin = new RectOffset(left, right, top, bottom);

        return style;
    }

    public static GUIStyle Border(this GUIStyle style, int left, int right, int top, int bottom)
    {
        style.border = new RectOffset(left, right, top, bottom);

        return style;
    }

    public static GUIStyle Named(this GUIStyle style, string name)
    {
        style.name = name;

        return style;
    }

    public static GUIStyle ClipText(this GUIStyle style)
    {
        style.clipping = TextClipping.Clip;

        return style;
    }

    public static GUIStyle Size(this GUIStyle style, int width, int height, bool stretchWidth, bool stretchHeight)
    {
        style.fixedWidth = width;
        style.fixedHeight = height;
        style.stretchWidth = stretchWidth;
        style.stretchHeight = stretchHeight;

        return style;
    }
}

public class HorizontalPaneState
{
    public const int SPLITTER_WIDTH = 9;
    public int id = 0;
    public bool isDraggingSplitter = false,
                isPaneWidthChanged = false;
    public float leftPaneWidth = -1,
                 initialLeftPaneWidth = -1,
                 lastAvailableWidth = -1,
                 availableWidth = 0,
                 minPaneWidthLeft = 75,
                 minPaneWidthRight = 75;

    /*
    * Unity can, apparently, recycle state objects.  In that event we want to
    * wipe the slate clean and just start over to avoid wackiness.
    */
    protected virtual void Reset(int newId)
    {
        id = newId;
        isDraggingSplitter = false;
        isPaneWidthChanged = false;
        leftPaneWidth = -1;
        initialLeftPaneWidth = -1;
        lastAvailableWidth = -1;
        availableWidth = 0;
        minPaneWidthLeft = 75;
        minPaneWidthRight = 75;
    }

    /*
    * Some aspects of our state are really just static configuration that
    * shouldn't be modified by the control, so we blindly set them if we have a
    * prototype from which to do so.
    */
    protected virtual void InitFromPrototype(int newId, HorizontalPaneState prototype)
    {
        id = newId;
        initialLeftPaneWidth = prototype.initialLeftPaneWidth;
        minPaneWidthLeft = prototype.minPaneWidthLeft;
        minPaneWidthRight = prototype.minPaneWidthRight;
    }

    /*
    * This method takes care of guarding against state object recycling, and
    * ensures we pick up what we need, when we need to, from the prototype state
    * object.
    */
    public void ResolveStateToCurrentContext(int currentId, HorizontalPaneState prototype)
    {
        if (id != currentId)
            Reset(currentId);
        else if (prototype != null)
            InitFromPrototype(currentId, prototype);
    }
}
