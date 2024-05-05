using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    bool canOpen = true;

    [SerializeField] GameObject inventory;

    void Start()
    {
        inventory.SetActive(false);
    }

    void Update()
    {
        OpenInventory();
    }

    void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canOpen)
        {
            canOpen = false;
            inventory.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !canOpen)
        {
            canOpen= true;
            inventory.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void CloseInventoryButton()
    {
        inventory.SetActive(false);
        Time.timeScale = 1f;
        canOpen = true;
    }
}
