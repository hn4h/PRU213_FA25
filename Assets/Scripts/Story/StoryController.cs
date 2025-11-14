using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public GameObject button;  
    public float delay = 5f;   

    void Start()
    {
        button.SetActive(false);          
        Invoke("ShowButton", delay);      
    }

    void ShowButton()
    {
        button.SetActive(true);           
    }

    public void ToLevelScreen(int sceneIndex)
    {
        SceneManager.LoadScene("LevelScreen");
    }

        public void ToMenuScreen(int sceneIndex)
    {
        SceneManager.LoadScene("MainMenuScreen");
    }
}
