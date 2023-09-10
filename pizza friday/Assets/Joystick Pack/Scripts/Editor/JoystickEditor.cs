using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Joystick), true)]
public class JoystickEditor : Editor
{
    private SerializedProperty handleRange;
    private SerializedProperty deadZone;
    private SerializedProperty deadAngleUp;
    private SerializedProperty deadAngleSide;
    private SerializedProperty axisOptions;
    private SerializedProperty snapX;
    private SerializedProperty snapY;
    protected SerializedProperty background;
    private SerializedProperty handle;
    private SerializedProperty state;

    protected Vector2 center = new Vector2(0.5f, 0.5f);

    protected virtual void OnEnable()
    {
        handleRange = serializedObject.FindProperty("handleRange");
        deadZone = serializedObject.FindProperty("deadZone");
        deadAngleUp = serializedObject.FindProperty("deadAngleUp");
        deadAngleSide = serializedObject.FindProperty("deadAngleSide");
        axisOptions = serializedObject.FindProperty("axisOptions");
        snapX = serializedObject.FindProperty("snapX");
        snapY = serializedObject.FindProperty("snapY");
        background = serializedObject.FindProperty("background");
        handle = serializedObject.FindProperty("handle");
        state = serializedObject.FindProperty("state");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawValues();
        EditorGUILayout.Space();
        DrawComponents();

        serializedObject.ApplyModifiedProperties();

        if(handle != null)
        {
            RectTransform handleRect = (RectTransform)handle.objectReferenceValue;
            handleRect.anchorMax = center;
            handleRect.anchorMin = center;
            handleRect.pivot = center;
            handleRect.anchoredPosition = Vector2.zero;
        }
    }

    protected virtual void DrawValues()
    {
        EditorGUILayout.PropertyField(handleRange, new GUIContent("Handle Range", "The distance the visual handle can move from the center of the joystick."));
        EditorGUILayout.PropertyField(deadZone, new GUIContent("Dead Zone", "The distance away from the center input has to be before registering."));
        EditorGUILayout.PropertyField(deadAngleUp, new GUIContent("Dead Angle Up", "max Angle to Y-Axel till whith the touch would be registratable in upward state"));
        EditorGUILayout.PropertyField(deadAngleSide, new GUIContent("Dead Angle Side", "max Angle to Y-Axel till whith the touch would be registratable in side state"));
        EditorGUILayout.PropertyField(axisOptions, new GUIContent("Axis Options", "Which axes the joystick uses."));
        EditorGUILayout.PropertyField(snapX, new GUIContent("Snap X", "Snap the horizontal input to a whole value."));
        EditorGUILayout.PropertyField(snapY, new GUIContent("Snap Y", "Snap the vertical input to a whole value."));
        EditorGUILayout.PropertyField(state, new GUIContent("State", "State of the joystick"));
    }

    protected virtual void DrawComponents()
    {
        EditorGUILayout.ObjectField(background, new GUIContent("Background", "The background's RectTransform component."));
        EditorGUILayout.ObjectField(handle, new GUIContent("Handle", "The handle's RectTransform component."));
    }
}