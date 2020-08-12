using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBg : MonoBehaviour
{
    private BoxCollider2D col;
    private float groundHorizontalLength;
    // Start is called before the first frame update
    void Start()
    {

        col = GetComponent<BoxCollider2D>();
        groundHorizontalLength = col.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -groundHorizontalLength)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 groundoffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundoffset;
    }
}
