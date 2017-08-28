using UnityEngine;
using System.Collections;

public class CameraRaycast : MonoBehaviour {

    private RaycastHit hit;
    private Vector3 fwd;
    public GameManager gm;
    private Inventory inv;
    private bool isHoveringPickup;
    private bool isHoveringTalk;
    private bool isHoveringDoor;
    public bool isShowingDoorLockedMessage;
    private Transform objectHit;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        isShowingDoorLockedMessage = false;
	}
	
	// Update is called once per frame
	void Update () {
	
        fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 4))
        {
            objectHit = hit.transform;

            if (objectHit.gameObject.tag == "Pickup")
            {
                isHoveringPickup = true;
                gm.PickupPrompt(isHoveringPickup);
            }
            else if (objectHit.gameObject.tag == "Door" 
                && objectHit.gameObject.GetComponent<DoorData>().isOpen == false 
                && isShowingDoorLockedMessage == false)
            {
                isHoveringDoor = true;
                gm.DoorPrompt(isHoveringDoor);
            }
            else if (objectHit.gameObject.tag == "NPC" 
                && objectHit.gameObject.GetComponent<NPCData>().hasSpoken == false)
            {
                isHoveringTalk = true;
                gm.TalkPrompt(isHoveringTalk);
            }
            else
            {
                isHoveringPickup = false;
                isHoveringTalk = false;
                isHoveringDoor = false;
                gm.PickupPrompt(isHoveringPickup);
                gm.TalkPrompt(isHoveringTalk);
            }
        }
        else
        {
            isHoveringPickup = false;
            isHoveringTalk = false;
            gm.PickupPrompt(isHoveringPickup);
            gm.TalkPrompt(isHoveringTalk);
        }

        if (Input.GetButtonDown("Fire1") && isHoveringPickup)
        {
            gm.PickupItem(objectHit.GetComponent<PickupItem>().itemID, objectHit.GetComponent<PickupItem>().quantity);
            objectHit.transform.parent.gameObject.SetActive(false);
            gm.GetComponent<AudioSource>().Play();
            Destroy(objectHit.transform.parent.gameObject);
        }

        if (Input.GetButtonDown("Fire1") && isHoveringTalk)
        {
            gm.TalkPrompt(false);
            StartCoroutine(gm.InitiateDialogue(objectHit.GetComponent<NPCData>().npc_id));
            objectHit.GetComponent<NPCData>().hasSpoken = true;
        }

        if (Input.GetButtonDown("Fire1") && isHoveringDoor)
        {
            if (objectHit.GetComponent<DoorData>().isLocked)
            {
                //If we're at the church door & have the key, then open, otherwise stay locked
                if (objectHit.GetComponent<DoorData>().doorID == 0 && (inv.HasItem(2)))
                {
                    objectHit.GetComponent<DoorData>().isOpen = true;
                    objectHit.GetComponent<Animation>().Play("DoorOpen");
                    objectHit.GetComponent<AudioSource>().Play();
                }
                else
                {
                    StartCoroutine(DoorLockedMessageTimer());
                    StartCoroutine(gm.ShowDoorLockedMessage());
                }
            }
            else
            {
                objectHit.GetComponent<DoorData>().isOpen = true;
                objectHit.GetComponent<Animation>().Play("DoorOpen");
                objectHit.GetComponent<AudioSource>().Play();
            }
        }
	}

    public IEnumerator DoorLockedMessageTimer()
    {
        isShowingDoorLockedMessage = true;
        yield return new WaitForSeconds(3);
        isShowingDoorLockedMessage = false;

    }
}
