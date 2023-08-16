using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLogic : MonoBehaviour
{
    public ASyncLoadManager asyncLoadManager; 
    public string SceneName;
    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("Completed") == 1)
        {
            SceneName = "Wiz Hub Variant";
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (FindObjectOfType<ActivateQuest>())
            {
                FindObjectOfType<ActivateQuest>().ResetActivateQuests();
            }

            Time.timeScale = 0f;
            //SceneManager.LoadScene(SceneName);
            asyncLoadManager.GoToLevel(SceneName);

            GetComponent<Collider>().enabled = false;
            StartCoroutine(EnableCollision(3));
        }
    }

    private IEnumerator EnableCollision(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
    }
}
