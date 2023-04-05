using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanas : MonoBehaviour
{
    [SerializeField] private TMP_InputField userNameInput;
    private string _userName;

    public void GetUserNameChange(string userName)
    {
        _userName = userName;
    }

    public void StartButton()
    {
        DataManager.Instance.userName = _userName;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
