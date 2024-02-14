using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	[SerializeField]
	Item[] items;

	public Item currentItem {get; private set;}

	private int currentItemIndex = 0;

	// Start is called before the first frame update
	void Start() {
		if(items != null && items.Length > 0) {
			currentItem = items[0];
			currentItem.Equip();
		}
	}

	public void CycleItem() {
		if(items != null && items.Length > 0) {
			currentItemIndex = (currentItemIndex + 1) % items.Length;
			currentItem = items[currentItemIndex];
			currentItem.Equip();
		}
	}

	public void Use() {
		currentItem?.Use();
	}

	public void StopUse() {
		currentItem?.StopUse();
	}
}