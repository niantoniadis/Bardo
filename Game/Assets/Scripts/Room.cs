using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject groundRockPrefab;
    public SpriteRenderer floor;

    float w;
    float h;
    // Start is called before the first frame update
    void Start()
    {
        w = floor.bounds.size.x;
        h = floor.bounds.size.y;

        Vector3 startPos = new Vector3(floor.bounds.center.x - floor.bounds.size.x/2,floor.bounds.center.y + floor.bounds.size.y/2);
        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                Vector3 pos = new Vector3(startPos.x + i, startPos.y - j);
                Vector3 posVariance = new Vector3(Random.Range(-0.35f, 0.35f), Random.Range(-0.35f,0.35f),0);
                Instantiate(groundRockPrefab, pos + posVariance, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
