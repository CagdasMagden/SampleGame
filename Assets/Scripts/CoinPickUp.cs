using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
  
    float coinScore;

    public CoinManager coinManager;

    [SerializeField] float coinPoints;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pickables"))
        {
            coinScore += coinPoints;
            coinManager.SetCoinCountTMP(coinScore);
            Destroy(other.gameObject);
        }
    }
}
