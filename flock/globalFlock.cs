using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{


    public GameObject fishPrefab;
    public static int tankSize = 20;
    public static int tankSizey = 5;

    static int numFish = 10;
    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;
    public static Vector3 initPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                       Random.Range(-tankSizey, tankSizey),
                                       Random.Range(-tankSize, tankSize));
            pos = initPos - pos;
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
            Vector3 next = new Vector3(Random.Range(-tankSize, tankSize),
                                       Random.Range(-tankSize, tankSize),
                                       Random.Range(-tankSize, tankSize));
            goalPos = -next;


        }
    }
}
