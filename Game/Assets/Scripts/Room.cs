using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool active = false;
    Bounds roomBounds;
    Player player;
    float possessionTimer;
    public Bounds RoomBounds
    {
        get
        {
            return roomBounds;
        }
    }

    public List<NewEnemy> Enemies
    {
        get { return activeEnemies; }
    }

    bool completed = false;
    public Chunk info;

    public List<GameObject> bullets;
    List<NewEnemy> activeEnemies;
    public GameObject exitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        roomBounds = GetComponent<BoxCollider2D>().bounds;
        possessionTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        possessionTimer -= Time.deltaTime;

        // possesion check
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Attempting possession");
            NewEnemy temp = ClickedEnemy();
            if (temp != null && possessionTimer <= 0.0f)
            {
                temp.possessed = true;
                temp.playerMovement.isPossessing = true;
                possessionTimer = 5f;
            }
            else
            {
                for (int i = 0; i < activeEnemies.Count; i++)
                {
                    if (activeEnemies[i].possessed)
                    {
                        activeEnemies[i].possessed = true;
                        activeEnemies[i].playerMovement.isPossessing = true;
                        break;
                    }
                }
            }
        }

        List<NewEnemy> delList = new List<NewEnemy>();
        if (active) {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (completed)
                {
                    delList.Add(activeEnemies[i]);
                }
                else if (activeEnemies[i].health <= 0)
                {
                    delList.Add(activeEnemies[i]);
                }
            }

            for(int i = 0; i < delList.Count; i++)
            {
                GameObject temp = delList[i].gameObject;
                activeEnemies.Remove(delList[i]);
                Destroy(temp);
            }

            if (activeEnemies.Count <= 0 && !completed)
                GenerateExits();
        }
    }

    void GenerateExits()
    {
        completed = true;
        Exit temp;
        if(info.TopNeighbor != null && (info.TopNeighbor.Level == info.Level || info.TopNeighbor.Level == info.Level - 1 || info.TopNeighbor.Level == info.Level + 1))
        {
            temp = Instantiate(exitPrefab, new Vector3(0, roomBounds.max.y, 0), Quaternion.identity).GetComponent<Exit>();
            temp.floorManager = info.manager;
            temp.targetChunk = info.TopNeighbor;
            temp.transform.parent = transform.GetChild(0);
        }
        if (info.RightNeighbor != null && (info.RightNeighbor.Level == info.Level || info.RightNeighbor.Level == info.Level - 1 || info.RightNeighbor.Level == info.Level + 1))
        {
            temp = Instantiate(exitPrefab, new Vector3(roomBounds.max.x, 0, 0), Quaternion.identity).GetComponent<Exit>();
            temp.floorManager = info.manager;
            temp.targetChunk = info.RightNeighbor;
            temp.transform.parent = transform.GetChild(0);
        }
        if (info.LeftNeighbor != null && (info.LeftNeighbor.Level == info.Level || info.LeftNeighbor.Level == info.Level - 1 || info.LeftNeighbor.Level == info.Level + 1))
        {
            temp = Instantiate(exitPrefab, new Vector3(roomBounds.min.x, 0, 0), Quaternion.identity).GetComponent<Exit>();
            temp.floorManager = info.manager;
            temp.targetChunk = info.LeftNeighbor;
            temp.transform.parent = transform.GetChild(0);
        }
        if (info.BottomNeighbor != null && (info.BottomNeighbor.Level == info.Level || info.BottomNeighbor.Level == info.Level - 1 || info.BottomNeighbor.Level == info.Level + 1))
        {
            temp = Instantiate(exitPrefab, new Vector3(0, roomBounds.min.y, 0), Quaternion.identity).GetComponent<Exit>();
            temp.floorManager = info.manager;
            temp.targetChunk = info.BottomNeighbor;
            temp.transform.parent = transform.GetChild(0);
        }
    }

    public void SetSelfActive(bool val)
    {
        transform.GetChild(0).gameObject.SetActive(val);
        active = val;
    }

    public void GenerateEnemyPattern(EnemyTemplates templates)
    {
        if (!completed)
        {
            activeEnemies = new List<NewEnemy>();
            NewEnemy tempEnemy = Instantiate(templates.eSP_Neutral[0], transform.position, Quaternion.identity).GetComponent<NewEnemy>();
            // set floor num
            tempEnemy.playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            tempEnemy.floor = tempEnemy.playerMovement.player.floorLevel;
            tempEnemy.SetStats();
            activeEnemies.Add(tempEnemy);
        }
    }

    // this goes in entity/room manager or what not
    private NewEnemy ClickedEnemy()
    {
        // gets the collider
        Collider2D clicked_collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        // checks if it got a collider
        if (clicked_collider != null)
        {
            // if it got a collider, it returns the cell it got from the click
            return clicked_collider.gameObject.GetComponent<NewEnemy>();
        }
        return null;
    }
}
