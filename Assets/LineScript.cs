using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public AudioSource audio;
    public float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, (time - audio.time) * 5, 0);
        if (transform.position.y < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
