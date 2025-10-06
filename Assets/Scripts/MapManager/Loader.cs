using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{

    public enum Scene{
        MainMenuScene,
        LoadingScene,
        SelectLevelScene,
        Level1Scene,
        Level2Scene,
        Level3Scene,
        Level4Scene,
        Level5Scene,
    }
    
    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void Load(int currentSceneIndex)
    {
        SceneManager.LoadScene(currentSceneIndex);
    }



    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
    public static void LoadTheNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        }
    }
}
