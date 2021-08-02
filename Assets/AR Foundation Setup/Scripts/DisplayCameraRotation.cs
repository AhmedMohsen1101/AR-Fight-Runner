using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayCameraRotation : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    void Update()
    {
        displayText.text = Camera.main.transform.eulerAngles.ToString();
    }
}
