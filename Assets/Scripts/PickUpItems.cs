using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{ // coin managerlar buna eklenebilir

    bool canPickUp;

    [SerializeField] ShowItemsInMenu itemShowSC;
    //[SerializeField] GameObject itemShow;
    
    [SerializeField] TextMeshProUGUI pickUpMessage;
    void Start()
    {
        //itemShowSC = itemShow.GetComponent<ShowItemsInMenu>();
        pickUpMessage.gameObject.SetActive(false);       
    }
   
    void Update()
    {
        PickUp();
    }

    void PickUp()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            itemShowSC.itemNames.Add(gameObject.name);
            Destroy(gameObject);
        }                  
        // DO OTHER THÝNGS
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canPickUp = true;
            pickUpMessage.text = "Press 'E' to pick up " + gameObject.name;
            pickUpMessage.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canPickUp = false;
            pickUpMessage.gameObject.SetActive(false);
        }
    }

    public string GetNameOfItem()
    {
        return gameObject.name;
    }
}
