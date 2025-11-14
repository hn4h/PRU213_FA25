using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // [SerializeField] GameObject LevelPanel;
    [SerializeField] GameObject OptionPanelUI;
    [SerializeField] GameObject MainMenuButtons;
    [SerializeField] GameObject ConfirmDeleteData;
    
    [SerializeField] Button playButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button quitButton;

    [SerializeField] Button continueButton;
    AudioManager audioManager;
   private void Awake(){
        OptionPanelUI.SetActive(false);
        MainMenuButtons.SetActive(true);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            ResetPlayerData();
            StartCoroutine(LoadSceneAfterDelay(Loader.Scene.Story));
            
        });
        continueButton.onClick.AddListener(() =>
        {
            audioManager.PlaySFX(audioManager.action);
            Loader.Load(Loader.Scene.LevelScreen);
        });
        optionButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            OptionPanelUI.SetActive(true);
        });
        quitButton.onClick.AddListener(() =>
        {
            audioManager.PlaySFX(audioManager.action);
            SceneManager.LoadScene("AboutUs");
        });
    }

    private void Update() {
        if (ConfirmDeleteData.activeSelf && OptionPanelUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ConfirmDeleteData.SetActive(false);
        }
        else if (OptionPanelUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            OptionPanelUI.SetActive(false);
        }
    }

    private IEnumerator LoadSceneAfterDelay(Loader.Scene scene)
    {
        // Đợi một chút để sound effect kịp phát
        yield return new WaitForSeconds(0.2f);
        Loader.Load(scene);
    }

    private void ResetPlayerData()
    {
        // Xóa toàn bộ dữ liệu lưu trong PlayerPrefs và đặt lại level mở khóa về 1
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
    }
}
