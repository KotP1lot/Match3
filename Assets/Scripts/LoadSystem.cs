using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour
{
    [SerializeField] ISaveLoadSO[] saveData;
    [SerializeField] bool delete;
    bool GemDestr;
    bool isFirstStart = true;

    private void Start()
    {
        foreach (var s in saveData)
        {
            s.Load();
            s.Setup();
        }
        if (Game.gameStat != null)
        {
            foreach (var s in saveData)
            {
                s.SaveGameStat(Game.gameStat);
            }
            Game.gameStat = null;
        }
        isFirstStart = false;
    }
    private void LateUpdate()
    {
        if (GemDestr) return;
        Gem[] gems = FindObjectsOfType<Gem>();
        foreach (var gem in gems)
        {
            Destroy(gem.gameObject);
        }
        GemDestr = true;
    }
    public void OnDisable()
    {
        Save();
    }
    public void OnApplicationFocus(bool focus)
    {
        if (focus && isFirstStart) return;
        if (!focus) 
        {
            Save();
        }
        if (focus) 
        {
 
            Load();
        }
    }
    public void Save() 
    {
        foreach (var s in saveData)
        {
            s.Save();
        }
    }

    public void Load() 
    {
        foreach (var s in saveData)
        {
            s.Load();
            s.Setup();
        }
    }
    public void CloseApp() 
    {
        Save();
        Application.Quit();
    }
    public void ClearPlayerPrefs() 
    {
        foreach (var s in saveData)
        {
                s.Clear();
        }
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
