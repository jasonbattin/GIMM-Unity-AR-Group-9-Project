using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int maxHealth = 3;
    int currentHealth;
    [Header("Health Bar")]
    public HeartHealthBar heartHealthBar; // Public variable to assign in the editor

    void Start()
    {
        currentHealth = maxHealth;
        if (heartHealthBar != null)
        {
            heartHealthBar.InitializeHearts(currentHealth);
        }
        else
        {
            Debug.LogError("HeartHealthBar is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) 
        {
            killObject();
        }
        if(transform.position.y < -40)
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
