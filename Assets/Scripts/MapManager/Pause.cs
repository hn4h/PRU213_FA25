using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public static Pause Instance {private set; get;}
    public GameObject pauseGameUI;
    [SerializeField] GameObject optionGameObject;
    [SerializeField] GameObject confirmToDeleteData;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public bool canPause = true;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button homeButton;

    private bool isPaused = false;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        pauseButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            if (!isPaused && canPause)
            {
                PauseScreen();
                player1.GetComponent<PlayerController>().CanMove = false;
                player2.GetComponent<PlayerController>().CanMove = false;
                isPaused = true;
            }
            else
            {
                if (confirmToDeleteData.activeSelf)
                {
                    confirmToDeleteData.SetActive(false);
                }
                else if (optionGameObject.activeSelf)
                {
                    HideOptionUI();
                }
                else
                {
                    ResumeGame();
                    player1.GetComponent<PlayerController>().CanMove = true;
                    player2.GetComponent<PlayerController>().CanMove = true;
                    isPaused = false;
                }
                
            }
            
        });
        resumeButton.onClick.AddListener(() =>{
            ResumeGame();
            player1.GetComponent<PlayerController>().CanMove = true;
            player2.GetComponent<PlayerController>().CanMove = true;
            audioManager.PlaySFX(audioManager.action);
        });
        optionButton.onClick.AddListener(() =>{
            optionGameObject.SetActive(true);
            audioManager.PlaySFX(audioManager.action);
        });
        
        homeButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            LoadHomeScene();
            isPaused = false;
            
        });

        confirmToDeleteData.SetActive(false);
    }

    void Start()
    {
        Instance = this;
        pauseGameUI.SetActive(false);
        HideOptionUI();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (!isPaused)
            {
                PauseScreen();

                player1.GetComponent<PlayerController>().CanMove = false;
                player2.GetComponent<PlayerController>().CanMove = false;
                
                isPaused = true;
            }
            else
            {
                if (confirmToDeleteData.activeSelf)
                {
                    confirmToDeleteData.SetActive(false);
                }
                else if (optionGameObject.activeSelf)
                {
                    HideOptionUI();
                }
                else
                {
                    ResumeGame();
                    player1.GetComponent<PlayerController>().CanMove = true;
                    player2.GetComponent<PlayerController>().CanMove = true;
                    isPaused = false;
                }
                
            }
        } 
        
    }
    public void PauseScreen()
    {
        Time.timeScale = 0f;
        pauseGameUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        // if (!MapManager.Instance.getQuesCanvas().gameObject.activeInHierarchy)
        // {
        //     Time.timeScale = 1f;
        // }
        Time.timeScale = 1f;
        pauseGameUI.SetActive(false);
        isPaused = false;     
    }


    public void LoadHomeScene()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
        Time.timeScale = 1;
        
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void HideOptionUI()
    {
        optionGameObject.SetActive(false);
    }

    private void ShowOptionUI()
    {
        optionGameObject.SetActive(true);
    }

}
