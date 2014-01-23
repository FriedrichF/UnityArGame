//-----------------------------------------------------------------------
// <copyright file="AllJoynClientServer.cs" company="AllSeen Alliance.">
// Copyright (c) 2012, AllSeen Alliance. All rights reserved.
//
//    Permission to use, copy, modify, and/or distribute this software for any
//    purpose with or without fee is hereby granted, provided that the above
//    copyright notice and this permission notice appear in all copies.
//
//    THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
//    WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
//    MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
//    ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
//    WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
//    ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
//    OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;
using AllJoynUnity;
using basic_clientserver;

public class AllJoynClientServer : MonoBehaviour
{

    private static int BUTTON_SIZE = 75;

    private Vector3 positionTmp = new Vector3(0,0.5f,0); 

    private const float MOTION = 0.1f;

    private static bool gameOver = false;

    void OnGUI()
    {

        int i = 0;
        int xStart = (Screen.height / 2) + 10 + ((i++) * BUTTON_SIZE);

        GUI.contentColor = Color.black;

        GUI.Box(new Rect(100, 45, 150, 25), "Gegner Punkte: " + BasicChat.points);

        if ((BasicChat.points + boxCollider.points) == 2 || gameOver)
        {
            gameOver = true;

            if (boxCollider.points > BasicChat.points)
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "SIEG!"))
                {
                    //basicChat.SendTheMsg("restart");
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
            else if (boxCollider.points == BasicChat.points)
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Unentschieden!"))
                {
                    //basicChat.SendTheMsg("restart");
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
            else
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Niederlage!"))
                {
                    //basicChat.SendTheMsg("restart");
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
        }

        /*
        if (BasicChat.AllJoynStarted)
        {
            if (GUI.Button(new Rect(0, xStart, (Screen.width) / 3, BUTTON_SIZE), "STOP ALLJOYN"))
            {
                basicChat.CloseDown();
            }
        }
        
        if (!BasicChat.AllJoynStarted)
        {
            if (GUI.Button(new Rect(((Screen.width) / 3) * 2, xStart, (Screen.width) / 3, BUTTON_SIZE), "START ALLJOYN"))
            {
                basicChat.StartUp();
            }
        }*/

        /*
        if (BasicChat.currentJoinedSession != null)
        {
            if (GUI.Button(new Rect(((Screen.width) / 3), 0, (Screen.width) / 3, BUTTON_SIZE),
                "Leave \n" + BasicChat.currentJoinedSession.Substring(BasicChat.currentJoinedSession.LastIndexOf("."))))
            {
                basicChat.LeaveSession();
            }
        }
        

        foreach (string name in BasicChat.sFoundName)
        {
            xStart = 10 + ((i++) * BUTTON_SIZE);
            if (GUI.Button(new Rect(Screen.width / 3, Screen.height - xStart, Screen.width / 3, BUTTON_SIZE), "Connect to: " + name.Substring(name.LastIndexOf("."))))
            {
                basicChat.JoinSession(name);
            }
        }*/
    }

    public bool isMotion(Vector3 firstVec, Vector3 secondVec)
    {
        float x = firstVec.x - secondVec.x;
        float y = firstVec.y - secondVec.y;
        float z = firstVec.z - secondVec.z;

        if (x < -MOTION || x > MOTION || y < -MOTION || y > MOTION || z < -MOTION || z > MOTION)
        {
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Starting up AllJoyn service and client");
        //basicChat = new BasicChat();
        //basicChat.StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (BasicChat.currentJoinedSession != null)
        {
            BasicChatVar.basicChat.SendPoints(boxCollider.points);
            //Koordinaten des Würfels senden
            if (isMotion(positionTmp, this.transform.localPosition) && !BasicChat.neueKoordinaten)
            {
                BasicChatVar.basicChat.SendCoordinates(this.transform.localPosition);
                positionTmp = this.transform.localPosition;
            }
            else if (BasicChat.neueKoordinaten)
            {
                this.transform.localPosition = BasicChat.newCoordinates;
                positionTmp = BasicChat.newCoordinates;
                BasicChat.neueKoordinaten = false;
            }
        }

        if (this.transform.localPosition.y < 0.5f)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.5f, this.transform.localPosition.z);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BasicChatVar.basicChat.CloseDown();
            Application.Quit();
        }
    }

    
}
