using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagescript : MonoBehaviour
{
    public string fileName;
    public string url;
    public string dataPath;

    // Use this for initialization
    IEnumerator Start()
    {

        fileName = "marker_copious.png";

#if UNITY_EDITOR
        Common.Log("Unity Editor");
        dataPath = "D:/songs/" + fileName;
#elif UNITY_ANDROID
        Common.Log("Unity Android");
        dataPath = Application.persistentDataPath + "/songs/" + fileName;
#endif

        Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);


        using (WWW www = new WWW("file://" + dataPath))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            Rect rect = new Rect(0, 0, tex.width, tex.height);

            Rect rect2 = new Rect(tex.width / 5f * 2, tex.height / 5f * 2, tex.width / 5f, tex.height / 5f);
            Sprite tempSprite = Sprite.Create(tex, rect2, new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = tempSprite;
        }
    }
}
