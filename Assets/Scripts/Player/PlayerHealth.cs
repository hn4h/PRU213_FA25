using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : MonoBehaviour
{

    public bool isFireboy;
    public event EventHandler OnPlayerDie;
    public void Die()
    {
        //play die animtion

        //respawn players to revive point
        Debug.Log("Player has died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnPlayerDie?.Invoke(this,EventArgs.Empty);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("lava"))
        {
            if (!isFireboy) 
            {
                
                Die();
            }
        }
        // Xử lý va chạm với Nước (Water)
        else if (other.CompareTag("water"))
        {
            if (isFireboy) 
            {
                Die();
            }
        }
        else if (other.CompareTag("poison"))
        {
            Die();
        }
    }

}
