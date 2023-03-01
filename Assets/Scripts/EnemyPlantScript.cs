using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlantScript : MonoBehaviour
{
    public float fireRate = 3.0f;
    public GameObject bullet;
    public Transform bulletPos;
    public float distance = 10.0f;
    Transform playerPos;

    void Start(){
        InvokeRepeating("Attack", fireRate, fireRate);
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Attack(){
        if(Vector2.Distance(transform.position, playerPos.position) <= distance){
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    public void InstantiateBullet(){
        Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
    }
}
