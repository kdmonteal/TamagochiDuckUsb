using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Limpieza : MonoBehaviour
{
    private TextMeshProUGUI cleannessText;
    GameObject statusManager;  
    public Slider slider;

    ControlEstados controlEstados;

    [SerializeField] private float speed = 1f;
    [HideInInspector] public float currentValue = 100;
    private int delay; //espera x frames

    private const int WAITFRAMES = 10;
    private const float MINVALUE = 0;
    private const float MAXVALUE = 100;

    void Start()
    {
        statusManager = GameObject.Find("EventSystem");  
        controlEstados = statusManager.GetComponent<ControlEstados>();
        cleannessText = GetComponent<TextMeshProUGUI>();
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
        if(controlEstados.isBathing)
        {
            if(currentValue < MAXVALUE)
            {
                RecoverCleanness();
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

    private void reductionRate(float multiplier) //reduce la barra de limpieza segun multiplicador
    {
        currentValue = currentValue - ((0.05f * speed) * multiplier);
        UpdateBar();
    }

    private float StatusesMultiplier(){ //lee que tanto le queda de hambre, diversion y sueño
        float multiplier = 1.0f;
        
        /*if(controlEstados.isPlaying){
            multiplier += 0.4f;
        }
        if(controlEstados.isSleeping){
            multiplier += 0.1f;
        }*/
        return multiplier; //devuelve un multiplicador
    }

    private void RecoverCleanness(){
        currentValue = currentValue + ((0.05f * speed * 1.5f));
        UpdateBar();
    }


    public void UpdateBar(){
        //cleannessText.text = (Mathf.Ceil(currentValue)).ToString();
        slider.value = ((Mathf.Ceil(currentValue)));
    }
}
