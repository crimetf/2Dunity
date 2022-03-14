using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform farBG;
    public Transform middleBG;

    public float minHeight = -1f;
    public float maxHeight = 2.5f;
    private float clampY;
    private Vector2 lastPos;
    private Vector2 nextPos;

    
    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        clampY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x, clampY, transform.position.z);

        nextPos = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBG.position += new Vector3(nextPos.x, nextPos.y, 0f);
        middleBG.position += new Vector3(nextPos.x, nextPos.y, 0f) * 0.5f;

        lastPos = transform.position;
    }
}
