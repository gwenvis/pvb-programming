using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DN.Puzzle.Color.Editor
{
    public class InteractiveEditor
    {
        /// <summary>
        /// position data is used to easily show the position on the editor gui
        /// instead of in game.
        /// </summary>
        private struct PositionData<T>
        {
            public T Data { get; set; }
            public Vector2 Position { get; set; }
        }
        
        public object SelectObject { get; private set; }
        
        private static readonly UnityEngine.Color DARK_COLOR = new UnityEngine.Color(0.6f, 0.6f, 0.6f);
        private static readonly Rect CircleRect = new Rect(0, 0, 64, 64);
        
        private Vector2 scrollPosition;
        private Texture2D backgroundTexture;
        private List<PositionData<NodeData>> nodes;
        private List<LineData> lines;

        public InteractiveEditor()
        {
            var colorTexture = new Texture2D(32, 32);
            for (int i = 0; i < colorTexture.width; i++)
            {
                for (int j = 0; j < colorTexture.height; j++)
                {
                    colorTexture.SetPixel(i,j, DARK_COLOR);
                }
            }

            ColorPuzzleTextureData.Load();
            
            nodes = new List<PositionData<NodeData>>();
            lines = new List<LineData>();
            backgroundTexture = colorTexture;
        }

        public void Load(IEnumerable<NodeData> nodesData, IEnumerable<LineData> linesData)
        {
            
        }

        public void ProcessMouseEvent(Event @event)
        {
            
        }

        public void ProcessKeyEvent(Event @event)
        {

        }

        public void AddLine(NodeData from, NodeData to)
        {
            
        }

        public void AddNode(NodeData node)
        {
            
        }

        public void Draw(Rect rect)
        {
            GUIStyle scrollViewBackground = new GUIStyle {normal = {background = backgroundTexture}};

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition,
                true,
                true,
                GUI.skin.horizontalScrollbar,
                GUI.skin.verticalScrollbar,
                scrollViewBackground,
                GUILayout.Width(rect.width),
                GUILayout.Height(rect.height));

            foreach (var line in lines)
            {
                
            }

            foreach (PositionData<NodeData> node in nodes)
            {
                Rect position = new Rect(CircleRect);
                position.position = node.Position;
                EditorGUI.DrawTextureAlpha(position, ColorPuzzleTextureData.GetColorTexture(node.Data.IsFinish));
            }


            EditorGUILayout.EndScrollView();
        }
    }

    public static class ColorPuzzleTextureData
    {
        public static Texture FinishNode { get; private set; }
        public static Texture Node { get; private set; }
        
        public static Texture Arrow { get; private set; }
        
        public static void Load()
        {
            FinishNode = Resources.Load<Texture>("FinishNode");
            Node = Resources.Load<Texture>("Node");

            Arrow = Resources.Load<Texture>("Arrow");
        }

        public static Texture GetColorTexture(bool isFinish) => isFinish ? FinishNode : Node;
    }

    public static class EditorGUIDrawLineExtension
    {
        public static void DrawLine(Vector2 begin, Vector2 end, float width, UnityEngine.Color color, Texture2D texture)
        {
            Matrix4x4 matrix = GUI.matrix;
            UnityEngine.Color guiColor = GUI.color;
            
            GUI.matrix = Matrix4x4.identity;
            GUI.color = color;

            Vector2 offset = new Vector2(0, -1);
            
            offset += new Vector2(0, 0.5f);
            Quaternion guiRot = Quaternion.FromToRotation(Vector2.right, end - begin);
            Matrix4x4 guiRotMat = Matrix4x4.TRS(begin, guiRot, new Vector3((end - begin).magnitude, width, 1));
            Matrix4x4 guiTransMat = Matrix4x4.TRS(offset, Quaternion.identity, Vector3.one);
            Matrix4x4 guiTransMatInv = Matrix4x4.TRS(-offset, Quaternion.identity, Vector3.one);

            GUI.matrix = guiTransMatInv * guiRotMat * guiTransMat;

            GUI.DrawTexture(new Rect(0, 0, 1, 1), texture);

            GUI.matrix = matrix;
            GUI.color = color;
        }
    }
}