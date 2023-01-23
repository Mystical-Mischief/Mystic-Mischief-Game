using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Face
{
    public Material EmotionMaterial;
    public Emotion emotion;
}
public enum Emotion
{
    Neutral,
    Happy,
    Sad,
    Angry
}
public class FaceChanger : MonoBehaviour
{
    public Face[] allFaces;
    public Face currentFace;
    public Emotion currentEmotion;
    private void Start()
    {
        findNewFace();
    }
    private void Update()
    {
        if(currentEmotion != currentFace.emotion)
        {
            findNewFace();
        }
    }
    void findNewFace() 
    {
        foreach (Face face in allFaces)
        {
            if (currentEmotion == face.emotion)
            {
                GetComponent<MeshRenderer>().material = face.EmotionMaterial;
                currentFace = face;
                return;
            }
        }
    }
}
