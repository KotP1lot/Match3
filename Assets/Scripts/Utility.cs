using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static List<T> Shuffle<T>(List<T> enemies)
    {
        for (int t = 0; t < enemies.Count; t++)
        {
            T tmp = enemies[t];
            int r = Random.Range(t, enemies.Count);
            enemies[t] = enemies[r];
            enemies[r] = tmp;
        }

        return enemies;
    }
}
