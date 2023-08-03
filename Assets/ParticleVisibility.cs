using UnityEngine;

public class ParticleVisibility : MonoBehaviour
{
    ParticleSystem particles; 

    private void Start()
    {
       particles = GetComponent<ParticleSystem>();
       particles.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            particles.Play(); 
        }

    }

   private void OnTriggerExit(Collider other)
   {
       particles.Stop();
   }
}
