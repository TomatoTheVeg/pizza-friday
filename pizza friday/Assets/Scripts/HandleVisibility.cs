using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleVisibility : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Image im;

    private void Start()
    {
        im = GetComponent<Image>();
    }

    void Update()
    {
        if (joystick.Vertical * joystick.Vertical + joystick.Horizontal * joystick.Horizontal > joystick.DeadZone * joystick.DeadZone)
        {
            im.enabled = true;
        }
        else
        {
            im.enabled = false;
        }
    }
}
