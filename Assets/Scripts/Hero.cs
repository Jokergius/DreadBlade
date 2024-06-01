using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int health;
    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite AliveHeart;
    [SerializeField] private Sprite DeadHeart;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource missAttack;
    [SerializeField] private AudioSource hitAttack;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    private bool isGrounded = false;

    public Joystick joystick;
    public  bool isAttacking = false;
    public  bool isRecharged = true;
    public bool isHurt = false;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private IEnumerator AttackAnimation(){
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator DamageAnimation(){
        yield return new WaitForSeconds(0.4f);
        isHurt =  false;
    }

    private IEnumerator AttackCoolDown(){
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    private IEnumerator EnemyOnAttack(Collider2D enemy){
        SpriteRenderer enemyColor = enemy.GetComponentInChildren<SpriteRenderer>();
        enemyColor.color = new Color(0.95f, 0.3f, 0.27f);
        yield return new WaitForSeconds(0.2f);
        enemyColor.color = new Color(1, 1, 1);
    }

    private IEnumerator HeroOnDamage(){
        SpriteRenderer heroColor = GetComponentInChildren<SpriteRenderer>();
        heroColor.color = new Color(0.95f, 0.3f, 0.27f);
        yield return new WaitForSeconds(0.2f);
        heroColor.color = new Color(1, 1, 1);
    }

    public static Hero Instance {get; set;}

    private States State{
        get{ return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int) value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;
    }

        private void FixedUpdate(){
        CheckGround();
    }

    private void Update(){
        if (isGrounded && !isAttacking && health>0 && !isHurt) State = States.idle;
        if (joystick.Horizontal != 0 && !isAttacking && health>0 && !isHurt) Run();
        
        if (health > lives){
            health = lives;
        }

        for (int i = 0; i < hearts.Length; i++){
            if(i < health) {hearts[i].sprite = AliveHeart;}
            else {hearts[i].sprite = DeadHeart;}
        }
        }

    private void Run(){
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * joystick.Horizontal;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void OnJumpButton(){
        if (isGrounded && !isAttacking) Jump();
    }

    private void Jump(){
        rb.velocity = Vector2.up * jumpForce;
        jumpSound.Play();
    }

    private void CheckGround(){
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if(!isGrounded && rb.velocity.y > 0.0f && health > 0 && !isHurt) State = States.jump;
        else if(!isGrounded && health>0 && !isHurt) State = States.fall;
        
    }

    public override void GetDamage(){
        if(health > 0)
        {
        damageSound.Play();
        //isHurt = true;
        //StartCoroutine(DamageAnimation());
        //State = States.takeDamage;
        StartCoroutine(HeroOnDamage());
        health -= 1;
        Debug.Log("Получен урон. Хп героя: " + health);
        if(health == 0){
            foreach(var h in hearts) h.sprite = DeadHeart;
            Die();
        }
        }

    }

    public override void Die(){
        health = 0;
        State = States.death;
        deathSound.Play();
        Invoke("SetDeathPanel", 0.8f);
    }

    private void SetDeathPanel(){
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void SetWinPanel(){
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Attack(){
        if(isGrounded && isRecharged && health>0  && !isHurt){
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());

        }
    }

    private void OnAttack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        if (colliders.Length == 0){
            missAttack.Play();
        }
        else hitAttack.Play();

        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
            StartCoroutine(EnemyOnAttack(colliders[i]));
        }
    }

}

public enum States{
    idle,
    run,
    jump,
    fall,
    takeDamage,
    attack,
    death
}