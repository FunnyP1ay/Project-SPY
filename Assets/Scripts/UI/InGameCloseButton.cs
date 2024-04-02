using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCloseButton : MonoBehaviour
{
    private Button closeButton;
    private Button returnGameButton;
    void Start()
    {
        closeButton = transform.Find("YButton").GetComponent<Button>();
        returnGameButton = transform.Find("NButton").GetComponent <Button>();
        SceneLoader.Instance.InGameButtonSetting();
    }


}
