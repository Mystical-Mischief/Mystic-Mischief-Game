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

        timeBetweenShots = startTimeBetweenShots;
    }

    new void Update()
    {
        base.Update();

        if (spottedPlayer == true)
        {
            if (timeBetweenShots <= 0)
            {
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
