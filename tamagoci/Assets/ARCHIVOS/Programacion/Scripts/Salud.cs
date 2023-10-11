using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Salud : MonoBehaviour
{
    public Slider slider;


    public GameObject cleannessObject;
    public GameObject hungerObject;
    public GameObject energyObject;


    GameObject statusManager;  
    ControlEstados controlEstados;


    Hambre hungerScript;
    Energia energyScript;
    Limpieza cleanScript;
    
    [SerializeField] private float speed = 1f;
    [HideInInspector] public float currentValue = 0f;
    private int delay = 0; //espera x frames

    private const int WAITFRAMES = 10;
    private const float MINVALUE = 0;
    private const float MAXVALUE = 100;

    void Start()
    {
        statusManager = GameObject.Find("EventSystem");  
        controlEstados = statusManager.GetComponent<ControlEstados>();
        hungerScript = hungerObject.GetComponent<Hambre>();
        energyScript = energyObject.GetComponent<Energia>();
        cleanScript = cleannessObject.GetComponent<Limpieza>();

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
        if(controlEstados.isSleeping)
        {
            if(currentValue > MINVALUE)
            {
                HealOverTime();
            }                   
        }
        else 
        {  
            if(currentValue < MAXVALUE)
            {
                GetSick(StatusesMultiplier());
            }            
        }
        UpdateBar();
    }

    private void GetSick(float multiplier) //reduce la barra de energia segun multiplicador
    {
        currentValue = currentValue + ((0.02f * speed) * multiplier);

    }


    private float StatusesMultiplier(){ //lee que tanto le queda de hambre y diversion
        float multiplier = 1.0f;
        if(hungerScript.currentValue <=5){
            multiplier += 1.3f;
        }
        if(energyScript.currentValue <= 1){
            multiplier += 0.8f;
        }
        if(cleanScript.currentValue <= 10){
            multiplier += 1.0f;
        }

        Debug.Log(multiplier);

        return multiplier; //devuelve un multiplicador
    }

    private void HealOverTime(){
        currentValue = currentValue - ((0.015f * speed));
    }

    private void UpdateBar(){
        slider.value = ((Mathf.Floor(currentValue)));
    }
}
