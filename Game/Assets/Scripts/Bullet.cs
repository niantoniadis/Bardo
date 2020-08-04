using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> variants;

    Vector2 direction;
    float speed;
    // Start is called before the first frame update
    void Start()
    { 
        int index = Mathf.FloorToInt(Random.Range(0.0f, 2.99f));
        GetComponent<SpriteRenderer>().sprite = variants[index];

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = direction * speed * Time.deltaTime;
        transform.position += newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
