using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Setup")]
    public int ragdolLayerMask;
    public List<Collider> ragdollParts = new List<Collider>();

    protected Rigidbody GetRigidbody;
    protected Collider GetMainCollider;
    protected Animator animator;

    protected virtual void OnEnable()
    {
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();

        if (GetMainCollider == null)
            GetMainCollider = gameObject.GetComponent<Collider>();
        
        if (GetRigidbody == null)
            GetRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    protected void TurnRagdoll()
    {
        animator.enabled = false;
        GetRigidbody.useGravity = false;
        GetMainCollider.enabled = false;
        foreach (var ragdollPart in ragdollParts)
        {
            ragdollPart.isTrigger = false;
            ragdollPart.attachedRigidbody.useGravity = true;
        }
    }

    #region Pre Setup
    [ContextMenu("Setup Ragdoll")]
    public void SetupRagdoll()
    {
        ragdollParts.Clear();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == this.gameObject)
                continue;

            colliders[i].gameObject.layer = ragdolLayerMask;
            colliders[i].isTrigger = true;
            colliders[i].attachedRigidbody.useGravity = false;
            colliders[i].attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            colliders[i].attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

            if (colliders[i].GetComponent<CharacterJoint>())
            {
                colliders[i].GetComponent<CharacterJoint>().enableCollision = true;
                colliders[i].GetComponent<CharacterJoint>().enableProjection = true;
                colliders[i].GetComponent<CharacterJoint>().enablePreprocessing = true;
            }
           
            if (!ragdollParts.Contains(colliders[i]))
                ragdollParts.Add(colliders[i]);
        }
    }
    #endregion
}
