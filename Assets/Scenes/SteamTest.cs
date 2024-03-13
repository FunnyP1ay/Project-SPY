
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Steamworks;

using UnityImage = UnityEngine.UI.Image;
using SteamImage = Steamworks.Data.Image;

public class SteamTest : MonoBehaviour
{
    public UnityImage avatarImage;

    private void Start()
    {
        /*���� AppId*/
        SteamClient.Init(480); //�⺻ �׽�Ʈ�� �� 480��
        print(SteamClient.Name);
    }
}
