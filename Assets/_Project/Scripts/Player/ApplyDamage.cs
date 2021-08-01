using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public float force;
    public ParticleSystem hitEffect;
    private Animator animator;
    private void OnEnable()
    {
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(animator.GetInteger("Attack")> 0)
        {
            StartCoroutine(ApplyDamageRoutine(collider));

        }
    }

    private IEnumerator ApplyDamageRoutine(Collider collider)
    {
        yield return new WaitForSeconds(0.1f);
        EnemyControl enemyControl = collider.GetComponentInParent<EnemyControl>();
        float randomForce = Random.Range(force , force);
        if (enemyControl != null)
        {
            enemyControl.TakeDamage(force, collider);
            if (hitEffect != null)
            {
                hitEffect.transform.position = this.transform.position;
                hitEffect.Play();
            }


        }
    }
}
