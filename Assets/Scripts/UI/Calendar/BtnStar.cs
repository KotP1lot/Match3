using UnityEngine;
using UnityEngine.UI;

public class BtnStar : MonoBehaviour
{
    [SerializeField] Image star;
    public void SetActive(bool isActive) 
    {
        star.gameObject.SetActive(isActive);
    }
}
