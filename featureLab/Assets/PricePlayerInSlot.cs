using UnityEngine;
using TMPro;

public class PricePlayerInSlot : MonoBehaviour
{
    public TextMeshProUGUI priceText;

    public void SetPrice(int price)
    {
        if (priceText != null)
        {
            priceText.text = price.ToString();
        }
    }
}
