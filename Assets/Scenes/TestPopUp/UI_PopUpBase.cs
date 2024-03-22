using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopUpBase : MonoBehaviour
{
    public enum PopUpResult
    {
        None,
        Ok,
        Cancel
    }

    protected Button closeButton;
    protected Action closeCallback;
    private void Start()
    {
        Init();
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
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
        transform.DOScale(1f, 0.2f).SetEase(Ease.InBounce);
    }
    public virtual void Close()
    {
        // 창을 닫을 때 수행 할 함수를  정의 
        closeCallback.Invoke();
        transform.DOScale(0f, 0.2f).SetEase(Ease.InBounce).onComplete += () => gameObject.SetActive(false);
    }
}
