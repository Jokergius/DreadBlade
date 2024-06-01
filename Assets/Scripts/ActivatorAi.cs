using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorAi : MonoBehaviour
{
    public FlyingMonster monster;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject == Hero.Instance.gameObject) {
            monster.AiActivate();
            Destroy(this.gameObject);
            }
    }
}
