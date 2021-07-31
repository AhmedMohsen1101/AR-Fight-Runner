using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum PoseType
{
    Portal,
    Room,
    BusinessMale,
}
[RequireComponent(typeof(ARRaycastManager))]
public class TapToSpawn : MonoBehaviour
{
    public PoseType poseType;
    public GameObject prefab;
    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ReferencePointManager referencePointManager;

    //private UIManager uiManager;
    void Start()
    {
    //    uiManager = GameObject.FindObjectOfType<UIManager>();
    //    referencePointManager = this.gameObject.GetComponent<ReferencePointManager>();
        arRaycastManager = this.gameObject.GetComponent<ARRaycastManager>();
       // planeManager = this.gameObject.GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        if(!TryToGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if(arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                if (poseType == PoseType.Portal)
                {
                    spawnedObject = Instantiate(prefab, hitPose.position, Quaternion.Euler(hitPose.rotation.x, hitPose.rotation.y + 180, hitPose.rotation.z));
                }
                else if (poseType == PoseType.Room)
                {
                    spawnedObject = Instantiate(prefab, hitPose.position, Quaternion.Euler(hitPose.rotation.x, hitPose.rotation.y, hitPose.rotation.z));
                }
                else if (poseType == PoseType.BusinessMale)
                {
                    spawnedObject = prefab;
                    spawnedObject.transform.position = hitPose.position;
                    spawnedObject.transform.rotation = hitPose.rotation;
                    spawnedObject.SetActive(true);
                }
            }
          
        }
    }
    //private Vector3 GetAnchorPosition()
    //{
    //    return spawnedObject.transform.parent.transform.position;
    //}
    //public string AnchorName()
    //{
    //    return referencePointManager.referencePoints[0].gameObject.name;
    //}
    private bool TryToGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
}
