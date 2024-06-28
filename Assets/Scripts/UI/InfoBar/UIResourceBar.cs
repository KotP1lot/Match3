using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image adBtn;
    private Resource resource;

    public void Setup(Resource resource)
    {
        this.resource = resource;
        resource.OnResourceChanged += OnResourceChangedHandler;
        OnResourceChangedHandler();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (resource != null)
        {
            if (focus)
            {

                resource.OnResourceChanged += OnResourceChangedHandler;
                OnResourceChangedHandler();
            }
            else
            {
                resource.OnResourceChanged -= OnResourceChangedHandler;
            }
        }
    }
    //private void ShowBtn(bool isLoaded)
    //{
    //    adBtn.gameObject.SetActive(true);
    //}

    private void OnResourceChangedHandler()
    {
        text.text = resource.Amount.ToString();
    }
}
