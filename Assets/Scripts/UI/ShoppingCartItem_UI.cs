using System;
using TMPro;
using UnityEngine;

public class ShoppingCartItem_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    public void SetItemText(string newString)
    {
        itemText.text = newString;
    }

}
