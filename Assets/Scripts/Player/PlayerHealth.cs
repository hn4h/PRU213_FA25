using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{

    public event EventHandler OnPlayerDie;
    public void Die()
    {
        //play die animtion

        //respawn players to revive point
        OnPlayerDie?.Invoke(this,EventArgs.Empty);
    }
}
