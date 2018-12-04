using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
	//public Text resultText;
	public InputField messageInputField;
	public Text messageText;
	string nickname;

	SocketIOComponent socket;

	void Start ()
    {
        GameObject io = GameObject.Find("SocketIO");
        socket = io.GetComponent<SocketIOComponent>();

		nickname = "Jimoon"; //나중에 서버닉네임 가져오기 구현
							 //socket.On("hello", Hello);

		socket.On("chat", UpdateMessage); //이렇게 호출할 수 있는 함수는 앞 인자의 SocketIOEvent객체를 받는 함수만 가능.
	}
	
	void UpdateMessage(SocketIOEvent chat)
	{
		string nick = chat.data.GetField("nick").str;
		string msg = chat.data.GetField("msg").str;

		messageText.text += string.Format("{0}:{1}\n", nick, msg);
	}

	//void Hello(SocketIOEvent e)
	//{
	//	Debug.Log("Hello");
	//	resultText.text = "Hello";
	//}

	//public void Hi()
	//{
	//	socket.Emit("hi"); //
	//}

	public void Send()
	{
		//자신의 메시지 화면에 표시
		string message = messageInputField.text;
		messageText.text += string.Format("{0}:{1}\n", nickname, message);

		//자신이 입력한 메시지 서버에 전송
		JSONObject obj = new JSONObject();
		obj.AddField("nick", nickname);
		obj.AddField("msg", message);

		socket.Emit("message", obj);

		messageInputField.text = "";
	}
}
