using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemsInMenu : MonoBehaviour
{
    Image image;
    
    public List<string> itemNames = new List<string>();
    public List<Image> images = new List<Image>();

    [SerializeField] TextMeshProUGUI itemDescription;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            image = child.GetComponent<Image>();
            images.Add(image);
        }
        //foreach (Image child in transform)
        //{
        //    image = child.GetComponent<Image>();
        //}         
    }
    // sonra daha fazla item i�in yap�lacak image i�in bir iteration yap�lacak ve bu k�s�m daha iyi yap�lacak durmadan update olmayacak
    void Update()
    {
        if (itemNames.Contains("Hermes Boots"))  // buras� iyile�ecek --- nas�l yap�l�r bilemedim
            images[0].color = Color.white;
        if (itemNames.Contains("Quiver Item"))
            images[1].color = Color.white;
    }
}
