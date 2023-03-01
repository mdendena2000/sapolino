using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variáveis privadas
    float horizontal;
    bool jump = false;
    bool canControlPlayer = true;
    Rigidbody2D rb;
    SpriteRenderer spr;
    Animator anim;
    AudioSource audioS;

    //Variáveis públicas
    public float speed;
    public float jumpForce;
    public GameObject colectParticle;
    public AudioClip jumpSFX, pickupSFX;

    void Start() {
        //Inicializando as variaveis que recebem os componentes

        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioS = GetComponentInChildren<AudioSource>();

    }

    void Update(){

        if (canControlPlayer == true){
            horizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
        }

        //Verifica se o player pressionou 0 botao Jump (espaco) e se a velocidade Y esta по limite
        if(Input.GetButtonDown("Jump") && (rb.velocity.y > -0.1f) && (rb.velocity.y < 0.1f)){
            jump = true;
        }

        //Flipagem do sprite do player
        if (horizontal > 0){
            spr.flipX = false;
        }
        else if (horizontal < 0){
            spr.flipX = true;
        }

        //Setagem dos parametros do animator
        anim.SetFloat("Speed", Mathf.Abs (horizontal));
        anim.SetFloat("SpeedY", rb.velocity.y);

    }

        void FixedUpdate(){
        if(jump == true){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
            audioS.PlayOneShot(jumpSFX, 0.2f);
        }
    }

    // Função de colisao do tipo "Trigger"
    private void OnTriggerEnter2D(Collider2D collision){
        // Verifica se o objeto colidido a tag "Kiwi"
        if(collision.CompareTag("Kiwi")){
            GameManager.gm.AddFruit();
            Instantiate(colectParticle, collision.transform.position, collision.transform.rotation);
            audioS.PlayOneShot(pickupSFX);

            // Destroi o objeto colidido
            Destroy(collision.gameObject);
        }

        // Verifica se o objeto colidido a tag "Enemy"
        if(collision.CompareTag("Enemy")){
            StartCoroutine(PlayerDeath());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            jump = true;
            collision.gameObject.GetComponent<EnemyScript>().EnemyDeath();
        }
    }

    public IEnumerator PlayerDeath(){
        canControlPlayer = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("PlayerHit");
        yield return new WaitForSeconds(3.0f);
        GameManager.gm.ReloadScene();

    }
}
