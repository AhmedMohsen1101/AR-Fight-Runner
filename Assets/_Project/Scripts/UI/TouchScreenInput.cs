using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreenInput : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public float threshold = 0.1f; 
    public CharacterControl characterControl;
    private bool canDrag;
    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;

        if (characterControl != null)
        {
            if (eventData.delta.x >= threshold)
            {
                characterControl.movement.destination.x += characterControl.movement.dashWorldBounds;
                canDrag = false;
            }
            if (eventData.delta.x <= -threshold)
            {
                characterControl.movement.destination.x -= characterControl.movement.dashWorldBounds;
                canDrag = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
    }
}
