using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public CharacterControl characterControl;
    public float minSpawnTime = 2;
    public float maxSpawnTime = 4;
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
        GameObject enemy = InstantiateEnemy();
        enemy.transform.position = GetRandomPosition();
        enemy.transform.SetParent(transform);
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
