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
    public DeathEffect deathEffect;
    public AudioSource[] kickSoundEffects;
    public Movement movement;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var ragdollPart in ragdollParts)
        {
            if (ragdollPart.GetComponent<ApplyDamage>())
            {
                ApplyDamage applyDamage = ragdollPart.gameObject.GetComponent<ApplyDamage>();
                applyDamage.force = this.force;
                applyDamage.deathEffect = this.deathEffect;
                applyDamage.kickSoundEffects = this.kickSoundEffects;

            }
        }
        transform.localPosition = Vector3.zero;
        movement.destination = transform.localPosition;
        movement.startPositionX = transform.localPosition.x;
    }
    private void FixedUpdate()
    {
        Dash(movement.destination);
    }

    public void Dash(Vector3 pos)
    {
        Vector3 lerpPos = Vector3.Lerp(transform.localPosition, pos, Time.fixedDeltaTime * movement.dashSpeed);
        transform.localPosition = lerpPos;
    }
    public void DashRight()
    {
        if (movement.destination.x < movement.dashWorldBounds + movement.startPositionX - 0.1f)
        {
            foreach (var effect in movement.dashEffects)
            {
                effect.Play();
            }
            movement.destination.x += movement.dashWorldBounds;
           
        }
    }
    public void DashLeft()
    {
        if (movement.destination.x > -movement.dashWorldBounds + movement.startPositionX + 0.1f)
        {
            foreach (var effect in movement.dashEffects)
            {
                effect.Play();
            }
            movement.destination.x -= movement.dashWorldBounds;
           
        }
    }
    public void FootKick()
    {
        if(animator.GetInteger("Attack") == 0)
        {
            int attack = Random.Range(1, (int)Kick.KICKING + 1);
            animator.SetInteger("Attack", attack);
        }
      
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
    public float startPositionX = 0;
    public Vector3 destination;
    public ParticleSystem[] dashEffects;
}