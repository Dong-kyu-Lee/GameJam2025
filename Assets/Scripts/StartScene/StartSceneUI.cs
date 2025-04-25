using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{

    public void MoveScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
