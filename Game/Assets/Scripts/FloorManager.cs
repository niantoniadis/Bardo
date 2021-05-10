using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject horWallPrefab;
    public GameObject verWallPrefab;
    public GameObject roomPrefab;
    public GameObject exitPrefab;
    public GameObject loadingMessage;

    public bool altGen;
    public static bool gameStarted = false;

    PlayerMovement player;

    [SerializeField]
    public List<GameObject> startTiles;
    Chunk rootChunk;
    Chunk currChunk;

    RoomTemplates templates;
    public EnemyTemplates e_templates;
    List<Chunk> allChunks;
    List<GameObject> allWalls;
    float timeSinceAddedChunk = 0f;

    int iterations = 1;
    int levelSize = 70;

    bool printed;
    bool satisfactory = false;
    bool generated = false;
    int minLevel;
    int maxLevel;

    int lastCount;

    public Chunk CurrChunk
    {
        get
        {
            return currChunk;
        }
    }

    public int LevelSize
    {
        get
        {
            return levelSize;
        }
    }
    public int Iterations
    {
        get
        {
            return iterations;
        }
        set
        {
            iterations = value;
        }
    }

    public List<Chunk> AllChunks
    {
        get
        {
            return allChunks;
        }
        set
        {
            allChunks = value;
        }
    }

    public bool Printed
    {
        get
        {
            return printed;
        }
    }
    private void Start()
    {
        printed = false;
        allChunks = new List<Chunk>();
        allWalls = new List<GameObject>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rootChunk = Instantiate(startTiles[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
        currChunk = rootChunk;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gameStarted = false;
        
    }

    private void Update()
    {
        if (!satisfactory)
        {
            if (!printed)
            {
                NeighborConfig();
            }
            else
            {
                minLevel = 0;
                maxLevel = 0;
                for (int i = 0; i < AllChunks.Count; i++)
                {
                    int level = AllChunks[i].Level;
                    if (level > maxLevel)
                    {
                        maxLevel = level;
                    }
                    else if (level < minLevel)
                    {
                        minLevel = level;
                    }
                }
                if (maxLevel == 2 && minLevel == -2)
                {
                    satisfactory = true;
                    //GenerateBossRooms();
                    loadingMessage.SetActive(false);
                }
                else
                {
                    GenerateDungeon();
                }
            }
        }
        else if(!generated)
        {
            generated = true;
            gameStarted = true;
            currChunk.GenerateFirstRoom();
            currChunk.visited = true;
            currChunk.ActivateColor();
            transform.position = new Vector3(currChunk.transform.position.x, currChunk.transform.position.y, 0);
        }
    }

    void NeighborConfig()
    {
        timeSinceAddedChunk += Time.deltaTime;
        if(AllChunks.Count > lastCount)
        {
            timeSinceAddedChunk = 0;
        }

        if (iterations >= 70 || timeSinceAddedChunk > 0.2f)
        {
            printed = true;
            for (int i = 0; i < allChunks.Count; i++)
            {
                int level = allChunks[i].Level;
                switch (level)
                {
                    case -2:
                        allChunks[i].SetColor(new Color(1f, 0f, 1f, 1f));
                        break;
                    case -1:
                        allChunks[i].SetColor(new Color(1f, 0.5f, 1f, 1f));
                        break;
                    case 0:
                        allChunks[i].SetColor(new Color(1f, 1f, 1f, 1f));
                        break;
                    case 1:
                        allChunks[i].SetColor(new Color(0.5f, 1f, 0.5f, 1f));
                        break;
                    case 2:
                        allChunks[i].SetColor(new Color(0f, 1f, 0f, 1f));
                        break;
                    default:
                        if (level > 0)
                        {
                            allChunks[i].SetColor(new Color(0f, 1f, 0f, 0.1f));
                        }
                        else
                        {
                            allChunks[i].SetColor(new Color(1f, 0f, 1f, 0.1f));
                        }
                        break;
                }

                allChunks[i].ActivateColor();

                for (int j = 0; j < allChunks.Count; j++)
                {
                    Vector2 neighborPos = allChunks[j].Pos;
                    Vector2 emptyNeighborsPos = allChunks[i].Pos;
                    Vector2 emptyNeighborsSize = allChunks[i].Size;
                    if (neighborPos == new Vector2(emptyNeighborsPos.x + emptyNeighborsSize.x, emptyNeighborsPos.y))
                    {
                        allChunks[i].RightNeighbor = allChunks[j];
                    }
                    else if (neighborPos == new Vector2(emptyNeighborsPos.x, emptyNeighborsPos.y + emptyNeighborsSize.y))
                    {
                        allChunks[i].TopNeighbor = allChunks[j];
                    }
                    else if (neighborPos == new Vector2(emptyNeighborsPos.x - emptyNeighborsSize.x, emptyNeighborsPos.y))
                    {
                        allChunks[i].LeftNeighbor = allChunks[j];
                    }
                    else if (neighborPos == new Vector2(emptyNeighborsPos.x, emptyNeighborsPos.y - emptyNeighborsSize.y))
                    {
                        allChunks[i].BottomNeighbor = allChunks[j];
                    }
                }

                //check allchunks [i] to see if it is a ladder tile that leads nowhere, if true, change the sprite of the chunk to a broken ladder tile
            }
            WallConfig();
        }
        lastCount = AllChunks.Count;
    }

    void WallConfig()
    {
        for(int i = 0; i < allChunks.Count; i++)
        {
            Chunk chunk = allChunks[i];
            //if (chunk.RightNeighbor == null || chunk.RightNeighbor.level != chunk.Level)
            //{
                allWalls.Add(Instantiate(verWallPrefab, new Vector3(chunk.Pos.x + chunk.Size.x/2, chunk.Pos.y, chunk.transform.position.z), Quaternion.identity));
            //}
            //if (chunk.TopNeighbor == null || chunk.TopNeighbor.Level != chunk.level)
            //{
                allWalls.Add(Instantiate(horWallPrefab, new Vector3(chunk.Pos.x, chunk.Pos.y + chunk.Size.y / 2, chunk.transform.position.z), Quaternion.identity));
            //}                                                                                                                                                     
            //if (chunk.LeftNeighbor == null || chunk.LeftNeighbor.Level != chunk.level)                                                                            
            //{                                                                                                                                                     
               allWalls.Add(Instantiate(verWallPrefab, new Vector3(chunk.Pos.x - chunk.Size.x / 2, chunk.Pos.y, chunk.transform.position.z), Quaternion.identity));
            //}                                                                                                                                                     
            //if (chunk.BottomNeighbor == null || chunk.BottomNeighbor.Level != chunk.level)                                                                        
            //{                                                                                                                                                     
                allWalls.Add(Instantiate(horWallPrefab, new Vector3(chunk.Pos.x, chunk.Pos.y - chunk.Size.y / 2, chunk.transform.position.z), Quaternion.identity));
            //}

            SpriteRenderer chunkRenderer = chunk.GetComponent<SpriteRenderer>();

            switch (chunkRenderer.sprite.name)
            {
                case "UR":
                    if(chunk.RightNeighbor == null || chunk.RightNeighbor.GetComponent<SpriteRenderer>().sprite.name != "DL")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "UT":
                    if (chunk.TopNeighbor == null || chunk.TopNeighbor.GetComponent<SpriteRenderer>().sprite.name != "DB")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "UL":
                    if (chunk.LeftNeighbor == null || chunk.LeftNeighbor.GetComponent<SpriteRenderer>().sprite.name != "DR")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "UB":
                    if (chunk.BottomNeighbor == null || chunk.BottomNeighbor.GetComponent<SpriteRenderer>().sprite.name != "DT")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "DR":
                    if (chunk.RightNeighbor == null || chunk.RightNeighbor.GetComponent<SpriteRenderer>().sprite.name != "UL")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "DT":
                    if (chunk.TopNeighbor == null || chunk.TopNeighbor.GetComponent<SpriteRenderer>().sprite.name != "UB")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "DL":
                    if (chunk.LeftNeighbor == null || chunk.LeftNeighbor.GetComponent<SpriteRenderer>().sprite.name != "UR")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
                case "DB":
                    if (chunk.BottomNeighbor == null || chunk.BottomNeighbor.GetComponent<SpriteRenderer>().sprite.name != "UT")
                    {
                        chunkRenderer.sprite = startTiles[0].GetComponent<SpriteRenderer>().sprite;
                    }
                    break;
            }
        }
    }

    public void GenerateDungeon()
    {
        timeSinceAddedChunk = 0f;
        iterations = 1;
        printed = false;
        int size = allChunks.Count;
        for (int i = 0; i < size; i++)
        {
            Chunk temp = allChunks[0];
            allChunks.RemoveAt(0);
            Destroy(temp.gameObject);
        }
        allChunks = new List<Chunk>();

        size = allWalls.Count;
        for (int i = 0; i < size; i++)
        {
            GameObject temp = allWalls[0];
            allWalls.RemoveAt(0);
            Destroy(temp);
        }
        allWalls = new List<GameObject>();
        rootChunk = Instantiate(startTiles[0], transform.position, Quaternion.identity).GetComponent<Chunk>();
        currChunk = rootChunk;
        player.player.room = currChunk.instance;
        player.player.floorLevel = currChunk.level;
    }

    void GenerateBossRooms()
    {
        for (int i = 0; i < allChunks.Count; i++)
        {
            Chunk chunk = allChunks[i];
            switch (chunk.Level)
            {
                case -2:
                    if (minLevel > -3)
                    {
                        if (chunk.GenerateBossRoom())
                        {
                            minLevel = -3;
                            chunk.SetColor(new Color(1f, 0f, 1f, 0.1f));
                        }
                    }
                    break;
                case 2:
                    if (maxLevel < 3)
                    {
                        if (chunk.GenerateBossRoom())
                        {
                            maxLevel = 3;
                            chunk.SetColor(new Color(0f, 1f, 0f, 0.1f));
                        }
                    }
                    break;
            }

            if(maxLevel == 3 && minLevel == -3)
            {
                return;
            }
        }
    }

    public void SwitchRooms(Chunk target)//, Vector2 dir)
    {
        currChunk.instance.SetSelfActive(false);
        currChunk = target;
        if (currChunk.instance == null)
        {
            currChunk.GenerateRoom();
            currChunk.visited = true;
            currChunk.ActivateColor();
        }
        else
        {
            currChunk.instance.SetSelfActive(true);
        }
        transform.position = new Vector3(currChunk.transform.position.x, currChunk.transform.position.y, 0);
        player.transform.position = currChunk.instance.RoomBounds.center;
        Player temp = player.GetComponent<Player>();
        temp.room = currChunk.instance;
        temp.floorLevel = currChunk.Level;
    }

    public bool PlayerWon()
    {
        bool result = true;

        for (int i = 0; i < allChunks.Count; i++)
        {
            if (!allChunks[i].instance)
            {
                result = false;
            }
            else if(!allChunks[i].instance.Completed)
            {
                result = false;
            }
        }

        return result;
    }
}
