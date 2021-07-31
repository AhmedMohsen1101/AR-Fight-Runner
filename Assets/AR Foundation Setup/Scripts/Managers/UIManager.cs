using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Button ClearThePointButton;
    [SerializeField] private Button EnablePlanesToggle;
    [SerializeField] private TMP_Text ReferencePointsText;
    private ARPlaneManager arPlaneManager;
    
    private void Start()
    {
        arPlaneManager = GameObject.FindObjectOfType<ARPlaneManager>();
        
    }
    private void OnEnable()
    {
        EnablePlanesToggle.onClick.AddListener(DisableDetectedPlanes);
    }
    private void OnDisable()
    {
        EnablePlanesToggle.onClick.AddListener(DisableDetectedPlanes);
    }
    private void DisableDetectedPlanes()
    {
        arPlaneManager.enabled = !arPlaneManager.enabled;

        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(arPlaneManager.enabled);
        }
        EnablePlanesToggle.GetComponentInChildren<TMP_Text>().text = arPlaneManager.enabled ?
            "Disable Detected Planes " : "Enable Detected Planes";
    }
   
}
