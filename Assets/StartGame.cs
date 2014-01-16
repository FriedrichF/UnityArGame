using UnityEngine;
using AllJoynUnity;
using basic_clientserver;

public class StartGame : MonoBehaviour {

    private static int BUTTON_SIZE = 75;

    private const float MOTION = 0.1f;

    void OnGUI()
    {
        int i = 0;
        int xStart = (Screen.height / 2) + 10 + ((i++) * BUTTON_SIZE);

        if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - BUTTON_SIZE/2, 200, BUTTON_SIZE), "Start Game!"))
        {
            Application.LoadLevel(1);
        }

        if (BasicChat.currentJoinedSession != null)
        {
            if (GUI.Button(new Rect(((Screen.width) / 3), 0, (Screen.width) / 3, BUTTON_SIZE),
                "Leave \n" + BasicChat.currentJoinedSession.Substring(BasicChat.currentJoinedSession.LastIndexOf("."))))
            {
                BasicChatVar.basicChat.LeaveSession();
            }
        }


        foreach (string name in BasicChat.sFoundName)
        {
            xStart = 10 + ((i++) * BUTTON_SIZE);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - xStart, Screen.width / 3, BUTTON_SIZE), "Connect to: " + name.Substring(name.LastIndexOf("."))))
            {
                BasicChatVar.basicChat.JoinSession(name);
            }
        }
    }

    void Start()
    {
        Debug.Log("Starting up AllJoyn service and client");
        BasicChatVar.basicChat = new BasicChat();
        //basicChat.StartUp();
    }
}
