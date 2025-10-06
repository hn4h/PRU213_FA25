
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("---------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip-----------")]
    // Music
    public AudioClip background;
    public AudioClip quizbackground;
    public AudioClip correctAns;
    public AudioClip incorrectAns;
    // SoundEffects
    public AudioClip jump1a;
    public AudioClip jump1b;
    public AudioClip jump1c;
    public AudioClip jump2a;
    public AudioClip jump2b;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip death;
    public AudioClip action;

    private float volumeMusic;
    private float volumeSFX;

    public float MusicVolume{get{return volumeMusic;} set{volumeMusic = value;}}
    public float SoundVolume{get{return volumeSFX;} set{volumeSFX = value;}}

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume",1);
        }
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume",1);
        }
        volumeMusic = PlayerPrefs.GetFloat("MusicVolume");
        volumeSFX = PlayerPrefs.GetFloat("SoundVolume");
        SFXSource.volume = volumeSFX;
        musicSource.volume = volumeMusic;     
        PlayBackgroundMusic(background);
    }

    private void Update() {
        SFXSource.volume = volumeSFX;
        musicSource.volume = volumeMusic;
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip background)
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    // public void ChangeVolumeMusic()
    // {
    //     volumeMusic += .1f;
    //     if (volumeMusic > 1f)
    //     {
    //         volumeMusic = 0f;
    //     }
    //     musicSource.volume = volumeMusic;
    //     PlayerPrefs.SetFloat("MusicVolume",musicSource.volume);
    //     PlayerPrefs.Save();
    // }
    public float GetvolumeMusic()
    {
        return volumeMusic;
    }
    // public void ChangeVolumeSFX()
    // {
    //     volumeSFX += .1f;
    //     if (volumeSFX > 1f)
    //     {
    //         volumeSFX = 0f;
    //     }
    //     SFXSource.volume = volumeSFX;
    //     PlayerPrefs.SetFloat("SoundVolume",SFXSource.volume);
    //     PlayerPrefs.Save();
    // }
    public float GetvolumeSFX()
    {
        return volumeSFX;
    }
}
