using UnityEngine;
public class Wall : GridObject
{
    [SerializeField] Sprite sprite;
    public override void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
