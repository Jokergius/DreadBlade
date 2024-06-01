using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    public Transform player;
    public LayerMask Player;
    private int maxHealth;
    public float attackRange = 1.8f;
    public Transform attackPos;
    private Animator anim;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private GameObject enemyPrefab;
    public BossBar healthBar;

    public bool isFase2 = false;
    public  bool summonRecharged = true;

    private bool attackRecharged;

    
	public bool isFlipped = false;

    private IEnumerator SummonCooldown(){
        yield return new WaitForSeconds(10f);
        summonRecharged = true;
    }



    private void Awake() 
    {
        lives = 10;
        maxHealth = lives;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();

    }

    public override void GetDamage(){
        
         lives--;
         healthBar.SetHealth(lives);
         Debug.Log("Хп монстра: " + lives);
         if(lives <= maxHealth/2){
            isFase2 = true;
         }
         if(lives < 1) Die();
    }

    public override void Die(){
        anim.SetTrigger("death");
        gameObject.tag = "enemy_dead";
        LevelController.Instance.EnemiesCount();
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

    public void Attack(){
        Collider2D collider = Physics2D.OverlapCircle(attackPos.position, attackRange, Player);
        attackSound.Play();
        if(collider != null){
            Hero.Instance.GetDamage();
        }
    }

    public void SummonStart(){
            if(summonRecharged && isFase2){
            anim.SetTrigger("summon");
            summonRecharged = false;
            StartCoroutine(SummonCooldown());
            }
    }


    public void Summoning(){
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
