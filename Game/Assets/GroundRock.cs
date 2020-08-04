using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRock : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> variants;
    // Start is called before the first frame update
    void Start()
    {
        int index = Mathf.FloorToInt(Random.Range(0.0f, 2.99f));
        GetComponent<SpriteRenderer>().sprite = variants[index];

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360f));
        float scaleModifier = Random.Range(-0.002f, 0.001f);
        transform.localScale = new Vector3(transform.localScale.x + scaleModifier, transform.localScale.y + scaleModifier, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
