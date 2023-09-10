using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal { get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; } }
    public float Vertical { get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    [SerializeField] public JoystickState state = JoystickState.Left;

    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public float DeadAngleUp
    {
        get { return deadAngleUp; }
        set { deadAngleUp = value; }
    }

    public float DeadAngleSide
    {
        get { return deadAngleSide; }
        set { deadAngleSide = value; }
    }

    public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }
    public bool SnapX { get { return snapX; } set { snapX = value; } }
    public bool SnapY { get { return snapY; } set { snapY = value; } }
    public bool IsInDeadZone { get { return isInDeadZone; } }

    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;
    [SerializeField] private float deadAngleUp = Mathf.PI / 2;
    [SerializeField] private float deadAngleSide = Mathf.PI/2;

    [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField] private bool snapX = false;
    [SerializeField] private bool snapY = false;
    private bool isInDeadZone = false;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform baseRect = null;

    private Canvas canvas;
    private Camera cam;

    private Vector2 input = Vector2.zero;

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        DeadAngleUp = deadAngleUp;
        DeadAngleSide = deadAngleSide;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        //if (input.normalized.y > Mathf.Cos(deadAngle))
       /// {
        handle.anchoredPosition = input * radius * handleRange;
        /*}
        else
        {
            handle.anchoredPosition = new Vector2(input.normalized.x, Mathf.Cos(deadAngle * Mathf.PI / 180))*input.magnitude * radius * handleRange;
        }*/
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        Debug.Log(state);
        if (magnitude > 1)
        {
            magnitude = 1;
        }
        if (magnitude > deadZone)
        {
            isInDeadZone = false;
            switch (state)
            {
                case JoystickState.Up:
                    if (normalised.y < Mathf.Cos(deadAngleUp * Mathf.PI / 180) && normalised.y > -Mathf.Cos(deadAngleUp * Mathf.PI / 180))
                    {
                        normalised.y = Mathf.Cos(deadAngleUp * Mathf.PI / 180);
                        /*
                        if (normalised.x > Mathf.Sin(deadAngle * Mathf.PI / 180))
                        {
                            normalised.x = Mathf.Sin(deadAngle * Mathf.PI / 180);
                        }*/
                        input = normalised * magnitude;
                    }
                    else if (normalised.y < -Mathf.Cos(deadAngleUp * Mathf.PI / 180))
                    {
                        isInDeadZone = true;
                        //input = Vector2.zero;
                    }
                    normalised.x = Mathf.Clamp(normalised.x, -Mathf.Sin(deadAngleUp * Mathf.PI / 180), Mathf.Sin(deadAngleUp * Mathf.PI / 180));
                    break;

                case JoystickState.Right:
                    if (normalised.x < Mathf.Cos(deadAngleSide * Mathf.PI / 180) && normalised.x > -Mathf.Cos(deadAngleSide * Mathf.PI / 180))
                    {
                        normalised.x = Mathf.Cos(deadAngleSide * Mathf.PI / 180);
                    }
                    else if (normalised.x < -Mathf.Cos(deadAngleSide * Mathf.PI / 180))
                    {
                        isInDeadZone = true;
                    }
                    normalised.y = Mathf.Clamp(normalised.y, -Mathf.Sin(deadAngleSide * Mathf.PI / 180), Mathf.Sin(deadAngleSide * Mathf.PI / 180));
                    break;

                case JoystickState.Left:

                    if (normalised.x < Mathf.Cos(deadAngleSide * Mathf.PI / 180) && normalised.x > -Mathf.Cos(deadAngleSide * Mathf.PI / 180))
                    {
                        normalised.x = -Mathf.Cos(deadAngleSide * Mathf.PI / 180);
                    }
                    else if (normalised.x > Mathf.Cos(deadAngleSide * Mathf.PI / 180))
                    {
                        isInDeadZone = true;
                    }
                    normalised.y = Mathf.Clamp(normalised.y, -Mathf.Sin(deadAngleSide * Mathf.PI / 180), Mathf.Sin(deadAngleSide * Mathf.PI / 180));
                    
                    break;
            }
            /*
           // Debug.Log("magnitude: " + magnitude);
            if (normalised.y < Mathf.Cos(deadAngle * Mathf.PI / 180) && normalised.y > -Mathf.Cos(deadAngle * Mathf.PI / 180))
            {
                normalised.y = Mathf.Cos(deadAngle * Mathf.PI / 180);
                /*
                if (normalised.x > Mathf.Sin(deadAngle * Mathf.PI / 180))
                {
                    normalised.x = Mathf.Sin(deadAngle * Mathf.PI / 180);
                }
                input = normalised * magnitude;
            }
            else if (normalised.y < -Mathf.Cos(deadAngle * Mathf.PI / 180))
            {
                isInDeadZone = true;
                //input = Vector2.zero;
            }
        */
            //normalised.x = Mathf.Clamp(normalised.x, -Mathf.Sin(deadAngle * Mathf.PI / 180), Mathf.Sin(deadAngle * Mathf.PI / 180));
            input = normalised * magnitude;
        }
        else {
            isInDeadZone = true;
            //Debug.Log("deadzonecheck");
            //input = Vector2.zero;
        }
        //input = normalised * magnitude;
    }

    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
            input = new Vector2(input.x, 0f);
        else if (axisOptions == AxisOptions.Vertical)
            input = new Vector2(0f, input.y);
    }

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }