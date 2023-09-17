using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{

    public void _RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
