using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum Direction
{
    Horizontal,
    Vertical
}

public enum Type
{
    Elevator,
    Door,
    LevelEntrance
}

public class Moveable : MonoBehaviour
{

    public bool IsTurnOn = false;
    public bool hasLight;
    [SerializeField] private Light2D light2d;
    [SerializeField] private float lightIntensity = 0.2f;
    [SerializeField] private Type _type;
    [SerializeField] private Direction direction;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform upperPos;
    [SerializeField] private Transform downPos;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private Transform openPosition;
    private bool isMoveDown;
    private bool isMoveRight;
    private Vector3 initPosition;


    private void Start() {
        initPosition = transform.position;
        if (!IsTurnOn && hasLight)
        {
            light2d.intensity = 0;   
        }
    }

    private void Update() {
        if (_type == Type.Elevator)
        {
            if (IsTurnOn)
            {
                if (direction == Direction.Horizontal)
                {
                    MoveHorizontal();
                }
                else
                {
                    MoveVertical();
                }
            }
            else
            {
                MoveToInitialPosition();
            }
            
        }
        else if(_type == Type.Door)
        {
            if (IsTurnOn)
            {
                OpenDoor();
            }
            else
            {
                MoveToInitialPosition();
            }
        }
        else
        {
            if (IsTurnOn)
            {
                
                OpenDoor();
                if (transform.position == openPosition.position)
                {
                    IsTurnOn = false;
                }
            }
        }

        if (hasLight)
        {
            if (IsTurnOn)
            {
                light2d.intensity = lightIntensity;
            }
            else if(!IsTurnOn && _type!=Type.LevelEntrance)
            {
                light2d.intensity = 0;
            }
        }
    }

    private void MoveHorizontal()
    {
        if (IsTurnOn)
        {  
            if (transform.position.x == leftPos.position.x)
            {
                isMoveRight = true;
            }
            else if(transform.position.x == rightPos.position.x)
            {
                isMoveRight = false;
            }

            if (isMoveRight)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        else{
            MoveToInitialPosition();
        }
    }

    private void MoveVertical()
    {
        if (IsTurnOn)
        {  
            if (transform.position.y == upperPos.position.y)
            {
                isMoveDown = true;
            }
            else if(transform.position.y == downPos.position.y)
            {
                isMoveDown = false;
            }

            if (isMoveDown)
            {
                MoveDown();
            }
            else
            {
                MoveUp();
            }
        }
        else{
            MoveToInitialPosition();
        }
    }

    private void MoveUp()
    {
        transform.position = Vector2.MoveTowards(transform.position,upperPos.position,speed * Time.deltaTime);
    }

    private void MoveDown()
    {
        transform.position = Vector2.MoveTowards(transform.position,downPos.position,speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        transform.position = Vector2.MoveTowards(transform.position,leftPos.position,speed * Time.deltaTime);
    }
    
    private void MoveRight()
    {
        transform.position = Vector2.MoveTowards(transform.position,rightPos.position,speed * Time.deltaTime);
    }

    private void MoveToInitialPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position,initPosition,speed * Time.deltaTime);
    }

    private void OpenDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position,openPosition.position,speed * Time.deltaTime);
    }

    public Direction GetDirection()
    {
        return direction;
    }
}
