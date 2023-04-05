
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

   #region Button Method
   
   public void BackToMenu()
   {
      SceneManager.LoadScene("Menu", LoadSceneMode.Single);
   }

   #endregion
}
