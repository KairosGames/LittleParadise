using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public CoinsManager coinsManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinsManager.coinsGet++;
            coinsManager.coinCatched.Play();
            gameObject.SetActive(false);
            coinsManager.lstCoinsLeft.Remove(gameObject);
        }
    }
}
