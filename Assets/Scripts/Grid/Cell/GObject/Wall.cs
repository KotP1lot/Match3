using System;
using UnityEngine;
[Serializable]
public class Wall : GridObject
{
    public override void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
