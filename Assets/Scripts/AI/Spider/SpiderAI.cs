using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : BaseEnemyAI
{
    ThirdPersonController player;
    public GameObject projectile;

    float timeBetweenShots;
    public float startTimeBetweenShots;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();

        //Essentially starts a cooldown for how often the spider can fire its web projectile
        timeBetweenShots = startTimeBetweenShots;
    }

    new void Update()
    {
        base.Update();

        if (spottedPlayer == true)
        {
            if (timeBetweenShots <= 0)
            {
                //fires projectile at the location the player was located at when the spider first shot. It does not follow player
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }

    public virtual void FoundPlayer(GameObject player)
    {
        base.FoundPlayer();

        if (player.GetComponent<ThirdPersonController>().canMove == false)
        {
            LostPlayer();
        }
    }
}
