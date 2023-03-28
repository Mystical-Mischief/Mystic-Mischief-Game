using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingRat : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Positions;
    [SerializeField]
    private float _speed = 5;
    private Transform _target;
    private int _index;
   
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _target = Positions[_index].transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _target = Positions[_index].transform;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); //makes the key towards a specific location
        if (Vector3.Distance(transform.position, _target.position) < 0.5) //if the key is close to the targeted location
        {
            _index++; //increase index
            if (_index == Positions.Length) // this helps reset the key flying positions
            {
                _index = 0;
            }
        }
        
    }

}
