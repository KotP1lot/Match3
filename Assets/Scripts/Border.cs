using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    public event Action<Direction> OnBorderDestroy;
    private Image image;
    private Sprite[] sprites;
    private int hp;
    private Direction direction;
    public void Setup(BorderSO so, Direction dir) 
    {
        direction = dir;
        sprites = so.hp_sprites;
        hp = sprites.Length;
        image.sprite = sprites[hp-1];
    }
    public void Subcribe(List<GridCell> cells) 
    {
        foreach (GridCell c in cells) 
        {
            c.OnGemDestroyinCell += OnGemDestroyedNierby;    
        }
    }

    private void OnGemDestroyedNierby() 
    {
        hp--;
        if (hp <= 0) 
        {
            OnBorderDestroy?.Invoke(direction);
            Destroy(gameObject);
        }
    }
}
