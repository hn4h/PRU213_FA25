using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    [SerializeField] GameObject OptionPanelUI;
    [SerializeField] GameObject MainMenuButtons;
    [SerializeField] GameObject ConfirmDeleteData;
    
    [SerializeField] Button playButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button quitButton;
    

   AudioManager audioManager;
   private void Awake(){
        OptionPanelUI.SetActive(false);
        MainMenuButtons.SetActive(true);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            LevelPanel.SetActive(true);
        });
        optionButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            OptionPanelUI.SetActive(true);
        });
        quitButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Application.Quit();
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
}
