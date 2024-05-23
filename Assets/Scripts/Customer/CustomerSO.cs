using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CustomerSO : ScriptableObject
{
    public Sprite baseImg;
    public List<Accessory> accessories;
}
[Serializable]
public struct Accessory 
{
    public AccessoryType type;
    public Sprite sprite;
}
public enum AccessoryType 
{
    hat,
    chest
}