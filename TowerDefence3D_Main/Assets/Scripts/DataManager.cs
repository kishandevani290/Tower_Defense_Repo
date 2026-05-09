using UnityEngine;

public static class Data_Manager
{
    public static int TotalCoins
    {
        get => PlayerPrefs.GetInt(Keys.TotalCoins, 0);
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
}
