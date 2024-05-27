using UnityEngine;

public class UIOneTip : MonoBehaviour
{
    [SerializeField] TipType type;
    public enum TipType 
    {
        calendar,
        chiefs,
        caffe,
        main
    }
    private void OnEnable()
    {
        int i = PlayerPrefs.GetInt(type.ToString());
        if (i == 1) gameObject.SetActive(false);
    }

    public void Close() 
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt(type.ToString(), 1);
    }

}
