using UnityEditor;
using UnityEngine;

namespace TheColorBall.Core
{
    [CustomEditor(typeof(GameManager))]
    public class SpawnLevel : Editor
    {
        public override void OnInspectorGUI()
        {
            GameManager gameManager = (GameManager)target;
            base.OnInspectorGUI();
            if (gameManager.isDebug)
            {
                if (GUILayout.Button("Spawn a Level"))
                {
                    gameManager.SpawnLevelEditor();
                }
                if (GUILayout.Button("Deform a Level"))
                {
                    gameManager.DeformLevelEditor();
                }
            }
        }
    }
}