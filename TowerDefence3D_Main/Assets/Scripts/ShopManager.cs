using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public UI_Shop_Turnet_item currentSelected_turnet;
    public UI_Shop_Turnet_item[] All_turnet_items;

    public static ShopManager manager;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        foreach (var turnet in All_turnet_items)
        { 
            turnet.Intialize_ShopThings();
        }
    }

    public void Update_shop()
    {
        foreach (var turnet in All_turnet_items)
        {
            turnet.Intialize_ShopThings();
        }
    }
}
