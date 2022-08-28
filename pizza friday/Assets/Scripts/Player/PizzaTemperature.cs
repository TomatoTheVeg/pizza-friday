using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTemperature : MonoBehaviour
{
    public float currPizzaTemperature, minPizzaTemperature, maxPizzaTemperature, naturalDecrease;
    void Start()
    {
        
    }

    void Update()
    {
        currPizzaTemperature -= naturalDecrease * Time.deltaTime;
        currPizzaTemperature = Mathf.Clamp(currPizzaTemperature, minPizzaTemperature, maxPizzaTemperature);
    }
}
