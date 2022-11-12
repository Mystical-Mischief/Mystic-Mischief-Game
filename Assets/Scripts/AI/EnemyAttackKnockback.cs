using UnityEngine;

public class EnemyAttackKnockback : MonoBehaviour
{
    [SerializeField]
    private float knockbackForce;

    private void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb != null && collider.gameObject.tag == "Player")
        {
            Vector3 direction = collider.transform.position - transform.position;
            //direction.y = 0;
            rb.AddForce(direction.normalized*knockbackForce,ForceMode.Impulse);
        }
    }
}
