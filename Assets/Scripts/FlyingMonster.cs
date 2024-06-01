using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonster : Entity
{
    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;
    private Animator anim;
    private Collider2D col;
    [SerializeField] private Collider2D triggerCol;

    private void Awake() 
    {
    sprite = GetComponentInChildren<SpriteRenderer>();
    anim = GetComponent<Animator>();
    aiPath = GetComponent<AIPath>();
    col = GetComponent<Collider2D>();
    lives = 2;
    }

    void Update(){
        sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject == Hero.Instance.gameObject && lives>0){
            Hero.Instance.GetDamage();
        }
    }

    public void AiActivate(){
        aiPath.enabled=true;
    }

    public override void Die(){
        anim.SetTrigger("death");
        gameObject.tag = "enemy_dead";
        Destroy(this.gameObject, 1);
        aiPath.enabled=false;
        LevelController.Instance.EnemiesCount();
    }
}
