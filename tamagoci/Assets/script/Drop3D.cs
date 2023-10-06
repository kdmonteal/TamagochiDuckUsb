using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop3D : MonoBehaviour
{
    bool isDrag;
    Transform focus;
    Camera cam;
    Vector3 screenPos;
    Vector3 offset;
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        isDrag = false;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                focus = hit.collider.transform;
                print("click = "+focus.name);

                screenPos = cam.WorldToScreenPoint(focus.position);
                offset = focus.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
                isDrag = true;
            }
        }
        else if(Input.GetMouseButtonUp(0) && isDrag == true)
        {
            isDrag = false;
        }
        else if (isDrag == true)
        {
            Vector3 currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
            Vector3 currentPos = cam.ScreenToWorldPoint(currentScreenPos)+ offset;

            focus.position = currentPos;
        }
    }
}
