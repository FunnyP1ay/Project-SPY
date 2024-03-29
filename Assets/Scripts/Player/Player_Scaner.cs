using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scaner : MonoBehaviour
{
    public GameObject scaner; // �˾� UI ������Ʈ
    public PlayerMove player;
    private void Awake()
    {
        player = GetComponent<PlayerMove>();
        DOTween.Init(); // DOTween �ʱ�ȭ
    }
    public void ShowPopup()
    {
        // �˾��� �ʱ� ���·� ���� (�ִϸ��̼� ���� ��)
        scaner.transform.localScale = Vector3.zero;

        // DoTween�� ����Ͽ� �������� 20�� �����ϴ� �ִϸ��̼��� ����
        scaner.transform.DOScale(20f, 1f); // 1�� ���� �������� 20�� ����
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
