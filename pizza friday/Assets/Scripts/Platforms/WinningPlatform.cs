using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPlatform : Platform
{
    private void Awake()
    {
        pt = PlatformType.WinningPlatform;
    }
}
