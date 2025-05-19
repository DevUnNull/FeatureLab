using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject[] unitPrefabs; // Prefab các tướng thật
    public ShopSlot[] slots;         // Slot trong cửa hàng (gán thủ công)

    void Start()
    {
        GenerateShop();
    }

    public void GenerateShop()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int rand = Random.Range(0, unitPrefabs.Length);
            GameObject chosen = unitPrefabs[rand];
            Sprite icon = chosen.GetComponent<SpriteRenderer>().sprite;
            slots[i].SetSlot(chosen, icon);
        }
    }

    // 🔁 Reset lại các slot chưa được mua
    public void ResetShop()
    {
        foreach (ShopSlot slot in slots)
        {
            if (!slot.HasBought())
            {
                // Xoá object cũ
                slot.ClearSlot();

                // Tạo mới
                int rand = Random.Range(0, unitPrefabs.Length);
                GameObject chosen = unitPrefabs[rand];
                Sprite icon = chosen.GetComponent<SpriteRenderer>().sprite;
                slot.SetSlot(chosen, icon);
            }
        }
    }
}
