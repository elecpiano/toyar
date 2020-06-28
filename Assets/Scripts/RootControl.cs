using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RootControl : MonoBehaviour
{
    private ARRaycastManager ARRaycast;
    private ARAnchorManager anchorManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private ARAnchor anchor;

    void Start()
    {
        ARRaycast = GetComponent<ARRaycastManager>();
        anchorManager = GetComponent<ARAnchorManager>();
    }

    void Update()
    {
        if (anchor != null || Input.touchCount == 0)
        {
            return;
        }



        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Ended)
        {
            return;
        }

        if (ARRaycast.Raycast(touch.position, s_Hits, TrackableType.FeaturePoint))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            anchor = anchorManager.AddAnchor(hitPose);
        }
    }
}
