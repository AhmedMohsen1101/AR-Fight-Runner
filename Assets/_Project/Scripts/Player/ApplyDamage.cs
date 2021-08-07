using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public float force;
    public DeathEffect deathEffect;
    public AudioSource[] kickSoundEffects;
    private Animator animator;

    private void OnEnable()
    {
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider collider)
    {
       
        if (animator.GetInteger("Attack") > 0)
        {
            if (collider.GetComponentInParent<EnemyControl>().isDead)
                return;

            collider.GetComponentInParent<EnemyControl>().isDead = true;

            StartCoroutine(ApplyDamageRoutine(collider));
        }
    }

    private IEnumerator ApplyDamageRoutine(Collider collider)
    {
        if (kickSoundEffects != null)
        {
            int randomSound = Random.Range(0, kickSoundEffects.Length);
            kickSoundEffects[randomSound].Play();
        }
        yield return new WaitForSeconds(0.1f);
        EnemyControl enemyControl = collider.GetComponentInParent<EnemyControl>();
        float randomForce = Random.Range(force, force);
        if (enemyControl != null)
        {
            enemyControl.TakeDamage(force);

            yield return new WaitForSeconds(0.1f);
            if (deathEffect != null)
            {
                deathEffect.PlayEffect(enemyControl.transform.position.x);
            }
        }

        ScoreManager.Instance.AddScore(1000);
    }
}
