using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject fireBullet;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(this.fireBullet, this.transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= this.transform.localScale.x;// * this.transform.localScale.x;
        }
    }
}
