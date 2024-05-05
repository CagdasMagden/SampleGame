using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatforms : MonoBehaviour
{
    public int whichWay = 1;
    
    [SerializeField] public float moveSpeed;
    [SerializeField] float maxTravelMetre;
    [SerializeField] float currentTravel;
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (currentTravel > maxTravelMetre)
        {
            whichWay = whichWay * -1;
            currentTravel = 0;
        }
        else
        {
            currentTravel += 1 * Time.deltaTime;
            transform.position += new Vector3(moveSpeed * whichWay * Time.deltaTime, 0, 0);
        }        
    }
}
