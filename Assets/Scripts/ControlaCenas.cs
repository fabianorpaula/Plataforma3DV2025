using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaCenas : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
