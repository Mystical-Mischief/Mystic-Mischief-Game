using UnityEngine;
using UnityEngine.AI;
public class SpiderAI : BaseEnemyAI
{
    PlayerController player;
    public GameObject projectile;
    public Animator anim;
    public Vector3 webOffset;
    public bool PlayerCanMove;
    float timeBetweenShots;
    public float startTimeBetweenShots;
    public float wanderRange;
    private Transform stunLocation;
    private float Speed;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Essentially starts a cooldown for how often the spider can fire its web projectile
        timeBetweenShots = startTimeBetweenShots;
        PlayerCanMove = player.canMove;
        Speed = ai.speed;
    }

    new void Update()
    {
           
        // if(!stunned)
        // {

            base.Update();
            PlayerCanMove = player.canMove;

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
        // }
        if (base.stunned == true)
        {
            anim.SetBool("Hurt", true);
            // Stun(timer);
            // timer -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("Hurt", false);
            stunLocation = transform;
        }
    }

        //stun function to be used to stun enemies for a specified amount of time
    public override void Stun(float time)
    {
        if(stunned && time > 0)
        {
            ps.Play(true);
            Debug.Log("Stunned");
            // Transform stunnedPos = base.ps.transform;
            targetPosition = stunLocation.position;
            base.ai.speed = 0;
            UpdateDestination(targetPosition);
        }
        else
        {
            base.ai.speed = Speed;
            timer = stunTime;
            stunned = false;
        }
    }
    public override void Patrol()
    {
        if (!stunned)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
            {
                atDestination = true;
                Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
                Vector3 finalPosition = hit.position;
                UpdateDestination(finalPosition);
            }
            else
            {
                atDestination = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Poop")
        {
            stunned = true;
        }
    }
    
    public virtual void ShootAnim()
    {
        anim.SetTrigger("Web");
        Instantiate(projectile, transform.position + webOffset, Quaternion.identity);
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

   

}
