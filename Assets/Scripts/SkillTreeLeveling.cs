using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeLeveling : MonoBehaviour
{
    public CowboyHat CowboyHat;
    public InvisibilityHat InvisibilityHat;
    public WizardHat WizardHat;

    GameObject _player;
    ThirdPersonController _playerController;
    GameObject SkillTree;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = (ThirdPersonController)_player.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) // increases the level of all hats by 1
    {
        if(other.gameObject.tag=="LevelUp")
        {
            CowboyHat.LevelUp();
            InvisibilityHat.LevelUp();
            WizardHat.LevelUp();
            other.gameObject.SetActive(false);
        }
    }

}
