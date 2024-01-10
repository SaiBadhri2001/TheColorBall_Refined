using UnityEditor;
using UnityEngine;

namespace Essentails
{
    [ExecuteInEditMode]
    [InitializeOnLoad]
    public class StyleHierarchy : EditorWindow
    {
        static ColorPalette colorPalette;
        static bool useStyleHierarchy = false;
        static GUIStyle labelStyle;
        [MenuItem("Essentials / Style Hierarchy / Settings")]
        public static void StyleEditorWindow()
        {
            StyleHierarchy styleHierarchy = CreateWindow<StyleHierarchy>();
            styleHierarchy.maxSize = new Vector2(330, 150);
            styleHierarchy.minSize = new Vector2(330, 150);
            styleHierarchy.Show();
        }
        static StyleHierarchy()
        {
            //make this false if you need to start the editor without the style hierarchy
            useStyleHierarchy = true;

            if (colorPalette)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
            }
            else
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindow;
            }
        }
        private void OnEnable()
        {
            if (useStyleHierarchy)
            {
                string colorPalettePath = FileSystem.ReadFromTextFile("SavedStyleHierarchyObjectPath", Application.dataPath);

                if (string.IsNullOrEmpty(colorPalettePath))
                {
                    FileSystem.CheckIfTextFileExistThenWrite("SavedStyleHierarchyObjectPath", Application.dataPath, AssetDatabase.GetAssetPath(colorPalette), false);
                    colorPalettePath = FileSystem.ReadFromTextFile("SavedStyleHierarchyObjectPath", Application.dataPath);
                }
                colorPalette = AssetDatabase.LoadAssetAtPath(colorPalettePath, typeof(ColorPalette)) as ColorPalette;
                if (!colorPalette)
                {
                    Debug.LogError("Cannot find the color palette asset in the path");
                    return;
                }
            }
        }
        private void OnGUI()
        {
            labelStyle = new(EditorStyles.boldLabel);
            labelStyle.alignment = TextAnchor.MiddleLeft;
            labelStyle.fontSize = 18;

            GUILayout.Label("ESSENTIALS", labelStyle);
            useStyleHierarchy = EditorGUILayout.Toggle("Use Style Hierarchy", useStyleHierarchy);

            if (useStyleHierarchy)
            {
                colorPalette = EditorGUILayout.ObjectField("Color Palette Reference", colorPalette, typeof(ColorPalette)) as ColorPalette;

                if (colorPalette)
                {
                    EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
                }
                else
                {
                    EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindow;
                    EditorGUILayout.HelpBox("Assign a color palette scriptable object", MessageType.Error);
                }
            }
            else
            {
                EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindow;
            }

            //save changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();

                if (colorPalette)
                {
                    FileSystem.CheckIfTextFileExistThenWrite("SavedStyleHierarchyObjectPath", Application.dataPath, AssetDatabase.GetAssetPath(colorPalette), false);
                }
            }
        }
        private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
        {
            if (colorPalette && useStyleHierarchy)
            {
                UnityEngine.Object instance = EditorUtility.InstanceIDToObject(instanceID);

                if (instance != null)
                {
                    for (int i = 0; i < colorPalette.colorDesigns.Count; i++)
                    {
                        var design = colorPalette.colorDesigns[i];

                        if (instance.name.StartsWith(design.keyChar))
                        {
                            string newName = instance.name.Substring(design.keyChar.Length);
                            EditorGUI.DrawRect(selectionRect, design.backgroundColor);
                            GUIStyle newStyle = new GUIStyle
                            {
                                alignment = design.textAlignment,
                                fontStyle = design.fontStyle,
                                normal = new GUIStyleState()
                                {
                                    textColor = design.textColor,
                                }
                            };
                            EditorGUI.LabelField(selectionRect, newName.ToUpper(), newStyle);
                        }
                    }
                }
            }
        }
    }
}