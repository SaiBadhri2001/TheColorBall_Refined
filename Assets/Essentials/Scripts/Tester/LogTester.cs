using UnityEngine;
using Essentails;
public class LogTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InteractiveLogger.Print("Hello I am Interactive Logger!!!", Color.green, TextType.Default);
        InteractiveLogger.Print("Hello am I Bold?!", Color.red, TextType.Bold);
        InteractiveLogger.Print("Wow, now I am Italic!!!", Color.magenta, TextType.Italic);
    }
}
