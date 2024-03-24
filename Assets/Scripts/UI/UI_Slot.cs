using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{

    public Image lawImageIcon;

    private void Start()
    {
        lawImageIcon = GetComponent<Image>();
    }
}
