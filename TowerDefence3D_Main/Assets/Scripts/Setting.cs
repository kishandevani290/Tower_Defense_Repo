using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static Setting settings;

    [Header("Setting_Visuals")]
    [SerializeField] Image[] img_Sound;
    [SerializeField] Sprite[] Sound_OnOff;
    [SerializeField] Image[] img_Music;
    [SerializeField] Sprite[] Music_OnOff;

    private void Awake() => settings = this;
    #region Sound --------------------------

    private void Start()
    {
        getsetMusic = getsetMusic;
        getsetSound = getsetSound;
        Soundmanger.Instance.initBGMUSIC();
    }

    public void onclick_Sound()
    {
        Soundmanger.Instance.play_click();
        getsetSound = getsetSound == 1 ? 0 : 1;
    }

    public int getsetSound
    {
        get => PlayerPrefs.GetInt(Keys.key_sound, 1);
        set
        {
            PlayerPrefs.SetInt(Keys.key_sound, value);
            for (int i = 0; i < img_Sound.Length; i++)
            {
                img_Sound[i].sprite = Sound_OnOff[value];
            }
        }
    }

    #endregion

    #region Music --------------------------

    public void onclick_Music()
    {
        Soundmanger.Instance.play_click();
        getsetMusic = getsetMusic == 1 ? 0 : 1;
        Soundmanger.Instance.initBGMUSIC();
    }

    public int getsetMusic
    {
        get => PlayerPrefs.GetInt(Keys.key_Music, 1);
        set
        {
            PlayerPrefs.SetInt(Keys.key_Music, value);
            for (int i = 0; i < img_Music.Length; i++)
            {
                img_Music[i].sprite = Music_OnOff[value];
            }
        }
    }
    #endregion
}