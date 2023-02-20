using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkthroughWalls : MonoBehaviour
{
    [SerializeField]
    private InvisibilityHat InvisibilityHat;
    private BoxCollider _wallCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if(InvisibilityHat != null && InvisibilityHat.getLevel()>2 && InvisibilityHat.IsInvisible())
        {
            if(collision.gameObject.CompareTag("wall"))
            {
                _wallCollider = collision.gameObject.GetComponent<BoxCollider>();
                _wallCollider.enabled = false;
                StartCoroutine(ActivateWallCollider(_wallCollider, .3f));
            }
        }
    }
    IEnumerator ActivateWallCollider(BoxCollider coll, float time)
    {
        if(coll != null)
        {
            yield return new WaitForSeconds(time);
            coll.enabled = true;
        }
    }
}
