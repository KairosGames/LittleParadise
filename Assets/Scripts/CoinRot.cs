using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRot : MonoBehaviour
{
    [SerializeField]  Vector3 rotSpeed = new Vector3(0,0,180.0f);


    void Update()
    {
        transform.Rotate(rotSpeed*Time.deltaTime);
    }
}
