using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinManager : MonoBehaviour
{
    public static GameWinManager Instance {private set; get;}
    [SerializeField] private GameObject winningUI;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Loader.Scene nextScene;
    AudioManager audioManager;

   private void Awake(){
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Hide();
        
        nextLevelButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            NextLevel();
        });

        backButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            ReturnHome();
        });
   }

    public void Hide()
    {
        winningUI.SetActive(false);
    }

    public void Show()
    {
        winningUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void NextLevel()
    {
        Loader.Load(nextScene);
        Time.timeScale = 1f;
    }

    public void ReturnHome()
    {
        Loader.Load(Loader.Scene.LevelScreen);
        Time.timeScale = 1f;
    }
}
