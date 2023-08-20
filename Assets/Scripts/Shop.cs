using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public List<Item> itemsAvailable = new List<Item>();
    public List<Button> itemsAvailableButton = new List<Button>();
    public List<GameObject> optionsList = new List<GameObject>();

    public CharacterOutfit characterOutfit;
    public CharacterOutfit characterOutfitUI;

    public TMP_Text TotalCoinsText;
    public int totalCoins = 1000;
    public GameObject notEnoughCoinsMessage;
    
    public int indexLastSelected;

    private void Start()
    {
        TotalCoinsText.text = totalCoins.ToString();
        SetItemPrice();
    }

    private void Update()
    {        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(indexLastSelected);
            SellItem(indexLastSelected);
        }
    }

    private void SetItemPrice()
    {
        foreach (Item item in itemsAvailable)
        {
            Transform itemPriceTransform = item.transform.parent.GetChild(1);
            itemPriceTransform.GetComponent<TMP_Text>().text = item.itemPrice.ToString();
        }
    }

    public void BuyItem(int index)
    {
        if (totalCoins >= itemsAvailable[index].itemPrice)
        {
            totalCoins -= itemsAvailable[index].itemPrice;
            TotalCoinsText.text = totalCoins.ToString();
            itemsAvailable[index].isAvailable = true;
            indexLastSelected = index;
            AudioManager.Instance.PlaySFX("Buy");
        }
        else
        {
            notEnoughCoinsMessage.SetActive(true);
            AudioManager.Instance.PlaySFX("NotEnoughMoney");
        }
    }

    public void SellItem(int index)
    {
        if (itemsAvailable[index].isAvailable)
        {
            totalCoins += itemsAvailable[index].itemPrice;
            TotalCoinsText.text = totalCoins.ToString();
            itemsAvailable[index].isAvailable = false;
            AudioManager.Instance.PlaySFX("Sell");
        }
    }

    public void EquipOutfit(int index)
    {
        if (itemsAvailable[index].isAvailable)
        {
            characterOutfit.Equip(itemsAvailable[index].itemIcon, itemsAvailable[index].indexBodyPart);
            characterOutfitUI.Equip(itemsAvailable[index].itemIcon, itemsAvailable[index].indexBodyPart);
            indexLastSelected = index;
            AudioManager.Instance.PlaySFX("Equip");
        }
        else
        {
            BuyItem(index);            
        }
    }

    public void ShowOptionsList(int index)
    {
        for (int i = 0; i < optionsList.Count; i++)
        {
            bool state = (index == i) ? true : false;
            optionsList[i].SetActive(state);
        }
    }

    public void MouseOverButton()
    {
        AudioManager.Instance.PlaySFX("MouseOver");
    }

    public void MouseClickButton()
    {
        AudioManager.Instance.PlaySFX("Click");
    }
}
