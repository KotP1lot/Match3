using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource music;
    float volume;
    public static Music Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        music = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("Music", 0.5f);
        music.volume = volume;
        DontDestroyOnLoad(gameObject);
    }
    public void Mute(bool isMute) 
    {
        volume = isMute ? 0 : 0.5f;
        music.volume = volume;
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }
}
