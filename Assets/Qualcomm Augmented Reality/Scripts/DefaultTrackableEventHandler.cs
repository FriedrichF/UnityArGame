/*==============================================================================
Copyright (c) 2010-2013 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES
 
    private TrackableBehaviour mTrackableBehaviour;

    private static bool autoFokusLost = true;
    private static string[] autoName = { "FrameMarkerAuto0", "FrameMarkerAuto1" };
    
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        if (this.transform.name == autoName[BasicChatVar.playerNumber])
        {
            autoFokusLost = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        if (this.transform.name == autoName[BasicChatVar.playerNumber])
        {
            autoFokusLost = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    void OnGUI()
    {
        //Wenn Fokus des Autos verloren ging, dann Meldung anzeigen
        if (autoFokusLost)
        {
            if (this.transform.name == "FrameMarkerAuto0" && BasicChatVar.playerNumber == 0)
            {
                GUI.Box(new Rect(Screen.width / 2 - 300 / 2, Screen.height / 2 - 75 / 2, 300, 75), "Sie Müssen den Focus auf das Grüne Auto legen!");
            }
            else if (this.transform.name == "FrameMarkerAuto1" && BasicChatVar.playerNumber == 1)
            {
                GUI.Box(new Rect(Screen.width / 2 - 300 / 2, Screen.height / 2 - 75 / 2, 300, 75), "Sie Müssen den Focus auf das Rote Auto legen!");
            }
        }
    }

    #endregion // PRIVATE_METHODS
}
