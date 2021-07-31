using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ReferencePointManager : MonoBehaviour
{
    private ARReferencePointManager aRReferencePointManager;

    private ARRaycastManager arRaycastManager;

    private ARPlaneManager arPlaneManager;

    [HideInInspector] public List<ARReferencePoint> referencePoints = new List<ARReferencePoint>();

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementIndicator placementIndicator;
    private void Start()
    {
        aRReferencePointManager = GetComponent<ARReferencePointManager>();

        arPlaneManager = GetComponent<ARPlaneManager>();

        arRaycastManager = GetComponent<ARRaycastManager>();

        placementIndicator = GetComponent<PlacementIndicator>();
    }
    void Update()
    {
        if (referencePoints.Count > 0)
            return;

        if (Input.touchCount == 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        if(touch.phase != TouchPhase.Began)
        {
            return;
        }
#if !UNITY_EDITOR
        AddAnchorPoint();
#endif
        //if(arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        //{
        //    Pose hitPose = hits[0].pose;

        //    ARReferencePoint referencePoint = aRReferencePointManager.AddReferencePoint(hitPose);

        //    if (referencePoint != null)
        //    {
        //        referencePoints.Add(referencePoint);
        //    }
        //}

    }
    private void AddAnchorPoint()
    {
        Pose pose = placementIndicator.placementPose;
        pose.rotation.y += 180;

        ARReferencePoint referencePoint = aRReferencePointManager.AddReferencePoint(pose);
        if (referencePoint != null)
        {
            referencePoints.Add(referencePoint);
            placementIndicator.enabled = false;
            placementIndicator.placementIndicator.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
#if !UNITY_EDITOR
        aRReferencePointManager.RemoveReferencePoint(referencePoints[0]);
#endif
    }
}
