using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public CharacterControl characterControl;
    public float minSpawnTime = 2;
    public float maxSpawnTime = 4;
    public ParticleSystem spawnEffect;
    private Vector3[] spawnPositions = new Vector3[3];

    private void Start()
    {
        if (characterControl == null)
            characterControl = GameObject.FindObjectOfType<CharacterControl>();

        Vector3 pos = transform.position;
        spawnPositions[0] = new Vector3(pos.x, pos.y, pos.z);
        spawnPositions[1] = new Vector3(pos.x + characterControl.movement.dashWorldBounds, pos.y, pos.z);
        spawnPositions[2] = new Vector3(pos.x + -characterControl.movement.dashWorldBounds, pos.y, pos.z);

        InvokeRepeating(nameof(Spawn), 0, Random.Range(minSpawnTime, maxSpawnTime));
    }
    private void Spawn()
    {
        Vector3 pos = GetRandomPosition();
        if(spawnEffect != null)
        {
            spawnEffect.transform.position = pos;
            spawnEffect.Play();
        }
        StartCoroutine(RountineSpawn(pos));
    }
    private IEnumerator RountineSpawn(Vector3 pos)
    {
        yield return new WaitForSeconds(0.3f);
        GameObject enemy = InstantiateEnemy();
        enemy.transform.SetParent(transform);
        enemy.transform.position = pos;
        enemy.transform.rotation = transform.localRotation;
        enemy.GetComponent<EnemyControl>().direction = this.transform.parent.forward;
    }
    private Vector3 GetRandomPosition()
    {
        int randomPosition = Random.Range(0, spawnPositions.Length);

        return spawnPositions[randomPosition];
    }

    private GameObject InstantiateEnemy()
    {
        return Instantiate(Resources.Load("Enemy Three"))as GameObject;
    }
}
