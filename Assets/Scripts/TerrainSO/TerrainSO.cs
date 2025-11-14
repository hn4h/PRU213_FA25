using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Terain")]
public class TerrainSO : ScriptableObject
{
   public String terrainName;
   public Player player;
}

public enum Player
{
    None,
    Player1,
    Player2
}
