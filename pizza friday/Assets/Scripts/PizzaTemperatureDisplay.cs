using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaTemperatureDisplay : MonoBehaviour
{
    [SerializeField] private PizzaTemperature pizza;
    private Text text;
    [SerializeField]private float k1, b1, k2, b2, mediumTemperature;
    void Start()
    {
        text = GetComponent<Text>();
        mediumTemperature = (pizza.minPizzaTemperature+pizza.maxPizzaTemperature) * 0.5f;
        k1 = 1 / (mediumTemperature - pizza.minPizzaTemperature);
        b1 = -pizza.minPizzaTemperature * k1;
        k2 = 1 /(mediumTemperature-pizza.maxPizzaTemperature);
        b2 = -pizza.maxPizzaTemperature * k2;
    }

    void Update()
    {
        text.text = Mathf.Floor(pizza.currPizzaTemperature)+ "°";
        if (pizza.currPizzaTemperature < mediumTemperature)
        {
            text.color = new Color(k1 * pizza.currPizzaTemperature + b1, k1 * pizza.currPizzaTemperature + b1, 1);
        }
        else
        {
            text.color = new Color(1, k2 * pizza.currPizzaTemperature + b2, k2 * pizza.currPizzaTemperature + b2);
        }

    }
}
