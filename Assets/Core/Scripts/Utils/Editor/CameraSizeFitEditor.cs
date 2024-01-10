using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TheColorBall.Utils
{
    [CustomEditor(typeof(CameraSizeFit))]
    public class CameraSizeFitEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CameraSizeFit cameraSizeFit = (CameraSizeFit)target;
            if (GUILayout.Button("Set Camera Size"))
            {
                cameraSizeFit.SetCameraSize();
            }
        }
    }

}