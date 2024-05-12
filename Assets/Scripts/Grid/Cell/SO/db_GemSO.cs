using UnityEngine;

[CreateAssetMenu()]
public class db_GemSO : ScriptableObject
{
    [SerializeField] GemSO[] gemSo;
    public GemSO GetGemSOByType(GemType type) 
    {
        foreach (GemSO gemSO in gemSo) 
        {
            if(gemSO.type == type)
                return gemSO;
        }
        Debug.LogError($"�� ��������� ��� {type} ����");
        return null;
    }
    public Sprite GetIconByType(GemType type) 
    {
        GemSO so = GetGemSOByType(type);
        return so.icon;
    }
}
