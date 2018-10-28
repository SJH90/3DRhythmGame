using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextLoadManager : MonoBehaviour
{

    public string dataPath;
    public Text pathText;
    public Text logText;


    public void printLog(string log)
    {
        logText.text += log + "\n";
    }

    IEnumerator Start()
    {

#if UNITY_EDITOR
        printLog("Unity Editor");
        dataPath = Application.dataPath + "/songs/" + "test.txt";
#elif UNITY_ANDROID
        printLog("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + "test.txt";
#endif
        pathText.text = dataPath;

        using (WWW www = new WWW("file://" + dataPath))
        {
            yield return www;
            //pathText.text = www.text;
            ParseText(www.text);

        }
    }

    public void ParseText(string text)
    {
        string[] stringLine = text.Split('\n');

        var lineCount = stringLine.Length;
        var lineNum = 0;
        var mNum = 0;

        while (lineNum < lineCount)
        {

            if (stringLine[lineNum].Contains("#NAME"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Debug.Log("name : " + splitLine[1]);

            }
            else if (stringLine[lineNum].Contains("#SOUND"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Debug.Log("sound : " + splitLine[1]);
                printLog(splitLine[1]);
                StartCoroutine(GetComponent<SoundLoadManager>().LoadSound(splitLine[1].Trim()));

            }
            else if (stringLine[lineNum].Contains("#BPM"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Debug.Log("bpm : " + splitLine[1]);

            }
            else if (stringLine[lineNum].Contains("#PRESET"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Debug.Log("preset : " + splitLine[1]);

            }
            else if (stringLine[lineNum].Contains("#DATA"))
            {
                Debug.Log("data");
                mNum = 0;
            }
            else if (stringLine[lineNum].Contains("#FIN"))
            {
                Debug.Log("fin");
                break;
            }
            else if (stringLine[lineNum].Contains("-----"))
            {
                string noteLine1 = stringLine[lineNum + 1].Trim();
                string noteLine2 = stringLine[lineNum + 2].Trim();
                Debug.Log("measure" + mNum++);
                Debug.Log("line1 : " + noteLine1);
                for (int i = 0; i < noteLine1.Length; i++)
                {
                    if (noteLine1[i] == '1' || noteLine1[i] == '2')
                    {
                        Debug.Log("note1 : " + (float)i / noteLine1.Length);
                    }
                }
                Debug.Log("line2 : " + noteLine2);
                for (int i = 0; i < noteLine2.Length; i++)
                {
                    if (noteLine2[i] == '1' || noteLine2[i] == '2')
                    {
                        Debug.Log("note2 : " + (float)i / noteLine2.Length);
                    }
                }


                lineNum++;
                lineNum++;
            }


            lineNum++;
        }
    }

}
