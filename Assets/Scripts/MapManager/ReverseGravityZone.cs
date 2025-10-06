using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ReverseGravityZone : MonoBehaviour
{
    public static ReverseGravityZone Instance {private set; get;}
    public event EventHandler OnReverseGravity;
    [SerializeField] private float reverseGravityTime = 3f;
    [SerializeField] private Slider slider;
    private bool canReverseGravity = false;
    private float timeCounter;
    private HashSet<GameObject> gameObjectSet = new HashSet<GameObject>();

    private void Awake() {
        Instance = this;
        timeCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("BlockPlatform") || other.CompareTag("OneWayPlatform"))
        {
            gameObjectSet.Add(other.gameObject);
            if (canReverseGravity)
            {
                ReverseGravityOfGameObject(other.gameObject);
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("BlockPlatform") || other.CompareTag("OneWayPlatform"))
        {
            ResetGravityOfGameObject(other.gameObject);
            gameObjectSet.Remove(other.gameObject);
        }
    }

    private void LateUpdate() {
        if (gameObjectSet!=null)
        {
            if (!canReverseGravity)
            {
                if (timeCounter < reverseGravityTime)
                {
                    slider.value = 1 - timeCounter/reverseGravityTime;
                    timeCounter += Time.deltaTime;
                }
                if (timeCounter >= reverseGravityTime)
                {
                    ReverseGravity(gameObjectSet);
                    timeCounter = 0f;
                    canReverseGravity = !canReverseGravity;
                }
            }
            else
            {
                if (timeCounter < reverseGravityTime)
                {
                    slider.value = 1 - timeCounter/reverseGravityTime;
                    timeCounter += Time.deltaTime;
                }
                if (timeCounter >= reverseGravityTime)
                {
                    ResetGravity(gameObjectSet);
                    timeCounter = 0f;
                    canReverseGravity = !canReverseGravity;
                }
            }
        }
        
    }

    private void ReverseGravity(HashSet<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            ReverseGravityOfGameObject(item);
        }
    }

    private void ResetGravity(HashSet<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            ResetGravityOfGameObject(item);
        }     
    }

    private void ReverseGravityOfGameObject(GameObject gameObject)
    {
        bool isReversed = false;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && !isReversed)
        {
            OnReverseGravity?.Invoke(this,EventArgs.Empty);
            rb.gravityScale = -Mathf.Abs(rb.gravityScale);
            isReversed = !isReversed;
            if (Mathf.Abs(gameObject.transform.rotation.w) == 1 || Mathf.Abs(gameObject.transform.rotation.y) == 1)
            {
                gameObject.transform.Rotate(180f,0f,0f);      
            }          
        }
        
        
    }

    private void ResetGravityOfGameObject(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            OnReverseGravity?.Invoke(this,EventArgs.Empty);
            rb.gravityScale = Mathf.Abs(rb.gravityScale);   
            if (Mathf.Abs(gameObject.transform.rotation.x) == 1)
            {
                gameObject.transform.Rotate(180f,0f,0f);      
            }
            if (Mathf.Abs(gameObject.transform.rotation.z) == 1)
            {
                gameObject.transform.Rotate(180f,0f,0);      
            }
        }
        
    }




}
