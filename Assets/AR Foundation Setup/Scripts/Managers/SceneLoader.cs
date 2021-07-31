using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if(Instance is null)
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
      
    }
    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadLevelAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void LoadLevel(string sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
