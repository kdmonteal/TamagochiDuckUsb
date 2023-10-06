using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hambre : MonoBehaviour
{
    public GameObject cleannessObject;
    public Slider slider;

    Limpieza cleanScript;
    GameObject statusManager;
    ControlEstados controlEstados;
    private TextMeshProUGUI hungerText;
    
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
        cleanScript = cleannessObject.GetComponent<Limpieza>();
        
        hungerText = GetComponent<TextMeshProUGUI>();
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
        if(controlEstados.isEating){
            if(currentValue < 100){
                InstantHungerRecover();
                InstantCleannessLoss(); //llamado aqui y no en Limpieza.cs para evitar que el trigger pase desapercibido por el otro script.
                controlEstados.isEating = false;
            }
        }else{
            if(currentValue > MINVALUE)
            {
                reductionRate(StatusesMultiplier());
            }
        }
    }

    private void reductionRate(float multiplier) //reduce la barra de hambre segun multiplicador
    {
        currentValue = currentValue - ((0.05f * speed) * multiplier);
        UpdateBar();
    }

    private float StatusesMultiplier(){ //lee que tanto le queda de diversion
        float multiplier = 1.0f;
        /*if(controlEstados.isPlaying){
            multiplier += 0.2f;
        }*/
        return multiplier; //devuelve un multiplicador
    }

    private void InstantHungerRecover(){ //instataneamente sube la barra de hambre al comer;
        currentValue = currentValue + 15;
        if(currentValue > MAXVALUE){
            currentValue = MAXVALUE;
        }
        UpdateBar();
    }

    private void InstantCleannessLoss(){
        float cleannessValue = cleanScript.currentValue;
        cleannessValue -= 15;
        if(cleannessValue < MINVALUE){
            cleannessValue = MINVALUE;
        }
        cleanScript.UpdateBar();
    }

    private void UpdateBar(){
        //hungerText.text = (Mathf.Ceil(currentValue)).ToString();
        slider.value = ((Mathf.Ceil(currentValue)));
    }
}
