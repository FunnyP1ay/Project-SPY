using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{

    public List<GameObject> weaponList = new List<GameObject>();
    public GameObject currentWeapon;
    public enum WeaponState
    {
        phone    = 0,
        pistol   = 1,
        skill    = 2
    }
    public WeaponState weaponState;
    private void Start()
    {
        weaponState = WeaponState.phone;
    }
    public void WeaponChange(int _value)    // 0 = phone, 1 = pistol, 2 = skill
    {
        ResetWeapon();
        weaponList[_value].gameObject.SetActive(true);
        currentWeapon = weaponList[_value];
    }

    private void ResetWeapon()
    {
        foreach(var weapon in weaponList)
        {
            weapon.gameObject.SetActive(false);
        }
        currentWeapon = null;
    }
}
