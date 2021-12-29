using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour
{
    public Camera cam;
    private bool hitToWall;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        hitToWall = cam.GetComponent<CameraDraw>().PaintRay(transform.position, 7);
        if (hitToWall)
        {
            Destroy(gameObject);
        }
    }
}
