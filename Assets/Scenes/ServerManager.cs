using System.Collections;
using System.Collections.Generic;
using System.IO;  // ����¿� ���õ� namespace
using System.Net; // ��Ʈ��ŷ�� ���õ� namespace
using System.Net.Sockets; // TCP(����) ��ſ� ���õ� namespace
using System.Threading;   // ��Ƽ �������� ������ namespace
using System;             // ����(Exception)�� �����ϱ� ���� namespace
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public Button connectButton;
    public TextMeshProUGUI logText;
    public string ipAddress = "127.0.0.1";
    public int port = 9999;

    // ���� ��/���, ��Ʈ��ŷ ��/���ſ� Ȱ��Ǵ� ��Ʈ��
    StreamReader reader = null;
    StreamWriter writer = null;

    private Queue<string> log = new Queue<string>();

    bool isConntected = false;
    private Thread serverThread;

    public void ServerConnectButtonClick()
    {
        if (isConntected == false)
        {
            //�������� (��Ƽ������)
            serverThread = new Thread(ServerStart);
            serverThread.IsBackground = true;
            serverThread.Start();
            isConntected = true;
        }
        else
        {
            serverThread.Abort();
            isConntected = false;

        }
    }

    private void ServerStart() // �������� ��Ŷ�� �޴�  ���μ����� ����
    {
        try
        {
            // �������� ȣ��(=> update�� ���, multithread ����)
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);

            tcpListener.Start();

            log.Enqueue("\n server Start");

            TcpClient tcpClient = tcpListener.AcceptTcpClient();

            log.Enqueue("\nClient Connected");

            //���ӵ� Ŭ���̾�Ʈ�κ��� ��/���� ��Ʈ�� ����
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());

            while (tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                if (string.IsNullOrEmpty(readString))
                {
                    continue;
                }
                log.Enqueue(readString);
            }
        }
        catch (Exception e) // � ���ܰ� �߻��ߴ��� �˷��ִ� Ŭ���� Exception
        {
            log.Enqueue("server exception caused"+ e.Message);
        }
    }

    private void Update()
    {
        if(log.Count > 0)
        {
            logText.text += log.Dequeue();
        }
       
    }
}
