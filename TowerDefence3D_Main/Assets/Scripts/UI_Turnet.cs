using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UI_Shop_Turnet_item : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] GameObject obj_selected,obj_coins;
    [SerializeField] TextMeshProUGUI txtcoinvalue;
    public TextMeshProUGUI txt_turnet_stock_value;
    [SerializeField] bool isCoin;
    [SerializeField] bool isDiamond;
    [SerializeField] int coinvalue;
    [SerializeField] public TurretData _turretData;
    public int Turnet_stock;

    public void Intialize_ShopThings()
    {
        Debug.Log("Index" + index);
        if (index == 0)
        {
            getset_TurnetUnlock = 1;
        }
        if (index == 0)
        {
            getset_currentstock = 1;
        }

        Debug.Log($"Index {index} current unlock state in PlayerPrefs: {getset_TurnetUnlock}");

        txtcoinvalue.text = coinvalue.ToString();
        Turnet_stock = getset_currentstock;
        txt_turnet_stock_value.text = Turnet_stock.ToString();
        
        if (Data_Manager.getsetCurrentTurnet == index)
        {
            ShopManager.manager.currentSelected_turnet = this;
            BuildManager.instance.SelectedTurret = _turretData.turretPrefab;
            obj_selected.SetActive(true);
            Debug.Log("This name Object is Activated:" + _turretData.turretPrefab.name);
        }
        else
        {
            obj_selected.SetActive(false);
        }
        Debug.Log(index);
    }

    public int getset_TurnetUnlock
    {
        get => PlayerPrefs.GetInt((Keys.key_turnet_unlock + index), 0);
        set => PlayerPrefs.SetInt((Keys.key_turnet_unlock + index), value);
    }

    public int getset_currentstock
    {
        get => PlayerPrefs.GetInt((Keys.key_turnet_stock + index),0);
        set => PlayerPrefs.SetInt((Keys.key_turnet_stock + index), value);
    }

    public void onclick_item()
    {
        Debug.Log("Button Click");
        //SoundManager.manager.playButton();
        if (getset_TurnetUnlock == 1)
        {
            Debug.Log("GO to if");
            if (Data_Manager.getsetCurrentTurnet != index)
            {
                ShopManager.manager.currentSelected_turnet.deactivatethis();
                activatethis();
                ShopManager.manager.currentSelected_turnet = this;
                BuildManager.instance.SelectedTurret = _turretData.turretPrefab;
            }
            else if(Data_Manager.getsetCurrentTurnet == index)
            {
                if (isCoin)
                {
                    if (coinvalue > Data_Manager.TotalCoins)
                    {
                        UIManager.Instance.showCoinPanel_on_notenough_coin();
                    }
                    else
                    {
                        GameManager.instance.Add_usingCoin(this, coinvalue);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Go to else");
            if (isCoin)
            {
                if (coinvalue > Data_Manager.TotalCoins)
                {
                    UIManager.Instance.showCoinPanel_on_notenough_coin();
                }
                else
                {
                    GameManager.instance.Add_usingCoin(this, coinvalue);
                }
            }
        }
    }

    void hideall_Op()
    {
        obj_selected.SetActive(false);
    }

    public void activatethis()
    {
        hideall_Op();
        getset_TurnetUnlock = 1;
        Data_Manager.getsetCurrentTurnet = index;
        obj_selected.SetActive(true);
    }

    public void deactivatethis()
    {
        hideall_Op();
    }

    public void UpdateStockAmount()
    { 
        Turnet_stock = getset_currentstock;
        txt_turnet_stock_value.text = Turnet_stock.ToString();
    }
}
