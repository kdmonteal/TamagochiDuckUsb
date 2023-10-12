using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Diversion : MonoBehaviour
{
    public GameObject energyObject;
    public Slider slider;

    private Energia energyScript;
    GameObject statusManager;  
    ControlEstados controlEstados;
    private TextMeshProUGUI funText;

    [SerializeField] private float speed = 1f;
    private float currentValue = 100;
    private int delay; //espera x frames

    private const int WAITFRAMES = 10;
    private const float MINVALUE = 0;
    private const float MAXVALUE = 100;

    void Start()
    {
        statusManager = GameObject.Find("EventSystem");  
        controlEstados = statusManager.GetComponent<ControlEstados>();
        energyScript = energyObject.GetComponent<Energia>();
        funText = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        delay++;
        if(delay == WAITFRAMES)
        {
            checkLimits();
            delay = 0;            
        }
    }

    private void checkLimits()
    {
        if(controlEstados.isPlaying)
        {
            if(currentValue < MAXVALUE)
            {
                RecoverFun();
            }                   
        }
        else 
        {
            if(currentValue > MINVALUE)
            {
                reductionRate(StatusesMultiplier());
            }            
        }
    }

    private void reductionRate(float multiplier)//reduce la barra de diversion segun multiplicador
    {
        currentValue = currentValue - ((0.05f * speed) * multiplier);
        UpdateBar();
    }

    private float StatusesMultiplier(){ //lee que tanto le queda de energia
        float multiplier = 1.0f;
        /*if(energyScript.currentValue < 20){
            multiplier += 0.6f;
        }*/
        return multiplier; //devuelve un multiplicador
    }

    private void RecoverFun(){
        currentValue += ((0.05f * speed * 1.5f));
        UpdateBar();
    }


    private void UpdateBar(){
        //funText.text = (Mathf.Ceil(currentValue)).ToString();
        slider.value = ((Mathf.Ceil(currentValue)));

    }
}
