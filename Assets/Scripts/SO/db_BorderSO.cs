using UnityEngine;

public class db_BorderSO : MonoBehaviour
{
    [SerializeField] BorderSO[] borders;
    public BorderSO GetByType(BorderType type)
    {
        foreach (BorderSO border in borders)
        {
            if (border.type == type)
                return border;
        }
        return null;
    }
}
