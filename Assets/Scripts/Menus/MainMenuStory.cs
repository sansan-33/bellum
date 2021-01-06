using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuStory : MonoBehaviour
{ 
   
    public void OnPointerClick()
    { 
        SceneManager.LoadScene("Scene_Main_Menu" );
    }
   
}
