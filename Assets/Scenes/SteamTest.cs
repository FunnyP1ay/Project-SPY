
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
        /*스팀 AppId*/
        SteamClient.Init(480); //기본 테스트용 은 480임
        print(SteamClient.Name);
    }
}
