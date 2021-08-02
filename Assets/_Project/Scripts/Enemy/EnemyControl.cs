using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : Character
{
    [SerializeField] private float speed = 6;
    public Vector3 direction;
    private bool isDead = false;
    protected override void OnEnable()
    {
        base.OnEnable();

        Destroy(gameObject, 15);
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            //Vector3 moveTowards = Vector3.MoveTowards(transform.position, -transform.forward, speed * Time.fixedDeltaTime);
            //transform.position = moveTowards;
              transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
           
    }
    public void TakeDamage(float force, Collider collider)
    {
        TurnRagdoll();
        Vector3 direction = new Vector3(Random.Range(0, 1), Random.Range(0.8f, 1), Random.Range(0.5f, 1));
        collider.attachedRigidbody.AddForce(direction * force, ForceMode.Force);
        isDead = true;
        Destroy(gameObject, 6);
        //collider.attachedRigidbody.AddExplosionForce(force, transform.forward, 10);
    }
    
}
