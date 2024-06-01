using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision.name);
        if(collision.gameObject.GetComponent<Entity>()){
            collision.gameObject.GetComponent<Entity>().Die();
        }
    }
}
