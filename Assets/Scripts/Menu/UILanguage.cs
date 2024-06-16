using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class UILanguage : MonoBehaviour
{
    private bool active = false;
    int data;

    private void Awake()
    {
        int data = PlayerPrefs.GetInt("Language", -1);
        if (data == -1)
        {
            data = Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Russian ? 1 : 0;
        }
        ChangeLocale(data);
    }

    public void Click() 
    {
        data = data == 0 ? 1 : 0;
        ChangeLocale(data);
    }
    public void ChangeLocale(int localeID) 
    {
        if (active) return;
        StartCoroutine(SetLocate(localeID));
    }

    IEnumerator SetLocate(int localeID) 
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        data = localeID;
        PlayerPrefs.SetInt("Language", data);
        PlayerPrefs.Save();
        active = false;
    }
}
