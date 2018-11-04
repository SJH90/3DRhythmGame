using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public AudioSource audioSource;
    public GameManager manager;
    public float time;
    public int line;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, (time - manager.accurTime) * 10 * MusicData.Speed, 0);
        if (manager.accurTime > time + 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
