using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeText : MonoBehaviour
{
    public TMP_Text myText;
    public float displayTime = 2.0f;


    void Start()
    {
        myText = GameObject.Find("TxtPanel").GetComponent<TMP_Text>();
    }

    public void ChangeText(string textReplace)
    {
        myText.text = textReplace;
        StartCoroutine(HideTextAfterDelay());
    }

    private IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        myText.text = "";
    }
}