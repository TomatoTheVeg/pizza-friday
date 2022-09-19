using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleVisibility : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Image im;
    private Animator anim;

    private void Start()
    {
        im = GetComponent<Image>();
        anim = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!joystick.IsInDeadZone)
        {
            if (!im.enabled)
            {
                AudioManager.instance.PlayUninterruptedSound(AudioManager.instance.FindSound("zoom"));
                anim.SetBool("IsZooming", true);
            }
            im.enabled = true;
        }
        else
        {
            im.enabled = false;
            AudioManager.instance.StopSound(AudioManager.instance.FindSound("zoom"));
            anim.SetBool("IsZooming", false);
        }
    }
}
