using UnityEngine;

public class InvisibleWallEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem windEffect = null;

    private void Start()
    {
        windEffect.enableEmission = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 15)
        {
            windEffect.transform.position = this.transform.position;
            windEffect.enableEmission = true;
        }
    }

   private void OnCollisionExit(Collision collision)
   {
       windEffect.enableEmission = false;
   }


}
