using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    private CanvasGroup invSlotsCG;
    private CanvasGroup invDetailCG;
    private CanvasGroup pickupCG;
    private CanvasGroup dialogueCG;

    private CanvasGroup speechCG;
    private Text speechText;

    private CanvasGroup doorLockedCG;

    private CanvasGroup pickupConfirmationCG;
    private Text pickupConfirmationText;
    private Image pickupConfirmationImg;

    private FirstPersonController fpscontroller;
    private CanvasManager canvasManager;
    private Inventory inv;
    private DialogueDatabase dialogueDatabase;

    private GameObject[] walkways;
    private GameObject WWW2Rem;

    private bool isInvShowing;

    IEnumerator ShowPickupConfirmation(int quantity, string itemName, Sprite itemSprite)
    {
        pickupConfirmationText.text = quantity + "x " + itemName;
        pickupConfirmationImg.sprite = itemSprite;

        pickupConfirmationCG.alpha = 1;
        yield return new WaitForSeconds(3);
        pickupConfirmationCG.alpha = 0;
    }

    public IEnumerator ShowDoorLockedMessage()
    {
        doorLockedCG.alpha = 1;
        yield return new WaitForSeconds(3);
        doorLockedCG.alpha = 0;

    }

    public void PickupItem(int itemID, int quantity)
    {
        inv.AddItem(itemID);
        StartCoroutine(ShowPickupConfirmation(quantity, inv.GetItemName(itemID), inv.GetItemSprite(itemID)));

        //If we've picked up the small doll, activate the walkway
        if (itemID == 3)
        {
            foreach (GameObject w in walkways)
            {
                w.SetActive(true);
            }

            WWW2Rem.SetActive(false);
        }
    }

    public void PickupPrompt(bool isHovering)
    {
        if (isHovering)
        {
            pickupCG.alpha = 1;
            pickupCG.GetComponentInChildren<Text>().text = "Pick up";
        }
        else
        {
            pickupCG.alpha = 0;
        }
    }

    public void DoorPrompt(bool isHovering)
    {
        if (isHovering)
        {
            pickupCG.alpha = 1;
            pickupCG.GetComponentInChildren<Text>().text = "Open";
        }
        else
        {
            pickupCG.alpha = 0;
        }
    }

    public void TalkPrompt(bool isHovering)
    {
        if (isHovering)
        {
            pickupCG.alpha = 1;
            pickupCG.GetComponentInChildren<Text>().text = "Talk";
        }
        else
        {
            pickupCG.alpha = 0;
        }
    }

    public IEnumerator InitiateDialogue(int npc_id)
    {
        List<Dialogue> npcDialogue = dialogueDatabase.FetchDialogueListByNPCID(npc_id);
        speechCG.alpha = 1;

        if (npcDialogue.Count > 0)
        {
            for (int i = 0; i < npcDialogue.Count; i++)
            {
                yield return new WaitForFixedUpdate();
                speechText.text = npcDialogue[i].Dialogue_Text;
                yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
            }
        }

        speechCG.alpha = 0;

        //If CF NPC, hand over Church Key after dialogue
        if (npc_id == 0)
        {
            inv.AddItem(2);
            StartCoroutine(ShowPickupConfirmation(1, inv.GetItemName(2), inv.GetItemSprite(2)));
            this.GetComponent<AudioSource>().Play();
        }
    }

    // Use this for initialization
	void Start () 
    {
        invSlotsCG = GameObject.FindGameObjectWithTag("InventoryPanel").GetComponent<CanvasGroup>();
        invDetailCG = GameObject.FindGameObjectWithTag("DetailPanel").GetComponent<CanvasGroup>();
        pickupCG = GameObject.FindGameObjectWithTag("PickupPanel").GetComponent<CanvasGroup>();
        dialogueCG = GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<CanvasGroup>();

        speechCG = GameObject.FindGameObjectWithTag("SpeechPanel").GetComponent<CanvasGroup>();
        speechText = GameObject.FindGameObjectWithTag("SpeechText").GetComponent<Text>();

        doorLockedCG = GameObject.FindGameObjectWithTag("DoorLockedPanel").GetComponent<CanvasGroup>();

        pickupConfirmationCG = GameObject.FindGameObjectWithTag("PickupConfPanel").GetComponent<CanvasGroup>();
        pickupConfirmationText = GameObject.FindGameObjectWithTag("PickupConfText").GetComponent<Text>();
        pickupConfirmationImg = GameObject.FindGameObjectWithTag("PickupConfImg").GetComponent<Image>();

        fpscontroller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        canvasManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasManager>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        dialogueDatabase = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueDatabase>();

        walkways = GameObject.FindGameObjectsWithTag("Walkway");
        WWW2Rem = GameObject.FindGameObjectWithTag("WWW2Rem");

        invSlotsCG.alpha = 0;
        invSlotsCG.blocksRaycasts = false;
        invDetailCG.alpha = 0;
        invDetailCG.blocksRaycasts = false;
        isInvShowing = false;

        pickupCG.alpha = 0;
        pickupCG.blocksRaycasts = false;
        dialogueCG.alpha = 0;
        dialogueCG.blocksRaycasts = false;
        speechCG.alpha = 0;
        speechCG.blocksRaycasts = false;
        doorLockedCG.alpha = 0;
        doorLockedCG.blocksRaycasts = false;

        pickupConfirmationCG.alpha = 0;
        pickupConfirmationCG.blocksRaycasts = false;

        foreach (GameObject w in walkways)
        {
            w.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetButtonDown("Inventory"))
        {
            canvasManager.GetComponent<AudioSource>().Play();

            if (!isInvShowing)
            {
                invSlotsCG.alpha = 1;
                invSlotsCG.blocksRaycasts = true;
                invDetailCG.alpha = 1;
                invDetailCG.blocksRaycasts = true;
                isInvShowing = true;
                fpscontroller.enabled = false;
                canvasManager.DisplayInventory(true);

            }
            else if (isInvShowing)
            {
                invSlotsCG.alpha = 0;
                invSlotsCG.blocksRaycasts = false;
                invDetailCG.alpha = 0;
                invDetailCG.blocksRaycasts = false;
                isInvShowing = false;
                fpscontroller.enabled = true;
                canvasManager.DisplayInventory(false);

            }
        }
	}
}
