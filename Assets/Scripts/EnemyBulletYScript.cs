using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletYScript : MonoBehaviour
{
    public float speed = 5.0f;

    private void Start(){
        Destroy(gameObject, 10.0f);
    }

    void Update(){
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Ground") || collision.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
