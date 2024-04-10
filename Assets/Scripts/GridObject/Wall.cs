using UnityEngine;
public class Wall : GridObject
{
    [SerializeField] GridObjectSO objectSO;
    public override void Clear()
    {
        GameObject.Destroy(gameObject);
    }
}
