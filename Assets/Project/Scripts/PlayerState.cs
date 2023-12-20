using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;} 
    int damageToGive = 1;
    public GameObject deathPanel;


    void Awake()
    {
        currentHealth = startingHealth;
        deathPanel.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damageToGive);
        }
        
        // if (collision.gameObject.CompareTag("Flag"))
        // {
        //     deathPanel.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = "Congratulations!";
        //     deathPanel.SetActive(true);

        // }
    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Flag"))
        {
            deathPanel.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = "Congratulations!";
            deathPanel.SetActive(true);

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            // Debug.Log("Hurt");
        }
        else
        {
            // Debug.Log("Die");

            deathPanel.SetActive(true);

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
