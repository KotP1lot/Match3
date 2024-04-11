using UnityEngine;
using UnityEngine.XR;

public class Chief : MonoBehaviour
{
    public GemType GemType;
    public string Name;
    public BonusGem BonusGemPrefab;
    public BonusGem BonusGem;
    public GridObjectSO BonusGemSO;
    public int YumyBonus;
    //private void Start()
    //{
    //    BonusGem = Instantiate(BonusGemPrefab, transform);
    //    BonusGem.gameObject.SetActive(false);
    //    BonusGem.OnBonusDestroy += HandleBonusDestroy;
    //  //  BonusGem.Setup();
    //}
    public int HandleBonus() 
    {
        BonusGem.Charge();
        return YumyBonus;
    }
    public void HandleBonusDestroy() 
    {
        BonusGem.gameObject.SetActive(false);
        BonusGem.transform.SetParent(transform);
        BonusGem.transform.localPosition = Vector3.zero;
    }
}
