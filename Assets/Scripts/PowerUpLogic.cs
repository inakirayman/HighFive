using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    public string powerup = "Magnet";
    private string Coin = "Coins";

    public GameObject _shield;

    private bool IsMagnet = false;
    private bool IsShield = false;
    private float magnetForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.ToLower().Contains(powerup))
        {
            switch (powerup.ToLower())
            {
                case "magnet":
                    IsMagnet = true;
                    break;
                case "shield":
                    IsShield = true;
                    GetComponent<PlayerControllerUITEST>().IsShielded = IsShield;
                    break;
            }

            Destroy(other.gameObject);
        }

        if (IsShield && other.CompareTag("Obstacles"))
        {
            IsShield = false;
            GetComponent<PlayerControllerUITEST>().IsShielded = IsShield;
            Destroy(other.gameObject);
            _shield.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (IsMagnet)
            Magnet();
        else if (IsShield)
            _shield.SetActive(true);
    }

    private void Magnet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f); // find all colliders within a certain radius

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(Coin)) // check if the collider has the desired tag
            {
                Vector3 direction = transform.position - collider.gameObject.transform.position; // calculate the direction towards the player
                collider.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * magnetForce); // apply a force towards the player
            }
        }
    }
}
