using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;

    // Weapons unlock states (size 9 = 9 weapons)
    public bool[] unlockedWeapons = new bool[9];

    // UI panels corresponding to each weapon (must match unlockedWeapons length)
    public Image[] weaponPanels;

    private int currentWeaponID = -1; // -1 = no weapon equipped

    void Start()
    {
        // Check that lengths match
        if (weaponPanels.Length != unlockedWeapons.Length)
        {
            Debug.LogError("weaponPanels length must equal unlockedWeapons length!");
        }

        // Example: unlock weapon 1 by default
        unlockedWeapons[0] = true;

        // Initialize panel colors
        UpdatePanelGlow();
    }

    void Update()
    {
        for (int i = 1; i <= unlockedWeapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                EquipWeapon(i);
            }
        }
    }

    void EquipWeapon(int weaponID)
    {
        int index = weaponID - 1;

        if (index < 0 || index >= unlockedWeapons.Length)
            return;

        if (!unlockedWeapons[index])
        {
            Debug.Log("Weapon " + weaponID + " is locked!");
            return;
        }

        if (currentWeaponID == weaponID)
        {
            Debug.Log("Weapon " + weaponID + " is already equipped.");
            return;
        }

        currentWeaponID = weaponID;
        Debug.Log("Equipped weapon ID: " + weaponID);

        if (weapon != null)
        {
            weapon.SetWeapon(weaponID);
        }

        UpdatePanelGlow();
    }

    void UpdatePanelGlow()
    {
        for (int i = 0; i < weaponPanels.Length; i++)
        {
            if (weaponPanels[i] == null) continue;

            if (!unlockedWeapons[i])
            {
                // Weapon locked → panel black
                weaponPanels[i].color = Color.black;
            }
            else if (i == currentWeaponID - 1)
            {
                // Equipped → panel yellow glow
                weaponPanels[i].color = Color.white;
            }
            else
            {
                // Unlocked but not equipped → panel white
                weaponPanels[i].color = Color.gray;
            }
        }
    }

    public void UnlockWeapon(int weaponID)
    {
        int index = weaponID - 1;

        if (index >= 0 && index < unlockedWeapons.Length)
        {
            unlockedWeapons[index] = true;
            Debug.Log("Weapon " + weaponID + " unlocked!");
        }
    }
}
