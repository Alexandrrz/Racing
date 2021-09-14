using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public void PauseActive()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void VolumeNull()
    {
       AudioListener.volume = 0;       
    }
    public void VolumeMax()
    {
        
        AudioListener.volume = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Highway");

    }
    public void Start_btn()
    {
        SceneManager.LoadScene("Highway");
    }
    public void Garage_btn()
    {
        SceneManager.LoadScene("Garage");
    }
    public void Menu_btn()
    {
        SceneManager.LoadScene("Main_menu");
        Time.timeScale = 1f;
    }
}
