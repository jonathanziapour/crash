using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    bool isHoveringPlay;

    // Use this for initialization
	void Start () {
        isHoveringPlay = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && isHoveringPlay)
            SceneManager.LoadScene("Main");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
         this.GetComponent<AudioSource>().Play();
         this.GetComponent<Text>().color = Color.blue;
         isHoveringPlay = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Text>().color = Color.white;
        isHoveringPlay = false;
    }
}
