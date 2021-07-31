using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class PlacementIndicator : MonoBehaviour
{
    public Pose placementPose { get; private set; }
    public bool placementPoseIsValid { get; private set; }
    public bool isPlaced { get; private set; }
    public bool isTranslate { get; set; }

    public Transform placementIndicator;
    private GameObject fittingRoom;

    private ARRaycastManager aRRaycastManager;
    private void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
    private void Update()
    {
#if !UNITY_EDITOR
        if (!isPlaced)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            PlaceObject();
        }

        if(isPlaced && isTranslate)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            MoveObject();
        }
#endif
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
        
        placementPoseIsValid = hits.Count > 0;
        
        if (placementPoseIsValid)
        {
            Vector3 pos = hits[0].pose.position;
            Vector3 camDirection = Camera.main.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(camDirection);
            rotation.x = rotation.z = 0;

            placementPose = new Pose(pos, rotation);
        }
    }

    private void UpdatePlacementIndicator()
    {
        placementIndicator.gameObject.SetActive(placementPoseIsValid);

        if (placementPoseIsValid)
        {
            placementIndicator.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
           
    }
    private void PlaceObject()
    {
        if (!placementPoseIsValid)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fittingRoom = Instantiate(Resources.Load("Grid")) as GameObject;

                fittingRoom.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

                isPlaced = true;
                placementPoseIsValid = false;
                placementIndicator.gameObject.SetActive(placementPoseIsValid);

                Destroy(GameObject.Find("Tap To Start Canvas"));
            }
        }
       
    }

    private void MoveObject()
    {
        if (!placementPoseIsValid)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fittingRoom.transform.position = placementPose.position;
                placementPoseIsValid = false;
                placementIndicator.gameObject.SetActive(placementPoseIsValid);

            }
        }
    }
   
}
