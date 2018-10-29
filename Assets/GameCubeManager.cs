using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCubeManager : MonoBehaviour
{
    public Sprite[] sprites;


    public GameObject noteObject;
    public AudioSource audioSource;
    public Music audioData;

    public float startTime;
    public float accurTime;

    public Text judgeText;
    public Transform laneObj1;
    public Transform laneObj2;

    public GameObject effect1;
    public GameObject effect2;

    public Material mGreat;
    public Material mGood;
    public Material mBad;


    private string text = "";

    public void getText(string text)
    {
        this.text = text;
    }
    // Use this for initialization
    IEnumerator Start()
    {
        string fileName = "test.txt";
        audioData = new Music();
        StartCoroutine(GetComponent<LoadManager>().LoadText(fileName));
        while ("".Equals(text))
        {
            yield return null;
        }
        ParseText(text);
        StartCoroutine(GetComponent<LoadManager>().LoadSound(audioData.FileName));
        audioSource = GetComponent<AudioSource>();
        while (audioSource.clip == null)
        {
            yield return null;
        }

        GameStart();
    }

    void GameStart()
    {
        Common.Log("start game");

        for (int i = 0; i < audioData.laneList1.Count; i++)
        {
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj1;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            // sc.manager = this;
            sc.time = audioData.laneList1[i];
            sc.line = -1;
        }

        for (int i = 0; i < audioData.laneList2.Count; i++)
        {
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj2;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            // sc.manager = this;
            sc.time = audioData.laneList2[i];
            sc.line = 1;
        }

        startTime = Time.time;
        Common.Log("start music");
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        accurTime = ((Time.time - startTime) + audioSource.time) / 2;
        //Debug.Log(audioSource.time);
    }

    public void Button1()
    {
        if (laneObj1.childCount > 0)
        {
            Transform note = laneObj1.GetChild(0);
            NoteScript ns = note.GetComponent<NoteScript>();
            judgeText.text += String.Format("{0:F2} \n", ns.time - accurTime);
            if (Mathf.Abs(ns.time - accurTime) < 0.1)
            {
                effect1.GetComponent<EffectScript>().Show(mGreat);
                Common.Log("Great");
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.2)
            {
                effect1.GetComponent<EffectScript>().Show(mGood);
                Common.Log("Good");
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.5)
            {
                effect1.GetComponent<EffectScript>().Show(mBad);
                Common.Log("Bad");
                Destroy(note.gameObject);
            }
        }
    }
    public void Button2()
    {
        if (laneObj2.childCount > 0)
        {
            Transform note = laneObj2.GetChild(0);
            NoteScript ns = note.GetComponent<NoteScript>();
            Material effectColor = null;
            judgeText.text += String.Format("{0:F2} \n", ns.time - accurTime);
            if (Mathf.Abs(ns.time - accurTime) < 0.1)
            {
                effectColor = mGreat;
                Common.Log("Great");
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.2)
            {
                effectColor = mGood;
                Common.Log("Good");
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.5)
            {
                effectColor = mBad;
                Common.Log("Bad");
                Destroy(note.gameObject);
            }
            effect2.GetComponent<EffectScript>().Show(effectColor);
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
                Common.Log("name : " + splitLine[1]);
                audioData.Name = splitLine[1].Trim();
            }
            else if (stringLine[lineNum].Contains("#SOUND"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("sound : " + splitLine[1]);
                audioData.FileName = splitLine[1].Trim();
            }
            else if (stringLine[lineNum].Contains("#BPM"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("bpm : " + splitLine[1]);
                audioData.Bpm = float.Parse(splitLine[1].Trim());

            }
            else if (stringLine[lineNum].Contains("#PRESET"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("preset : " + splitLine[1]);
                audioData.Preset = float.Parse(splitLine[1].Trim());

            }
            else if (stringLine[lineNum].Contains("#DATA"))
            {
                Common.Log("data");
                mNum = 0;
            }
            else if (stringLine[lineNum].Contains("#FIN"))
            {
                Common.Log("fin");
                break;
            }
            else if (stringLine[lineNum].Contains("-----"))
            {
                Common.Log("measure");
                mNum++;
                string noteLine1 = stringLine[lineNum + 1].Trim();
                string noteLine2 = stringLine[lineNum + 2].Trim();
                for (int i = 0; i < noteLine1.Length; i++)
                {
                    if (noteLine1[i] == '1' || noteLine1[i] == '2')
                    {
                        audioData.addLane1(mNum + (float)i / noteLine1.Length);
                    }
                }
                for (int i = 0; i < noteLine2.Length; i++)
                {
                    if (noteLine2[i] == '1' || noteLine2[i] == '2')
                    {
                        audioData.addLane2(mNum + (float)i / noteLine2.Length);
                    }
                }


                lineNum++;
                lineNum++;
            }


            lineNum++;
        }
    }


}