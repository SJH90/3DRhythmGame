using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject noteObject;
    public AudioSource audioSource;

    public float startTime;
    public float accurTime;
    public float playTime;

    public Text angleText;
    public Text speedText;
    public Text presetText;

    public Transform gameArea;

    public Transform laneObj1;
    public Transform laneObj2;
    public Transform laneObj3;
    public Transform laneObj4;

    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;

    public Material mGreat;
    public Material mGood;
    public Material mBad;


    private string text = "";

    void Start()
    {
        MusicData.Speed = 1;
        MusicData.Preset = -0.40f;
        speedText.text = "Speed " + String.Format("{0:F1}", MusicData.Speed);
        presetText.text = String.Format("{0:F2}", MusicData.Preset);

        ResetGame();
    }

    public void StopGame()
    {

    }
    public void ResetGame()
    {
        StopAllCoroutines();
        MusicData.Reset();

        foreach (Transform child in laneObj1)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in laneObj2)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in laneObj3)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in laneObj4)
        {
            GameObject.Destroy(child.gameObject);
        }

        startTime = 0;
    }

    public void StartGame()
    {
        ResetGame();
        StartCoroutine(LoadData());
    }

    public void getText(string text)
    {
        this.text = text;
    }
    // Use this for initialization
    IEnumerator LoadData()
    {
        string fileName = "test.txt";
        text = "";
        StartCoroutine(GetComponent<LoadManager>().LoadText(fileName));
        while ("".Equals(text))
        {
            yield return null;
        }

        ParseText(text);


        audioSource = GetComponent<AudioSource>();
        audioSource.clip = null;
        StartCoroutine(GetComponent<LoadManager>().LoadSound(MusicData.FileName));
        while (audioSource.clip == null)
        {
            yield return null;
        }

        Common.Log("start game1");
        GameStart();
    }

    void GameStart()
    {
        Common.Log("start game");

        for (int i = 0; i < MusicData.laneList1.Count; i++)
        {
            Vector3 originScale = noteObject.transform.localScale;
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj1;
            obj.transform.localScale = originScale;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            sc.manager = this;
            sc.time = MusicData.laneList1[i];
            sc.line = -1;
        }

        for (int i = 0; i < MusicData.laneList2.Count; i++)
        {
            Vector3 originScale = noteObject.transform.localScale;
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj2;
            obj.transform.localScale = originScale;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            sc.manager = this;
            sc.time = MusicData.laneList2[i];
            sc.line = 1;
        }

        for (int i = 0; i < MusicData.laneList3.Count; i++)
        {
            Vector3 originScale = noteObject.transform.localScale;
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj3;
            obj.transform.localScale = originScale;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            sc.manager = this;
            sc.time = MusicData.laneList3[i];
            sc.line = 1;
        }

        for (int i = 0; i < MusicData.laneList4.Count; i++)
        {
            Vector3 originScale = noteObject.transform.localScale;
            GameObject obj = Instantiate(noteObject);
            obj.transform.parent = laneObj4;
            obj.transform.localScale = originScale;
            obj.SetActive(true);
            NoteScript sc = obj.GetComponent<NoteScript>();
            sc.audioSource = audioSource;
            sc.manager = this;
            sc.time = MusicData.laneList4[i];
            sc.line = 1;
        }

        startTime = Time.time;
        Common.Log("start music");
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // SpeedUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // SpeedDown();
        }
        accurTime = ((Time.time - startTime));// + audioSource.time) / 2;
        //Debug.Log(audioSource.time);
    }

    public void SpeedUp()
    {
        MusicData.Speed = Mathf.Min(MusicData.Speed + 0.5f, 5f);
        speedText.text = "Speed " + String.Format("{0:F1}", MusicData.Speed);
    }
    public void SpeedDown()
    {
        MusicData.Speed = Mathf.Max(MusicData.Speed - 0.5f, 0.5f);
        speedText.text = "Speed " + String.Format("{0:F1}", MusicData.Speed);
    }
    public void AngleUp()
    {
        Vector3 origin = gameArea.rotation.eulerAngles;
        origin.x = Mathf.Min(Mathf.RoundToInt(origin.x + 5f), 50f);
        gameArea.rotation = Quaternion.Euler(origin);
        angleText.text = "Angle " + origin.x;
    }
    public void AngleDown()
    {
        Vector3 origin = gameArea.rotation.eulerAngles;
        origin.x = Mathf.Max(Mathf.RoundToInt(origin.x - 5f), 0f);
        gameArea.rotation = Quaternion.Euler(origin);
        angleText.text = "Angle " + origin.x;
    }
    public void PresetUp()
    {
        MusicData.Preset += 0.01f;
        presetText.text = String.Format("{0:F2}", MusicData.Preset);
    }
    public void PresetDown()
    {
        MusicData.Preset -= 0.01f;
        presetText.text = String.Format("{0:F2}", MusicData.Preset);
    }
    public void Button1()
    {
        ButtonAction(1);
    }
    public void Button2()
    {
        ButtonAction(2);
    }
    public void Button3()
    {
        ButtonAction(3);
    }
    public void Button4()
    {
        ButtonAction(4);
    }
    public void ButtonAction(int num)
    {
        Transform laneObj;
        GameObject effectObj;
        switch (num)
        {
            case 1:
                laneObj = laneObj1;
                effectObj = effect1;
                break;
            case 2:
                laneObj = laneObj2;
                effectObj = effect2;
                break;
            case 3:
                laneObj = laneObj3;
                effectObj = effect3;
                break;
            default:
                laneObj = laneObj4;
                effectObj = effect4;
                break;

        }

        if (laneObj.childCount > 0)
        {
            Transform note = laneObj.GetChild(0);
            NoteScript ns = note.GetComponent<NoteScript>();
            if (Mathf.Abs(ns.time - accurTime) < 0.1)
            {
                effectObj.GetComponent<EffectScript>().Show(mGreat);
                //Common.Log("Great" + (ns.time - accurTime));
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.2)
            {
                effectObj.GetComponent<EffectScript>().Show(mGood);
                //Common.Log("Good" + (ns.time - accurTime));
                Destroy(note.gameObject);
            }
            else if (Mathf.Abs(ns.time - accurTime) < 0.5)
            {
                effectObj.GetComponent<EffectScript>().Show(mBad);
                //Common.Log("Bad" + (ns.time - accurTime));
                Destroy(note.gameObject);
            }
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
                MusicData.Name = splitLine[1].Trim();
            }
            else if (stringLine[lineNum].Contains("#SOUND"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("sound : " + splitLine[1]);
                MusicData.FileName = splitLine[1].Trim();
            }
            else if (stringLine[lineNum].Contains("#BPM"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("bpm : " + splitLine[1]);
                MusicData.Bpm = float.Parse(splitLine[1].Trim());

            }
            else if (stringLine[lineNum].Contains("#PRESET"))
            {
                string[] splitLine = stringLine[lineNum].Split(' ');
                Common.Log("preset : " + splitLine[1]);
                //MusicData.Preset = float.Parse(splitLine[1].Trim());
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
                string noteLine3 = stringLine[lineNum + 3].Trim();
                string noteLine4 = stringLine[lineNum + 4].Trim();

                for (int i = 0; i < noteLine1.Length; i++)
                {
                    if (noteLine1[i] == '1' || noteLine1[i] == '2')
                    {
                        MusicData.addLane1(mNum + (float)i / noteLine1.Length);
                    }
                }
                for (int i = 0; i < noteLine2.Length; i++)
                {
                    if (noteLine2[i] == '1' || noteLine2[i] == '2')
                    {
                        MusicData.addLane2(mNum + (float)i / noteLine2.Length);
                    }
                }
                for (int i = 0; i < noteLine3.Length; i++)
                {
                    if (noteLine3[i] == '1' || noteLine3[i] == '2')
                    {
                        MusicData.addLane3(mNum + (float)i / noteLine3.Length);
                    }
                }
                for (int i = 0; i < noteLine4.Length; i++)
                {
                    if (noteLine4[i] == '1' || noteLine4[i] == '2')
                    {
                        MusicData.addLane4(mNum + (float)i / noteLine4.Length);
                    }
                }


                lineNum++;
                lineNum++;
            }


            lineNum++;
        }
    }


}
