using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PoliceIcon : MonoBehaviour
{
    public Sprite           policeIcon;
    public int              policeIconCount=0;
    public List<Image>      currentPoliceIcon_List;
    public void PoliceIconSetting(int _value)
    {
        foreach(Image _image in currentPoliceIcon_List)
        {
            _image.gameObject.SetActive(false);
            _image.sprite = null;
        }

        policeIconCount += _value;
       if (policeIconCount <= 0)
       {
          policeIconCount = 0;
       }
       else
       {
          if(policeIconCount > 3)
             policeIconCount = 3;
            for (int i = 0; i < policeIconCount; i++)
            {
                currentPoliceIcon_List[i].gameObject.SetActive(true);
                currentPoliceIcon_List[i].sprite = policeIcon;
            }
       }
    }
}
