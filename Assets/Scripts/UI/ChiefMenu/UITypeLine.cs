using System;
using System.Collections.Generic;
using UnityEngine;

public class UITypeLine : MonoBehaviour
{
    public event Action<GemType> OnTypeClick;
    [SerializeField] UITypeBtn btn;
    UITypeBtn activeBtn;
    [SerializeField]List<UITypeBtn> typeBtns = new();
    public void Setup()
    {
        GemType[] gemTypes = (GemType[])Enum.GetValues(typeof(GemType));
        for(int i = 0; i < gemTypes.Length; i++)
        {
            typeBtns[i].Setup(gemTypes[i]);
            typeBtns[i].OnTypeBtnClick += OnTypeBtnClick;
        }
        typeBtns[0].Setup(GemType.sweet);
        typeBtns[3].Setup(GemType.fish);
        OnTypeBtnClick(typeBtns[0]);
    }

    private void OnTypeBtnClick(UITypeBtn obj)
    {
        activeBtn = obj;
        activeBtn.SetActive(true);
        foreach (UITypeBtn type in typeBtns) 
        {
            if (type == activeBtn) continue;
            type.SetActive(false);
        }
        OnTypeClick?.Invoke(activeBtn.gemType);
    }
}
