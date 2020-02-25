using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    float[] mortarXs = new float[10];
    public Vector3 bossAreaStart;
    public Vector3 bossAreaPlayerLimit;
    public Vector3 bossAreaEnd;
    public GameObject mortarShot;
    public GameObject projectile;

    float playerWidth = 0.6f;



    bool areaEntered = false;
    bool immune = true;
    bool initiating = true;

    bool MortarBarrageStarted = false;


    int shotArc = 30;
    int numberOfShots = 35;
    float timeBetweenShots = 0.05f;
    float bulletSpeed = 100f;
    Vector3 startDisplacement;

    float initiatingTime = 4;
    float attackWaitTime = 0;
    int myHealth = 1000;
    int myArmour = 1;
    int stage = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                //idling waiting for player
                break;
            case 1:
                StageOne();
                break;
            case 2:
                StageTwo();
                break;
            case 3:
                StageThree();
                break;
            case 4:
                StageFour();
                break;
            case 5:
                DeathStage();
                break;
        }
    }

    public void DamageBoss(int damage)
    {

        int modifiedDamage = damage - myArmour;
        if (modifiedDamage < 0)
        {
            modifiedDamage = 0;
            return;
        }
        if (!immune)
        {
            myHealth -= modifiedDamage;
        }
        if (myHealth < 0)
        {
            stage = 5;
            initiating = true;
            immune = true;
        }
        else if(myHealth < 250)
        {
            stage = 4;
            initiating = true;
            immune = true;
        }
        else if(myHealth < 500)
        {
            stage = 3;
            initiating = true;
            immune = true;
        }
        else if(myHealth < 750)
        {
            stage = 2;
            initiating = true;
            immune = true;
        }
    }

    #region Stages
    void StageOne()
    {
        if (initiating)
        {
            initiatingTime -= Time.deltaTime;
            if(initiatingTime < 0)
            {
                initiating = false;
                immune = false;
                initiatingTime = 4;
            }
        }
        else
        {
            if(attackWaitTime < 0)
            {
                Mortar();
                attackWaitTime = 5;
            }
            else
            {
                attackWaitTime -= Time.deltaTime;
            }
        }
    }
    void StageTwo()
    {
        if (initiating)
        {
            initiatingTime -= Time.deltaTime;
            if (initiatingTime < 0)
            {
                initiating = false;
                immune = false;
                initiatingTime = 5;
            }
        }
        else
        {
            if (attackWaitTime < 0)
            {
                MiniGun();
                attackWaitTime = 6;
            }
            else
            {
                attackWaitTime -= Time.deltaTime;
            }
        }
    }
    void StageThree()
    {
        if (initiating)
        {
            initiatingTime -= Time.deltaTime;
            if (initiatingTime < 0)
            {
                initiating = false;
                immune = false;
                initiatingTime = 5;
            }
        }
        else
        {
            if (attackWaitTime < 0)
            {
                Laser();
                attackWaitTime = 8;
            }
            else
            {
                attackWaitTime -= Time.deltaTime;
            }
        }
    }
    void StageFour()
    {
        if (initiating)
        {
            initiatingTime -= Time.deltaTime;
            if (initiatingTime < 0)
            {
                initiating = false;
                immune = false;
                initiatingTime = 5;
            }
        }
        else
        {
            if (attackWaitTime < 0)
            {
                Missles();
                attackWaitTime = 10;
            }
            else
            {
                attackWaitTime -= Time.deltaTime;
            }
        }
    }
    void DeathStage()
    {

    }
    #endregion

    #region Attacks

    void Mortar()
    {
        //set a safe space for player to be able to go and DEFINETLY not be hit
        float positionRange = bossAreaPlayerLimit.x - bossAreaStart.x - 2 * playerWidth;
        if (positionRange < 0)
        {
            print("Error expected as boss zone is too small");
        }
        float safeZoneX = Random.Range(0f, positionRange);
        for (int mm = 0; mm <= 9; mm++)
        {
            print(mm);
            if (mm < 0)
            {
                mm = 0;
            }
            float potentialLandingLocation = Random.Range(bossAreaStart.x, bossAreaPlayerLimit.x);
            if (potentialLandingLocation > safeZoneX - playerWidth && potentialLandingLocation < safeZoneX + playerWidth)
            {
                mm--;
            }
            else
            {

                mortarXs[mm] = potentialLandingLocation;
            }
        }
        MortarBarrageStarted = true;
        print(MortarBarrageStarted + " " + attackWaitTime);
        print("creating shots");

        for (int mm = 0; mm <= 9; mm++)
        {
            Instantiate<GameObject>(mortarShot, new Vector3(mortarXs[mm], 0, 0), Quaternion.identity);
        }

        //shoot dumby shots
        //put hit area in those areas
        //make true shots fall from the sky
    }

    void MiniGun()
    {
        startDisplacement = -gameObject.transform.right * 0.15f;
        for (int ss = 0; ss < numberOfShots; ss++)
        {
            Invoke("CreateBullet", timeBetweenShots * ss);
        }
    }

    void Laser()
    {
        //gets a random angle from range
        //shoots a raycast out for laser
        //gets distance and makes lase thing out of parts to the point
        //gradually changes angle giving a scanning laser effect
        //does laser things
    }
    void Missles()
    {
        //does missile things
    }



    #endregion


    void CreateBullet()
    {
        // determine rotation of bullet and the direction
        GameObject bullet = Instantiate(projectile, (gameObject.transform.position + startDisplacement), gameObject.transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(-shotArc / 2, shotArc / 2))));
        bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.right * bulletSpeed);
    }


}
