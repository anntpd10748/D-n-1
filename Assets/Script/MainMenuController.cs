using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Lv1()
    {
        SceneManager.LoadScene(2);
    }
    public void Lv2()
    {
        SceneManager.LoadScene(3);
    }
    public void Lv3()
    {
        SceneManager.LoadScene(4);
    }
}
