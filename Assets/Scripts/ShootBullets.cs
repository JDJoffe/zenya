using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootBullets : NetworkBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float shootTimer;
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= .3)
        {
            if (this.isLocalPlayer && Input.GetKey(KeyCode.Space))
            {
                this.CmdShoot();
            }
            shootTimer = 0;
        }
    }

    [Command]
    void CmdShoot()
    {
        //instantiate
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        //give speed + get rigidbody
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
        NetworkServer.Spawn(bullet);
        //destroy after 1 second
        Destroy(bullet, 1.0f);
    }
}
