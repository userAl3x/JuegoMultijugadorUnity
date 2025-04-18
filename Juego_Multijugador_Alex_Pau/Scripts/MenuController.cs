using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{   
    //Carga la escena de conexion con el servidor
    public void LoadGame()
    {
        SceneManager.LoadScene("Loading");
    }

    //Finaliza el juego o aplicación
    public void ExitGame()
    {
        Application.Quit();
    }
}

