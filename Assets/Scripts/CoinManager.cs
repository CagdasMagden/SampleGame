using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // para belli bir b�y�kl��� ge�ti�i zaman text b�lmesini a��yor --- ondan dolay� rakam b�lmesi artt���nda sola do�ru gitmesini istiyordum full
    // ama �ok �zerinde duramad�m o y�zden paray� o noktalara gelmeyecek �ekilde ayarlad�k

    int maxMoney = 9999;

    public TextMeshProUGUI textMeshPro;

       
    public void SetCoinCountTMP(float score) 
    {
        textMeshPro.text = score.ToString();
        if (score > 9999) 
            textMeshPro.text = maxMoney.ToString();
    }
}
