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
            "���� ������ �ſ� ���� �ϰ����� ����˴ϴ�. ",
            "�ֱ� �� �α��� �ð��ִٴ� �ҽ��Դϴ�.",
            "������ ���� ! SPY THE MAN ! �����մϴ�!",
            "���� �� 9�� �� �����̼� ! ��ġ�� ������ !",
            "(����) �ֿ䰡 And ������ ��ȣȸ ȸ�� ���� ��",
            "���� ���� �Ʒ��忡�� ������ Ż���߽��ϴ�.",
            "���ΰ� ������ ���� ������ �켱���Ǹ� ��ǥ�߽��ϴ�.",
            "�𿩶� �������� ! û�ұ� ��������� �Դϴ�",
            "�ܹ��� �ִ� �����?�� ������ ���۵ƽ��ϴ�",
            "�ֱ� ������ �ް��ϰ� �����ϰ� �ֽ��ϴ�.",
            "���������� ���� ���ڰ� �α⸦ �����ֽ��ϴ�.",
            "���� �����ɾ��� �Ű�(�汸��)�� ��ǥ�߽��ϴ�",
            "���ÿ� �����̰� �ִٴ� �ҹ��� �����ֽ��ϴ�.",
            "���� û����̿� �����̺� �м��� ���� �ϰ��ֽ��ϴ�.",
            "���� �̼����� �󵵴� �������� �����ϴ�",
            "��Ӹ� ����Ʈ�� �ֽ��� 10% ���������ϴ�.",
            "�۳� ������� ���۳⺸�� ������ 2.0 �̿����ϴ�.",
            "���� ���� �츮���� ������� 2.1% �Դϴ�.",
            "���ΰ� �ڵ����� ���ϸ� ������ �̶�� �������ϴ�",
            "û���� ���� '���'���� 200������ ���� �����Դϴ�.",
            "AI�� ���� ���ڸ� ������ �ɰ������� �ֽ��ϴ�",
            "������ ��� Ŀ�� �Һ� �����ϰ� �ֽ��ϴ�.",
            "��Ӹ� ����Ʈ�� �ֽ��� 8% �ö����ϴ�.",
            "û�� �Ǿ��� �ɰ������� �־� ��ȸ������ �����Դϴ�.",
            "�ҿ��� ��Ӹ� ���� ������ �� 210$ �𿴽��ϴ�.",
            "���� �̿��� ģ�� ķ����'�����̰� ����' �����Դϴ�.",
            "�ڽ��� ��Ÿ��� �����ϴ� �ù��� ��Ÿ�� ȭ���Դϴ�.",
            "�Ӽӿ� ��û ��ġ�� ��������ϴ� ������! �Ӽӿ� ��û..",
            "���� ����Ʈ ����'å ���� �ȴ�'�� 200���� �ȷȽ��ϴ�.",
            "�ؿܿ��� ���ݿ� ����� �����ڰ� ���������ϴ�.",
            
            
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
        while (true)
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
            var newsText = Instantiate(newsPrefab, newsPos);
            newsText.newsText.text = news;
            newsText.newsText.fontSize = 40f;

            yield return new WaitForSeconds(4f); // �� ���� ǥ�� �� ��� ���
        }
    }
    private IEnumerator NextNewsSet()
    {
        while (true)
        {
            print("�Ϲݴ����� �����߽��ϴ�");
            rand = Random.Range(0, baseNews_List.Count);
            AddNews(baseNews_List[rand]);
            yield return new WaitForSeconds(3.9f);
        }
    }

    // UI �г��� ȭ���� ������� Ȯ���ϴ� �Լ�
  
    // ���ο� ���� �߰�
 

}
