using UnityEngine;

public class Soundmanger : MonoBehaviour
{
    public static Soundmanger Instance;

    [SerializeField] AudioSource Background_sound;
    [SerializeField] AudioSource asource;

    private bool Is_music_playing;

    [Header("Sound_SFX_Atributes")]
    [SerializeField] AudioClip Purchasedturnet_sfx,Click_sfx;
    [SerializeField] AudioClip Clip_win;

    [Header("Music_Atributes")]
    [SerializeField] AudioClip clipBG;

    private void Awake()
    {
        Instance = this;
    }

    public void initBGMUSIC()
    {
        if (Setting.settings.getsetMusic != 0)
        {
            if (!Is_music_playing)
            {
                Is_music_playing = true;
                Background_sound.clip = clipBG;
                Background_sound.Play();
                Background_sound.loop = true;
            }
        }
        else
        {
            Is_music_playing = false;
            Background_sound.Stop();
        }
    }
    
    public void play_click()
    {
        if (Setting.settings.getsetSound == 0) return;
        asource.PlayOneShot(Click_sfx);
    }

    public void play_win()
    {
        if (Setting.settings.getsetSound == 0) return;
        asource.PlayOneShot(Clip_win);
    }

    public void Playturnet_purchasesound()
    {
        if (Setting.settings.getsetSound == 0) return;
        asource.PlayOneShot(Purchasedturnet_sfx);
    }
}
