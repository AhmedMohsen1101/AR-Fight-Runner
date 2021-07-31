using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PreperationManager : MonoBehaviour
{
    [SerializeField] private Canvas prepCanvas;
    [SerializeField] private Canvas TapCanvas;
    private ARPlaneManager aRPlaneManager;
    
    private void Start()
    {
        aRPlaneManager = FindObjectOfType<ARPlaneManager>();
    }
    private void Update()
    {
        if (aRPlaneManager.trackables.count == 0)
            return;
        TapCanvas.enabled = true;
        Destroy(prepCanvas.gameObject);
        Destroy(gameObject);
    }
}
