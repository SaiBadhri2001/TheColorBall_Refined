using System.Collections.Generic;
using UnityEngine;

namespace Essentails
{
    /// <summary>
    /// Details of custom design
    /// </summary>
    [System.Serializable]
    public class ColorDesign
    {
        [Tooltip("Rename gameObject begin with this keychar")]
        public string keyChar;
        [Tooltip("Don't forget to change alpha to 255")]
        public Color textColor = Color.white;
        [Tooltip("Don't forget to change alpha to 255")]
        public Color backgroundColor = Color.white;
        public TextAnchor textAlignment;
        public FontStyle fontStyle;
    }

    /// <summary>
    /// ScriptableObject:Save list of ColorDesign
    /// </summary>
    [CreateAssetMenu(fileName = "Color Palette", menuName = "Essentials / Color Palette")]
    public class ColorPalette : ScriptableObject
    {
        public List<ColorDesign> colorDesigns = new List<ColorDesign>();
    }
}