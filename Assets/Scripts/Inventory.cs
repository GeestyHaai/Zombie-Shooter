using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{
    public WeaponScript weaponScript;  // Reference to the script that handles weapon logic (e.g., equipping)
    [Header("Inventory State")]
    public List<int> ownedWeaponIDs = new List<int>();   // IDs player owns
    private int equippedWeaponID = -1;                   // Currently equipped

    [Header("UI Setup")]
    public List<WeaponPlane> weaponPlanes = new List<WeaponPlane>();

    [Header("Colors")]
    public Color equippedColor = Color.yellow;  // Glowing
    public Color ownedColor = Color.white;      // Neutral
    public Color lockedColor = Color.gray;      // Darkened


    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        HandleNumberKeyInput();
    }

    // -------------------------
    // INPUT
    // -------------------------
    void HandleNumberKeyInput()
    {
        for (int i = 0; i < weaponPlanes.Count && i < 9; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                TryEquipWeapon(weaponPlanes[i].weaponID);
            }
        }
    }

    // -------------------------
    // INVENTORY LOGIC
    // -------------------------

    public void PurchaseWeapon(int weaponID)
    {
        if (!ownedWeaponIDs.Contains(weaponID))
        {
            ownedWeaponIDs.Add(weaponID);

            // Auto-equip first weapon purchased
            if (equippedWeaponID == -1)
                equippedWeaponID = weaponID;

            UpdateUI();
        }
    }

    void TryEquipWeapon(int weaponID)
    {
        if (ownedWeaponIDs.Contains(weaponID))
        {
            equippedWeaponID = weaponID;
            UpdateUI();
        }
        // If not owned → do nothing
    }

    public int GetCurrentWeaponID()
    {
        return equippedWeaponID;
    }

    // -------------------------
    // UI UPDATE
    // -------------------------

    void UpdateUI()

    {
        foreach (var plane in weaponPlanes)
        {
            if (!ownedWeaponIDs.Contains(plane.weaponID))
            {
                plane.image.color = lockedColor;
            }
            else if (plane.weaponID == equippedWeaponID)
            {
                plane.image.color = equippedColor;
            }
            else
            {
                plane.image.color = ownedColor;
            }
        }
    }
}

[System.Serializable]
public class WeaponPlane
{
    public int weaponID;   // Assign matching ID
    public Image image;    // Assign UI Image in Inspector
}
