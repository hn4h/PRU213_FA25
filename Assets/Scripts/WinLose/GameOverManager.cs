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
    [SerializeField] private TextMeshProUGUI losingTimesText;
    [SerializeField] private TextMeshProUGUI losingTimesInGameText; // Hiển thị khi đang chơi
    
    AudioManager audioManager;

    // Per-level death counter (persists across scene reloads within the same level)
    private static int s_LoseCount = 0;
    private static string s_CurrentLevelName = null;

   private void Awake(){
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Reset counter when entering a new level
        string levelName = SceneManager.GetActiveScene().name;
        if (s_CurrentLevelName != levelName)
        {
            s_CurrentLevelName = levelName;
            s_LoseCount = 0;
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

    public void Show()
    {
        // Increase lose count and update UI each time player loses
        s_LoseCount++;
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
        s_LoseCount = 0;
        s_CurrentLevelName = null;
    }

    private void RefreshLosingTimesUI()
    {
        if (losingTimesText != null)
        {
            losingTimesText.text = $"Losing times: {s_LoseCount}";
        }
        if (losingTimesInGameText != null)
        {
            losingTimesInGameText.text = $"Losing times: {s_LoseCount}";
        }
    }

    private void Start()
    {
        // Ensure in-game label shows current value as soon as play starts
        RefreshLosingTimesUI();
    }
}
