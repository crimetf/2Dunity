using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem;
    public bool isHeal;
    private bool isCollected;

    public GameObject pickupEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;
                isCollected = true;
                UIController.instance.UpdateGemCount();

                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                AudioManager.instance.PlaySFX(6);
            }

            if (isHeal)
            {
                if(PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
                {
                    PlayerHealth.instance.HealPlayer();
                    isCollected = true;

                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);

                    AudioManager.instance.PlaySFX(7);
                }
            }
        }
    }
}
