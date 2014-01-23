using UnityEngine;
using System.Collections;

public class boxCollider : MonoBehaviour {

    public static int points = 0;

    private GUIStyle pointStyle;

    //private bool picked = false;

    public static ArrayList pickedObject = new ArrayList();

    void OnCollisionStay(Collision other)
    {
        if (other != null)
        {
            //Debug.Log(" Kollision!! Kollision!!Kollision!! Kollision!! Kollision!!" + other.transform.name);
            if (other.transform.tag == "Cube")
            {
                other.transform.gameObject.transform.parent = GameObject.Find("Auto").transform.parent;
                other.rigidbody.constraints |= (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);
                other.rigidbody.useGravity = false;
                if (!pickedObject.Contains(other.transform.name))
                {
                    pickedObject.Add(other.transform.name);
                }
                //picked = true;
            }
            if (other.transform.name == "ZielBox")
            {
                if (pickedObject.Count > 0)
                {
                    Debug.Log("------------------- Punkte ---------- " + points);
                    foreach(string pickObj in pickedObject){
                        points++;
                        Destroy(GameObject.Find(pickObj));
                    }
                    pickedObject.Clear();
                    //picked = false;
                }
            }
        }
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Box(new Rect(100, 10, 150, 25), "Anzahl Punkte: " + points);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(" You just hit " + hit.collider.gameObject.name);
                if (hit.collider.name == "Auto")
                {
                    Destroy(GameObject.Find("Cube"));
                }
            }
        }
    }

    void Start()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
    }
}
