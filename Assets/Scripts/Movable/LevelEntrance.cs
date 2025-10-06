using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelEntrance : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public event EventHandler OnOutOfTheMap;
    //public bool CanMove {get{return canMove;} set{canMove = value;}}
    [SerializeField] private float speed;
    [SerializeField] private Transform nextLevelPosition;
    [SerializeField] private GameObject[] wallArray;

    // private bool canMove;
    private HashSet<GameObject> playersOnEntrance = new HashSet<GameObject>();
    private bool canLift;
    private bool winsoundPlayed;
    private void Start() {
        foreach (GameObject item in wallArray)
            {
                item.SetActive(false);
            }
            winsoundPlayed = false;
    
    }
    private void LiftEntrance()
    {
        transform.position = Vector2.MoveTowards(transform.position,nextLevelPosition.position,speed * Time.deltaTime);
    }

    private void Update() {
        if (playersOnEntrance.Count == 2 && canLift == false)
        {
            canLift = true;
            foreach (GameObject item in wallArray)
            {
                item.SetActive(true);
            }
        }
        else if (canLift)
        {
            LiftEntrance();
        }
            if (transform.position == nextLevelPosition.position)
            {
                OnOutOfTheMap?.Invoke(this,EventArgs.Empty);
            }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            playersOnEntrance.Add(other.gameObject);
            if (playersOnEntrance.Count == 2 && !winsoundPlayed)
            {
                audioManager.PlaySFX(audioManager.win);
                audioManager.StopMusic();
                winsoundPlayed = true;
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if(playersOnEntrance.Contains(other.gameObject))
            {
                playersOnEntrance.Remove(other.gameObject);
            }

        }
    }

}
