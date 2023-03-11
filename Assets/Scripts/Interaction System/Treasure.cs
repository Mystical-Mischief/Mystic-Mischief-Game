using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject[] treasures;
    public static float treasureValue;
    private int currentSize;
    // Start is called before the first frame update
    void Awake()
    {
        currentSize =(int)treasureValue/treasures.Length;
        treasures[currentSize].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void AddValue(float value)
    {
        treasureValue+=(value);
    }
    public static void SetValue(float value)
    {
        treasureValue = (value);
    }
}
