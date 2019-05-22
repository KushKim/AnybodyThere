using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	public void ChangeInGame()
    {
        SceneManager.LoadScene("InGame");
    }
    public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}
