using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLoadManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Stem1Clip;
    private string strFile;
    public Text logText;

    public IEnumerator LoadSound(string fileName)
    {
        printLog("start load " + fileName);

#if UNITY_EDITOR
        printLog("Unity Editor");
        //strFile = Application.dataPath + "/songs/" + fileName;
        strFile = "E:" + "/songs/" + fileName;
#elif UNITY_ANDROID
        printLog("Unity Android");
        strFile = Application.persistentDataPath + "/songs/" + fileName;
#endif

        printLog(strFile);
        WWW www = new WWW("file://" + strFile);
        yield return www; // code will wait till file is completely read


#if UNITY_EDITOR
        printLog("Unity Editor");
        audioSource.clip = www.GetAudioClip(false, false, AudioType.WAV);
#elif UNITY_ANDROID
        printLog("Unity Android");
        audioSource.clip = www.GetAudioClip(false, false);
#endif


        printLog("load complete");
        audioSource.Play();
    }

    public void printLog(string log)
    {
        logText.text += log + "\n";
    }
}

