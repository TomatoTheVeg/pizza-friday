using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : Platform
{
    public float bounceBoost;
    private void Awake()
    {
        pt = PlatformType.BouncePlatform;
    }

    void Update()
    {
        
    }
}
