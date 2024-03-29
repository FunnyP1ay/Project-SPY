using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_News : MonoBehaviour
{
    public float moveSpeed = 100f; // UI �г��� �̵� �ӵ�
    private RectTransform rectTransform;
    private float panelWidth;

    private string baseNews_1 = "���� ������ ���� �Դϴ�.";
    private string baseNews_2 = "�ֱ� �� �α��� �ð��ֽ��ϴ�.";
    private string baseNews_3 = "������ ����. intern SPY �� �÷��� �غ����� !";
    private string baseNews_4 = "���� �� 9�� �����̼� ! ��ġ�� ������ !";
    private string baseNews_5 = "�ֿ䰡�� ������ ��ȣȸ ȸ�� ���� !";
    private string baseNews_6 = "�𿩶� �������� ! û�ұ� �����մϴ� !";
    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        panelWidth = rectTransform.rect.width;
    }

    void Update()
    {
        // UI �г��� ���������� �̵�
        rectTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // UI �г��� ȭ���� ����� ����
        if (IsOffScreen())
        {
            // ��Ȱ��ȭ�ϰų� ����
            gameObject.SetActive(false);
            // �Ǵ� Destroy(gameObject);
        }
    }

    // UI �г��� ȭ���� ������� Ȯ���ϴ� �Լ�
    bool IsOffScreen()
    {
        // UI �г��� ������ ��ǥ�� ȭ�� ���� ������ ������ ȭ���� ��� ������ �Ǵ�
        float rightEdge = rectTransform.position.x + panelWidth / 2;
        return rightEdge < 0f;
    }
}
