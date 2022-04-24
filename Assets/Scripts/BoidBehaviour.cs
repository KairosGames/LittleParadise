using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    internal BoidManager manager;

    [SerializeField] float attractionArea = 50;
    [SerializeField] float alignmentArea = 9;
    [SerializeField] float repulsionArea = 5;

    [SerializeField] float attractionForce = 20;
    [SerializeField] float alignmentForce = 3;
    [SerializeField] float repulsionForce = 15;

    [SerializeField] public Vector3 speed = new Vector3();
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float minSpeed = 12;

    [SerializeField] public Vector3 target = new Vector3();
    [SerializeField] float targetForce = 20;

    [SerializeField] public float highLimit;


    void Update()
    {
        Vector3 sumForces = new Vector3();
        float forcesNumber = 0;

        for(int i = 0; i < manager.number; i++)
        {
            var lstBoids = manager.lstBoids;
            var boid = lstBoids[i];
            if (lstBoids[i] != this)
            { 
                Vector3 gapVec = boid.transform.position - transform.position;
                Vector3 localForce = new Vector3();
                if (gapVec.sqrMagnitude < attractionArea * attractionArea)
                {
                    if (gapVec.sqrMagnitude > alignmentArea * alignmentArea)
                    {
                        float boost = ((gapVec.magnitude-alignmentArea)/(attractionArea-alignmentArea))*4;
                        localForce = gapVec.normalized * attractionForce * boost;
                    }
                    else
                    {
                        if (gapVec.sqrMagnitude > repulsionArea * repulsionArea)
                        {
                            localForce = boid.speed.normalized * alignmentForce;
                        }
                        else
                        {
                            float boost = (1 - (gapVec.magnitude / repulsionArea)) * 4;
                            localForce = gapVec.normalized * -1 * repulsionForce * boost;
                        }
                    }
                }
                sumForces += localForce;
                forcesNumber++;
            }
        }

        sumForces /= forcesNumber;

        
        Vector3 gapVecTarget = target - transform.position;
        Vector3 localForceTarget = gapVecTarget.normalized * targetForce;
        sumForces += localForceTarget;

        speed += -speed * 10 * (Vector3.Angle(sumForces, speed) / 180.0f) * Time.deltaTime;
        speed += sumForces * Time.deltaTime;

        if (speed.sqrMagnitude >= maxSpeed * maxSpeed) { speed = speed.normalized * maxSpeed; }
        if (speed.sqrMagnitude <= maxSpeed * minSpeed) { speed = speed.normalized * minSpeed; }

        if (speed.sqrMagnitude > 0)
            transform.LookAt(transform.position + speed);

        transform.position += speed * Time.deltaTime;

        if (transform.position.y <= highLimit)
            transform.position = new Vector3(transform.position.x, highLimit, transform.position.z);

    }
}
