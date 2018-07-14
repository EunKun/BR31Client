using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BR31Lobby;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System;

public class Test : MonoBehaviour {

    [SerializeField]
    public string connectHost;
    [SerializeField]
    public string playerName;

    NetworkStream stream;

    public void Send(JObject data)
    {
        // byte[]를 이런 식으로 작성하는 것을 직렬화라고 하고, 이러한 방식으로(직렬화 하여) 데이터를 만들어 서버 송수신을 하게 된다.
        byte[] payload = Encoding.UTF8.GetBytes(data.ToString());
        byte[] header = BitConverter.GetBytes((short)payload.Length); 

        stream.Write(header, 0, header.Length); // 해더를 서버에 보낸다. 
        stream.Write(payload, 0, payload.Length); // 패킷을 서버에 보낸다. 이러한 방식으로 서버 전송이 이루어진다
    }

    void Start()
    {
        TcpClient client  = new TcpClient();
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 16333);
        client.Connect(endpoint);
        stream = client.GetStream();

        JObject joinRoomReq = new JObject()
        {
            ["ReqType"] = "JoinRoomReq",
            ["PlayerName"] = playerName,
            ["SubmitNumberReq"] = "",// 접속시 부여받을 networkID를 받아오려면??
            //["Name"] = "전필규",
            //["SubmitNumber"] = 3
        };

        Send(joinRoomReq);

        // 패킷의 사이즈(2), 직렬화된 Json Object => 하나의 패킷
        
    }
}
