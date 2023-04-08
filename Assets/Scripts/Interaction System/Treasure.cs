using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] allAudioClips;
    public GameObject[] treasures;
    public static float treasureValue;
    private int currentSize;
    public bool add;
    // Start is called before the first frame update
    void Awake()
    {
        currentSize =(int)treasureValue/treasures.Length;
        treasures[currentSize].SetActive(true);
    }
    private void Update()
    {
        if (add)
        {
            //AddValue(1);
            if((int)treasureValue / treasures.Length<treasures.Length)
            {
                currentSize = (int)treasureValue / treasures.Length;
            }
            else
            {
                currentSize = treasures.Length - 1;
            }
            add = false;
        }
    }
    public static void AddValue(float value)
    {
        treasureValue+=(value);
    }
    public static void SetValue(float value)
    {
        treasureValue = (value);
    }
    public void PlaySound()
    {
        if(currentSize < 5)
        {
            audioSource.PlayOneShot(allAudioClips[currentSize]);
        }
        else
        {
            audioSource.PlayOneShot(allAudioClips[treasures.Length - 1]);
        }
        print(currentSize);
    }
}
