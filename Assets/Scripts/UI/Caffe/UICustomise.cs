using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICustomise : MonoBehaviour
{
    [SerializeField] List<Image> images;

    public void Setup(InteriorLvlInfo lvlInfo) 
    {
        for(int i =0; i<3;i++) 
        {
            images[i].sprite = lvlInfo.sprites[i];
        }
    }
}
