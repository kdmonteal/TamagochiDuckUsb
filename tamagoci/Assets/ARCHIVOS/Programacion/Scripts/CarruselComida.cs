using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarruselComida : MonoBehaviour
{
    public GameObject[] foodItems;
    public int selectedFood = 0;

    void Start()
    {
        foodItems[selectedFood].SetActive(false);
        selectedFood = (selectedFood + 1) % foodItems.Length;
        foodItems[selectedFood].SetActive(true);
    }

    void Update()
    {
        
    }
}
