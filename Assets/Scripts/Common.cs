using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Common : MonoBehaviour
{
    public Text text;
    public static Text logText;
    public static int i = 1;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "";
        logText = text;
    }

    public static void Log(string str)
    {
        logText.text += i++ + " : " + str + "\n";
    }
}
