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

    //[SerializeField]
    //private float difficultySpikeTimer;
    //[SerializeField]
    //private float spikeOver;
    //[SerializeField]
    //    private float maxInterval =3;
    //public void Update()
    //{
    //    spawnInterval += Time.deltaTime;
    //    if (spawnInterval >= maxInterval)
    //    {
    //        SpawnEnemy(this.spawnInterval);
    //       // InvokeRepeating("SpawnEnemy", this.spawnInterval, this.spawnInterval);
    //        spawnInterval = 0;
    //    }
    //    //spontaneous difficulty spike hehe
    //    difficultySpikeTimer += Time.deltaTime;
    //    if (difficultySpikeTimer >= 10)
    //    {
    //        maxInterval = 1f;
            
    //        spikeOver += Time.deltaTime;
           
    //        if (spikeOver >= 4)
    //        {
    //            difficultySpikeTimer = 0;
    //            maxInterval = 3f;
               
    //            spikeOver = 0;
               
    //        }
           
           
    //    }
        
    //}
   
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