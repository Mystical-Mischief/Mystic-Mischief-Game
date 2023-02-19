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
            windEffect.transform.localPosition = this.transform.position;
            windEffect.transform.LookAt(this.transform);
            windEffect.enableEmission = true;
        }
    }

   private void OnCollisionExit(Collision collision)
   {
       windEffect.enableEmission = false;
   }


}
