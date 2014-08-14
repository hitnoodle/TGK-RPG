using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShopKeeper : MonoBehaviour {
	public string idShop;
	public float minConversationRange = 0.3f;
	
	private float sellValue;
	private List<ItemAmountManager> listSellItem = new List<ItemAmountManager>();
	private GameObject player;
	private FighterInventory fighterInventory;
	private PlayerControl playerControl;
	private List<ItemAmountManager> listBuyItemManager = new List<ItemAmountManager>();	
	private List<ItemAmountManager> listSellItemManager = new List<ItemAmountManager>();	
	private Shop shop;

	// Use this for initialization
	void Start () {
		LoadShop(idShop);

		player = GameObject.Find ("Player");
		playerControl = player.GetComponent<PlayerControl>();
		fighterInventory = playerControl.fighterInventory.GetComponent<FighterInventory>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool InRange(){
		Vector3 playerPosition = player.transform.position;
		Vector3 shopKeeperPosition = transform.position;
		float range = Mathf.Sqrt(((playerPosition.x - shopKeeperPosition.x)*(playerPosition.x - shopKeeperPosition.x))
		                         +((playerPosition.y - shopKeeperPosition.y)*(playerPosition.y - shopKeeperPosition.y)));
		if (range <= minConversationRange) {
			return true;		
		} else {
			return false;
		}
	}

	public void BuyItem(){
		int totalPrice = 0;
		for (int i = 0;i < listBuyItemManager.Count; i++) {
			totalPrice += (listBuyItemManager[i].item.value * listBuyItemManager[i].amount) ;				
		}
		if (totalPrice > playerControl.money) {
			ShowWarning();
		} else {
			playerControl.money -= totalPrice;
			listBuyItemManager = new List<ItemAmountManager>();
		}
	}

	public void SellItem(){
		int totalPrice = 0;
		for (int i = 0;i < listSellItemManager.Count; i++) {
			totalPrice += Mathf.CeilToInt((listSellItemManager[i].item.value * listSellItemManager[i].amount * sellValue));				
		}
		player.GetComponent<PlayerControl>().money += totalPrice;
		listSellItemManager = new List<ItemAmountManager>();
	}

	public void AddToBuyList(Item item, int amount){
		bool found = false;
		for(int i = 0;i < listSellItem.Count;i++){
			if(listSellItem[i].item.Equals(item) && amount > listSellItem[i].amount){
				amount = listSellItem[i].amount;
			}
		}

		RemoveFromList (item, amount);
		for(int i = 0;i < listBuyItemManager.Count;i++){
			if(listBuyItemManager[i].item.Equals(item)){
				listBuyItemManager[i].amount += amount;
				found = true;
				break;
			}
		}
		if(!found){ 
			ItemAmountManager newItemManager = new ItemAmountManager(item,amount);
			listBuyItemManager.Add(newItemManager);
		}
		fighterInventory.AddToInventory (item, amount);
	}

	public void AddToSellList(Item item, int amount){
		bool found = false;
		if(amount > fighterInventory.amounts[fighterInventory.items.IndexOf(item)]){
			amount = fighterInventory.amounts[fighterInventory.items.IndexOf(item)];
		}
		for(int i = 0;i < listSellItemManager.Count;i++){
			if(listSellItemManager[i].item.Equals(item)){
				listSellItemManager[i].amount += amount;
				found = true;
				break;
			}

		}
		if(!found){ 
			ItemAmountManager newItemManager = new ItemAmountManager(item,amount);
			listSellItemManager.Add(newItemManager);
		}
		fighterInventory.RemoveFromInventory (item, amount);
	}

	public void AddToList (Item item, int amount) {
		bool found = false;
		for(int i = 0;i < listSellItem.Count;i++){
			if(listSellItem[i].item.Equals(item)){
				listSellItem[i].amount += amount;
				found = true;
				break;
			}
		}
		if(!found){ 
			ItemAmountManager newItemManager = new ItemAmountManager(item,amount);
			listSellItem.Add(newItemManager);
		}
	}
	
	public void RemoveFromList (Item item, int amount) {
		for(int i = 0;i < listSellItem.Count;i++){
			if(listSellItem[i].item.Equals(item)){
				listSellItem[i].amount -= amount;
				break;
			}
		}
	}

	public void ShowWarning(){
	
	}

	public void CancelBuySpecificItem(Item item){
		int index = IndexOfItem(listBuyItemManager, item);
		if(isFound(index)){
			fighterInventory.RemoveFromInventory(listBuyItemManager[index].item,listBuyItemManager[index].amount);
			AddToList(listBuyItemManager[index].item,listBuyItemManager[index].amount);	
			listBuyItemManager.RemoveAt(index);
		}
	}

	public void CancelSellSpecificItem(Item item){
		int index = IndexOfItem(listSellItemManager, item);
		if(isFound(index)){
			fighterInventory.AddToInventory (listSellItemManager[index].item,listSellItemManager[index].amount);	
			listSellItemManager.RemoveAt(index);
		}
	}

	public void CancelBuyItem(){
		for (int i = 0; i < listBuyItemManager.Count; i++) {
			fighterInventory.RemoveFromInventory (listBuyItemManager[i].item,listBuyItemManager[i].amount);	
			AddToList(listBuyItemManager[i].item,listBuyItemManager[i].amount);	
		}
		listBuyItemManager = new List<ItemAmountManager>();
	}

	public void CancelSellItem(){
		for (int i = 0; i < listSellItemManager.Count; i++) {
			fighterInventory.AddToInventory (listSellItemManager[i].item,listSellItemManager[i].amount);	
		}
		listSellItemManager = new List<ItemAmountManager>();
	}


	public int IndexOfItem(List<ItemAmountManager> itemManager, Item item){
		for(int i = 0;i < itemManager.Count;i++){
			if(itemManager[i].item.Equals(item)){
				return i;
			}
		}
		return -1;
	}

	public bool isFound(int index){
		if(index == -1){
			return false;
		} else {
			return true;
		}
	}

	void LoadShop(string shopId)
	{
		this.shop = (Shop)XmlManager.LoadInstanceAsXml(shopId, typeof(Shop));
		
		if (this.shop == null)
			Debug.LogError("[Shop] No shop at file " + shopId);

		sellValue = this.shop.sellValue;

		foreach(SellItem sellItem in this.shop.listSellItem){
			LoadItemManager(sellItem);
			Debug.Log(sellItem);
		}

	}

	void LoadItemManager(SellItem sellItem){
		ItemAmountManager itemManager;

		Item item = (Item)XmlManager.LoadInstanceAsXml(sellItem.itemId, typeof(Item));
		itemManager =  new ItemAmountManager(item, sellItem.amount);
		if (itemManager == null)
			Debug.LogError("[Shop] No shop at file " + sellItem.itemId);

		listSellItem.Add(itemManager);
	}




}
