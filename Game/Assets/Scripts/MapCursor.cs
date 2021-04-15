using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour
{
    FloorManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<FloorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = manager.CurrChunk.Pos;
    }
}
