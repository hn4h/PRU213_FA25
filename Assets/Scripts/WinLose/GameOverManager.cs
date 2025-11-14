using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance {private set; get;}
    [SerializeField] private GameObject losingUI;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI losingTimesPlayer1Text;
    [SerializeField] private TextMeshProUGUI losingTimesInGamePlayer1Text;
    [SerializeField] private TextMeshProUGUI losingTimesPlayer2Text;
    [SerializeField] private TextMeshProUGUI losingTimesInGamePlayer2Text;

    AudioManager audioManager;

    // Per-level death counter (persists across scene reloads within the same level)
    private static int p1_LoseCount = 0;
    private static int p2_LoseCount = 0;
    private static string s_CurrentLevelName = null;

   private void Awake(){
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Reset counter when entering a new level
        string levelName = SceneManager.GetActiveScene().name;
        if (s_CurrentLevelName != levelName)
        {
            s_CurrentLevelName = levelName;
            p1_LoseCount = 0;
            p2_LoseCount = 0;
        }
        Hide();
        
        playAgainButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            PlayAgain();
        });
        
        homeButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            ReturnHome();
        });
   }

    public void Hide()
    {
        losingUI.SetActive(false);
    }

    public void Show1Player1()
    {
        // Increase lose count and update UI each time player loses
        p1_LoseCount++;
        RefreshLosingTimesUI();

        losingUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Show1Player2()
    {
        // Increase lose count and update UI each time player loses
        p2_LoseCount++;
        RefreshLosingTimesUI();

        losingUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void PlayAgain()
    {
        Loader.Load(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ReturnHome()
    {
        Loader.Load(Loader.Scene.LevelScreen);
        Time.timeScale = 1f;
        // Leaving the level: reset session counter
        p1_LoseCount = 0;
        p2_LoseCount = 0;
        s_CurrentLevelName = null;
    }

    private void RefreshLosingTimesUI()
    {
        if (losingTimesPlayer1Text != null)
        {
            losingTimesPlayer1Text.text = $"Losing times P1: {p1_LoseCount}";
        }
        if (losingTimesInGamePlayer1Text != null)
        {
            losingTimesInGamePlayer1Text.text = $"Losing times P1: {p1_LoseCount}";
        }
        if (losingTimesPlayer2Text != null)
        {
            losingTimesPlayer2Text.text = $"Losing times P2: {p2_LoseCount}";
        }
        if (losingTimesInGamePlayer2Text != null)
        {
            losingTimesInGamePlayer2Text.text = $"Losing times P2: {p2_LoseCount}";
        }
    }

    private void Start()
    {
        // Ensure in-game label shows current value as soon as play starts
        RefreshLosingTimesUI();
    }
}
