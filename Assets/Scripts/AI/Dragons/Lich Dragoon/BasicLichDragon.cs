using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLichDragon : MonoBehaviour
{
    public List<GameObject> teleportPoints = new List<GameObject>();
    public List<Transform> teleportPoints2 = new List<Transform>();
    public bool Teleport;
    public GameObject tpDestination;
    public float Speed;
    public GameObject Player;
    public Rigidbody  projectile;
    public bool attacked;
    public float resetAttackTime;
    public float projectileSpeed;
    public float meleeDist;
    public float attackTimes;
    public float chaseTime;
    public Transform WarpLocation;
    public bool ChasingPlayer;
    public float maxAttackTimes;
    public bool CanAttack;
    private float startSpeed;
    public bool closeToPlayer;
    public float timeUntilChase;
    public float resetChaseTime;
    public int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        // startPosition = teleportPoints[3].transform;
        startSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        if(closeToPlayer == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        }

        if(dist <= meleeDist)
        {
            closeToPlayer = true;
        }
        else
        {
            closeToPlayer = false;
        }

        if (Teleport == true)
        {
            WarpToNewLocation();
        }

        if (attacked == false && CanAttack == true)
        {
            Ranged();
        }
        // else
        // {
        //     Speed = startSpeed;
        // }
        // if (dist <= meleeDist)
        // {
        //     CanAttack = false;
        //     Melee();
        // }
        if (attackTimes >= maxAttackTimes)
        {
            ChasingPlayer = true;
            CanAttack = false;
            Invoke(nameof(ChasePlayer), timeUntilChase);
        }
        // Transform.LookAt(Player.transform.position);
    }

    public void WarpToNewLocation()
    {
        Debug.Log("Teleporting");
        foreach(GameObject tpPoint in teleportPoints)
        {
            if (tpPoint.GetComponent<TeleportPoints>().Colliding == false)
            {
                tpDestination = tpPoint;
                break;
            }
        }
        transform.position = tpDestination.transform.position;
        Teleport = false;
    }

    public void WarpAfterAttack()
    {
        Debug.Log("Teleporting");
        WarpLocation = teleportPoints2[randomNumber];
        transform.position = WarpLocation.position;
        Teleport = false;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") && ChasingPlayer == false)
        {
        Teleport = true;
        }
        if (other.gameObject.CompareTag("Player") && ChasingPlayer == true)
        {
            Debug.Log("HitPlayer");
            ResetAttackChase();
        }
    }

        void Ranged()
    {
        // anim.SetTrigger("Spit");
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, Player.transform.rotation);
        // Speed = 0;
        //projectile.LookAt(Player.transform);

        clone.velocity = (Player.transform.position - clone.position).normalized * projectileSpeed;
        Invoke(nameof(ResetAttack), resetAttackTime);
        attacked = true;
        attackTimes++;
    }

    public void Melee()
    {

    }

        void ChasePlayer()
    {
        if (ChasingPlayer == true)
        {
        int LayerPlayerOnly = LayerMask.NameToLayer("PlayerOnly");
        gameObject.layer = LayerPlayerOnly;
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        // Speed = startSpeed;
        // Invoke(nameof(ResetAttack2), chaseTime); 
        }
    }

    //This is for reseting the melee attack.
    void ResetAttackChase()
    {
        Invoke(nameof(ResetAttackRanged), resetChaseTime);
        int LayerDragon = LayerMask.NameToLayer("Dragon");
        gameObject.layer = LayerDragon;
        NewRandomNumber();
        // transform.position = startPosition.position;
        WarpAfterAttack();
        attackTimes = 0;
        ChasingPlayer = false;
    }
    
    public void ResetAttackRanged()
    {
        CanAttack = true;
    }

    public void ResetAttack()
    {
        attacked = false;
        // Speed = startSpeed;
    }

    int lastNumber;
    //This is the random number generator. This is used for selecting patrol points at random.
    public virtual void NewRandomNumber()
    {
        randomNumber = UnityEngine.Random.Range(1, 3);
        if (randomNumber == lastNumber)
        {
            randomNumber = UnityEngine.Random.Range(1, 3);
        }
        lastNumber = randomNumber;
    }
}
