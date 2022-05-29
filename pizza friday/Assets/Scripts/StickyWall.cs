using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWall : Platform
{
    [Range(0.0f, 1.0f)]
    public float stickiness;

    private void Awake()
    {
        pt = PlatformType.StickyWall;
    }

    void Update()
    {
        
    }
}
