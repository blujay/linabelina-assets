using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Button startButton;
	public Button closeControlsButton;
	public Canvas startMenu;
	public Canvas controlsMenu;
	public Canvas credits;
	public Button closeCreditsButton;
	public Button controlsText;
	public string sceneToLoad;


	// Use this for initialization
	void Start () {
		controlsText = controlsText.GetComponent<Button> ();
		controlsMenu = controlsMenu.GetComponent<Canvas> ();
		startMenu = startMenu.GetComponent<Canvas> ();
		credits = credits.GetComponent<Canvas> ();
		startButton = startButton.GetComponent<Button> ();
		closeControlsButton = closeControlsButton.GetComponent<Button> ();
		controlsMenu.enabled = false;
		credits.enabled = false;
		startMenu.enabled = true;
	}
	
	public void StartPress(){
		controlsMenu.enabled = true;
		startMenu.enabled = false;
	}

	public void LoadScene(){
		SceneManager.LoadScene (sceneToLoad);
	}

	public void LoadCredits(){
		credits.enabled = true;
		startMenu.enabled = false;
	}

	public void LoadControls(){
		controlsMenu.enabled = true;
		startMenu.enabled = false;
	}

	public void CloseControlMenu(){
		controlsMenu.enabled = false;
		startMenu.enabled = true;
	}

	public void CloseCredits(){
		credits.enabled = false;
		startMenu.enabled = true;
	}


}
