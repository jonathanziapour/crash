using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour {

    public void DisplayInventory(bool isDisplayed)
    {
        if(isDisplayed)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if(!isDisplayed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
