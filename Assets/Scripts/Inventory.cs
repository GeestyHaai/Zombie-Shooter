using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{
    [Header("UI Planes (Assign 9 in Inspector)")]
    public Image[] weaponPlanes;

    [Header("Weapon Unlock States (Size must be 9)")]
    [SerializeField]
    private bool[] unlockedWeapons = new bool[9];

    [Header("Colors")]
    public Color equippedColor = Color.yellow;
    public Color unlockedColor = Color.white;
    public Color lockedColor = Color.gray;

    private int equippedWeaponID = 1;

    void Awake()
    {
        // Ensure array size is always 9
        if (unlockedWeapons == null || unlockedWeapons.Length != 9)
        {
            unlockedWeapons = new bool[9];
        }
    }

    void Start()
    {
        // If weapon 1 isn't unlocked in inspector, force it
        if (!unlockedWeapons[0])
        {
            unlockedWeapons[0] = true;
        }

        equippedWeaponID = 1;
        UpdateUI();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                TryEquipWeapon(i + 1);
            }
        }
    }

    void TryEquipWeapon(int weaponID)
    {
        int index = weaponID - 1;

        if (unlockedWeapons[index])
        {
            equippedWeaponID = weaponID;
            UpdateUI();
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

    public void SetWeaponUnlocked(int weaponID, bool state)
    {
        int index = weaponID - 1;

        if (index >= 0 && index < 9)
        {
            unlockedWeapons[index] = state;
            UpdateUI();
        }
    }

    public int GetCurrentWeaponID()
    {
        return equippedWeaponID;
    }

    void UpdateUI()
    {
        for (int i = 0; i < weaponPlanes.Length; i++)
        {
            int weaponID = i + 1;

            if (!unlockedWeapons[i])
            {
                weaponPlanes[i].color = lockedColor;
            }
            else if (weaponID == equippedWeaponID)
            {
                weaponPlanes[i].color = equippedColor;
            }
            else
            {
                weaponPlanes[i].color = unlockedColor;
            }
        }
    }
}
