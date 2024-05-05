using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // para belli bir büyüklüðü geçtiði zaman text bölmesini aþýyor --- ondan dolayý rakam bölmesi arttýðýnda sola doðru gitmesini istiyordum full
    // ama çok üzerinde duramadým o yüzden parayý o noktalara gelmeyecek þekilde ayarladýk

    int maxMoney = 9999;

    public TextMeshProUGUI textMeshPro;

       
    public void SetCoinCountTMP(float score) 
    {
        textMeshPro.text = score.ToString();
        if (score > 9999) 
            textMeshPro.text = maxMoney.ToString();
    }
}
