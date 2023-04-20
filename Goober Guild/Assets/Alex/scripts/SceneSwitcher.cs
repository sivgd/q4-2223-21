using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   public void playGame()
   {
		SceneManager.LoadScene("TopDownMovement 1");
   }
}
