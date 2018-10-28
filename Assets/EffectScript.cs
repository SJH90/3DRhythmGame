using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public void Show(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
