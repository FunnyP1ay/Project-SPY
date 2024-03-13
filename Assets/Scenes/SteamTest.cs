
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Steamworks;

using UnityImage = UnityEngine.UI.Image;
using SteamImage = Steamworks.Data.Image;

public class SteamTest : MonoBehaviour
{
    public UnityImage avatarImage;


    private async void Start()
    {
        /*스팀 AppId*/
        SteamClient.Init(480); //플레이어 자기 자신 id는 480이다. 외우기
        print(SteamClient.Name);
        //비동기
        SteamImage? avatarNullableImage = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
        SteamImage avatarImage;
        if (avatarNullableImage.HasValue)
        {
            avatarImage = avatarNullableImage.Value;
            Texture2D avatarTexture = new Texture2D((int)avatarImage.Width, (int)avatarImage.Height,
                TextureFormat.ARGB32, false);
            avatarTexture.filterMode = FilterMode.Trilinear;

            for (int x = 0; x < avatarImage.Width; x++)
            {
                for(int y = 0; y < avatarImage.Height; y++)
                {
                    var P = avatarImage.GetPixel(x, y);
                    var color = new Color(P.r / 255f, P.g / 255f, P.b / 255f, P.a / 255f); //유니티 엔진의 컬러로 변환하는 중
                    avatarTexture.SetPixel(x, (int)avatarImage.Height - y, color);
                }
            }

            avatarTexture.Apply();

            Sprite avatarSprite = 
                Sprite.Create(avatarTexture, 
                new Rect(0, 0, avatarTexture.width, avatarTexture.height),
                new Vector2(0.5f,0.5f));

           this.avatarImage.sprite = avatarSprite;
        }
        Test();
    }
    private async void Test()
    {
        foreach (var friend in SteamFriends.GetFriends())
        {
            SteamImage? friendImage = await SteamFriends.GetLargeAvatarAsync(friend.Id);
            if(friendImage.HasValue)
            {
                var friendSprite =SteamImageToSprite(friendImage.Value);
                var friendImageUI = Instantiate(avatarImage, avatarImage.transform.parent);
                friendImageUI.sprite = friendSprite;

                
            }
        }
    }
    Sprite SteamImageToSprite(SteamImage _id)
    {

        SteamImage friendImage = _id;
            Texture2D avatarTexture = new Texture2D((int)friendImage.Width, (int)friendImage.Height,
                TextureFormat.ARGB32, false);
            avatarTexture.filterMode = FilterMode.Trilinear;

            for (int x = 0; x < friendImage.Width; x++)
            {
                for (int y = 0; y < friendImage.Height; y++)
                {
                    var P = friendImage.GetPixel(x, y);
                    var color = new Color(P.r / 255f, P.g / 255f, P.b / 255f, P.a / 255f); //유니티 엔진의 컬러로 변환하는 중
                    avatarTexture.SetPixel(x, (int)friendImage.Height - y, color);
                }
            }

            avatarTexture.Apply();

            Sprite avatarSprite =
                Sprite.Create(avatarTexture,
                new Rect(0, 0, avatarTexture.width, avatarTexture.height),
                new Vector2(0.5f, 0.5f));
        return avatarSprite;
    }

}
