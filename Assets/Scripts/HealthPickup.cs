using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private PlayerController _playerController;
    
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _playerController = other.GetComponent<PlayerController>();
            if (_playerController.currentHealth < _playerController.maxHealth)
            {
                _playerController.Heal();
                Destroy(gameObject);
            }
        }
    }
}
