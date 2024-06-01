using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Entity
{
    [SerializeField] private float speed = 1f;
    private SpriteRenderer sprite;
    Transform player;
    Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    public bool isFlipped = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject == Hero.Instance.gameObject){
            Hero.Instance.GetDamage();
        }
    }
    private void Start(){
        lives = 1; 
    }

    private void Move(){
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void Update(){
        Move();
        LookAtPlayer();
    }

    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x < player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

    public override void Die(){
        anim.SetTrigger("death");
        col.enabled = false;
        gameObject.tag = "enemy_dead";
        speed = 0;
        LevelController.Instance.EnemiesCount();
        Destroy(this.gameObject, 0.8f);

    }


}
