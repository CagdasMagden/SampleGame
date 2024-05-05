using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] TextMeshProUGUI itemDescription;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject hoveredObject = eventData.pointerEnter;
        Image image = hoveredObject.GetComponent<Image>();
        if (image.color == Color.white)
        {
            switch (hoveredObject.name)
            {
                case "Hermes Boots Inv":
                    itemDescription.text = "Hermesin Ayakkabýlarý art arda iki zýplayýþ yapmaya olanak verir.";
                    break;
                case "Quiver Item Inv":
                    itemDescription.text = "Hermesin Sadaðý ok oluþturmak için gereken süreyi kýsaltýr.";
                    break;
                default:
                    itemDescription.text = "";
                    break;
            }
            itemDescription.gameObject.SetActive(true);
        }      
        else
        {
            itemDescription.text = "Bu eþyanýn özelliklerini öðrenmek için önce eþyayý bulmalýsýn.";
            itemDescription.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescription.gameObject.SetActive(false); 
    }
}
