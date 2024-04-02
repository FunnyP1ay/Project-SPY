using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EventCarmera : MonoBehaviour
{
    private void Awake()
    {
        UI_Manager.Instance.eventCarmera = this;
    }
    private void OnEnable()
    {
        StartCoroutine(OnCorutine());
    }
    IEnumerator OnCorutine()
    {
        yield return new WaitForSecondsRealtime(4f);
        gameObject.SetActive(false);
    }
}
