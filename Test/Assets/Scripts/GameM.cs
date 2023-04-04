using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public static GameM instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemContainer allItemsContainer;
    public SeedContainer allSeedsContainer;
    public DragAndDropC dragAndDropController;

    public ToolbarC toolbarControllerGlobal;
}
