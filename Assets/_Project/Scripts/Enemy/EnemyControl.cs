using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{

}
public class EnemyControl : Character
{
    [SerializeField] private float speed = 6;
    [SerializeField] private AudioSource hitSoundEffect;
    public Collider hipsCollider;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool isDead = false;
   
    protected override void OnEnable()
    {
        base.OnEnable();
        int randomRunAnim = Random.Range(0, 3);
        Debug.Log(randomRunAnim);
        animator.SetInteger("Run", randomRunAnim);
        Destroy(gameObject, 15);
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
             transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
           
    }
    public void TakeDamage(float force)
    {
        TurnRagdoll();
        Debug.Log("TakeDamage");
        float randomForce = Random.Range(force * 0.5f, force);
        Vector3 direction = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(2.25f, 4f), Random.Range(2f, 3f));
        hipsCollider.attachedRigidbody.AddForce(direction * randomForce, ForceMode.Force);
        if (hitSoundEffect != null)
            hitSoundEffect.Play();

        Destroy(gameObject, 1);
    }
    
}
