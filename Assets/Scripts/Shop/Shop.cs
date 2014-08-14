using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Shop {
	public string shopId;
	public int sellValue;
	public List<SellItem> listSellItem;
}
