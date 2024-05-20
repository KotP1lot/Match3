using UnityEngine;

public class BtnStarsManager : MonoBehaviour
{
    [SerializeField] BtnStar[] btnStars;

    public void Setup(int stars) 
    {
        for (int i = 0; i < 3; i++)
        {
            btnStars[i].gameObject.SetActive(i+1<=stars);
        }
    }
}
