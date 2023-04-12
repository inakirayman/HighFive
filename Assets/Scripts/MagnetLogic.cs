using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class MagnetLogic : MonoBehaviour
{
    public string powerup = "Magnet";
    private string Coin = "Coins";

    private bool IsMagnet = false;
    private float magnetForce = 10f;
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            IsMagnet = true;
            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (IsMagnet)
            Magnet();
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
