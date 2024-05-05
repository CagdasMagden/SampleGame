using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    bool haveArrow;
    bool canReload = true;
    bool haveQuiver;

    float arrowNumber = 3f;
    float maxArrow = 3f;
    float reloadSpeed = 30f;

    Transform arrowSpawnPoint;

    ShowItemsInMenu showItemsInMenu;
    [SerializeField] GameObject itemVisual;

    [SerializeField] TextMeshProUGUI arrowAmountText;
    [SerializeField] GameObject arrow; // yeni bir bullet yapýlabilir
    
    void Start()
    {
        arrowSpawnPoint = transform.GetChild(2).GetComponent<Transform>(); // playerin 3. cocuðunun componenti
        showItemsInMenu = itemVisual.GetComponent<ShowItemsInMenu>();
    }
    
    void OnShoot(InputValue value)
    {
        if (value.isPressed && arrowNumber > 0)
            Shoot();
    }

    void Shoot()
    {
        Instantiate(arrow, arrowSpawnPoint.position, transform.rotation);
        arrowNumber--;
        arrowAmountText.text = "x " + arrowNumber.ToString();
        GetItemBuffs(showItemsInMenu.itemNames);
        Reload();
    }

    void Reload()
    {
        if (canReload)
        {
            canReload = false;
            StartCoroutine(ArrowReload());                 
        }
    }

    void GetItemBuffs(List<string> nameItem)
    {
        if (nameItem.Contains("Quiver Item"))
            reloadSpeed = 20f;
    }

    IEnumerator ArrowReload()
    {
        while (arrowNumber < maxArrow)
        {            
            yield return new WaitForSeconds(reloadSpeed);
            arrowNumber++;
            arrowAmountText.text = "x " + arrowNumber.ToString();
        }
        canReload = true;
    }
}
