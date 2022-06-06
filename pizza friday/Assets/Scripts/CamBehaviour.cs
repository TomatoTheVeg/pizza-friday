using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    GameObject player;
    public float xSpeed = 8f;
    public float ySpeed = 8f;
    private Vector3 targetPosition;
    [Tooltip("0,1 - камера перемещаеться вместе с игроком, 1 - камера не сдвинеться пока игрок полностью не выйдет за рамки экрана")]
    [Range(0.1f, 1.0f)] public float vertialUnmovableField, horizontalUnmovableField;
    bool isMoving = false;
    Camera mainCam;
     GameObject unmovableFieldVisualisation;

     private Vector2 screenRectInRealWorld, unmovableRect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        unmovableFieldVisualisation = transform.Find("Camera border").gameObject;
        float horisontal, vertical;
        mainCam = GetComponent<Camera>();
        vertical = mainCam.orthographicSize;
        horisontal = vertical * Screen.width / Screen.height;
        screenRectInRealWorld = new Vector2(horisontal, vertical);
        unmovableRect = new Vector2(screenRectInRealWorld.x * horizontalUnmovableField, screenRectInRealWorld.y * vertialUnmovableField);
        unmovableFieldVisualisation.transform.localScale *=unmovableRect;
        targetPosition = transform.position;
    }
    bool CheckXMargin()
    {
        float diff = targetPosition.x - player.transform.position.x;
        if (Mathf.Abs(diff) > unmovableRect.x)
        {
            targetPosition.x = targetPosition.x - 2*unmovableRect.x*diff/Mathf.Abs(diff);
            return true;
        }
        return false;
    }


    bool CheckYMargin()
    {
        float diff = targetPosition.y - player.transform.position.y;
        if (Mathf.Abs(diff) > unmovableRect.y)
        {
            targetPosition.y = targetPosition.y - 2*unmovableRect.y * diff / Mathf.Abs(diff);
            return true;
        }
        return false;
    }


    void LateUpdate()
    {
        CheckXMargin();
        CheckYMargin();
        MovingCamera();                                                                                                              
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }



    public void MoveCamera(Vector3 targtPos)
    {
        isMoving = true;
        targetPosition = targtPos;
    }

    public void ChangeCameraProperies(float _vertical, float _horizontal)
    {
        vertialUnmovableField = _vertical;
        horizontalUnmovableField = _horizontal;
        unmovableRect.Set(screenRectInRealWorld.x * horizontalUnmovableField, screenRectInRealWorld.y * vertialUnmovableField);
        unmovableFieldVisualisation.transform.localScale *= unmovableRect;
    }

    private void MovingCamera()
    {
        float targetX;
        float targetY;
        targetX = Mathf.Lerp(transform.position.x, targetPosition.x, xSpeed * Time.deltaTime);
        targetY = Mathf.Lerp(transform.position.y, targetPosition.y, ySpeed * Time.deltaTime);
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

}
