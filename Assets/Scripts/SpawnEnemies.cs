using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnEnemies : NetworkBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 1.0f;

    [SerializeField]
    private float enemySpeed = 1.0f;

    [SerializeField]
    private float difficultySpikeTimer;
    [SerializeField]
    private float spikeOver;
    public void Update()
    {
        //spontaneous difficulty spike hehe
        difficultySpikeTimer += Time.deltaTime;
        if (difficultySpikeTimer >= 10)
        {
            spawnInterval = 1f;
            
            spikeOver += Time.deltaTime;
           
            if (spikeOver >= 2)
            {
                difficultySpikeTimer = 0;
                spawnInterval = 3f;
               
                spikeOver = 0;
               
            }

           
        }
        
    }
   
    public override void OnStartServer()
    {
       
        //call void
        InvokeRepeating("SpawnEnemy", this.spawnInterval, this.spawnInterval);
    }

    void SpawnEnemy()   
{
        //enemy spawnpos
        Vector2 spawnPosition = new Vector2(Random.Range(-4.0f, 4.0f), this.transform.position.y);
        //instantiate
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity) as GameObject;
        //get rigitbody to move enemy
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -this.enemySpeed);
        //network
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10);
        //destroy after 10 seconds
    }

}