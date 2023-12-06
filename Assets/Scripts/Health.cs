using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) 
        {
            killObject();
        }
        if(transform.position.y < -10)
        {
            killObject();
            
        }
    }

    // kill object
    private void killObject()
    {
        if (gameObject.tag.Equals("Player"))
        {
            Score.gameOver();
        }
        Destroy(gameObject);
    }

    // take damage
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            killObject();
        }
    }
}
