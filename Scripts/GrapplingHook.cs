using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public bool hit;
    public GameObject player;
    public Rigidbody2D rb2D;
    public CameraDraw cameraDraw;
    public SpringJoint2D hookConnection;
    private LineRenderer hookRope;
    Vector3[] ropePosition = new Vector3[2];


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        cameraDraw = Camera.main.GetComponent<CameraDraw>();
        hookRope = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ropePosition[0] = player.transform.position;
        ropePosition[1] = transform.position;
        hookRope.SetPositions(ropePosition);
        hit = cameraDraw.CheckRay(transform.position);

        if (hit)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;

            if (!player.GetComponent<SpringJoint2D>())
            {
                hookConnection = player.AddComponent<SpringJoint2D>();
                hookConnection.connectedAnchor = transform.position;
                hookConnection.autoConfigureDistance = false;
                hookConnection.dampingRatio = 1;
                


            }

            hookConnection.distance -= Input.GetAxis("Vertical") * 0.02f;
        }
        else
        {
            if(rb2D.gravityScale == 0)
            {
                Destroy(gameObject);
            }
            Destroy(player.GetComponent<SpringJoint2D>());
        }
    }

}
