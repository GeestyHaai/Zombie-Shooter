using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeapon(int weaponID)
    {
        Debug.Log("Weapon script received weapon ID: " + weaponID);
        // Here you can add logic to change the weapon's appearance, stats, etc. based on the weaponID
    }
}
