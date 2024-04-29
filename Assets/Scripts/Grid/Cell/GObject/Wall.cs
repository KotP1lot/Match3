using System;
using UnityEngine;
[Serializable]
public class Wall : GridObject
{
    [SerializeField] Sprite sprite;
    public override void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
