using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{

    public enum Direction
    {
        OneDirection,
        TwoDirections,
    }
    [SerializeField] private Direction direction;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private float speed;

    private int currentPointIndex = 0;
    private bool isMovingForward = true;
    
    private void Update() {
        if (Vector2.Distance(wayPoints[currentPointIndex].transform.position,transform.position) < 0.1f)
        {
            if (direction == Direction.OneDirection)
            {
                MoveOneDirection();
            }
            else{
                MoveTwoDirection();
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentPointIndex].transform.position,speed*Time.deltaTime);
    }

    private void MoveOneDirection()
    {
        currentPointIndex++;
        if (currentPointIndex >= wayPoints.Length)
        {
            currentPointIndex = 0;
        }
    }

    private void MoveTwoDirection()
    {
        if (isMovingForward)
            {
                currentPointIndex++;
                if (currentPointIndex >= wayPoints.Length)
                {
                    currentPointIndex = wayPoints.Length - 1;
                    isMovingForward = false; 
                }
            }
            else
            {
                currentPointIndex--;
                if (currentPointIndex < 0)
                {
                    currentPointIndex = 0;
                    isMovingForward = true; 
                }
            }
    }

}
