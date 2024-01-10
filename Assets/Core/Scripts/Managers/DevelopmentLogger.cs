using TMPro;
using UnityEngine;

public class DevelopmentLogger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI logPrinterField;
    public static DevelopmentLogger Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    public void Print(string contant)
    {
        logPrinterField.SetText(contant);
    }
    public void AddText(string contant, string seperator)
    {
        logPrinterField.SetText(logPrinterField.text + seperator + contant);
    }
}
