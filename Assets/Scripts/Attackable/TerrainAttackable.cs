using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAttackable : AbstractAttackable
{
    [SerializeField] private TerrainSO terrainSO;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (other.tag != terrainSO.player.ToString())
            {
                other.GetComponent<PlayerHealth>().Die();
            }
        }
    }
}
