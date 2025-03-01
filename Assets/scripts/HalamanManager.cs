using UnityEngine;
using UnityEngine.SceneManagement;

public class HalamanManager : MonoBehaviour
{
    public bool isEscapeToExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
            {
                Application.Quit();
            }
            else
            {
                KembaliMenu();
            }
        }
        
    }

    public void MulaiPermainan()
    {
        SceneManager.LoadScene("Main");
    }

    public void KembaliMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
