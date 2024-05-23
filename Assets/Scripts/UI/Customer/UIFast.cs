using System.Collections.Generic;
using UnityEngine;

public class UIFast : MonoBehaviour
{
    [SerializeField] FastRow prefab;
    List<FastRow> rows = new();
    public void Setup(Dictionary<GemType, Fastidiousness> fastidiousnesses)
    {
        foreach (FastRow row in rows) 
        {
            Destroy(row.gameObject);
        }
        rows.Clear();
        foreach (GemType key in fastidiousnesses.Keys)
        {
            FastRow row = Instantiate(prefab, transform);
            row.Setup(fastidiousnesses[key].type, key);
            rows.Add(row);
        }
    }
}
