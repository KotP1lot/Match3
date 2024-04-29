using System;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    public event Action OnFloorDestroy;
    private Image image;
    private Sprite[] sprites;
    private int hp;
    public void Setup(FloorSO so)
    {
        sprites = so.hp_sprites;
        hp = sprites.Length;
        image.sprite = sprites[hp - 1];
    }
    public void Subcribe(GridCell cell)
    {
        cell.OnGemDestroyinCell += OnGemDestroyedNierby;
    }
    private void OnGemDestroyedNierby()
    {
        hp--;
        if (hp <= 0)
        {
            OnFloorDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}