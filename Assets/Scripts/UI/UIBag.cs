using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBag : UIWindows
{
    // public Transform[] pages; // 背包的分页容器
    // private int itemsPerPage;  // 每页显示的物品数量
    // private Dictionary<int, Item> itemCache = new Dictionary<int, Item>(); // 缓存Item数据
    //
    // private bool isUpdating = false; // 控制UI更新频率
    //
    // void Start()
    // {
    //     itemsPerPage = pages[0].childCount;
    //     
    //     // 订阅背包更新事件
    //     BagManager.Instance.OnBagChanged += UpdateBagUI;
    //     
    //     UpdateBagUI();  // 初始化时更新UI
    // }
    //
    // public void UpdateBagUI()
    // {
    //     if (isUpdating) return; // 如果正在更新，跳过
    //
    //     StartCoroutine(UpdateBagUICoroutine());
    // }
    //
    // private IEnumerator UpdateBagUICoroutine()
    // {
    //     isUpdating = true;
    //     
    //     // 延迟更新UI，避免频繁更新导致性能问题
    //     yield return new WaitForEndOfFrame();
    //     
    //     // 清空所有页面内容
    //     foreach (Transform page in pages)
    //     {
    //         foreach (Transform slot in page)
    //         {
    //             ClearSlotContent(slot);
    //         }
    //     }
    //
    //     // 填充物品到每个页面
    //     int itemIndex = 0;
    //     for (int pageIndex = 0; pageIndex < pages.Length; pageIndex++)
    //     {
    //         Transform page = pages[pageIndex];
    //         for (int slotIndex = 0; slotIndex < page.childCount; slotIndex++)
    //         {
    //             Transform slot = page.GetChild(slotIndex); // 当前格子
    //
    //             if (itemIndex < BagManager.Instance.FindBagLength())
    //             {
    //                 BagItem bagItemData = BagManager.Instance.bagItems[itemIndex];
    //                 Item itemData = GetItemData(bagItemData.ItemId); // 获取缓存的Item数据
    //
    //                 // 更新格子内容
    //                 UpdateSlot(slot, itemData, bagItemData.Quantity);
    //                 itemIndex++;
    //             }
    //             else
    //             {
    //                 // 清空多余格子
    //                 ClearSlotContent(slot);
    //             }
    //         }
    //     }
    //
    //     isUpdating = false;
    // }
    //
    // private Item GetItemData(int itemId)
    // {
    //     if (!itemCache.ContainsKey(itemId))
    //     {
    //         itemCache[itemId] = DataManager.Instance.GetItemById(itemId);
    //     }
    //     return itemCache[itemId];
    // }
    //
    // private void UpdateSlot(Transform slot, Item item, int quantity)
    // {
    //     Image iconImage = slot.Find("Icon")?.GetComponent<Image>();
    //     if (iconImage != null)
    //     {
    //         iconImage.sprite = Resources.Load<Sprite>(item.Icon);
    //         iconImage.enabled = true;
    //     }
    //
    //     Text quantityText = slot.Find("Quantity")?.GetComponent<Text>();
    //     if (quantityText != null)
    //     {
    //         quantityText.text = quantity > 1 ? $"x{quantity}" : "";
    //         quantityText.enabled = true;
    //     }
    // }
    //
    // private void ClearSlotContent(Transform slot)
    // {
    //     Image iconImage = slot.Find("Icon")?.GetComponent<Image>();
    //     if (iconImage != null)
    //     {
    //         iconImage.sprite = null;
    //         iconImage.enabled = false;
    //     }
    //
    //     Text quantityText = slot.Find("Quantity")?.GetComponent<Text>();
    //     if (quantityText != null)
    //     {
    //         quantityText.text = "";
    //         quantityText.enabled = false;
    //     }
    // }
    //
    // // 记得取消订阅事件，防止内存泄漏
    // void OnDestroy()
    // {
    //     BagManager.Instance.OnBagChanged -= UpdateBagUI;
    // }
}
