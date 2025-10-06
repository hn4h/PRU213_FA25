using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSideEffector : MonoBehaviour
{
    [SerializeField] private Moveable moveableGameObject;
    [SerializeField] private Effector2D effector2D;

    private void Update() {
        if (moveableGameObject.IsTurnOn)
        {
            effector2D.enabled = true;
        }
        else
        {
            effector2D.enabled = false;
        }
    }
}
