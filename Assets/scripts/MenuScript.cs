using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Text txt;
    private double money_from_users;

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
	public void OnClickExit()
    {
        Application.Quit();
    }
    
    public void TickChanges()
    {
        txt.text = txt.text + "- 1$";
    }
}
