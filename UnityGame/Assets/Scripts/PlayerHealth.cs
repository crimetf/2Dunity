using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int currentHealth;
    public int maxHealth;

    public float invincibilityLength;
    private float invincibilityCounter;

    private SpriteRenderer SR;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            
            if(invincibilityCounter <= 0)
            {
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibilityCounter <= 0)
        {

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibilityCounter = invincibilityLength;
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.55f);

                AudioManager.instance.PlaySFX(9);
                PlayerController.instance.Knockback();
            }

            UIController.instance.UpdateHealth();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealth();
    }
}
