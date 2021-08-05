using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreenInput : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public float threshold = 0.25f; 
    public CharacterControl characterControl;
    private bool canDrag;

    private void OnEnable()
    {
        if (characterControl == null)
            characterControl = GameObject.FindObjectOfType<CharacterControl>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;

        if (characterControl != null)
        {
            if (eventData.delta.x >= threshold)
            {
                characterControl.DashRight();
                canDrag = false;
            }
            if (eventData.delta.x <= -threshold)
            {
                characterControl.DashLeft();
                canDrag = false;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
    }
}
