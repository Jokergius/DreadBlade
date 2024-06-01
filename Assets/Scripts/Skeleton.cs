using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private AudioSource deathSound;
    private float dir;
    private SpriteRenderer sprite;
    Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;
    

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject == Hero.Instance.gameObject){
            Hero.Instance.GetDamage();
        }
        else if(collision.gameObject.CompareTag("wall")){
            dir *= -1f;
        }
    }
    private void Start(){
        dir = 1f;
        lives = 3; 
    }

    private void Move(){
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        // if (colliders.Length > 0) dir *= -1;

        // transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        // sprite.flipX = dir.x < 0.0f; 
    }



    private void Update(){
        if (lives>0){
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            sprite.flipX = dir < 0.0f;
        }
    }

    public override void GetDamage(){
        
         lives--;
         dir *= -1f;
         if(lives < 1) Die();
    }

    public override void Die(){
        anim.SetBool("Death", true);
        deathSound.Play();
        speed = 0;
        gameObject.tag = "enemy_dead";
        col.isTrigger = true;
        col.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        Destroy(this.gameObject, 0.7f);
        LevelController.Instance.EnemiesCount();
    }
}
