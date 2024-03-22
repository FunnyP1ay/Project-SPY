using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopUpResult
{
    None,
    Ok,
    Cancel
}

public class UI_PopUpBase : MonoBehaviour
{
    protected Button closeButton;
    protected Action closeCallback;
    private void Start()
    {
        Init();
    }
    protected virtual void Awake()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
    }

    protected virtual void Init()
    {
        closeButton.onClick.AddListener(Close);
    }
    
    public virtual void Open(Action closeCallback = null) // 
    {
        this.closeCallback = closeCallback;
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        // 창을 닫을 때 수행 할 함수를  정의 
        closeCallback.Invoke();
        gameObject.SetActive(false);
    }
}
