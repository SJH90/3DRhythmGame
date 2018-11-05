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
            Common.Log("LoadText Complete");
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
        dataPath = "D:" + "/songs/" + fileName + ".wav";
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + fileName + ".mp3";
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

    public string url;
    public IEnumerator LoadImage(string fileName)
    {
        fileName = Application.dataPath + "marker_copious.png";

#if UNITY_EDITOR
        Common.Log("Unity Editor");
        dataPath = Application.dataPath + "/songs/" + fileName;
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + fileName;
#endif


        url = Application.dataPath + "marker_copious.png";
        Debug.Log(url);
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);


        using (WWW www = new WWW(url))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            Sprite tempSprite = Sprite.Create(tex, new Rect(0, 0, tex.width / 5f, tex.height / 5f), new Vector2(.5f, .5f), tex.width / 5f);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

}
