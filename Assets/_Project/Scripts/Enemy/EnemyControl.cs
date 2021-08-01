using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : Character
{
    [SerializeField] private float speed = 6;

    private bool isDead = false;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void FixedUpdate()
    {
        if (!isDead)
            transform.Translate(-transform.forward * speed * Time.fixedDeltaTime);
    }
    public void TakeDamage(float force, Collider collider)
    {
        TurnRagdoll();
        Vector3 direction = new Vector3(Random.Range(0, 1), Random.Range(0.8f, 1), Random.Range(0.5f, 1));
        collider.attachedRigidbody.AddForce(direction * force, ForceMode.Force);
        isDead = true;
        //collider.attachedRigidbody.AddExplosionForce(force, transform.forward, 10);
    }
}
