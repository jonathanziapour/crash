using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	private Inventory inv;
	public int id;
	public Sprite btnPushed;
	public Sprite btnDefault;
    private Text detailTitle;
    private Text detailBody;

	void Start()
	{
		inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
        detailTitle = GameObject.Find("DetailTitle").GetComponent<Text>();
        detailBody = GameObject.Find("DetailBody").GetComponent<Text>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (inv.items [id].ID != -1) 
		{
			this.transform.GetComponent<Image> ().sprite = btnPushed;
            detailTitle.text = inv.items[id].Title.ToString();
            detailBody.text = inv.items[id].Description.ToString();
            this.GetComponent<AudioSource>().Play();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (inv.items [id].ID != -1) 
		{
			this.transform.GetComponent<Image> ().sprite = btnDefault;
            detailTitle.text = "";
            detailBody.text = "";
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();
		if (inv.items [id].ID == -1) {
			inv.items [droppedItem.slot] = new Item ();
			inv.items [id] = droppedItem.item;
			droppedItem.slot = id;
		} 
		else if(droppedItem.slot != id)
		{
			Transform item = this.transform.GetChild (0);
			item.GetComponent<ItemData> ().slot = droppedItem.slot;
			item.transform.SetParent (inv.slots[droppedItem.slot].transform);
			item.transform.position = inv.slots [droppedItem.slot].transform.position;

			droppedItem.slot = id;
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items [droppedItem.slot] = item.GetComponent<ItemData> ().item;
			inv.items [id] = droppedItem.item;
		}
	}
}
