using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDraw : MonoBehaviour
{
    GameObject []points;
    [SerializeField] int  numberOfPoints;
    [SerializeField] float timeBetweenPoints, originalSizeOfTheDots;
    [SerializeField] GameObject examplePoint, player;
    ObjectThrow objthr;
    [SerializeField] Joystick joystick;
    void Start()
    {
        points = new GameObject[numberOfPoints];
        float sizeOfTheDots = originalSizeOfTheDots;
        for (int i = 0; i < numberOfPoints; ++i) {
            points[i] = Instantiate(examplePoint,transform);
            sizeOfTheDots = sizeOfTheDots * Mathf.Sqrt((-(float)i / numberOfPoints) + 1F);
            points[i].transform.localScale = new Vector3(sizeOfTheDots, sizeOfTheDots, sizeOfTheDots);
        }
        objthr = player.GetComponent<ObjectThrow>();
    }

    void Update()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            for (int i = 0; i < numberOfPoints; ++i)
            {
                points[i].SetActive(true);
            }
                DrawProjectory(new Vector2(objthr.pushStrength*joystick.Horizontal, objthr.pushStrength * joystick.Vertical));
        }
        else
        {
            for (int i = 0; i < numberOfPoints; ++i)
            {
                points[i].SetActive(false);
            }
        }
    }

    private void DrawProjectory(Vector2 startVelocity)
    {
        for(int i = 0; i < numberOfPoints; ++i)
        {
            points[i].transform.localPosition = CoordCalc(startVelocity, (i+1)*timeBetweenPoints);
        }
    }

    private Vector2 CoordCalc(Vector2 startVelocity, float time)
    {
        float X, Y;
        X = startVelocity.x * time;
        Y = startVelocity.y * time + Physics.gravity.y* time * time * 0.25f;
        return new Vector2(X, Y);
    }
}
