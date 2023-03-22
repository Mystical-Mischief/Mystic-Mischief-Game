using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookDragon : MonoBehaviour
{
    private GameObject _dragon;
    private Transform _dragonTransform;
    private CinemachineTargetGroup TargetGroup;
    // Start is called before the first frame update
    void Start()
    {
        _dragon = GameObject.FindGameObjectWithTag("Dragon");
        if(_dragon != null)
        {
            _dragonTransform =_dragon.GetComponent<Transform>();
            TargetGroup = GetComponent<CinemachineTargetGroup>();
            TargetGroup.m_Targets[1].target = _dragonTransform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Dragon);
    }
}
