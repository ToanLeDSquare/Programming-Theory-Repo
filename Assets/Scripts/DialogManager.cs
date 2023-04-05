using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogPnl;
    [SerializeField] private TextMeshProUGUI animalNameText;

    public static DialogManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public IEnumerator ShowDialog(string animalName, string action)
    {
        dialogPnl.SetActive(true);
        animalNameText.text = "- This is " + animalName + "\n" + "\n" + "- Action: " + action;
        yield return new WaitForSeconds(5f);
        HideDialog();
    }

    public void HideDialog()
    {
        dialogPnl.SetActive(false);
    }
}
