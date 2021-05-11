using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Chunk leftNeighbor = null;
    public Chunk rightNeighbor = null;
    public Chunk topNeighbor = null;
    public Chunk bottomNeighbor = null;

    int index;
    Vector2 pos;
    Vector2 size;
    Color color;
    public int level;
    public bool visited;

    public RoomTemplates templates;
    public FloorManager manager;
    public Room instance = null;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<FloorManager>();
        List<Chunk> temp = manager.AllChunks;
        temp.Add(this);
        manager.AllChunks = temp;
        pos = transform.position;
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    public Chunk LeftNeighbor
    {
        get
        {
            return leftNeighbor;
        }
        set
        {
            leftNeighbor = value;
        }
    }

    public Chunk RightNeighbor
    {
        get
        {
            return rightNeighbor;
        }
        set
        {
            rightNeighbor = value;
        }
    }
    public Chunk TopNeighbor
    {
        get
        {
            return topNeighbor;
        }
        set
        {
            topNeighbor = value;
        }
    }
    public Chunk BottomNeighbor
    {
        get
        {
            return bottomNeighbor;
        }
        set
        {
            bottomNeighbor = value;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return pos;
        }
    }

    public Vector2 Size
    {
        get
        {
            return size;
        }
    }

    public bool GenerateBossRoom()
    {
        bool success = false;
        if (level == 2)
        {
            if (topNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.upperTopRooms[1], new Vector3(pos.x, pos.y, 0) + new Vector3(0, size.y, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = 3;
            }
            else if (rightNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.upperRightRooms[1], new Vector3(pos.x, pos.y, 0) + new Vector3(size.x, 0, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = 3;
            }
            else if (bottomNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.upperBottomRooms[1], new Vector3(pos.x, pos.y, 0) - new Vector3(0, size.y, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = 3;
            }
            else if (leftNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.upperLeftRooms[1], new Vector3(pos.x, pos.y, 0) - new Vector3(size.x, 0, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = 3;
            }
        }
        else
        {
            if (topNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.lowerTopRooms[1], new Vector3(pos.x, pos.y, 0) + new Vector3(0, size.y, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = -3;
            }
            else if (rightNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.lowerRightRooms[1], new Vector3(pos.x, pos.y, 0) + new Vector3(size.x, 0, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true; 
                chunk.Level = -3;
            }
            else if (bottomNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.lowerBottomRooms[1], new Vector3(pos.x, pos.y, 0) - new Vector3(0, size.y, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = -3;
            }
            else if (leftNeighbor == null)
            {
                Chunk chunk = Instantiate(templates.lowerLeftRooms[1], new Vector3(pos.x, pos.y, 0) - new Vector3(size.x, 0, 0), Quaternion.identity).GetComponent<Chunk>();
                success = true;
                chunk.Level = -3;
            }
        }
        return success;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public void ActivateColor()
    {
        if (visited)
        {
            foreach (SpriteRenderer sR in GetComponentsInChildren<SpriteRenderer>())
            {
                sR.color = color;
            }
        }
        else
        {
            foreach (SpriteRenderer sR in GetComponentsInChildren<SpriteRenderer>())
            {
                sR.color = new Color(0.1415094f, 0.1415094f, 0.1415094f);
            }
        }
    }

    public void GenerateRoom()
    {
        instance = Instantiate(manager.roomPrefab, new Vector3(0,0,0), Quaternion.identity).GetComponent<Room>();
        instance.info = gameObject.GetComponent<Chunk>();
        instance.exitPrefab = manager.exitPrefab;
        instance.GenerateEnemyPattern(manager.e_templates);
        instance.SetSelfActive(true);
    }

    public void GenerateFirstRoom()
    {
        instance = Instantiate(manager.roomPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Room>();
        instance.info = gameObject.GetComponent<Chunk>();
        instance.exitPrefab = manager.exitPrefab;
        instance.SetFirstActive(true);
    }

    public bool PlayerWon()
    {
        return manager.PlayerWon();
    }
}
