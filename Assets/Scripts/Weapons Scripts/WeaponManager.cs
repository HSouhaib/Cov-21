using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] guns;

    private int currentGun;

     // Deactivate all guns at the start 
    private void Start()
    {
        DeactivateAllGun();

    }

    // Set all Guns in the array to false ( guns deactivated).
    void DeactivateAllGun()
    {
        for (int i = 0; i < guns.Length; i++)
            guns[i].SetActive(false);
    }
    
    // Deactivate the default gun and replace it by the another gun in the array ..
    public void ActivateGun(int NewGunIndex)
    {
        guns[currentGun].SetActive(false);
        guns[NewGunIndex].SetActive(true);
        currentGun = NewGunIndex;

    }





} // class






