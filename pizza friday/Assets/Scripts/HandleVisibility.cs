using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleVisibility : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] AudioClip zoomClip;
    [SerializeField] AudioSource src;
    Image im;

    private void Start()
    {
        im = GetComponent<Image>();
    }

    void Update()
    {
        if (joystick.Horizontal!=0||joystick.Vertical!=0)
        {
            im.enabled = true;
            if (!src.isPlaying)
            {
                src.clip = zoomClip;
                src.Play();
            }
        }
        else
        {
            im.enabled = false;
        }
    }
}
