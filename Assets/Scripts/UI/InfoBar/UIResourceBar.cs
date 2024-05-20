using TMPro;
using UnityEngine;

public class UIResourceBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private Resource resource;

    public void Setup(Resource resource)
    {
        this.resource = resource;
        resource.OnResourceChanged += OnResourceChangedHandler;
        OnResourceChangedHandler();
    }
    private void OnResourceChangedHandler()
    {
        text.text = resource.Amount.ToString();
    }
}
