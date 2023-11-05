using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfStars {get; private set;}

    public UnityEvent<PlayerInventory> OnStarCollected;
    public void CollectStar()
    {
        numberOfStars++;
        OnStarCollected.Invoke(this);
    }
}
