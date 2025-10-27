using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LevelUI : MonoBehaviour
{
    [SerializeField] Button BackButton;
    [SerializeField] private Button[] levelButtons;
    AudioManager audioManager;
    
    private void Start()
    {
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
        }
        
        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevel; i++)
        {
            levelButtons[i].interactable = true;
        }
        if (audioManager != null)
        {
            BackButton.onClick.AddListener(() =>
            {
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.MainMenuScene));
            });


            levelButtons[0].onClick.AddListener(() =>
            {
                // Chọn Level 1
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Level1Scene));
            });

            levelButtons[1].onClick.AddListener(() =>
            {
                // Chọn Level 2
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Level2Scene));
            });

            levelButtons[2].onClick.AddListener(() =>
            {
                // Chọn Level 3
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Level3Scene));
            });

            levelButtons[3].onClick.AddListener(() =>
            {
                // Chọn Level 4
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Level4Scene));
            });

            levelButtons[4].onClick.AddListener(() =>
            {
                // Chọn Level 5
                audioManager.PlaySFX(audioManager.action);
                StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Level5Scene));
            });
        }
    }
    private IEnumerator LoadSceneAfterDelay(Loader.Scene scene)
    {
        // Đợi một chút để sound effect kịp phát
        yield return new WaitForSeconds(0.2f);
        Loader.Load(scene);
    }
}
