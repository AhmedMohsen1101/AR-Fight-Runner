﻿using System.Collections;
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
        Destroy(GetRigidbody);
        Destroy(GetMainCollider);
        foreach (var ragdollPart in ragdollParts)
        {
            ragdollPart.attachedRigidbody.useGravity = true;
            ragdollPart.isTrigger = false;
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
            colliders[i].attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            colliders[i].attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            if (colliders[i].GetComponent<CharacterJoint>())
            {
                colliders[i].GetComponent<CharacterJoint>().enableCollision = false;
                colliders[i].GetComponent<CharacterJoint>().enableProjection = false;
                //colliders[i].GetComponent<CharacterJoint>().enablePreprocessing = false;
            }

            if (!ragdollParts.Contains(colliders[i]))
                ragdollParts.Add(colliders[i]);
        }
    }
    #endregion
}
