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

    //Anzeigen der Overlays
    void OnGUI()
    {

        int i = 0;
        int xStart = (Screen.height / 2) + 10 + ((i++) * BUTTON_SIZE);

        //Anzahl eigener Punkte
        GUI.Button(new Rect(100, 10, 150, 25), "Anzahl Punkte: " + boxCollider.points);
        //Anzahl Gegner Punkte
        GUI.Button(new Rect(100, 45, 150, 25), "Gegner Punkte: " + BasicChat.points);

        //Überprüfen ob Punktzahl erreicht wurde
        if ((BasicChat.points + boxCollider.points) == 2 || gameOver)
        {
            gameOver = true;

            if (boxCollider.points > BasicChat.points)
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "SIEG!"))
                {
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
            else if (boxCollider.points == BasicChat.points)
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Unentschieden!"))
                {
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
            else
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Niederlage!"))
                {
                    gameOver = false;
                    BasicChatVar.basicChat.restartGame();
                }
            }
        }
    }

    void Start()
    {
        //Kamera FocusMode einstellen
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
    }

    // Update is called once per frame
    void Update()
    {
        if (BasicChat.currentJoinedSession != null)
        {
            //Aktuelle Punkte über Alljoyn versenden
            BasicChatVar.basicChat.SendPoints(boxCollider.points);
            //Koordinaten des Würfels senden
            //if (isMotion(positionTmp, this.transform.localPosition) && !BasicChat.neueKoordinaten)
            //{
            //    BasicChatVar.basicChat.SendCoordinates(this.transform.localPosition);
            //    positionTmp = this.transform.localPosition;
            //}
            //else if (BasicChat.neueKoordinaten)
            //{
            //    this.transform.localPosition = BasicChat.newCoordinates;
            //    positionTmp = BasicChat.newCoordinates;
            //    BasicChat.neueKoordinaten = false;
            //}
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BasicChatVar.basicChat.CloseDown();
            Application.Quit();
        }
    }

    
}
