using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class UILanguage : MonoBehaviour
{
    private bool active = false;
    int data;
    public static UILanguage Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        int data = PlayerPrefs.GetInt("Language", -1);
        if (data == -1)
        {
            data = Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Russian ? 1 : 0;
        }
        Debug.Log(Application.systemLanguage.ToString());
        ChangeLocale(data);
        DontDestroyOnLoad(gameObject);
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
