using UnityEngine;

public class SpiderAI : BaseEnemyAI
{
    ThirdPersonController player;
    public GameObject projectile;
    public Animator anim;

    public bool PlayerCanMove;
    float timeBetweenShots;
    public float startTimeBetweenShots;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();

        //Essentially starts a cooldown for how often the spider can fire its web projectile
        timeBetweenShots = startTimeBetweenShots;
        PlayerCanMove = player.canMove;
    }

    new void Update()
    {
        PlayerCanMove = player.canMove;
        if (target == Player && PlayerCanMove)
        {
            Player.GetComponent<ThirdPersonController>().Targeted = true;
        }
        
        // if (Saved = false && Player.GetComponent<ThirdPersonController>().Saved == true)
        // {
        //     SaveEnemy();
        //     Saved = true;
        // }
        // if (Player.GetComponent<ThirdPersonController>().Loaded == true)
        // {
        //     Saved = false;
        //     LoadEnemy();
        // }
        //if the ai doesnt see the player then it will patrol and look for it
        if (!spottedPlayer)
        {
            EnemyDetection();
            Patrol();
        }
        //if the ai finds the player it will do what it does when it sees the player
        else
        {
            FoundPlayer();
        }

        if (!PlayerCanMove)
        {
            LostPlayer();
            spottedPlayer = false;
        }
        if (spottedPlayer == true && PlayerCanMove)
        {
            if (timeBetweenShots <= 0)
            {
                //fires projectile at the location the player was located at when the spider first shot. It does not follow player
                ShootAnim();
                if (!PlayerCanMove)
                {
                    LostPlayer();
                    spottedPlayer = false;
                }
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
                if (!PlayerCanMove)
                {
                    LostPlayer();
                    spottedPlayer = false;
                }
            }
        }
    }

    public virtual void ShootAnim()
    {
        anim.SetTrigger("Web");
        Instantiate(projectile, transform.position, Quaternion.identity);
        timeBetweenShots = startTimeBetweenShots;
    }

    //public virtual void FoundPlayer(GameObject player)
    //{
    //    base.FoundPlayer();

    //    if (player.GetComponent<Web>().isStun == true)
    //    {
    //        LostPlayer();
    //    }
    //}

    public override void EnemyDetection()
    {
        if(PlayerCanMove)
        {
            EnemyVision.FieldOfView();
            spottedPlayer = EnemyVision.PlayerDetected; //checks if the ai saw the player
            if (spottedPlayer)
            {
                target = EnemyVision.LocationTarget; //the ai would move towards the player, and the target would be also the current player location
            }
        }
        else
        {
            LostPlayer();
        }
    }
    public override void FoundPlayer()
    {
        if(PlayerCanMove)
        {
            EnemyVision.FieldOfView();
            spottedPlayer = EnemyVision.PlayerDetected;
            if (spottedPlayer)
            {
                UpdateDestination(target.position);
                //checks if the ai can see the player
                EnemyVision.FieldOfView();
                spottedPlayer = EnemyVision.PlayerDetected;
                //This would happen if the player goes out of range while the ai was targeting the player
                if (!spottedPlayer)
                {
                    LostPlayer();
                }
            }
            else
            {
                LostPlayer();
            }
        }
        else
        {
            LostPlayer();
        }
    }
    public override void UpdateDestination(Vector3 newDestination)
    {
        if(PlayerCanMove && target != null)
        {
            ai.destination = newDestination;
        }
        else
        {
            LostPlayer();
            ai.destination = newDestination;
        }
    }
}
