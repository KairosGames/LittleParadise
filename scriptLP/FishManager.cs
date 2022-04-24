using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField] Transform[] Targets;

    [SerializeField] FishBehaviour prefabBoid;
    [SerializeField] public int number = 30;
    [SerializeField] float startSpeed = 1;
    [SerializeField] float spread = 10;
    [SerializeField] float minHighLimit;
    [SerializeField] float maxHighLimit;

    public List<FishBehaviour> lstBoids = new List<FishBehaviour>();

    [SerializeField] float paramTimeMin = 5;
    [SerializeField] float paramTimeMax = 8;
    float timer;
    sbyte nTarg = 0;

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            FishBehaviour f = Instantiate(prefabBoid);
            Vector3 positionBoid = (Random.insideUnitSphere * spread) + transform.position;
            positionBoid.y = Mathf.Abs(positionBoid.y);
            f.transform.position = positionBoid;
            f.speed = (positionBoid - transform.position).normalized * startSpeed;
            f.transform.parent = transform;
            f.manager = this;
            f.minHighLimit = minHighLimit;
            f.maxHighLimit = maxHighLimit;
            lstBoids.Add(f);

            timer = Random.Range(paramTimeMin, paramTimeMax+1);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(paramTimeMin, paramTimeMax + 1);
            nTarg++;
            if (nTarg>= Targets.Length) { nTarg = 0; }
        }

        for (int i = 0; i < number; i++)
        {
            lstBoids[i].target = Targets[nTarg].position;
        }
    }
}
