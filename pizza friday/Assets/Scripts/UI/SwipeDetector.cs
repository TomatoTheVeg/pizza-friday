using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;
	[SerializeField] GameObject player;
	[SerializeField] Camera cam;
	[SerializeField] double sensDistance;
	bool isTouching = false;
	Touch currTouch;
	ObjectThrow objthr;

    private void Start()
    {
		objthr = player.GetComponent<ObjectThrow>();
    }

    void Update()
	{

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began && Vector2.Distance(cam.ScreenToWorldPoint(touch.position), player.transform.position) < sensDistance&& !isTouching)
			{
				currTouch = touch;
				isTouching = true;
				Debug.Log("Pepe popo");
			}
			
			if (touch.phase == TouchPhase.Ended)
            {
				Debug.Log("fine "+ Vector2.Distance(cam.ScreenToWorldPoint(touch.position), player.transform.position));
            }
			
		}
		if (currTouch.phase == TouchPhase.Ended)
		{
			isTouching = false;
			Debug.Log("fly m fucker");
			Vector2 touchVector = new Vector2(player.transform.position.x - cam.ScreenToWorldPoint(currTouch.position).x, player.transform.position.y - cam.ScreenToWorldPoint(currTouch.position).y);
			objthr.Push(new Vector2(touchVector.x / touchVector.magnitude, touchVector.y / touchVector.magnitude));
		}
	}

	void DetectSwipe()
	{

	}
}
