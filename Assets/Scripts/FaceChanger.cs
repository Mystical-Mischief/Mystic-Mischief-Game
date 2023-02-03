using System;
using UnityEngine;

//general set up for each face
[Serializable]
public class Face
{
    public Texture EmotionTexture;
    public Emotion emotion;
}
//list of all emotions! can be added upon
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
    private Face currentFace;

    public Material EmotionMaterial;

    //finds face on start
    private void Start()
    {
        findNewFace();
    }
                          //Made a slightly better if statement - Corey
    private void Update() //Could be turned into event, does not need to check the face every frame. 
    {
        if(currentEmotion != currentFace.emotion)
        {
            findNewFace();
        }
        
    }
    void findNewFace() //Finds a face that has the correct emotion, then changes the texture of the Material that shows the emotion. 
    {
        foreach (Face face in allFaces)
        {
            if (currentEmotion == face.emotion)
            {
                currentFace = face;
                currentTexture = face.EmotionTexture;
                EmotionMaterial.SetTexture("_MainTex", currentTexture);
                return;
            }
        }
    }
}
