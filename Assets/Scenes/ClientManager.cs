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
public class ClientManager : MonoBehaviour
{
    public TextMeshProUGUI logText;
    public RectTransform textArea;

    public TMP_InputField   ipAddress;
    public TMP_InputField   port;

    StreamReader reader;
    StreamWriter writer;

    private Queue<string> log = new Queue<string>();

    public void ClientConnectButtonClick()
    {
        // ������ ���� ������ ����
        Thread thread = new Thread(ClientStart);
        thread.IsBackground = true;
        thread.Start();
    }
    private void ClientStart()
    {
        try
        {
            //���� ��� Ŭ���̾�Ʈ ��ü ����
            TcpClient tcpClient = new TcpClient();
            // ���� ���� ��Ŷ�� ���޵� �������� ���� �����Ͱ� ���ǵǾ� �ִ� IPEndPoint Ŭ���� ����
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress.text), int.Parse(port.text));
            
            tcpClient.Connect(endPoint); // ���� ������ �����Ͽ� listener�� Ȧ���� �� �ֵ��� ��

            log.Enqueue("Ŭ���̾�Ʈ�� ���ӵ�");

            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;

            while (tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                log.Enqueue(readString);
            }
        }
        catch(Exception e)
        {
            log.Enqueue("Exception Caused : "+ e.Message);
        }
    }

    public void MessageToServer(string message)
    {
        writer.WriteLine(message);
        log.Enqueue(message);
    }

    private void Update()
    {

        if(log.Count > 0)
        {
            var logText = Instantiate(this.logText,textArea);
            logText.text = log.Dequeue();
        }
    }

}
