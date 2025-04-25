using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void MoveScene()
    {
        GameManager.Instance.DestroySelf();
        SceneManager.LoadScene("StartScene");
    }
}
