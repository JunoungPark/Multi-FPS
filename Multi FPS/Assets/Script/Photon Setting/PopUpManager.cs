using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public Text errorMessage;

    private static GameObject preFab;

    public static PopUpManager Show(string message)
    {
        if (preFab == null)
        {
            preFab = (GameObject)Resources.Load("Error PopUp");
        }
        GameObject obj = Instantiate(preFab);
        
        PopUpManager errorUi = obj.GetComponent<PopUpManager>();

        errorUi.UpdateContent(message);

        return errorUi;
    }
    public void UpdateContent(string message)
    {
        errorMessage.text = message;
    }

    public void CanclePopUp()
    {
        Destroy(gameObject);
    }
}
