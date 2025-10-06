using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Moveable))]
public class MoveableEditor : Editor {
    SerializedProperty hasLight;
    SerializedProperty isTurnOn;
    SerializedProperty light2d;
    SerializedProperty lightIntensity;
    SerializedProperty speedProp;
    SerializedProperty typeProp;
    SerializedProperty directionProp;
    SerializedProperty upperPosProp;
    SerializedProperty downPosProp;
    SerializedProperty leftPosProp;
    SerializedProperty rightPosProp;
    SerializedProperty openPosProp;

    private void OnEnable()
    {
        isTurnOn = serializedObject.FindProperty("IsTurnOn");
        hasLight = serializedObject.FindProperty("hasLight");
        light2d = serializedObject.FindProperty("light2d");
        lightIntensity = serializedObject.FindProperty("lightIntensity");
        speedProp = serializedObject.FindProperty("speed");
        typeProp = serializedObject.FindProperty("_type");
        directionProp = serializedObject.FindProperty("direction");
        upperPosProp = serializedObject.FindProperty("upperPos");
        downPosProp = serializedObject.FindProperty("downPos");
        leftPosProp = serializedObject.FindProperty("leftPos");
        rightPosProp = serializedObject.FindProperty("rightPos");
        openPosProp = serializedObject.FindProperty("openPosition");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        EditorGUILayout.PropertyField(isTurnOn);
        EditorGUILayout.PropertyField(hasLight);

        bool isHasLight = (bool) hasLight.boolValue;
        if (isHasLight)
        {
            EditorGUILayout.PropertyField(light2d);
            EditorGUILayout.PropertyField(lightIntensity);
        }
        EditorGUILayout.PropertyField(speedProp);
        EditorGUILayout.PropertyField(typeProp);

        Type selectedType = (Type)typeProp.enumValueIndex;

        if (selectedType == Type.Elevator)
        {
            EditorGUILayout.PropertyField(directionProp);
            Direction selectedDirection = (Direction)directionProp.enumValueIndex;
            if (selectedDirection == Direction.Horizontal)
            {
                EditorGUILayout.PropertyField(leftPosProp);
                EditorGUILayout.PropertyField(rightPosProp);
            }
            else if (selectedDirection == Direction.Vertical)
            {
                EditorGUILayout.PropertyField(upperPosProp);
                EditorGUILayout.PropertyField(downPosProp);
            }
        }
        else
        {
            EditorGUILayout.PropertyField(openPosProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
};
#endif 


