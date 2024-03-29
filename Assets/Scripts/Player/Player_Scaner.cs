using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scaner : MonoBehaviour
{
    public GameObject scaner; // 팝업 UI 오브젝트
    public PlayerMove player;
    private void Awake()
    {
        player = GetComponent<PlayerMove>();
        DOTween.Init(); // DOTween 초기화
    }
    public void ShowPopup()
    {
        // 팝업을 초기 상태로 설정 (애니메이션 시작 전)
        scaner.transform.localScale = Vector3.zero;

        // DoTween을 사용하여 스케일을 20로 변경하는 애니메이션을 생성
        scaner.transform.DOScale(20f, 1f); // 1초 동안 스케일을 20로 변경
        StartCoroutine(ScanerWaitTime());
    }
    IEnumerator ScanerWaitTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        scaner.transform.localScale = Vector3.zero;
        player.isScanerOn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CitizenINFO _citizen))
        {
            _citizen.Show_INFO_Panel();
        }
    }
}
