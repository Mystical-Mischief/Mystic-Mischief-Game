using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDProjectrileScript : MonoBehaviour
{
    public GameObject particles;
    public int TimeToDestroy = 2;
    private GameObject Player;
    public float speed;
    public bool canDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(5));
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ExecuteAfterTime2(0.3f));

    }
    void Update()
    {
        Vector3 targetDirection = Player.transform.position - transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
    }

        IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject, TimeToDestroy);
    }
            IEnumerator ExecuteAfterTime2(float time2)
    {
        yield return new WaitForSeconds(time2);
        // Instantiate(particles, transform.position, transform.rotation);
        canDestroy = true;
    }

        private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Player")
        {  
            // other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject);
        }
        if (other.gameObject.tag != "Player" && canDestroy == true)
        {  
            // other.gameObject.GetComponent<ThirdPersonController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
