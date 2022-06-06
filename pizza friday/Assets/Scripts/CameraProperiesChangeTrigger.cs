using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperiesChangeTrigger : MonoBehaviour
{
    [Tooltip("0,1 - ������ ������������� ������ � �������, 1 - ������ �� ���������� ���� ����� ��������� �� ������ �� ����� ������")]
    [Range(0.1f, 1.0f)]public  float vertialUnmovableField, horizontalUnmovableField;
}
