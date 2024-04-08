using UnityEngine;
public class Wall : GridObject
{
    [SerializeField] GridObjectSO objectSO;
    public void Start() 
    {
       // Setup(objectSO);
    }
    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(gameObject);
    }
}
