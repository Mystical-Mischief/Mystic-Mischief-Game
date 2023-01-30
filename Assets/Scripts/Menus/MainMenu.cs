using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject Saves;
    [SerializeField] EventSystem eventSystem;

    public void PlayGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void GoToLevel1() => SceneManager.LoadScene(2);
    
    public void GoToLevel2() => SceneManager.LoadScene(6);
    
    public void GoToLevel3() => SceneManager.LoadScene(7);

    public void GoToWizardHub() => SceneManager.LoadScene(8);

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Saves.GetComponent<SaveGeneral>().Loadmenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }
}
