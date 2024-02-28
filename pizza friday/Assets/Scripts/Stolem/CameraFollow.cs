using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float view;
    [SerializeField] public Vector3 offset;
    [SerializeField] private Rigidbody2D target;
    [SerializeField] private float smoothSpeed;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 viewPos = new Vector2(target.velocity.x * view * GetComponent<Camera>().aspect, target.velocity.y * view);
        transform.position = offset + (Vector3)(target.position + viewPos);
    }
}
