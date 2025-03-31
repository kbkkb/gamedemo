using System.Collections.Generic;
using System.Linq;
using Code.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIWindows
{
    public Transform[] pages; // 背包的分页容器
    private int itemsPerPage;  // 每页显示的物品数量

    void Start()
    {
        itemsPerPage = pages[0].childCount;
        
        // 订阅背包更新事件
        BagManager.Instance.OnBagChanged += UpdateBagUI;
        
        UpdateBagUI();  // 初始化时更新UI
    }

    public void UpdateBagUI()
    {
        // 清空所有页面内容
        foreach (Transform page in pages)
        {
            foreach (Transform slot in page)
            {
                ClearSlot(slot);
            }
        }

        // 填充物品到每个页面
        int itemIndex = 0;
        for (int pageIndex = 0; pageIndex < pages.Length; pageIndex++)
        {
            Transform page = pages[pageIndex];
            for (int slotIndex = 0; slotIndex < page.childCount; slotIndex++)
            {
                Transform slot = page.GetChild(slotIndex); // 当前格子

                if (itemIndex < BagManager.Instance.FindBagLegth())
                {
                    BagItem bagItemData = BagManager.Instance.bagItems[itemIndex];
                    Item itemData = DataManager.Instance.GetItemById(bagItemData.ItemId);

                    // 更新格子内容
                    UpdateSlot(slot, itemData, bagItemData.Quantity);
                    itemIndex++;
                }
                else
                {
                    // 清空多余格子
                    ClearSlot(slot);
                }
            }
        }
    }

    private void UpdateSlot(Transform slot, Item item, int quantity)
    {
        Image iconImage = slot.Find("Icon").GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = Resources.Load<Sprite>(item.Icon);
            iconImage.enabled = true;
        }

        Text quantityText = slot.Find("Quantity").GetComponent<Text>();
        if (quantityText != null)
        {
            quantityText.text = quantity > 1 ? $"x{quantity}" : "";
            quantityText.enabled = true;
        }
    }

    private void ClearSlot(Transform slot)
    {
        Image iconImage = slot.Find("Icon").GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }

        Text quantityText = slot.Find("Quantity").GetComponent<Text>();
        if (quantityText != null)
        {
            quantityText.text = "";
            quantityText.enabled = false;
        }
    }

    // 记得取消订阅事件，防止内存泄漏
    void OnDestroy()
    {
        BagManager.Instance.OnBagChanged -= UpdateBagUI;
    }
}
