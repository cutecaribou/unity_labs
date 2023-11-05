using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarUi : MonoBehaviour
{
    private TextMeshProUGUI starTxt;
    
    void Start()
    {
        starTxt = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStarText(PlayerInventory inventory)
    {
        starTxt.text = inventory.numberOfStars.ToString();
    }
}
