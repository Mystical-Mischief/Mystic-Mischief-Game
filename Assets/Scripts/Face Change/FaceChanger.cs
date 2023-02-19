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



    [SerializeField] private Emotion _currentEmotion;
    public Emotion CurrentEmotion
    {
        get
        {
            return _currentEmotion;
        }
        set
        {
            if(_currentEmotion != value)
            {
                _currentEmotion = value;
                OnEmotionChange?.Invoke(this, _currentEmotion);
            }
        }
    }

    private Texture currentTexture;
    private Face currentFace;

    public Material EmotionMaterial;


    public event EventHandler<Emotion> OnEmotionChange;


    //finds face on start
    private void Start()
    {
        //findNewFace();
        OnEmotionChange += findNewFace;

        CurrentEmotion = Emotion.Neutral; 
    }
                          //Made a slightly better if statement - Corey
    private void FixedUpdate() //Could be turned into event, does not need to check the face every frame. 
    {
        OnEmotionChange?.Invoke(this, _currentEmotion);
    }
    public void findNewFace(object sender, Emotion emotion) //Finds a face that has the correct emotion, then changes the texture of the Material that shows the emotion. 
    {


        foreach (Face face in allFaces)
        {
            if (emotion == face.emotion)
            {
                currentFace = face;
                currentTexture = face.EmotionTexture;
                EmotionMaterial.SetTexture("_MainTex", currentTexture);
                return;
            }
        }


    }
}
