using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Mode
    {
        Hold,
        Toggle,
    }
public class SwitchBehaviour : MonoBehaviour
{



    [SerializeField] private bool canOpenSwitch;
    [SerializeField] private bool canCloseSwitch;
    [SerializeField] private Mode mode;
    [SerializeField] private Moveable[] moveableGameObject;
    [SerializeField] private SwitchBehaviour remainButton;

    
    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();
    private float switchSizeY;
    private Vector3 switchUpPos;
    private Vector3 switchDownPos;
    private float switchSpeed = 1f;
    private float switchDelay = 0.5f;
    private bool isPressingSwitch = false;
    
    private void Awake() {
        switchSizeY = transform.localScale.y/6;
        switchUpPos = transform.position;
        switchDownPos = new Vector3(transform.position.x, transform.position.y - switchSizeY,transform.position.z);
    }

    private void Update() {
        if (isPressingSwitch)
        {
            MoveSwitchDown();
        }
        else
        {
            MoveSwitchUp();
        }    
    }

    private void MoveSwitchDown()
    {
        if (transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,switchDownPos,switchSpeed * Time.deltaTime);
        }
    }

    private void MoveSwitchUp()
    {
        if (transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,switchUpPos,switchSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")|| other.gameObject.CompareTag("BlockPlatform"))
        {
            
            playersOnEntrance.Add(other.gameObject);
            isPressingSwitch = true;
            if (mode == Mode.Toggle)
            {
                foreach (Moveable item in moveableGameObject)
                {
                    if (canOpenSwitch && !item.IsTurnOn)
                    {
                        item.IsTurnOn = !item.IsTurnOn;
                    }
                    else if(canCloseSwitch && item.IsTurnOn)
                    {
                        item.IsTurnOn = !item.IsTurnOn;
                    }
                }
            }
            else
            {
                foreach (Moveable item in moveableGameObject)
                {
                    if (canOpenSwitch && canCloseSwitch && !item.IsTurnOn)
                    {
                        item.IsTurnOn = true;
                    }
                }
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player1") || other.CompareTag("Player2")||other.gameObject.CompareTag("BlockPlatform"))
        {
            playersOnEntrance.Remove(other.gameObject);
            if (mode == Mode.Hold)
            {
                if (remainButton != null)
                {
                    if (playersOnEntrance.Count == 0 && remainButton.GetGameObjectSet().Count == 0)
                    {
                        foreach (Moveable item in moveableGameObject)
                        {
                            if (canOpenSwitch && canCloseSwitch && item.IsTurnOn)
                            {
                                item.IsTurnOn = !item.IsTurnOn;
                            }
                        }
                    }
                }
                else
                {
                    if (playersOnEntrance.Count == 0)
                    {
                        foreach (Moveable item in moveableGameObject)
                        {
                            if (canOpenSwitch && canCloseSwitch && item.IsTurnOn)
                            {
                                item.IsTurnOn = !item.IsTurnOn;
                            }
                        }
                    }
                }
  
            }
            if (playersOnEntrance.Count == 0)
            {
                StartCoroutine(SwitchUpDelay(switchDelay));  
            }
        }
    }

    public HashSet<GameObject> GetGameObjectSet()
    {
        return playersOnEntrance;
    }

    IEnumerator SwitchUpDelay(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        isPressingSwitch = false;
    }

    
}
