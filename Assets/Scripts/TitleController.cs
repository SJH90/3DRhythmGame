using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public GameObject PopupQuitGame;

	public void ShowPopup(){
		PopupQuitGame.SetActive(true);
	}

	public void HidePopup(){
		PopupQuitGame.SetActive(false);
	}

	public void ChangeScene(int sceneNum){
		SceneManager.LoadScene(sceneNum);
	}

	public void QuitApplication(){
		Application.Quit();
	}
}
