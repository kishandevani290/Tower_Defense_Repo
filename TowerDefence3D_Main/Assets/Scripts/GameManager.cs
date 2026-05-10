using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("AllLevels")]
    public LevelData[] allLevels;

    public int PlayerLives = 20;
    public int WaveRoundnumber = 0;
    
    public Image healthBarFill;

    private int CurrentLives;

    private bool isGameOver = false;

    public int EnemyScrore = 0;
    
    public WaveSpwaner waveSpawner;

    public int currentleveindex;

    public int BestScrore;

    private void Start()
    {
        CurrentLives = PlayerLives;
        UpdateHealthBar();
        waveSpawner.enabled = false;
    }

    private void Awake()
    {
        instance = this;
    }

    public void Add_usingCoin(UI_Shop_Turnet_item item, int coins)
    {
        UIManager.Instance.UpdatetotalCoins(-coins);
        ShopManager.manager.currentSelected_turnet.deactivatethis();
        ShopManager.manager.currentSelected_turnet = item;
        ShopManager.manager.currentSelected_turnet.activatethis();
        ShopManager.manager.currentSelected_turnet.getset_currentstock++;
        ShopManager.manager.currentSelected_turnet.Turnet_stock++;
        Debug.Log("turmetstock" + ShopManager.manager.currentSelected_turnet.Turnet_stock);
        ShopManager.manager.currentSelected_turnet.txt_turnet_stock_value.text = ShopManager.manager.currentSelected_turnet.Turnet_stock.ToString();
        BuildManager.instance.SelectedTurret = ShopManager.manager.currentSelected_turnet._turretData.turretPrefab;
        Soundmanger.Instance.Playturnet_purchasesound();
    }

    public void TakeDamage(int amount)
    {
        if (isGameOver) return;

        PlayerLives -= amount;
        
        UpdateHealthBar();

        if (PlayerLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null && CurrentLives > 0)
        {
            healthBarFill.fillAmount = (float)PlayerLives / CurrentLives;
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        UIManager.Instance.LoosePanel.SetActive(true);
        UIManager.Instance.RoundsSurvived.text = GameManager.instance.WaveRoundnumber.ToString();
        BuildManager.instance.ClearAllTowerss();
        CurrentLives = PlayerLives;
        UpdateHealthBar();
        Debug.Log("GAME OVER!");
    }

    public void PlayLevel()
    {
        currentleveindex = getsetLevel;
        if (currentleveindex >= 0 && currentleveindex < allLevels.Length)
        { 
            LevelData selectedlevel = allLevels[currentleveindex];
            UIManager.Instance.mainmenupanel.SetActive(false);
            UIManager.Instance.gameplaypanel.SetActive(true);

            waveSpawner.InitializeLevel(selectedlevel);
        }
    }

    public int getsetLevel
    {
        get => PlayerPrefs.GetInt(Keys.Key_level, 0);
        set
        {
            PlayerPrefs.SetInt(Keys.Key_level, value > allLevels.Length - 1 ? 0 : value); // Limit To save level only to available level
        }
    }

    public void OnLevelWin()
    {
        Soundmanger.Instance.play_win();
        Data_Manager.Levelnumber++;
        getsetLevel++;
        UIManager.Instance.CurrentScrore.text = EnemyScrore.ToString();
        if (EnemyScrore > Data_Manager.BestScrore)
        {
            Data_Manager.BestScrore = EnemyScrore;
            BestScrore = Data_Manager.BestScrore;
        }
        UIManager.Instance.BestScrore.text = Data_Manager.BestScrore.ToString();
        UIManager.Instance.WinPanel.SetActive(true);
        CurrentLives = PlayerLives;
        UpdateHealthBar();
        BuildManager.instance.ClearAllTowerss();
    }

    public void NextLevel()
    {
        currentleveindex = getsetLevel;
        if (currentleveindex >= 0 && currentleveindex < allLevels.Length)
        {
            LevelData selectedlevel = allLevels[currentleveindex];
            UIManager.Instance.WinPanel.SetActive(false);
            UIManager.Instance.gameplaypanel.SetActive(true);
            waveSpawner.InitializeLevel(selectedlevel);
        }
    }

    public void MenuPanel_btn()
    {
        UIManager.Instance.WinPanel.SetActive(false);
        UIManager.Instance.gameplaypanel.SetActive(false);
        UIManager.Instance.mainmenupanel.SetActive(true);
    }

    public void RetryLevel()
    {
        UIManager.Instance.LoosePanel.SetActive(false);
        UIManager.Instance.gameplaypanel.SetActive(true);
        currentleveindex = getsetLevel;
        if (currentleveindex >= 0 && currentleveindex < allLevels.Length)
        {
            LevelData selectedlevel = allLevels[currentleveindex];
            UIManager.Instance.WinPanel.SetActive(false);
            UIManager.Instance.gameplaypanel.SetActive(true);
            waveSpawner.InitializeLevel(selectedlevel);
        }
    }

    public void MenuPanel_btn_from_loosePanel()
    {
        UIManager.Instance.LoosePanel.SetActive(false);
        UIManager.Instance.gameplaypanel.SetActive(false);
        UIManager.Instance.mainmenupanel.SetActive(true);
    }
}