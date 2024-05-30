using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour
{
    [SerializeField] ISaveLoadSO[] saveData;
    [SerializeField] bool delete;
    [SerializeField] TextMeshProUGUI text;
    bool GemDestr;
    private void Start()
    {
        foreach (var s in saveData)
        {
            if (delete)
            {
                s.Clear();
            }
            s.Load();
            s.Setup();
        }
        if (delete)
        {
            PlayerPrefs.DeleteAll();
        }
        if (Game.gameStat != null)
        {
            foreach (var s in saveData)
            {
                s.SaveGameStat(Game.gameStat);
            }
            Game.gameStat = null;
        }
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
