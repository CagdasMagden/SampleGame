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
                    itemDescription.text = "Hermesin Ayakkab�lar� art arda iki z�play�� yapmaya olanak verir.";
                    break;
                case "Quiver Item Inv":
                    itemDescription.text = "Hermesin Sada�� ok olu�turmak i�in gereken s�reyi k�salt�r.";
                    break;
                default:
                    itemDescription.text = "";
                    break;
            }
            itemDescription.gameObject.SetActive(true);
        }      
        else
        {
            itemDescription.text = "Bu e�yan�n �zelliklerini ��renmek i�in �nce e�yay� bulmal�s�n.";
            itemDescription.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescription.gameObject.SetActive(false); 
    }
}
