using UnityEngine;
using UnityEngine.UI;

public class UIMusicBtn : MonoBehaviour
{
    [SerializeField] Sprite mute;
    [SerializeField] Sprite play;
    [SerializeField] Image img;
    private bool isMute = true;
    private void Start()
    {
        isMute = PlayerPrefs.GetFloat("Music", 0.5f) == 0;
        img.sprite = isMute?mute:play;
    }
    public void Click() 
    {
        isMute = !isMute;
        img.sprite = isMute ? mute : play;
        Music.Instance.Mute(isMute);
    }
}
