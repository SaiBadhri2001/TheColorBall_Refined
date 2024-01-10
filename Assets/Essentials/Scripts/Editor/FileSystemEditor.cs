using UnityEditor;
using UnityEngine;

namespace Essentails
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(FileSystemDriver))]
    public class FileSystemEditor : Editor
    {
        private SerializedProperty _isTextFileSerializedObject;
        private SerializedProperty _fileNameSerializedObject;
        private SerializedProperty _pathSerializedObject;
        private SerializedProperty _contentSerialziedObject;
        private SerializedProperty _fileFormatSerializedObject;

        bool _isDefaultPath;
        private void OnEnable()
        {
            _isTextFileSerializedObject = serializedObject.FindProperty("IsTxtFile");
            _fileNameSerializedObject = serializedObject.FindProperty("FileName");
            _pathSerializedObject = serializedObject.FindProperty("Path");
            _contentSerialziedObject = serializedObject.FindProperty("TextForFile");
            _fileFormatSerializedObject = serializedObject.FindProperty("FileFormat");
        }
        public override void OnInspectorGUI()
        {
            FileSystemDriver fileSystemTester = (FileSystemDriver)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_isTextFileSerializedObject);

            if (!fileSystemTester.IsTxtFile)
            {
                EditorGUILayout.PropertyField(_fileFormatSerializedObject);
            }
            else
            {
                fileSystemTester.FileFormat = ".txt";
            }

            EditorGUILayout.PropertyField(_fileNameSerializedObject);

            _isDefaultPath = EditorGUILayout.Toggle("Use Default Path", _isDefaultPath);

            if (_isDefaultPath)
            {
                fileSystemTester.Path = Application.persistentDataPath;
            }
            else
            {
                EditorGUILayout.PropertyField(_pathSerializedObject);
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_contentSerialziedObject);

            serializedObject.ApplyModifiedProperties();
        }
    }
}