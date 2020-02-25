﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyScript : GenericEnemy
{
    #region Variables and Shit
    float followtime = 1.5f;
    float waitTime = -1;
    float shootingtime = 0.2f;
    public bool patrolling = true;
    bool movingLeft = false;
    int patrolsDone = 0;
    bool shooting = true;
    RaycastHit2D leftHitGround;
    RaycastHit2D rightHitGround;

    public string gunType;
#endregion

#region Start & Update
void Start()
{
    //assigns a variables for the grunts stats
    myArmour = 1;
    myHealth = 15;
    mySpeed = 1;
    myType = EnemyType.Heavy;

    //checks the gun type that is set in the inspector and assigns the enemies gun depending on that
    if (gunType == "Pistol")
    {
        //give the players game object the weapon script and sets it as weapon
        gameObject.AddComponent<EnemyPistol>();
        myWeapon = gameObject.GetComponent<EnemyPistol>();
    }
    if (gunType == "Rifle")
    {
        //give the players game object the weapon script and sets it as weapon
        gameObject.AddComponent<EnemyRifle>();
        myWeapon = gameObject.GetComponent<EnemyRifle>();
    }
    if (gunType == "LMG")
    {
        //give the players game object the weapon script and sets it as weapon
        gameObject.AddComponent<EnemyLMG>();
        myWeapon = gameObject.GetComponent<EnemyLMG>();
    }
}

void Update()
{
    //starts the enemies basic loop

    // if the enemy has spotted a target it goes into the attack/chase loop
    if (enemySpotted)
    {
        // the player constantly goes between following the target and shooting at them until the enemy is to close to the target
        //if the follow timer is out then the attack function runs
        if (followtime <= 0)
        {
            Attack();
        }
        //iif the follow timer is still above 0 then the follow function will run
        else
        {
            Follow();
        }
    }
    else
    {
        //if the enemy has not been spotted it runs the patrol/idle function
        PatrolOrIdle();
    }
}
#endregion

#region OnTriggers
//the triggers are used to make detect if the player is in range and to set the player as the shooting target
void OnTriggerEnter2D(Collider2D collision)
{
    GameObject GO = collision.gameObject;
    //checks if the object entering the trigger is the player
    if (GO.CompareTag("Player"))
    {
        //assigns the variables related to having spotted the players
        enemySpotted = true;
        target = GO;
        //sets variables so if the player leaves the range the enemy goes on a new patrol immediatly after
        patrolling = true;
        waitTime = -1;
        CheckFacing();
    }
}
void OnTriggerExit2D(Collider2D collision)
{
    GameObject GO = collision.gameObject;
    //checks if the object exiting the trigger is the player
    if (GO.CompareTag("Player"))
    {
        //assigns the variables related to having no longer got vision of the player
        enemySpotted = false;
        target = null;
        CheckFacing();
    }
}
#endregion

#region Basic Functions
// the attack function is what is called whenever the bot is attacking the player
public override void Attack()
{
    //for attacking it works off of the idea of shoot time, this allows the attacking phase to be broken in to two segments. The Pause and the attack. this gives the player something to react to as the pause, which is always the same length, always happens before they shoot.
    //with this if the enemy is still in the attacking phase and not ready to move on (the shooting time < 1.5 seconds) this if statement runs
    if (shootingtime < 1.5f)
    {
        //when the state runs it increase the shooting time variable by deltatime to ensure it progresses through the stage
        shootingtime += Time.deltaTime;
        //if the shooting time is over 1 second and it hasnt started the shooting phase it passes the if below and calls an attack from the enemies weapon
        if (shootingtime > 1 && shooting)
        {
            // sets the variable keeping track of shooting started to false to avoid the weapon doing its shoot function multiple times
            shooting = false;
            shoot();
        }
    }
    // once the shoot time is over 1.5 seconds it resets the variables so that the enemy will enter the follow track and be prepared for next shoot cycle
    else
    {
        followtime = 3;
        shootingtime = 0;
        shooting = true;
    }
}
//the follow is what allows the enemy to "chase" the player
public override void Follow()
{
    //initially the direction of the enemy is checked to ensure that it is facing the player before it begins moving towards them
    CheckFacing();
    //Next it checks that the bot can move left, that the player is to the left and that the player is not in the minimum range
    if (CheckWalk("Left") && transform.position.x > target.transform.position.x && (target.transform.position.x - transform.position.x) < -1.5f)
    {
        //the enemy will move left assuming that the above was correct via translate at the speed of its global speed
        transform.Translate(-transform.right * mySpeed * Time.deltaTime, Space.Self);
    }
    //checks that the bot can move right, that the player is to the right and that the player is not in the minimum range
    else if (CheckWalk("Right") && transform.position.x < target.transform.position.x && (target.transform.position.x - transform.position.x) > 1.5f)
    {
        //the enemy will move right assuming that the above was correct via translate at the speed of its global speed
        transform.Translate(transform.right * mySpeed * Time.deltaTime, Space.Self);
    }
    // the else exists as a catch all in the case that the bot cant or doesnt need to move and puts the bot into into an attack phase 
    else
    {
        Attack();
    }
    //this gradurally decreases everytime the enemy follows the player even if it does an attack from the follow function. THis is used to the main update function to decide if the enemy is going to do an attack while the enemy is chasing the player
    followtime -= Time.deltaTime;
}
//the patrol or idle fucntion dictates what the bot does while the player has not been spotted by the bot
public override void PatrolOrIdle()
{
    //Checks if the enemy is set to patrol and is no longer waiting to begin the patrol
    if (patrolling && waitTime < 0)
    {
        #region moving right
        //checks if the enemy is moving right and if it is possible to move right
        if (!movingLeft && CheckWalk("Right"))
        {
            //checks that the object detected beneath the bot is infact able to be walked on
            if (rightHitGround.transform.gameObject.CompareTag("Barrier"))
            {
                //the enemy will move right via translate at the speed of its global speed
                transform.Translate(transform.right * mySpeed * Time.deltaTime, Space.Self);
                print("Moving Right");
            }
        }
        // if the above was false but it is still moving right but one of the checks is false this runs
        else if (!movingLeft && (rightHitGround == false || Physics2D.BoxCastAll(transform.position + new Vector3(0.18f, 0, 0), new Vector3(0.05f, 0.55f, 0), 0f, Vector3.zero).Length > 2))
        {
            print("turning to go left");
            // this changes the way that the bot faces and allows them to turn and face while also adding to the patrol end counter
            transform.rotation = Quaternion.Euler(0, 0, 0);
            movingLeft = true;
            patrolsDone++;
        }
        #endregion

        #region moving left
        //checks if the enemy is moving left and if it is possible to move left
        if (movingLeft && CheckWalk("Left"))
        {
            //checks that the object detected beneath the bot is infact able to be walked on
            if (leftHitGround.transform.gameObject.CompareTag("Barrier"))
            {
                //the enemy will move left via translate at the speed of its global speed
                transform.Translate(-transform.right * mySpeed * Time.deltaTime, Space.Self);
                print("Moving Left");
            }
        }
        // if the above was false but it is still moving right but one of the checks is false this runs
        else if (movingLeft && (leftHitGround == false || Physics2D.BoxCastAll(transform.position + new Vector3(-0.18f, 0, 0), new Vector3(0.05f, 0.6f, 0), 0f, Vector3.zero).Length > 2))
        {
            print("turning to go right");
            // this changes the way that the bot faces and allows them to turn and face while also adding to the patrol end counter
            transform.rotation = Quaternion.Euler(0, 180, 0);
            movingLeft = false;
            patrolsDone++;
        }
        #endregion

        #region patrol end
        // if the enemy has done 2 laps of their patrol area this runs
        if (patrolsDone == 2)
        {
            //moves patrolsdone off 2 so this doesnt run again
            patrolsDone++;
            //invokes the patrolling over function after a random amount of time
            Invoke("PatrollingOver", Random.Range(2f, 4.5f));
        }

        //IMPORTANT do not delete even though is says it isnt uses
        //ends patrol so that the enemy goes back into an idle/wait phase
        void PatrollingOver()
        {
            //resets the patrolling variables
            patrolling = false;
            patrolsDone = 0;
        }
        #endregion
    }
    //if the enemy had a no wait time and patrolling false it puts the bot into a new idle/wait
    else if (waitTime <= 0)
    {
        //sets a random wait time
        waitTime = Random.Range(4f, 10f);
    }
    //runs when the enemyis waiting
    else
    {
        //decrease wait time
        waitTime -= Time.deltaTime;
        //runs when the wait time is over 
        if (waitTime < 0)
        {
            //sets patrolling true to get enemy patrolling again
            patrolling = true;
        }

    }

}
#endregion

#region OtherFunctions
//checks that the enemy is facing the right direction
void CheckFacing()
{
    //checks if a target is selected
    if (target != null)
    {
        //checks if the target is to the right
        if (target.transform.position.x - transform.position.x > 0)
        {
            //target right variables set
            transform.rotation = Quaternion.Euler(0, 180, 0);
            movingLeft = false;
        }
        //since target is not right then assumed left
        else
        {
            //target left variables set
            transform.rotation = Quaternion.Euler(0, 0, 0);
            movingLeft = true;
        }
    }
}

//works to shoot the enemies gun via the attached gun script
void shoot()
{
    //calls weapons shoot funtion
    myWeapon.StartShoot();
}

//Works to check if the ground can be walked on/to by the enemy. is given the direction as either "Right" or "Left" and return a bool if the terrain is naviable
bool CheckWalk(string direction)
{
    //returns false be default
    bool returnThis = false;

    //if told to check right
    if (direction == "Right")
    {
        //raycasts down for floor info
        rightHitGround = Physics2D.Raycast(transform.position + new Vector3(0.15f, -0.31f, 0), Vector3.down, 0.2f);
        //returns a bool based on if there is ground beneath is and if there nothing infront
        returnThis = rightHitGround != false && Physics2D.BoxCastAll(transform.position + new Vector3(0.18f, 0, 0), new Vector3(0.05f, 0.55f, 0), 0f, Vector3.zero).Length <= 2;
    }
    //if told to check right
    else if (direction == "Left")
    {
        //raycasts down for floor info
        leftHitGround = Physics2D.Raycast(transform.position + new Vector3(-0.15f, -0.31f, 0), Vector3.down, 0.2f);
        //returns a bool based on if there is ground beneath is and if there nothing infront
        returnThis = leftHitGround != false && Physics2D.BoxCastAll(transform.position + new Vector3(-0.18f, 0, 0), new Vector3(0.05f, 0.6f, 0), 0f, Vector3.zero).Length <= 2;
    }
    //in the case a valid dirrection was not good
    else
    {
        //debugs and returns false
        print("The direction given for " + gameObject.name + "was incorrect");
        return false;
    }
    //returns set value
    return returnThis;
}
    #endregion
}
