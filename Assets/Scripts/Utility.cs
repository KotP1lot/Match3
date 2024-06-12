using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static List<T> Shuffle<T>(List<T> objects)
    {
        for (int t = 0; t < objects.Count; t++)
        {
            T tmp = objects[t];
            int r = Random.Range(t, objects.Count);
            objects[t] = objects[r];
            objects[r] = tmp;
        }

        return objects;
    }
}
