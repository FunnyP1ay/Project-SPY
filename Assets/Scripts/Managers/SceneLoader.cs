using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);


    }

    // �ٸ� ������ ��ȯ�ϴ� �Լ�
    public void SwitchToGameScene()
    {
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
