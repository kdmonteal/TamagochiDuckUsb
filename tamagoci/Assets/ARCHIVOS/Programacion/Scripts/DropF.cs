using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropF : MonoBehaviour
{
    GameObject statusManager;
    public GameObject hungerObject;

    ControlEstados controlEstados;
    RaycastHit2D hit;
    Camera cam;
    Vector3 pos;
    Vector3 mousePos;
    Vector3 initialPosition;
    Transform focus;
    Hambre hungerScript;
    bool isDrag;

    private void Start()
    
    {
        statusManager = GameObject.Find("EventSystem");  
        hungerScript = hungerObject.GetComponent<Hambre>();        
        controlEstados = statusManager.GetComponent<ControlEstados>();
        initialPosition = transform.position;
        isDrag = false;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition));

            if(hit.collider != null)
            {

                focus = hit.transform;
                focus.position = initialPosition;
                print("Cliked = "+hit.collider.transform.name);
                isDrag = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDrag == true)
        {
            isDrag = false;
            focus.position = initialPosition;
            hungerScript.Eat();

        }
        else if (isDrag == true)
        {
            mousePos = Input.mousePosition;
            mousePos.z = -cam.transform.position.z;
            pos = cam.ScreenToWorldPoint(mousePos);

            focus.position = new Vector3(pos.x, pos.y, focus.position.z);
        }
        
    }

}
