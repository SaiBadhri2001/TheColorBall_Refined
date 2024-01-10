using UnityEngine;
using System.Diagnostics;
namespace Essentails
{
    public sealed class InteractiveLogger
    {
        public const string LOGGER_SYMBOL = "ENABLE_LOG";
        [Conditional(LOGGER_SYMBOL)]
        public static void Print(object content, Color color, TextType type = TextType.Default)
        {
            switch (type)
            {
                case TextType.Default:
                    {
                        UnityEngine.Debug.Log(GetFormatedString(content, color));
                        break;
                    }
                case TextType.Bold:
                    {
                        UnityEngine.Debug.Log("<b>" + GetFormatedString(content, color) + "</b>");
                        break;
                    }
                case TextType.Italic:
                    {
                        UnityEngine.Debug.Log("<i>" + GetFormatedString(content, color) + "</i>");
                        break;
                    }
            }
        }
        private static string GetFormatedString(object content, Color color)
        {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), content);
        }
        [Conditional(LOGGER_SYMBOL)]
        public static void Print(string content)
        {
            UnityEngine.Debug.Log(content);
        }
        [Conditional(LOGGER_SYMBOL)]
        public static void Print(object content, string hexCode)
        {
            if (hexCode.Contains("#"))
            {
                UnityEngine.Debug.Log("<color=#" + hexCode + ">" + content + "</color>");
            }
            else
            {
                UnityEngine.Debug.Log("<color=" + hexCode + ">" + content + "</color>");
            }
        }
        [Conditional(LOGGER_SYMBOL)]
        public static void Print(object content)
        {
            UnityEngine.Debug.Log(content);
        }
        [Conditional(LOGGER_SYMBOL)]
        public static void PrintErrors(object content)
        {
            UnityEngine.Debug.LogError(content);
        }
    }
    public enum TextType
    {
        Bold,
        Italic,
        Default
    }
}
