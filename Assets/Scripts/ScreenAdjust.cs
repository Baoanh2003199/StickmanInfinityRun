using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    public bool maintainWidth = true;
    float defaultWidth;
    public int adaptPosition;
    float defaultHeight;
    public Vector3 CamPos;
    // Start is called before the first frame update
    void Start()
    {
        CamPos = Camera.main.transform.position;
        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
        defaultHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (maintainWidth)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;
            Camera.main.transform.position = new Vector3(CamPos.x, adaptPosition * (defaultWidth - Camera.main.orthographicSize), CamPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(adaptPosition * (defaultWidth - Camera.main.orthographicSize), CamPos.y,CamPos.z);
        }
    }
}
