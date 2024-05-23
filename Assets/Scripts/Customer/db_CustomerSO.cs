using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class db_CustomerSO : ScriptableObject
{
    public List<CustomerSO> customers;
    public List<Sprite> GetRandomCustomer() 
    {
        CustomerSO customer= customers[UnityEngine.Random.Range(0, customers.Count)];
        List<Sprite> sprite= new(){ customer.baseImg };
        AccessoryType[] accessoryTypes = (AccessoryType[])Enum.GetValues(typeof(AccessoryType));
        foreach (AccessoryType type in accessoryTypes) 
        {
            List<Accessory> accessory = customer.accessories.FindAll(x => x.type == type);
            if (accessory.Count == 0) continue;
            int random = UnityEngine.Random.Range(0, accessory.Count+1);
            if (random == accessory.Count) continue;
            else 
            {
                sprite.Add(accessory[random].sprite);
            }
        }
        return sprite;
    }
}

