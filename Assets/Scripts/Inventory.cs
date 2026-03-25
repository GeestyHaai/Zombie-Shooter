using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{
    [Header("UI Planes (Assign 9 in Inspector)")]
    public Image[] weaponPlanes;

    [Header("Weapon Unlock States")]
    [SerializeField] private bool[] unlockedWeapons = new bool[9];

    [Header("Colors")]
    public Color equippedColor = Color.yellow;
    public Color unlockedColor = Color.white;
    public Color lockedColor = Color.gray;

    [Header("Weapon System")]
    [SerializeField] private WeaponScript weaponScript;     // Drag your WeaponScript here

    private int equippedWeaponID = 1;

    private void Awake()
    {
        if (unlockedWeapons == null || unlockedWeapons.Length != 9) 
        {
            unlockedWeapons = new bool[9];
        }
    }

    private void Start()
    {
        if (!unlockedWeapons[0])
        {
            unlockedWeapons[0] = true;
        }

        equippedWeaponID = 1;
        UpdateUI();

        // Equip the starting weapon
        if (weaponScript != null) 
        {
            weaponScript.SetWeapon(1);
        }
    }

    private void Update()
    {
        for (int slot = 0; slot < 9; slot++)
        {
            if (Input.GetKeyDown((slot + 1).ToString()))
            {
                TryEquipWeapon(slot + 1);
            }
        }
    }

    private void TryEquipWeapon(int weaponID)
    {
        int index = weaponID - 1;

        if (index >= 0 && index < 9 && unlockedWeapons[index])
        {
            equippedWeaponID = weaponID;
            UpdateUI();

            // Directly call SetWeapon
            if (weaponScript != null)
            {
                weaponScript.SetWeapon(weaponID);
            }
            else
            {
                Debug.LogWarning("WeaponScript reference is missing on WeaponInventoryUI!");
            }
        }
    }

    public void UnlockWeapon(int weaponID)
    {
        int index = weaponID - 1;
        if (index >= 0 && index < 9)
        {
            unlockedWeapons[index] = true;
            UpdateUI();
        }
    }

    public int GetCurrentWeaponID()
    {
        return equippedWeaponID;
    }

    private void UpdateUI()
    {
        for (int slot = 0; slot < weaponPlanes.Length; slot++)
        {
            int weaponID = slot + 1;

            if (!unlockedWeapons[slot])
                weaponPlanes[slot].color = lockedColor;
            else if (weaponID == equippedWeaponID)
                weaponPlanes[slot].color = equippedColor;
            else
                weaponPlanes[slot].color = unlockedColor;
        }
    }
}
