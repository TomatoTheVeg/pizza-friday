using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystikToPlayer : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject player;

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(player.transform.position);
    }
}
