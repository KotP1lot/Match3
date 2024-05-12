using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LvlBtn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI text;
    private LvlSO LvlSO;
    public static LvlSO LvL;
    public void Setup(LvlSO so) 
    {
        text.text = so.name;
        LvlSO = so;
    }
    private static void ChooseLVL(LvlSO so) 
    {
        LvL = so;
    }
    public void OnCklick() 
    {
        ChooseLVL(LvlSO);
        SceneManager.LoadScene(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCklick();
    }
}
