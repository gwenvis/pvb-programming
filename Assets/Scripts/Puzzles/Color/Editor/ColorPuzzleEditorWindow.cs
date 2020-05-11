using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DN.Puzzle.Color.Editor
{
    public class ColorPuzzleEditorWindow : EditorWindow
    {
        private static readonly Rect LeftRatio = new Rect(0, 0, 0.2f, 0.2f);
        private static readonly Rect RightRatio = new Rect(0.2f, 0f, 0.8f, 0.8f);

        private InteractiveEditor interactiveEditor;
        private ColorLevelData puzzleData;
        private bool isDirty;
        private bool loaded;
        private bool unsaved;

        private Event unityEvent;

        [MenuItem("Window/Color Puzzle Editor man")]
        private static void ShowWindow()
        {
            var window = GetWindow<ColorPuzzleEditorWindow>("Puzzle Editor");
            window.minSize = new Vector2(100,100);
            window.maxSize = new Vector2(2000, 2000);
            window.Show();
        }

        private void OnEnable()
        {
            interactiveEditor = new InteractiveEditor();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            // horizontal group
            Rect verticalRect = EditorGUILayout.BeginVertical(GUILayout.Width(LeftRatio.width * position.width),
                GUILayout.MinWidth(0),
                GUILayout.MaxWidth(1000));
            // group
            DrawDataOptions();
            DrawSelectedProperties();
            // end group
            EditorGUILayout.EndVertical();
            
            interactiveEditor.Draw(new Rect(RightRatio.x * position.width, RightRatio.y * position.height,
                RightRatio.width * position.width, RightRatio.height * position.height));
            // horizontal group end
            EditorGUILayout.EndHorizontal();
        }

        private void Update()
        {
            if (Event.current != null && !Event.current.Equals(unityEvent))
            {
                unityEvent = Event.current;

                if (unityEvent.isMouse)
                {
                    interactiveEditor.ProcessMouseEvent(unityEvent);
                }
                else if (unityEvent.isKey)
                {
                    interactiveEditor.ProcessKeyEvent(unityEvent);
                }
            }
        }

        private void DrawDataOptions()
        {
            var data = (ColorLevelData) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), puzzleData,
                typeof(ColorLevelData), false);

            isDirty = data != puzzleData;

            if (GUILayout.Button("Load"))
            {
                if (unsaved && loaded && !EditorUtility.DisplayDialog("Unsaved",
                    "The current puzzle data is unsaved. Are you sure you want to reload without saving?", "Yes",
                    "No"))
                {
                    return;
                }
                
                Load();
            }

            if (GUILayout.Button("Save"))
            {
                Save();
            }

            if (GUILayout.Button("Create New"))
            {
                if (loaded && unsaved)
                {
                    if (!EditorUtility.DisplayDialog("Unsaved",
                        "The current loaded puzzle data is unsaved. Exit without saving?", "Yes", " No"))
                    {
                        return;
                    }
                }

                ScriptableObject instance = CreateInstance(typeof(ColorLevelData));
                AssetDatabase.CreateAsset(instance, $"Assets/Data/ColorPuzzle{GUID.Generate().ToString()}.asset");
                AssetDatabase.SaveAssets();

                puzzleData = (ColorLevelData) instance;
                Selection.activeObject = instance;

                Load();
            }
        }

        private void DrawSelectedProperties()
        {
            
        }

        private void DrawToolButtons()
        {
            if (GUILayout.Button("Add Node"))
            {
                
            }
        }

        private void Load()
        {
            if (puzzleData == null)
            {
                Debug.LogError("Please assign something to load in first.");
                return;
            }

            loaded = true;
            unsaved = true;

            interactiveEditor.Load(puzzleData.Nodes, puzzleData.Lines);
        }

        private void Save()
        {
            if (puzzleData == null)
            {
                Debug.LogError("Please assign something to load first, before saving it.");
                return;
            }

            unsaved = false;
        }
    }
}