using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FrameRate
{
    FPS_30 = 30,
    FPS_60 = 60,
    FPS_70 = 70,
    FPS_90 = 90,
    FPS_120 = 120,
    FPS_144 = 144,

}
public class GameManager : MonoBehaviour
{
    public FrameRate frameRate;
    private void Awake()
    {
        Application.targetFrameRate = (int)frameRate;


        DontDestroyOnLoad(this);
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }   
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
