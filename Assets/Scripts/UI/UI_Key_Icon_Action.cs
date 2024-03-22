using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class UI_Key_Icon_Action : MonoBehaviour
{
    public  GameObject  F_Key_Icon;
    public void F_Key_SetActive_True()
    {
        F_Key_Icon.SetActive(true);
    }
    public void F_key_SetActive_False()
    {
        F_Key_Icon.SetActive(false);
    }
}