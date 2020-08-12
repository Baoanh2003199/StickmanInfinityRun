using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player Player;
    private Vector3 LastPlayerPos;
    private float DistanceToMove;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>();
        LastPlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToMove = Player.transform.position.x - LastPlayerPos.x;
        transform.position = new Vector3(transform.position.x + DistanceToMove, transform.position.y, transform.position.z);
        LastPlayerPos = Player.transform.position;

    }
}
