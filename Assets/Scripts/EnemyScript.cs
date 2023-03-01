using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3;
    public Transform rayOrigin;
    public float rayDistance = 1;

    RaycastHit2D groundInfo;

    void Update(){
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        groundInfo = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayDistance);

        if(groundInfo.collider == false){

            speed = speed * -1;

            Vector3 tempScale = transform.localScale;
            tempScale.x = tempScale.x * -1;
            transform.localScale = tempScale;
            
        }
    }

    public void EnemyDeath(){
        speed = 0;
        BoxCollider2D[] bc = GetComponents<BoxCollider2D>();
        for(int i = 0; i < 2; i++){
            bc[i].enabled = false;
        }

        GetComponent<Animator>().SetTrigger("Hit");
    }

    public void DestroyEnemy(){
        Destroy(gameObject);
    }

}