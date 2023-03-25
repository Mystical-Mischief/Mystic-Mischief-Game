using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class AllCamraPoints
{
    public List<Transform> cameraPoint = new List<Transform>();
}

public class CameraPan : MonoBehaviour
{
    public float Speed;
    public List<Transform> activeCameraPoints = new List<Transform>();
    public List<AllCamraPoints> cameraPointss = new List<AllCamraPoints>();
    public bool Pan;
    public bool finished;
    public Transform target;
    public int patrolNum;
    public bool atDestination;
    public float lastPosition;
    public int listNum;
    public CameraLogic cLogicScript;
    public GameObject camgameobject;
    public int lastWaypoints;
    private GameObject Player;
    private bool done;
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        // pMenu = GameObject.Find("PauseMenu");
        Player = GameObject.FindGameObjectWithTag("Player");
        //Sets a new set of waypoints.
        NewPath();
        if (Pan == true)
        {
            transform.position = cameraPointss[listNum].cameraPoint[0].position;
        }
        if (target == null)
        {
            target = activeCameraPoints[0].transform;
            // UpdateDestination(target.position);
        }
        lastWaypoints = (cameraPointss.Count - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pan == true && pauseMenu.GetComponent<PauseMenu>().Paused == false)
        {
            camgameobject.SetActive(false);
            cLogicScript.enabled = false;
            Player.GetComponent<PlayerController>().canMove = false;
            PanCamera();
        }
        if (Pan == false && done == false)
        {
            camgameobject.SetActive(true);
            Player.GetComponent<PlayerController>().canMove = true;
            cLogicScript.enabled = true;
            done = true;
        }
        if (finished && listNum != lastWaypoints)
        {
            print("points cleared");
            listNum = listNum + 1;
            NewPath();
            transform.position = Vector3.MoveTowards(transform.position, activeCameraPoints[0].position, Speed * Time.deltaTime);
            lastPosition = activeCameraPoints.Count - 1;
            patrolNum = 0;
            target = activeCameraPoints[patrolNum];
            finished = false;
        }
        if (finished && listNum >= lastWaypoints)
        {
            Invoke(nameof(StopPanning), 3f);
        }
    }

    public void PanCamera()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        float timeCount = 5.0f;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        // transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, timeCount * Speed);
        if (dist < 1f && atDestination == false)
        {
            atDestination = true;
            //if the player isnt at the last point
            if (patrolNum <=  activeCameraPoints.Count - 1)
            {
                patrolNum++;
                print(patrolNum);
                target = activeCameraPoints[patrolNum];
            }
            //if the player is at the last point go to the first one
            if(patrolNum > activeCameraPoints.Count - 1)
            {
                // patrolNum = 0;
                finished = true;
            }
            if (target != null)
            {
                // atDestination = false;
                // transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
            }
        }
        else
        {
            atDestination = false;
        }
    }

    public void StopPanning()
    {
        Pan = false;
    }

    public void NewPath()
    {
        activeCameraPoints.Clear();
        activeCameraPoints.AddRange(cameraPointss[listNum].cameraPoint);
        transform.rotation = activeCameraPoints[0].rotation;
        transform.position = activeCameraPoints[0].position;
    }
}
