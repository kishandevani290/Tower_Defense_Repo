using UnityEngine;

public static class Data_Manager
{
    public static int TotalCoins
    {
        get => PlayerPrefs.GetInt(Keys.TotalCoins,2000);
        set
        {
            PlayerPrefs.SetInt(Keys.TotalCoins, value);
        }
    }

    public static int BestScrore
    {
        get => PlayerPrefs.GetInt(Keys.BestScrore, 0);
        set
        {
            PlayerPrefs.SetInt(Keys.BestScrore, value);
        }
    }

    public static int getsetCurrentTurnet
    {
        get => PlayerPrefs.GetInt(Keys.key_current_turnet, 0);
        set
        {
            PlayerPrefs.SetInt(Keys.key_current_turnet, value);
        }
    }

    public static int Levelnumber
    {
        get => PlayerPrefs.GetInt(Keys.Key_levelnumber, 1);
        set
        {
            PlayerPrefs.SetInt(Keys.Key_levelnumber, value);
        }
    }
}
