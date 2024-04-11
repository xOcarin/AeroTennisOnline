using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour
{
 
    public string sceneName;

    // Function to be called when the button is clicked
    public void TransitionToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    
    
    public void QuitToDesktop()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
