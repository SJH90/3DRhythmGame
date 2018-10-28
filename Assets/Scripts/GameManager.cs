using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /*
		텍스트 로드
		음악 로드

		텍스트 분석

		preset
		bpm
		노트

		1 일반노트
		2 롱노트

		sample.txt

		#BPM 120
		#PRESET 1000
		-----
		0
		0
		-----
		11
		0220
		-----
		
	 */

    public float bpm;
    public float preset;
    public GameObject beatLine;
    public AudioSource audio;
    public List<float> list;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        float time = (float)60 * 4 / 170;
        preset = 0.3f;
        list.Add(0 * time - preset);
        list.Add(1 * time - preset);
        list.Add(2 * time - preset);
        list.Add(3 * time - preset);
        list.Add(4 * time - preset);
        list.Add(5 * time - preset);
        list.Add(6 * time - preset);
        list.Add(7 * time - preset);
        list.Add(8 * time - preset);

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = Instantiate(beatLine);
            obj.transform.parent = transform.parent;
            obj.SetActive(true);
            LineScript sc = obj.GetComponent<LineScript>();
            sc.audio = audio;
            sc.time = list[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(audio.time);
    }
}
