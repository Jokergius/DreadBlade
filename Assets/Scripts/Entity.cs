using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int lives;
    public virtual void GetDamage(){
        
         lives--;
         Debug.Log("Хп монстра: " + lives);
         if(lives < 1) Die();
    }

    public virtual void Die(){
        Destroy(this.gameObject);
        LevelController.Instance.EnemiesCount();
    }
}
