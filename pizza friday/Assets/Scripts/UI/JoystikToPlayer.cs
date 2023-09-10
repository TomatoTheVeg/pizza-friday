using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystikToPlayer : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(player.transform.position);
    }
}
