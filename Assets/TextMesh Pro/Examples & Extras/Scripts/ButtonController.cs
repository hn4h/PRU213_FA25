using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Loadlevel(int sceneIndex)
    {
        SceneManager.LoadScene("LevelScreen");
    }
    public void LoadMain(int sceneIndex)
    {
        SceneManager.LoadScene("MainScreen");
    }
    public void LoadLevel1(int sceneIndex)
    {
        SceneManager.LoadScene("Demo");
    }

}
