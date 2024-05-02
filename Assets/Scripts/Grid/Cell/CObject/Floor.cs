using System;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    public event Action OnFloorDestroy;
    private Image image;
    private Sprite[] sprites;
    private int hp;
    private GridCell cell;
    public void Setup(FloorSO so)
    {
        image = GetComponent<Image>();
        sprites = so.hp_sprites;
        hp = sprites.Length;
        image.sprite = sprites[hp - 1];
    }
    public void Subcribe(GridCell cell)
    {
        this.cell=cell;
        cell.OnGemDestroyinCell += OnGemDestroyedNierby;
    }
    private void OnGemDestroyedNierby()
    {
        hp--;
        if (hp <= 0)
        {
            OnFloorDestroy?.Invoke();
            EventManager.instance.OnFloorCleaned?.Invoke();
            cell.OnGemDestroyinCell -= OnGemDestroyedNierby;
            Destroy(gameObject);
            return;
        }
        image.sprite = sprites[hp - 1];
    }
}