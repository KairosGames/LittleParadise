using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public AudioSource coinCatched;

    [SerializeField] GameObject[] Coins;
    public List<GameObject> lstCoinsLeft;

    [SerializeField] Text score;

    internal int coinsGet;
    internal bool won = false;

    void Start()
    {
        for (int i = 0; i<Coins.Length; i++)
        {
            Coins[i].GetComponent<CoinBehaviour>().coinsManager = this;
            if(Coins[i].activeSelf)
            {
                lstCoinsLeft.Add(Coins[i]);
            }
            coinsGet = 0;
            won = false;
        }

    }

    void Update()
    {
        score.text = $"{coinsGet}/8";

        if (lstCoinsLeft.Count == 0)
        {
            won = true;
        }

    }
}
