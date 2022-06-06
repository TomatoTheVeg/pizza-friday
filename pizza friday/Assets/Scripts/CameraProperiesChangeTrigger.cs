using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperiesChangeTrigger : MonoBehaviour
{
    [Tooltip("0,1 - камера перемещаеться вместе с игроком, 1 - камера не сдвинеться пока игрок полностью не выйдет за рамки экрана")]
    [Range(0.1f, 1.0f)]public  float vertialUnmovableField, horizontalUnmovableField;
}
