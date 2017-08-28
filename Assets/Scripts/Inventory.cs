using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start()
	{
		database = GetComponent<ItemDatabase>();

		slotAmount = 6;
		inventoryPanel = GameObject.Find("InventoryPanel");
		slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

		for(int i = 0;i < slotAmount; i++)
		{
			items.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<ItemSlot>().id = i;
			slots[i].transform.SetParent(slotPanel.transform);
		}
            
	}

    public string GetItemName(int id)
    {
        Item itemToReturn = database.FetchItemByID(id);
        return itemToReturn.Title;
    }

    public Sprite GetItemSprite(int id)
    {
        Item itemToReturn = database.FetchItemByID(id);
        return itemToReturn.Sprite;
    }

	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID(id);

		if (itemToAdd.Stackable && CheckIfItemIsInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					data.amount++;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					break;
				}
			}
		} 
		else 
		{
			for (int i = 0; i < items.Count; i++) 
			{
				if (items [i].ID == -1) 
				{
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.GetComponent<ItemData> ().item = itemToAdd;
					itemObj.GetComponent<ItemData> ().slot = i;
					itemObj.GetComponent<ItemData> ().amount = 1;
					itemObj.transform.SetParent (slots [i].transform);
                    itemObj.transform.position = slots[i].transform.position;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.Title;
					break;
				}
			}
		}
	}

	bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++) 
		{
			if (items [i].ID == item.ID) {
				return true;
			}
		}
		return false;
	}

    public bool HasItem(int itemID)
    {
        Item itemToCheck = database.FetchItemByID(itemID);

        for (int i = 0; i < items.Count; i++) 
        {
            if (items [i].ID == itemToCheck.ID) {
                return true;
            }
        }
        return false;
    }
}
