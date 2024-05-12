using System;
using UnityEngine;
using UnityEngine.UI;

public class FastRow : MonoBehaviour
{
    [SerializeField] Image emotion;
    [SerializeField] Image gem;
    [SerializeField] db_GemSO gemSO;
    [SerializeField] Emotion[] emotions;
    [Serializable]
    public struct Emotion 
    {
        public FastidiousnessType type;
        public Sprite sprite;
    } 
    public void Setup(FastidiousnessType type, GemType gemType) 
    {
        gem.sprite = gemSO.GetIconByType(gemType);
        emotion.sprite = GetEmotionByType(type);
    }
    private Sprite GetEmotionByType(FastidiousnessType type) 
    {
        return Array.Find(emotions, emotion => emotion.type == type).sprite;
    }
}
