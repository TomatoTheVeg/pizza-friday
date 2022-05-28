using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaTemperatureDisplay : MonoBehaviour
{
    [SerializeField] private PizzaTemperature pizza;
    private Text text;
    [SerializeField]private float k1, b1, k2, b2, a, b, c=0, mediumTemperature;
    void Start()
    {
        text = GetComponent<Text>();
        mediumTemperature = (pizza.minPizzaTemperature+pizza.maxPizzaTemperature) * 0.5f;
        k1 = 1 / (mediumTemperature - pizza.minPizzaTemperature);
        b1 = -pizza.minPizzaTemperature * k1;
        k2 = 1 /(mediumTemperature-pizza.maxPizzaTemperature);
        b2 = -pizza.maxPizzaTemperature * k2;
        a = 1 / (mediumTemperature * mediumTemperature - pizza.minPizzaTemperature * mediumTemperature);
        b = -a * pizza.minPizzaTemperature;
    }

    void Update()
    {
        text.text = Mathf.Floor(pizza.currPizzaTemperature)+ "°";
        if (pizza.currPizzaTemperature < mediumTemperature)
        {
            text.color = new Color(a * pizza.currPizzaTemperature*pizza.currPizzaTemperature + b*pizza.currPizzaTemperature, a * pizza.currPizzaTemperature * pizza.currPizzaTemperature + b * pizza.currPizzaTemperature, 1);
        }
        else
        {
            text.color = new Color(1, a * Mathf.Pow(pizza.currPizzaTemperature - 2*mediumTemperature,2) + b * (pizza.currPizzaTemperature - 2 * mediumTemperature), a * Mathf.Pow(pizza.currPizzaTemperature - 2 * mediumTemperature, 2) + b * (pizza.currPizzaTemperature - 2 * mediumTemperature));
        }

    }
}
