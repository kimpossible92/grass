using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitReaction : PlayerHit
{
    GameObject toolbar;
    ToolbarC toolbarController;
    public override void Hit()
    {
        toolbar = GameObject.FindWithTag("toolbar");
        toolbarController = GetComponent<ToolbarC>();
        
        switch (toolbarController.GetItem.Name)
        {
            case "Food_Corn":
                Debug.Log("Zjadłeś kukurydze");
                AddHunger(20);
                break;

            case "Food_Parsley":
                Debug.Log("Zjadłeś pietruszkę");
                AddHealth(5);
                AddHunger(10);
                break;

            case "Food_Potato":
                Debug.Log("Zjadłeś ziemniaka");
                AddHealth(5);
                AddHunger(40);
                break;

            case "Food_Strawberry":
                Debug.Log("Zjadłeś truskawkę");
                AddHealth(30);
                AddHunger(10);
                break;

            case "Food_Tomato":
                Debug.Log("Zjadłeś pomidora");
                AddHunger(30);
                break;
        }
        GameM.instance.inventoryContainer.RemoveItem(GameM.instance.toolbarControllerGlobal.GetItem, 1);
        toolbar.SetActive(!toolbar.activeInHierarchy);
        toolbar.SetActive(true);
    }

    void AddHunger(int add)
    {
        if(HungerC.currentHunger + add < 100)
            HungerC.currentHunger += add;
        else
            HungerC.currentHunger = 100;
    }

    void AddHealth(int add)
    {
        if (HealthC.currentHealth + add < 100)
            HealthC.currentHealth += add;
        else
            HealthC.currentHealth = 100;
    }
}