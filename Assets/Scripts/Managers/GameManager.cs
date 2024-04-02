using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isFirst = true;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);


    }

    public void GameOver()
    {
        isFirst = false;
        SceneLoader.Instance.SwitchToMainMenu();
        SceneLoader.Instance.MainMenuButtonSetting();
    }
    public void ResetDATA()
    {
        if (isFirst == false)
        {
            CityControlData.Instance.ResetDATA();
            MapData.Instance.ResetDATA();
            UI_Manager.Instance.ResetDATA();
        }
    }
    public void MainMenuRaod()
    {

    }
    public void GameColse()
    {
        Application.Quit();
    }

}
