using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponControll : MonoBehaviour
{

    public List<GameObject> weaponList = new List<GameObject>();
    public GameObject currentWeapon;
    public enum WeaponState
    {
        none    = 0,
        equip   = 1,
        skill   = 2
    }
    public WeaponState weaponState;
    private void Start()
    {
        weaponState = WeaponState.none;
    }
    public void WeaponChange(int _value)    // 0 = None, 1 = Weapon, 2 = skill
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
