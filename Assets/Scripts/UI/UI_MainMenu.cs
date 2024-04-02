using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button closeButton;

    private void Start()
    {
        startButton = transform.Find("GameStartButton").GetComponent<Button>();
        closeButton = transform.Find("GameCloseButton").GetComponent <Button>();

        SceneLoader.Instance.startButton = this.startButton;
        SceneLoader.Instance.closeButton = this.closeButton;
        SceneLoader.Instance.MainMenuButtonSetting();
    }
}
