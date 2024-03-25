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
        // 서버와 붙을 스레드 생성
        Thread thread = new Thread(ClientStart);
        thread.IsBackground = true;
        thread.Start();
    }
    private void ClientStart()
    {
        try
        {
            //소켓 통신 클라이언트 객체 생성
            TcpClient tcpClient = new TcpClient();
            // 내가 보낸 패킷이 전달될 목적지에 대한 데이터가 정의되어 있는 IPEndPoint 클래스 생성
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress.text), int.Parse(port.text));
            
            tcpClient.Connect(endPoint); // 실제 서버를 연결하여 listener가 홀딩할 수 있도록 함

            log.Enqueue("클라이언트가 접속됨");

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
