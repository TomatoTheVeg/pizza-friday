using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyPlatform : Platform
{
    private void Awake()
    {
        pt = PlatformType.DeadlyPlatform;
    }
}
