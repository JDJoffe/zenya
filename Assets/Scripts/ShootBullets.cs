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
                this.CmdShoot(transform.position);
             
            }
            shootTimer = 0;
        }
    }

    // commands only run on server
    //RPC (Remote Procedure Call) runs on client but called from server

    [ClientRpc]
    void RpcClientShot(Vector3 pos)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * bulletSpeed;
        Destroy(bullet, 1f);
    }


    [Command]
    void CmdShoot(Vector3 pos)
    {
        //tell client to spawn bullet
        RpcClientShot(pos);
    }
}
