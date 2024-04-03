using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public Button startButton;
    public Button closeButton;
    public Button mainMenuButton;
    public Button returnGameButton;
    public void MainMenuButtonSetting()
    {
        startButton.onClick.AddListener(() => SwitchToGameScene());
        closeButton.onClick.AddListener(() => SwitchToClose());
    }
    public void InGameButtonSetting()
    {
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.GameOver());
        returnGameButton.onClick.AddListener(() => UI_Manager.Instance.PopUp(UI_Manager.Instance.ui_ClosePanel.gameObject, true));
    }

    // 다른 씬으로 전환하는 함수
    public void SwitchToGameScene()
    {
        
        GameManager.Instance.ResetDATA();
        SceneManager.LoadScene("GameScene");
        
    }
    public void SwitchToClose()
    {
        Application.Quit();
    }
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
