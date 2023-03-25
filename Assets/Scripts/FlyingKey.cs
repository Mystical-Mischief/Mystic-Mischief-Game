using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingKey : MonoBehaviour
{
    [SerializeField]
    private GameObject[] FlyingPositions;
    [SerializeField]
    private float _speed = 5;
    private Transform _target;
    private int _index;
    private bool _canMove;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _target = FlyingPositions[_index].transform;
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove) //this prevents the key from flying away if it gets picked up
        {
            _target = FlyingPositions[_index].transform;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); //makes the key towards a specific location
            if (Vector3.Distance(transform.position, _target.position) < 0.5) //if the key is close to the targeted location
            {
                _index++; //increase index
                if (_index == FlyingPositions.Length) // this helps reset the key flying positions
                {
                    _index = 0;
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag=="PickUp")
        {
            StartCoroutine(DestroyKey());
        }
    }

    private IEnumerator DestroyKey()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
