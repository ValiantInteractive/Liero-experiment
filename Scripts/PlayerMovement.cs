using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CameraDraw cameraDraw;

    public Rigidbody2D rb2D;

    public float moveSpeed;
    public float reducedSpeed;
    public float diggingSpeed;
    public float jumpForce;

    public bool digging; 

    public GameObject testrayGround;
    public bool grounded;

    public GameObject testrayFront;
    public bool frontHit;

    public GameObject testrayHead;
    public bool headHit;

    public GameObject testraySlope;
    public bool slopeHit; 

    public GameObject ammo;
    public GameObject ammoSpawn;

    public GameObject hook;
    public GameObject hookInstance; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = cameraDraw.CheckRay(testrayGround.transform.position);
        frontHit = cameraDraw.CheckRay(testrayFront.transform.position);
        headHit = cameraDraw.CheckRay(testrayHead.transform.position);
        slopeHit = cameraDraw.CheckRay(testraySlope.transform.position);


        if(Input.GetAxisRaw("Horizontal") != 0)
        {

            if(frontHit == false)
            {
                if(slopeHit == true)
                {
                    transform.Translate(Input.GetAxis("Horizontal") * reducedSpeed * Time.deltaTime,
                        reducedSpeed * Time.deltaTime, 0);
                }


                transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(Input.GetAxis("Horizontal") * diggingSpeed * Time.deltaTime,
                       Input.GetAxis("Vertical") * diggingSpeed * Time.deltaTime, 0);
                digging = cameraDraw.PaintRay(transform.position, 10);

            }


            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1); // Flip
        }



        if(headHit == true && rb2D.velocity.y > 0)
        {
            rb2D.velocity = Vector2.zero;
        }


        if (grounded == true && rb2D.velocity.y <= 0){

            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;


        }
        else
        {
            rb2D.gravityScale = 1;

        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ammoInstance = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            ammoInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * 10 * transform.localScale.x;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (!GetComponent<SpringJoint2D>() && hookInstance == null)
            {
                hookInstance = Instantiate(hook, ammoSpawn.transform.position, Quaternion.identity);
                hookInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * 10 * transform.localScale.x;
            }
            else
            {
                Destroy(hookInstance);
                Destroy(GetComponent<SpringJoint2D>());

            }
        }

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2D.velocity = Vector2.up * jumpForce;
        }

    }
}
