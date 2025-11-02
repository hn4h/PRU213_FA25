using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrapAttackable : AbstractAttackable
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.GetComponent<PlayerHealth>().Die();
        }
    }
}
