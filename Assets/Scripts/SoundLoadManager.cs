using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLoadManager : MonoBehaviour
{
    public Text text;
    public string dataPath;
    public AudioSource audioSource;

    public IEnumerator LoadSound(string fileName)
    {
        dataPath = Application.persistentDataPath + "/songs/" + fileName;
        Debug.Log(dataPath);
        audioSource = GetComponent<AudioSource>();

        using (WWW www = new WWW(dataPath))
        {
            yield return www;

            audioSource.clip = www.GetAudioClip();
            // www.GetAudioClip;

            if (audioSource.clip != null)
            {
                text.text = "yes";
                audioSource.Play();
            }
            else
            {

                text.text = "no";
            }
        }
    }


}
