using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance {get;private set;}

    [SerializeField] private Transform optionPanelUI;
    [SerializeField] private Button returnButton;

    [Header("Sound&Music Button")]
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Delete Data Button")]
    [SerializeField] private Button deleteDataButton;
    [SerializeField] private Transform confirmToDeleteData;
    [SerializeField] private Button confirmDeleteButton;
    [SerializeField] private Button cancelDeleteButton;

    [Header("PlayerController Button")]
    [SerializeField] private Button player1_jumpButton;
    [SerializeField] private Button player1_moveDownButton;
    [SerializeField] private Button player1_moveLeftButton;
    [SerializeField] private Button player1_moveRightButton;
    [SerializeField] private Button player2_jumpButton;
    [SerializeField] private Button player2_moveDownButton;
    [SerializeField] private Button player2_moveLeftButton;
    [SerializeField] private Button player2_moveRightButton;
    [SerializeField] private TextMeshProUGUI player1_jumpText;
    [SerializeField] private TextMeshProUGUI player1_moveDownText;
    [SerializeField] private TextMeshProUGUI player1_moveLeftText;
    [SerializeField] private TextMeshProUGUI player1_moveRightText;
    [SerializeField] private TextMeshProUGUI player2_jumpText;
    [SerializeField] private TextMeshProUGUI player2_moveDownText;
    [SerializeField] private TextMeshProUGUI player2_moveLeftText;
    [SerializeField] private TextMeshProUGUI player2_moveRightText;
    [SerializeField] private Transform pressKeyToRebindTransform;

       AudioManager audioManager;
    private void Awake() {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        confirmToDeleteData.gameObject.SetActive(false);
        HidePressKeyToRebindTransform();


        returnButton.onClick.AddListener(()=>{
            optionPanelUI.gameObject.SetActive(false);
            confirmToDeleteData.gameObject.SetActive(false);
            audioManager.PlaySFX(audioManager.action);
        });

        //===========Audio==========

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume",1);
            LoadMusicVolume();
        }
        else
        {
            LoadMusicVolume();
        }

        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume",1);
            LoadSoundVolume();
        }
        else
        {
            LoadSoundVolume();
        }
        

        //========================================
        deleteDataButton.onClick.AddListener(() =>
        {
            confirmToDeleteData.gameObject.SetActive(true);
            audioManager.PlaySFX(audioManager.action);
        });

        confirmDeleteButton.onClick.AddListener(() => {
            GameInput.Instance.ResetKeyMap();
            confirmToDeleteData.gameObject.SetActive(false);
            UpdateTextVisual();
            Time.timeScale = 1;
            audioManager.PlaySFX(audioManager.action);
        });
        cancelDeleteButton.onClick.AddListener(() => {
            confirmToDeleteData.gameObject.SetActive(false);
            audioManager.PlaySFX(audioManager.action);
        });

        //========================================

        player1_jumpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_Jump);audioManager.PlaySFX(audioManager.action);});
        player1_moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveDown);audioManager.PlaySFX(audioManager.action);});
        player1_moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveLeft);audioManager.PlaySFX(audioManager.action);});
        player1_moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveRight);audioManager.PlaySFX(audioManager.action);});
        player2_jumpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_Jump);audioManager.PlaySFX(audioManager.action);});
        player2_moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveDown);audioManager.PlaySFX(audioManager.action);});
        player2_moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveLeft);audioManager.PlaySFX(audioManager.action);});
        player2_moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveRight);audioManager.PlaySFX(audioManager.action);});
        //UpdateTextVisual();
    }

    private void Start() {
        UpdateTextVisual();
        //UpdateVisual();
    }

    private void UpdateTextVisual()
    {
        player1_jumpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_Jump);
        player1_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveDown);
        player1_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveLeft);
        player1_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveRight);
        player2_jumpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_Jump);
        player2_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveDown);
        player2_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveLeft);
        player2_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveRight);
    }

    private void ShowPressKeyToRebindTransform()
    {
        pressKeyToRebindTransform.gameObject.SetActive(true);
    }

    private void HidePressKeyToRebindTransform()
    {
        pressKeyToRebindTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressKeyToRebindTransform();
        GameInput.Instance.RebindBinding(binding,() => {
            HidePressKeyToRebindTransform();
            UpdateTextVisual();
        });
    }
    // private void UpdateVisual()
    // {
    //     soundEffectsText.text ="Sound Effects: " + Mathf.Round(PlayerPrefs.GetFloat("SoundVolume")*10f);
    //     musicText.text ="Music: " + Mathf.Round(PlayerPrefs.GetFloat("MusicVolume")*10f) ;
    // }

    private void LoadSoundVolume()
    {
        soundEffectSlider.value  = PlayerPrefs.GetFloat("SoundVolume"); 
    }

    private void LoadMusicVolume()
    {
        musicSlider.value  = PlayerPrefs.GetFloat("MusicVolume");   
    }

    public void ChangeVolumeMusic()
    {   
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.MusicVolume = musicSlider.value;  
            SaveMusicVolume();
        }
    }
    public void ChangeVolumeSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SoundVolume = soundEffectSlider.value;  
            SaveSoundVolume();
        }
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume",musicSlider.value);
    }

    private void SaveSoundVolume()
    {
        PlayerPrefs.SetFloat("SoundVolume",soundEffectSlider.value);
    }


}
