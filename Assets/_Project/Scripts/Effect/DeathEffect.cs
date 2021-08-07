using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private Transform startPoint;

    public void PlayEffect(float posX)
    {
        if(deathEffect != null)
        {
            LeanTween.reset();
            Vector3 pos = startPoint.position;
            pos.x = posX;
            deathEffect.transform.position = pos;
            deathEffect.Play();
            LeanTween.moveLocalY(deathEffect.gameObject, 0.75f, 3f);
        }
    }

}
