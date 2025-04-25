using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneUI : MonoBehaviour
{
    public void MoveScene()
    {
        GameManager.Instance.DestroySelf();
        SceneManager.LoadScene("StartScene");
    }
}
