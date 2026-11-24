using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour {


    private void Update() {

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {

            //InfoButton();

        }


        if (Input.GetKeyDown(KeyCode.R)) {

            ReloadGame();

        }


        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit(); 

        }

    }


    public void InfoButton() {

        Application.OpenURL("https://www.youtube.com/watch?v=U1OwkMb9QIk");

    }

    public void ReloadGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }



}