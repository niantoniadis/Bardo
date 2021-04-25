using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnim : MonoBehaviour
{
    string text = "...";
    Text componenet;
    // Start is called before the first frame update
    void Start()
    {
        componenet = GetComponent<Text>();
        StartCoroutine(DisplayOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplayOverTime()
    {
        for(int i = 0; i <= text.Length; i++)
        {
            componenet.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(DisplayOverTime());
    }
}
