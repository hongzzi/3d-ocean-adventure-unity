using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{

    public float speed = 0.001f;
    float rotationSpeed = 4.0f;
    float minSpeed = 0.8f;
    float maxSpeed = 2.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;
    public Transform tr;
    public Vector3 newGoalPos;

    bool turning = false;


    // Use this for initialization
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, globalFlock.initPos) >= globalFlock.tankSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = globalFlock.initPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed + Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
                ApplyRules();
        }

        transform.Translate(-Time.deltaTime * speed, 0, 0); // 방향값 설정해줘야함 

    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allFish;

        Vector3 vcentre = globalFlock.initPos;
        Vector3 vavoid = globalFlock.initPos;
        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlock.goalPos;

        float dist;

        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != globalFlock.initPos)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
        }
    }


}
