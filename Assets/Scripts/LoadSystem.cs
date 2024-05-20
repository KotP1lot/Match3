using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    [SerializeField] ISaveLoadSO[] saveData;
    [SerializeField] bool delete;
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
        if (Game.gameStat != null)
        {
            foreach (var s in saveData)
            {
                s.SaveGameStat(Game.gameStat);
            }
            Game.gameStat = null;
        }
    }
}
