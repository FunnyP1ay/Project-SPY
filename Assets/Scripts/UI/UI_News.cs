using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_News : MonoBehaviour
{
    public List<string>     baseNews_List;
    public List<string>     playerNews_List;

    public UI_News_Value    newsPrefab;
    public RectTransform    newsPos;
    private Queue<string>   newsQueue       = new Queue<string>();
    private Queue<string>   playerQueue     = new Queue<string>();
    private int             rand;

    public string playerNew_1 = "���ó� �Ѱ����� �߻��ߴٴ� �ҽ��Դϴ�.";
    public string playerNew_2 = "������ ���ɿ��� ����� �߽��ϴ�.";
    public string playerNew_3 = "���� ������ ������ ������� �� �Դϴ�.";

    public string LawerNew_1 = "���ΰ� ������� ���� �߽��ϴ�.";
    public string LawerNew_2 = "���ΰ� ������� ���� �߽��ϴ�.";
    public string LawerNew_3 = "���ΰ� ���� ������ ���� �߽��ϴ�.";
    public string LawerNew_4 = "���ΰ� ���� ����� ���� �߽��ϴ�.";
    public string LawerNew_5 = "���ΰ� ���� ������ ���� �߽��ϴ�.";
    public string LawerNew_6 = "���ΰ� ���� ����� ���� �߽��ϴ�.";
    public string LawerNew_7 = "���ΰ� ���� ������ ���� �߽��ϴ�.";
    public string LawerNew_8 = "���ΰ� ���� ����� ���� �߽��ϴ�.";
    public string LawerNew_9 = "���ΰ� ���ַ��� ���� �߽��ϴ�.";
    public string LawerNew_10 = "���ΰ� ���ַ��� ���� �߽��ϴ�.";
    public string LawerNew_11 = "���ΰ� �� ������ ���� �߽��ϴ�.";
    public string LawerNew_12 = "���ΰ� �� ����� ���� �߽��ϴ�.";
    public string LawerNew_13 = "���ΰ� �ݷ����� ������ ���� �߽��ϴ�.";
    public string LawerNew_14 = "���ΰ� �ݷ����� ����� ���� �߽��ϴ�.";
    public string LawerNew_15 = "���ΰ� ���� �λ��� ���� �߽��ϴ�.";
    public string LawerNew_16 = "���ΰ� ���� ������ ���� �߽��ϴ�.";
    public string LawerNew_17 = "���ΰ� ������ �̺�Ʈ�� �����ϱ�� �߽��ϴ�.";
    public string LawerNew_18 = "���ΰ� ������ �̺�Ʈ�� �ߴ��ϱ�� �߽��ϴ�.";
    public string LawerNew_19 = "���ΰ� �ι����� ��븦 ���� �߽��ϴ�.";
    public string LawerNew_20 = "���ΰ� �ܹ߷��� ���� �߽��ϴ�.";
    public string LawerNew_21 = "���ΰ� �ｺ�� ���� ������ ���� �߽��ϴ�.";
    public string LawerNew_22 = "���ΰ� �ｺ�� ������ ���� �߽��ϴ�.";
    public string LawerNew_23 = "���ΰ� �۷��̵� ������ ���� �߽��ϴ�.";
    public string LawerNew_24 = "���ΰ� �۷��̵� ������ ���� �߽��ϴ�.";
    public string LawerNew_25 = "���ΰ� ������ �츮 ����� ��ȭ�� ���� �߽��ϴ�.";
    public string LawerNew_26 = "���ΰ� ������ ������ ���� �߽��ϴ�.";
    public string LawerNew_27 = "���ΰ� ������ ������ �ߴ� �߽��ϴ�.";

    private void Awake()
    {
     
        baseNews_List = new List<string> {
            " ���� ������ �ſ� ���� �Դϴ�. ",
            "�ֱ� �� �α��� �ð��ֽ��ϴ�.",
            "������ ����. Spy The Man ! �����մϴ�!",
            "���� �� 9�� �����̼� ! ��ġ�� ������ ",
            "�ֿ䰡�� ������ ��ȣȸ ȸ�� ���� ��",
            "�𿩶� �������� ! û�ұ� ��������� �Դϴ�",
            "�ܹ��� �ִ� �����?�� ������ ���۵ƽ��ϴ�",
            "(�ܵ�) ���ظ��� ��ġ���� �ڵ��� ������",
            "(�ܵ�) �ǻ� : ������ �ֻ�� ��� ������.",
            "���� ���������� �Ű�(�汸��)�� ��ǥ�߽��ϴ�",
            "�ֱ� �����̰� �ִٴ� �ҹ��� �����ֽ��ϴ�.",
            "�ֱ� �����̺� �м��� ���� �ϰ��ֽ��ϴ�.",
            "���� �̼����� �󵵴� �������� �����ϴ�",
            "��Ӹ� ����Ʈ�� �ֽ��� 10% ���������ϴ�.",
            "�۳� ������� 0.1���� 2.0 �̿����ϴ�.",
            "���� ���� ���� ������� 2.1% �Դϴ�.",
            "���ΰ� �ڵ����� ���ϸ� �������Դϴ�.",
            "û���� ���� �汸������ ���� �����Դϴ�.",
            "AI�� ���� ���ڸ� ������ �ɰ������� �ֽ��ϴ�",
            "������ ��� Ŀ�� �Һ� �����ϰ� �ֽ��ϴ�.",
            "��Ӹ� ����Ʈ�� �ֽ��� 8% �ö����ϴ�."
        };  //�ȿ� ���� ����
        playerNews_List = new List<string> { "���ó� �Ѱ����� �߻��ߴٴ� �ҽ��Դϴ�.","������ ���ɿ��� ����� �߽��ϴ�.","���� ������ ������ ������� �� �Դϴ�."
        };
    }
    void Start()
    {

        StartCoroutine(ShowNewsRoutine());
        StartCoroutine(NextNewsSet());
    }

  
    public void AddNews(string news)
    {
        print("������ �����߽��ϴ�");
        newsQueue.Enqueue(news);
    }
    public void PlayerNew()
    {
        rand = Random.Range(0, playerNews_List.Count);

        playerQueue.Enqueue(playerNews_List[rand]);
    }
    private IEnumerator ShowNewsRoutine()
    {
        while (newsQueue.Count > 0)
        {
            string news;
            if (playerQueue.Count > 0)
            {
                news = playerQueue.Dequeue();
            }
            else
            {
                news = newsQueue.Dequeue(); // Queue���� ���� ������
            }

            // UI �ؽ�Ʈ ����
            var newsText = Instantiate(newsPrefab, newsPrefab.transform);
            newsText.newsText.text = news;
            newsText.newsText.fontSize = 40f;

            yield return new WaitForSeconds(10f); // �� ���� ǥ�� �� ��� ���
        }
    }
    private IEnumerator NextNewsSet()
    {
        while (newsQueue.Count < 3)
        {
            print("�Ϲݴ����� �����߽��ϴ�");
            rand = Random.Range(0, baseNews_List.Count);
            AddNews(baseNews_List[rand]);
            yield return new WaitForSeconds(8f);
        }
    }

    // UI �г��� ȭ���� ������� Ȯ���ϴ� �Լ�
  
    // ���ο� ���� �߰�
 

}
