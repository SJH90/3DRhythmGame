using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLoadManager : MonoBehaviour
{

    public string dataPath;

    IEnumerator Start()
    {
        dataPath = Application.dataPath + "/test.txt";
        Debug.Log(dataPath);


        using (WWW www = new WWW(dataPath))
        {
            yield return www;
            ParseText(www.text);
        }
    }

    public void ParseText(string text)
    {
        Debug.Log(text);
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
            else if (stringLine[lineNum].Equals("-----"))
            {
                Debug.Log("line");
                // string noteLine1 = stringLine[lineNum + 1];
                // string noteLine2 = stringLine[lineNum + 2];
                // Debug.Log("measure" + mNum++);
                // Debug.Log("line1 : " + noteLine1);
                // Debug.Log("line2 : " + noteLine2);

                // lineNum++;
                // lineNum++;
            }


            lineNum++;
        }
    }

}
