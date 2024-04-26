using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    [SerializeField] GameObject cell;
    [SerializeField] float offset;
    [SerializeField] int widht;
    [SerializeField] int height;
    [SerializeField] float space;
    void Start()
    {
        List<Vector3> vectors = SquareFormation(transform.position, new Vector2(7, 7), 1f);
        foreach (Vector3 v in vectors) 
        {
            GameObject g = Instantiate(cell,transform);
            cell.transform.position = v;
        }
    }
    public List<Vector3> SquareFormation(Vector3 center, Vector2 size, float _spread, float nthOffset = 0)
    {
        var positions = new List<Vector3>();
        var middleOffset = new Vector3(size.x * 0.5f, size.y * 0.5f,0);

        for (var x = 0; x < size.x; x++)
        {
            for (var z = 0; z < size.y; z++)
            {
                var pos = new Vector3(x + (z % 2 == 0 ? 0 : nthOffset), z, 0);



                pos *= _spread;

                pos = center + pos;
                positions.Add(pos);
            }
        }
        return positions;
    }

}
