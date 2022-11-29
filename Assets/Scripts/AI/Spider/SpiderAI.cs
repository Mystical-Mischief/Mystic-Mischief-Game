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
        base.Update();
        PlayerCanMove = player.canMove;
        if (!PlayerCanMove)
        {
            LostPlayer();
        }
        if (spottedPlayer == true && PlayerCanMove)
        {
            if (timeBetweenShots <= 0)
            {
                //fires projectile at the location the player was located at when the spider first shot. It does not follow player
                ShootAnim();
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
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
