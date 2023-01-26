using System;

using UnityEngine;

[Serializable]
public class Face
{
    public Texture EmotionTexture;
    public Emotion emotion;
}
public enum Emotion
{
    Neutral,
    Happy,
    Sad,
    Angry,
    Blink,
    Shock,
    Greed
}
public class FaceChanger : MonoBehaviour
{
    public Face[] allFaces;
    public Emotion currentEmotion;
    
    private Texture currentTexture;

    public Material EmotionMaterial;


    private void Start()
    {
        findNewFace();
    }
    private void Update() //Could be turned into event, does not need to check the face every frame. 
    {
            findNewFace();
    }
    void findNewFace() //Finds a face that has the correct emotion, then changes the texture of the Material that shows the emotion. 
    {
        foreach (Face face in allFaces)
        {
            if (currentEmotion == face.emotion)
            {            
                currentTexture = face.EmotionTexture;
                EmotionMaterial.SetTexture("_MainTex", currentTexture);
                return;
            }
        }
    }
}
