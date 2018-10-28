using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    private string dataPath;
    private string text;

    public IEnumerator LoadText(string fileName)
    {

#if UNITY_EDITOR
        Common.Log("Unity Editor");
        dataPath = Application.dataPath + "/songs/" + fileName;
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + fileName;
#endif

        using (WWW www = new WWW("file://" + dataPath))
        {
            yield return www;
            GetComponent<GameManager>().getText(www.text);
        }
    }


    private AudioSource audioSource;

    public IEnumerator LoadSound(string fileName)
    {
        Common.Log("start load " + fileName);
        audioSource = GetComponent<AudioSource>();

#if UNITY_EDITOR
        Common.Log("Unity Editor");
        dataPath = "E:" + "/songs/" + fileName;
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + fileName;
#endif

        Common.Log(dataPath);
        WWW www = new WWW("file://" + dataPath);
        yield return www; // code will wait till file is completely read


#if UNITY_EDITOR
        Common.Log("Unity Editor");
        audioSource.clip = www.GetAudioClip(false, false, AudioType.WAV);
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        audioSource.clip = www.GetAudioClip(false, false);
#endif

        Common.Log("load complete");
        //audioSource.Play();
    }


}
