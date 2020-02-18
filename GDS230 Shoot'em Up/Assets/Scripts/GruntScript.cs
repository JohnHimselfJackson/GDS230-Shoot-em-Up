using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : GenericEnemy
{
    float waitTime = -1;
    bool patroling = false;
    bool movingLeft = true;

    public float movementSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpotted)
        {

        }
        else 
        {
            PatrolOrIdle();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject GO = collision.gameObject;
        if (GO.CompareTag("Player"))
        {
            EnemySpotted = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject GO = collision.gameObject;
        if (GO.CompareTag("Player"))
        {
            EnemySpotted = false;
        }
    }

    public override void Attack()
    {

    }
    public override void Follow()
    {

    }

    public override void PatrolOrIdle()
    {
        if (patroling || waitTime < 0)
        {
            RaycastHit2D leftHitGround = Physics2D.Raycast(transform.position + new Vector3(-0.15f, -0.31f, 0), Vector3.down, 0.2f);
            RaycastHit2D rightHitGround = Physics2D.Raycast(transform.position + new Vector3(0.15f, -0.31f, 0), Vector3.down, 0.2f);

            if (!movingLeft && rightHitGround != false && Physics2D.BoxCastAll(transform.position + new Vector3(0.18f, 0, 0), new Vector3(0.05f, 0.55f, 0), 0f, Vector3.zero).Length <=2)
            {
                if (rightHitGround.transform.gameObject.CompareTag("Barrier"))
                {
                    transform.Translate(transform.right * movementSpeed * Time.deltaTime, Space.Self);
                }
            }
            else if(!movingLeft && (rightHitGround == false || Physics2D.BoxCastAll(transform.position + new Vector3(0.18f, 0, 0), new Vector3(0.05f, 0.55f, 0), 0f, Vector3.zero).Length > 2))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                movingLeft = true;
            }

            if (movingLeft && leftHitGround != false && Physics2D.BoxCastAll(transform.position + new Vector3(-0.18f, 0, 0), new Vector3(0.05f, 0.6f, 0), 0f, Vector3.zero).Length <= 2 )
            {
                if (leftHitGround.transform.gameObject.CompareTag("Barrier"))
                {
                    transform.Translate(-transform.right * movementSpeed* Time.deltaTime, Space.Self);
                }
            }
            else if(movingLeft && (leftHitGround == false || Physics2D.BoxCastAll(transform.position + new Vector3(-0.18f, 0, 0), new Vector3(0.05f, 0.6f, 0), 0f, Vector3.zero).Length > 2))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                movingLeft = false;
            }


            //do patrol
            //if(/*patrol ended*/)
            {
                patroling = false;
            }

        }
        else if (waitTime <= 0)
        {

            waitTime = Random.Range(2f, 5f);
            print("set wait time as " + waitTime);
        }
        else
        {
            waitTime -= Time.deltaTime;
            print("new wait time is " + waitTime);

        }

    }



}
