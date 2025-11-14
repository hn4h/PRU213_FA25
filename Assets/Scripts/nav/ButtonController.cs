using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void Loadlevel(int sceneIndex)
    {
        SceneManager.LoadScene("Story");
    }
    //public void LoadMain(int sceneIndex)
    //{
    //    SceneManager.LoadScene("MainScreen");
    //}
    //public void LoadLevel1(int sceneIndex)
    //{
    //    SceneManager.LoadScene("senceExample");
    //}

}
