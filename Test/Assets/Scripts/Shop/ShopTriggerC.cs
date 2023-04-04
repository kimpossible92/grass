using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerC : MonoBehaviour
{
    [SerializeField] private UIShopC uiShop;
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        uiShop.Show();
        FindObjectOfType<SoundMM>().Play("Parrot");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        uiShop.Hide();
    }
}
