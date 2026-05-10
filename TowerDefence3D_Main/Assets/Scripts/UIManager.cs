using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Tween coinTween;

    [SerializeField] private TextMeshProUGUI[] totalcoins;

    [Header("AllGamePanel")]
    [SerializeField] private GameObject CoinshopPanel;
    [SerializeField] private GameObject SettingPanel;
    public GameObject mainmenupanel;
    public GameObject gameplaypanel;
    public GameObject LoosePanel;
    public GameObject WinPanel;

    [Header("Level Text")]
    [SerializeField] private TextMeshProUGUI Levelnumber_txt;

    public TextMeshProUGUI RoundsSurvived;

    public TextMeshProUGUI BestScrore;
    public TextMeshProUGUI CurrentScrore;

    public int Currentlevelnumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdatetotalCoins(0);
        Currentlevelnumber = Data_Manager.Levelnumber;
        Levelnumber_txt.text = "Level " + Currentlevelnumber.ToString();
    }

    public void UpdatetotalCoins(int coin)
    {
        int startCoins = Data_Manager.TotalCoins;
        int targetCoins = startCoins + coin;

        Data_Manager.TotalCoins = targetCoins;
        PlayerPrefs.Save();

        AnimateCoins(startCoins, targetCoins);
    }

    private void AnimateCoins(int from, int to)
    {
        coinTween?.Kill();

        int animatedValue = from;

        coinTween = DOTween.To(
            () => animatedValue,
            x =>
            {
                animatedValue = x;
                UpdateAllCoinTexts(animatedValue);
            },
            to,
            0.7f
        ).SetEase(Ease.OutCubic);
    }

    private void UpdateAllCoinTexts(int value)
    {
        for (int i = 0; i < totalcoins.Length; i++)
        {
            if (totalcoins[i] != null)
                totalcoins[i].text = value.ToString();
        }
    }

    public void showCoinPanel_on_notenough_coin()
    {
        CoinshopPanel.SetActive(true);
    }

    public void OpenSettingPanel()
    {
        Soundmanger.Instance.play_click();
        SettingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        Soundmanger.Instance.play_click();
        SettingPanel.SetActive(false);
    }

    public void OpenCoinPanel()
    {
        Soundmanger.Instance.play_click();
        CoinshopPanel.SetActive(true);
    }

    public void CloseCoinPanel()
    {
        Soundmanger.Instance.play_click();
        CoinshopPanel.SetActive(false);
    }

    public void PurchaseCoin()
    {
        Soundmanger.Instance.play_click();
        UpdatetotalCoins(500);
    }

    public void Close_coinPanel()
    {
        Soundmanger.Instance.play_click();
        CoinshopPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
