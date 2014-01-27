using UnityEngine;
using AllJoynUnity;
using basic_clientserver;

public class StartGame : MonoBehaviour {

    private static int BUTTON_SIZE = 50;

    private const float MOTION = 0.1f;

    private bool playerToggle = false;

    public static string playerName = "";
    private static bool server = false;

    GUIStyle menuStyle = new GUIStyle();

    private string tmpSession;

    //GUI Elemente für das Menü erzeugen
    void OnGUI()
    {
        int i = 0;
        //int xStart = (Screen.height / 2) + 10 + ((i++) * BUTTON_SIZE);
        int xStart = 0;

        
        menuStyle.fontSize = 20;
        GUI.Box(new Rect(Screen.width / 4, 10, Screen.width / 2, Screen.height - 20), "<color=green><size=20>RoboTx Droid Ar Game</size></color>");

        playerToggle = GUI.Toggle(new Rect(Screen.width / 4+50, 50, 400, 20), playerToggle, "<size=16>2 Player</size>");

        if (playerToggle)
        {
            GUI.Box(new Rect(Screen.width / 4 + 50, 90, Screen.width / 2 - 100, Screen.height - 170), "<b>Spiel erstellen</b>");

            //Player Name erstellen
            GUI.Label(new Rect(Screen.width / 4 + 60, 128, 50, BUTTON_SIZE), "Name:");
            playerName = GUI.TextField(new Rect(Screen.width / 4 + 115, 120, 200, BUTTON_SIZE), playerName);

            if (GUI.Button(new Rect(Screen.width / 4 + 340, 120, 150, BUTTON_SIZE), "Session erstellen") && playerName != "")
            {
                BasicChatVar.basicChat.CloseDown();
                BasicChatVar.basicChat.StartUp();
                BasicChatVar.basicChat.createAndAdvertiseSession(playerName);
            }

            //Gefundene Alljoyn Sessions
            GUI.Label(new Rect(Screen.width / 2 - 56, 200, 200, BUTTON_SIZE), "<b>Geöffnete Session</b>");
            string[] nameWithoutGUUIDCon;
            foreach (string name in BasicChat.sFoundName)
            {
               xStart = 50 + ((i++) * BUTTON_SIZE);
               nameWithoutGUUIDCon = name.Substring(name.LastIndexOf(".")).Split('_');
               if (GUI.Button(new Rect(Screen.width / 4 + 50 + 10, 200 + xStart, 400, BUTTON_SIZE), "Verbinden: " + nameWithoutGUUIDCon[1]))
                {
                   BasicChatVar.basicChat.JoinSession(name);
                }
            }

            //Bereits hinzugefügt Sessions
            GUI.Label(new Rect(Screen.width / 2 - 60, 250 + xStart, 200, BUTTON_SIZE), "<b>Session beigetreten</b>");
            string[] nameWithoutGUUIDDisc;
            if (BasicChat.currentJoinedSession != null)
            {
                if (BasicChat.sFoundName.Contains(BasicChat.currentJoinedSession))
                {
                    BasicChatVar.playerNumber = 0;
                }
                else
                {
                    BasicChatVar.playerNumber = 1;
                }

                nameWithoutGUUIDDisc = BasicChat.currentJoinedSession.Substring(BasicChat.currentJoinedSession.LastIndexOf(".")).Split('_');
                if (GUI.Button(new Rect(Screen.width / 4 + 50 + 10, xStart + 280, 400, BUTTON_SIZE),
                    "Verlassen: " + nameWithoutGUUIDDisc[1]))
                {
                    BasicChatVar.basicChat.LeaveSession();
                }

            }

            GUI.Box(new Rect(Screen.width / 2 - 56, Screen.height - 200, 200, BUTTON_SIZE), BasicChatVar.playerNumber.ToString());

            
        }

        //StartButton
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - BUTTON_SIZE - 20, 200, BUTTON_SIZE), "Start Game!"))
        {
            if (BasicChat.currentJoinedSession == null)
            {
                BasicChatVar.playerNumber = 0;
            }

            Application.LoadLevel(1);
        }
    }

    void Start()
    {
        Debug.Log("Starting up AllJoyn service and client");
        BasicChatVar.basicChat = new BasicChat();
        //basicChat.StartUp();
    }
}
