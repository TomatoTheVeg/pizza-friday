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
        if (joystick.Horizontal!=0||joystick.Vertical!=0)
        {
            if (!im.enabled)
            {
                AudioManager.instance.PlayUninterruptedSound("zoom");
            }
            im.enabled = true;
        }
        else
        {
            im.enabled = false;
            AudioManager.instance.StopSound("zoom");
        }
    }
}
