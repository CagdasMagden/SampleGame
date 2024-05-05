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
    // sonra daha fazla item için yapýlacak image için bir iteration yapýlacak ve bu kýsým daha iyi yapýlacak durmadan update olmayacak
    void Update()
    {
        if (itemNames.Contains("Hermes Boots"))  // burasý iyileþecek --- nasýl yapýlýr bilemedim
            images[0].color = Color.white;
        if (itemNames.Contains("Quiver Item"))
            images[1].color = Color.white;
    }
}
