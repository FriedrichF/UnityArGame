using UnityEngine;
using System.Collections;

public class boxCollider : MonoBehaviour {

    public static int points = 0;

    private GUIStyle pointStyle;

    private string[] autoObject = { "Auto0", "Auto1"};

    public static ArrayList pickedObject = new ArrayList();

    void OnCollisionEnter(Collision other)
    {
        //Wenn es sich um eine Kollision handelt und das Auto das eigene ist
        if (other != null && this.transform.name == autoObject[BasicChatVar.playerNumber])
        {
            //Debug.Log(" Kollision!! Kollision!!Kollision!! Kollision!! Kollision!!" + other.transform.name);
            if (other.transform.tag == "Cube")
            {
                //Überträgt die Kindelemente an das Auto Objekt
                other.transform.gameObject.transform.parent = GameObject.Find(autoObject[BasicChatVar.playerNumber]).transform.parent;
                
                //Fixiert das neue Objekt an der aktuellen Position
                other.rigidbody.constraints |= (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);
                
                //Stellt Gravity aus
                other.rigidbody.useGravity = false;

                //Wenn das kollidierte Objekt noch nicht in der Liste ist, zu dieser hinzufügen
                if (!pickedObject.Contains(other.transform.name))
                {
                    pickedObject.Add(other.transform.name);
                }
            }

            //Wenn es sich um die Zielbox handelt sollen eventuell aufgeladene Objekte abgeelgt werden und Punkte gezählt werden
            if (other.transform.name == "ZielBox")
            {
                if (pickedObject.Count > 0)
                {
                    //Debug.Log("------------------- Punkte ---------- " + points);
                    foreach(string pickObj in pickedObject){
                        points++;
                        Destroy(GameObject.Find(pickObj));
                    }
                    pickedObject.Clear();
                }
            }
        }
    }

    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log(" You just hit " + hit.collider.gameObject.name);
        //        if (hit.collider.name == "Auto")
        //        {
        //            Destroy(GameObject.Find("Cube"));
        //        }
        //    }
        //}
    }
}
