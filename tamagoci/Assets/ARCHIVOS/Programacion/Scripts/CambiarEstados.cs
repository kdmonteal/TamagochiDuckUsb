using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarEstados : MonoBehaviour
{
    GameObject statusManager;  
    ControlEstados controlEstados;
    public Light myLight;

    public bool luz = true;

    void Start()
    {
        statusManager = GameObject.Find("EventSystem");  
        controlEstados = statusManager.GetComponent<ControlEstados>();
    }

    public void ClickBombillo(){
        luz = !luz;
        controlEstados.isSleeping = luz; 
        Debug.Log("Luz esta en "+luz);
        if (luz == true)
            {
                myLight.intensity = 0.1f;
            }
        else
        {
                myLight.intensity = 1f;
        }
        
    }
}
