using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BoidManager : MonoBehaviour
{

    [SerializeField] Transform Target;

    [SerializeField] BoidBehaviour prefabBoid;
    [SerializeField] public int number = 30;
    [SerializeField] float startSpeed = 1;
    [SerializeField] float spread = 10;
    [SerializeField] float highLimit;

    public List<BoidBehaviour> lstBoids = new List<BoidBehaviour>();

    float angleRot = 0;
    [SerializeField] float speedRot;
    float dist;

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            BoidBehaviour b = Instantiate(prefabBoid);
            Vector3 positionBoid = (Random.insideUnitSphere * spread) + transform.position;
            positionBoid.y = Mathf.Abs(positionBoid.y);
            b.transform.position = positionBoid;
            b.speed = (positionBoid - transform.position).normalized * startSpeed;
            b.transform.parent = transform;
            b.manager = this;
            b.highLimit = highLimit;
            lstBoids.Add(b);

            dist = Target.localPosition.magnitude;
        }
    }

    void Update()
    {
        for (int i = 0; i < number; i++)
        {
            lstBoids[i].target = Target.position;
        }

        angleRot += Time.deltaTime*speedRot;
        if(angleRot>=360) { angleRot = 0; }

        Target.localPosition = new Vector3(Mathf.Cos(angleRot)*dist, Target.localPosition.y, Mathf.Sin(angleRot) * dist);
    }
}
