using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//list of properties the object can take like being moved or rotated
public enum Properties
{
    Rotation,
    Transformation,
    Open,
}
//object to make it easier to manage for designers
[Serializable]
public class ObjectWhipProperties
{
    public Properties properties;
    public Vector3 updatedVector3;
    public int time;
    [HideInInspector]
    public int propertyNumber;
}
public class WhippableObject : MonoBehaviour
{

    public ObjectWhipProperties[] itemProperties;
    bool rotate;
    bool transition;
    bool open;
    public bool runProperties;
    List<Vector3> currentVectors = new List<Vector3>();
    int numOfProps;
    private void Start()
    {
        //activate is ran to find all the properties and determine which ones need to be ran and what vectors are used for them
        Activate();
    }
    private void Update()
    {
        //checks to see if the properties should be ran
        if (runProperties)
        {
            //rotation logic of checking which vector is the roatation and how long it will take to rotate
            if (rotate)
            {
                int tempNum = 0;
                int tempTimer = 0;
                if (tempTimer == 0)
                {
                    foreach (ObjectWhipProperties prop in itemProperties)
                    {
                        if (prop.properties == Properties.Rotation)
                        {
                            tempNum = prop.propertyNumber;
                            tempTimer = prop.time;
                        }
                    }
                }
                //lerp so its smooth instead of instant
                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, currentVectors[tempNum], Time.deltaTime / tempTimer));
                
                //checks to see if its close to the final rotation. if it is it will stop it
                if (Vector3.Distance(transform.rotation.eulerAngles, Quaternion.Euler(currentVectors[tempNum]).eulerAngles) <= 2)
                {
                    rotate = false;
                }
            }
            //transformation logic to see which vector it will transform to and which vector is the correct vector
            if (transition)
            {
                int tempNum = 0;
                int tempTimer = 0;
                if(tempTimer == 0)
                {
                    foreach (ObjectWhipProperties prop in itemProperties)
                    {
                        if (prop.properties == Properties.Transformation)
                        {
                            tempNum = prop.propertyNumber;
                            tempTimer = prop.time;
                        }
                    }
                }
                //lerp so it transistions over time instead of being instant
                transform.position = Vector3.Lerp(transform.position, currentVectors[tempNum], Time.deltaTime / tempTimer);
                //if its at the position end it
                if (transform.position == currentVectors[tempNum])
                {
                    transition = false;
                }
            }
            //open is not used rn as im not sure how they want me to use the "Open" function. will ask about that later
            if (open)
            {
                int tempNum = 0;
                int tempTimer = 0;
                if(tempTimer == 0)
                {
                    foreach (ObjectWhipProperties prop in itemProperties)
                    {
                        if (prop.properties == Properties.Open)
                        {
                            tempNum = prop.propertyNumber;
                            tempTimer = prop.time;
                        }
                    }
                }
                open = true;
            }
        }
        
    }
    //function to calculate what functions will be ran and how they will be ran
    public void Activate()
    {
        int startingNumber = 0;
        //goes through all the properties and matches the correct vector with the correct number to be accessed later (all vectors will go to the right property)
        foreach(ObjectWhipProperties item in itemProperties)
        {
            print(item.updatedVector3);
            switch (item.properties)
            {
                case Properties.Rotation:
                    //if its rotation add the vector to the list, set the number up correctly and tell the update to rotate
                    currentVectors.Add(item.updatedVector3);
                    item.propertyNumber = startingNumber;
                    rotate = true;
                    break;
                case Properties.Transformation:
                    //if its transformation add the vector to the list, set the number up correctly and tell the update to transform
                    currentVectors.Add(item.updatedVector3);
                    item.propertyNumber = startingNumber;
                    transition = true;
                    break;
                case Properties.Open:
                    //if its open add the vector to the list, set the number up correctly and tell the update to open
                    currentVectors.Add(item.updatedVector3);
                    item.propertyNumber = startingNumber;
                    open = true;
                    break;
                default:
                    break;
            }
            startingNumber++;
        }
    }
}
