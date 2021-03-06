using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDraw : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckRay(Vector3 testray)
    {
        RaycastHit hit;

        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(testray)), out hit))
        {
            return false;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if(tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            return true;

        }
        else
        {
            return false; 
        }
    }

    public bool PaintRay(Vector3 location, int radius)
    {
        RaycastHit hit;

        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(location)), out hit))
        {
            return false;
        }


        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;


        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            
            Circle(tex, (int)pixelUV.x, (int)pixelUV.y, radius, Color.clear);
            tex.Apply();

            return true;

        }
        else
        {
            return false;
        }


    }

    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

            for (y = 0; y <= d; y++)
            {

                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);
            }
        }
    }



}
