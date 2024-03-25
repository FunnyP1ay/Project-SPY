using System.Collections;
using System.Collections.Generic;
using System.IO;  // 입출력에 관련된 namespace
using System.Net; // 네트워킹에 관련된 namespace
using System.Net.Sockets; // TCP(소켓) 통신에 관련된 namespace
using System.Threading;   // 멀티 스레딩에 관련한 namespace
using System;             // 예외(Exception)을 참조하기 위한 namespace
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public Button connectButton;
    public TextMeshProUGUI logText;
    public string ipAddress = "127.0.0.1";
    public int port = 9999;

    // 파일 입/출력, 네트워킹 송/수신에 활용되는 스트림
    StreamReader reader = null;
    StreamWriter writer = null;

    private Queue<string> log = new Queue<string>();

    bool isConntected = false;
    private Thread serverThread;

    public void ServerConnectButtonClick()
    {
        if (isConntected == false)
        {
            //서버열기 (멀티스레딩)
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

    private void ServerStart() // 서버에서 패킷을 받는  프로세스를 실행
    {
        try
        {
            // 지속으로 호출(=> update와 흡사, multithread 구현)
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);

            tcpListener.Start();

            log.Enqueue("\n server Start");

            TcpClient tcpClient = tcpListener.AcceptTcpClient();

            log.Enqueue("\nClient Connected");

            //접속된 클라이언트로부터 송/수진 스트림 생성
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
        catch (Exception e) // 어떤 예외가 발생했는지 알려주는 클래스 Exception
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
