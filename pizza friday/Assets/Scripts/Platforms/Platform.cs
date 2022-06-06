using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isJumpable = true, canKill;
    [Range(1f, 10f)]
    public float Roughness = 1;
    public float temperatureChange = 0;
    protected PlatformType pt = PlatformType.Undefined;
    public PlatformType PlatformType { get{return pt;}}
}
