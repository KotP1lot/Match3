using System.Collections.Generic;
using UnityEngine;

public class UIFast : MonoBehaviour
{
    [SerializeField] FastRow prefab;

    public void Setup(Dictionary<GemType, Fastidiousness> fastidiousnesses)
    {
        foreach (GemType key in fastidiousnesses.Keys)
        {
            FastRow row = Instantiate(prefab, transform);
            row.Setup(fastidiousnesses[key].type, key);
        }
    }
}
