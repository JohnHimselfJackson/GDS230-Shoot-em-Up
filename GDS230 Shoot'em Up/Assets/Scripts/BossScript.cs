using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    bool areaEntered = false;
    bool immune = true;
    bool initiating = true;
    float initiatingTime = 4;
    float attackWaitTime = 0;
    int myHealth = 1000;
    int myArmour = 1;
    int stage = 0;

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
                initiatingTime = 5;
            }
        }
        else
        {
            if(attackWaitTime < 0)
            {
                Mortar();
                attackWaitTime = 10;
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
                attackWaitTime = 10;
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
                attackWaitTime = 10;
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
        //put mortar shots randomly in not that area
        //shoot dumby shots
        //put hit area in those areas
        //make true shots fall from the sky
        //on impact replace with explosions
    }

    void MiniGun()
    {
        //does mortar things
    }

    void Laser()
    {
        //does mortar things
    }
    void Missles()
    {
        //does mortar things
    }



    #endregion




}
