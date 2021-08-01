using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kick
{
    NONE = 0,
    MMA_KICK = 1,
    MMA_KICK1 = 2,
    KICKING = 3,
}
public class CharacterControl : Character
{
    public float force;
    public ParticleSystem hitEffect;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public Movement movement;
    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var ragdollPart in ragdollParts)
        {
            if (ragdollPart.GetComponent<ApplyDamage>())
            {
                ApplyDamage applyDamage = ragdollPart.gameObject.AddComponent<ApplyDamage>();
                applyDamage.force = this.force;
                applyDamage.hitEffect = this.hitEffect;
            }
        }

        movement.destination = transform.position;
    }

    public void Dash(Vector3 pos)
    {
        Vector3 lerpPos = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * movement.dashSpeed);
        transform.position = lerpPos;
    }
    public void FootKick()
    {
        if(animator.GetInteger("Attack") == 0)
        {
            int attack = Random.Range(1, (int)Kick.KICKING + 1);
            Debug.Log(attack);
            animator.SetInteger("Attack", attack);
        }
      
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FootKick();
        }
#endif
        Vector3 currentPos = transform.position;

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (movement.dashEffect != null)
                movement.dashEffect.Play();

            if (currentPos.x < movement.dashWorldBounds)
            {
                if (movement.destination.x < movement.dashWorldBounds)
                    movement.destination.x += movement.dashWorldBounds;
            } 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (movement.dashEffect != null)
                movement.dashEffect.Play();

            if (currentPos.x > -movement.dashWorldBounds)
            {
                if (movement.destination.x > -movement.dashWorldBounds)
                    movement.destination.x -= movement.dashWorldBounds;
            }
        }

        Dash(movement.destination);
    }

    [ContextMenu("Attach Attack Behaviour")]
    private void AttachAttackScript()
    {
        foreach (var ragdollPart in ragdollParts)
        {
            if (!ragdollPart.GetComponent<ApplyDamage>())
            {
                ApplyDamage applyDamage = ragdollPart.gameObject.AddComponent<ApplyDamage>();
                applyDamage.force = this.force;
            }
        }
    }
}
[System.Serializable]
public class Movement
{
    public float dashSpeed = 15;
    public float dashWorldBounds = 1;
    public Vector3 destination;
    public ParticleSystem dashEffect;
}