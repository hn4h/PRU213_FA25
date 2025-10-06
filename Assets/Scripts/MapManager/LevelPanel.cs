using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelPanel : MonoBehaviour
{   
    [SerializeField] private Button quitButton;
    [SerializeField] private Button[] levelButtons;

    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        int unlockLevel = PlayerPrefs.GetInt("UnlockedLevel",1);

        quitButton.onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            gameObject.SetActive(false);
        });

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevel; i++)
        {
            levelButtons[i].interactable = true;
        }

        levelButtons[0].onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Loader.Load(Loader.Scene.Level1Scene);
        });
        levelButtons[1].onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Loader.Load(Loader.Scene.Level2Scene);
        });
        levelButtons[2].onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Loader.Load(Loader.Scene.Level3Scene);
        });
        levelButtons[3].onClick.AddListener(() =>{
            audioManager.PlaySFX(audioManager.action);
            Loader.Load(Loader.Scene.Level4Scene);
        });


    } 
}
