using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EasyUtils
{
    public class itemdrop : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDrop = null;
        public GameObject Player;

        public void Drop()
        {
            ///SetActive(true);
            transform.parent = null;
            
            onDrop?.Invoke();
        }

        void Update()
        {
             transform.position = Player.transform.position;
        }
    }
}

