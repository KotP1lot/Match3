using UnityEngine;
[CreateAssetMenu()]
public class BorderSO : ScriptableObject
{
        public BorderType type;
        public Sprite[] hp_sprites; 
}
public enum BorderType 
{
    simplyWood,

}
