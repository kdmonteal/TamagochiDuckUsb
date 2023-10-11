using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Energia : MonoBehaviour
{
    public Slider slider;
    public GameObject hungerObject;
    GameObject statusManager;  
    ControlEstados controlEstados;
    Hambre hungerScript;
    private TextMeshProUGUI energyText;
    
    [SerializeField] private float speed = 1f;
    [HideInInspector] public float currentValue = 100;
    private int delay = 0; //espera x frames

    private const int WAITFRAMES = 10;
    private const float MINVALUE = 0;
    private const float MAXVALUE = 100;

    void Start()
    {
        statusManager = GameObject.Find("EventSystem");  
        controlEstados = statusManager.GetComponent<ControlEstados>();
        energyText = GetComponent<TextMeshProUGUI>();
        hungerScript = hungerObject.GetComponent<Hambre>();
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
            if(currentValue < MAXVALUE)
            {
                RecoverEnergy();
            }                   
        }
        else 
        {
            if(currentValue > MINVALUE)
            {
                reductionRate(StatusesMultiplier());
            }            
        }
        UpdateBar();
    }

    private void reductionRate(float multiplier) //reduce la barra de energia segun multiplicador
    {
        currentValue = currentValue - ((0.05f * speed) * multiplier);

    }
    

    private float StatusesMultiplier(){ //lee que tanto le queda de hambre y diversion
        float multiplier = 1.0f;
        if(hungerScript.currentValue <=30){
            multiplier += 0.3f;
        }
        if(controlEstados.isPlaying){
            multiplier += 0.5f;
        }

        return multiplier; //devuelve un multiplicador
    }

    private void RecoverEnergy(){
        currentValue = currentValue + ((0.04f * speed * 1.5f));
    }

    private void UpdateBar(){
        slider.value = ((Mathf.Ceil(currentValue)));
    }
}
