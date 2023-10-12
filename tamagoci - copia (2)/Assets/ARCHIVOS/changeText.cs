using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class changeText : MonoBehaviour
{

    public TMP_Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GameObject.Find("TxtPanel").GetComponent<TMP_Text>();
    }

    public void ChangeText(string textReplace)
    {
        myText.text = textReplace;
    }
}
