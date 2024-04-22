using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LvlSO : ScriptableObject
{
    [SerializeField]
    public GridObject[] gridObjects;
    public int S;
}
