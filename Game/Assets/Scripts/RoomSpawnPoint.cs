using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour
{
    public int spawnDir;
    //1 - right
    //2 - top
    //3 - left
    //4 - bottom
    //5 - upper right
    //6 - upper top
    //7 - upper left
    //8 - upper bottom
    //9 - lower right
    //10 - lower top
    //11 - lower left
    //12 - lower bottom

    RoomTemplates templates;
    FloorManager manager;
    Chunk parent;

    float waitTime;

    int rand;
    public bool spawned = false;

    public bool Spawned
    {
        get
        {
            return spawned;
        }
        set
        {
            spawned = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (spawnDir < 0)
        {
            spawned = true;
        }

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<FloorManager>();
        parent = transform.parent.gameObject.GetComponent<Chunk>();

        Invoke("Spawn", 0.1f);
        Debug.Log("this");
    }

    // Update is called once per frame

    private void Update()
    {

    }
    void Spawn()
    {
        Chunk spawnedChunk;

        if (manager.Iterations == manager.LevelSize)
            spawned = true;
        if (!spawned)
        {
            switch (spawnDir)
            {
                case 1:
                    if(!manager.altGen)
                        rand = Random.Range(0, templates.rightRooms.Count);
                    else
                        rand = Random.Range(0, templates.rightRooms.Count - 1);
                    spawnedChunk = Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity).GetComponent<Chunk>();
                    spawnedChunk.Level = parent.Level;
                    spawnedChunk.LeftNeighbor = parent;
                    manager.Iterations += 1;
                    break;
                case 2:
                    if (!manager.altGen)
                        rand = Random.Range(0, templates.topRooms.Count);
                    else
                        rand = Random.Range(0, templates.topRooms.Count-1);
                    spawnedChunk = Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity).GetComponent<Chunk>();
                    spawnedChunk.Level = parent.Level;
                    spawnedChunk.BottomNeighbor = parent;
                    manager.Iterations += 1;
                    break;
                case 3:
                    if (!manager.altGen)
                        rand = Random.Range(0, templates.leftRooms.Count);
                    else
                        rand = Random.Range(0, templates.leftRooms.Count - 1);
                    spawnedChunk = Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity).GetComponent<Chunk>();
                    spawnedChunk.Level = parent.Level;
                    spawnedChunk.RightNeighbor = parent;
                    manager.Iterations += 1;
                    break;
                case 4:
                    if (!manager.altGen)
                        rand = Random.Range(0, templates.bottomRooms.Count);
                    else
                        rand = Random.Range(0, templates.bottomRooms.Count - 1);
                    spawnedChunk = Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity).GetComponent<Chunk>();
                    spawnedChunk.Level = parent.Level;
                    spawnedChunk.TopNeighbor = parent;
                    manager.Iterations += 1;
                    break;
                case 5:
                    if (parent.Level < 2)
                    {
                        //rand = Random.Range(0, templates.upperRightRooms.Count);
                        spawnedChunk = Instantiate(templates.upperRightRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level + 1;
                        spawnedChunk.LeftNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 6:
                    if (parent.Level < 2)
                    {
                        //rand = Random.Range(0, templates.upperTopRooms.Count);
                        spawnedChunk = Instantiate(templates.upperTopRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level + 1;
                        spawnedChunk.BottomNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 7:
                    if (parent.Level < 2)
                    {
                        //rand = Random.Range(0, templates.upperLeftRooms.Count);
                        spawnedChunk = Instantiate(templates.upperLeftRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level + 1;
                        spawnedChunk.RightNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 8:
                    if (parent.Level < 2)
                    {
                        //rand = Random.Range(0, templates.upperBottomRooms.Count);
                        spawnedChunk = Instantiate(templates.upperBottomRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level + 1;
                        spawnedChunk.TopNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 9:
                    if (parent.Level > -2)
                    {
                        //rand = Random.Range(0, templates.lowerRightRooms.Count);
                        spawnedChunk = Instantiate(templates.lowerRightRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level - 1;
                        spawnedChunk.LeftNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 10:
                    if (parent.Level > -2)
                    {
                        //rand = Random.Range(0, templates.lowerTopRooms.Count);
                        spawnedChunk = Instantiate(templates.lowerTopRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level - 1;
                        spawnedChunk.BottomNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 11:
                    if (parent.Level > -2)
                    {
                        //rand = Random.Range(0, templates.lowerLeftRooms.Count);
                        spawnedChunk = Instantiate(templates.lowerLeftRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level - 1;
                        spawnedChunk.RightNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
                case 12:
                    if (parent.Level > -2)
                    {
                        //rand = Random.Range(0, templates.lowerBottomRooms.Count);
                        spawnedChunk = Instantiate(templates.lowerBottomRooms[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
                        spawnedChunk.Level = parent.Level - 1;
                        spawnedChunk.TopNeighbor = parent;
                        manager.Iterations += 1;
                    }
                    break;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnPoint"))
        {
            if (!spawned && !collision.gameObject.GetComponent<RoomSpawnPoint>().Spawned)
            {
                collision.gameObject.GetComponent<RoomSpawnPoint>().Spawned = true;
            }
        }
        if (collision.gameObject.CompareTag("Chunk"))
        {
            spawned = true;
        }
    }
}
