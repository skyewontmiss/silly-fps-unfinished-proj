using System.Collections.Generic;
using UnityEngine;
using Boxfight2.Client.Player;

namespace Boxfight2.Client.Weapons
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<GunController> _weapons;
        private int _currentWeaponIndex;

        private void Start()
        {
            // Deactivate all weapons except for the first one
            for (int i = 1; i < _weapons.Count; i++)
            {
                _weapons[i].gameObject.SetActive(false);
            }

            _currentWeaponIndex = 0;
            _weapons[_currentWeaponIndex].gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                // Scroll up
                CycleWeapon(1);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                // Scroll down
                CycleWeapon(-1);
            }

            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    SelectWeapon(i);
                    break;
                }
            }
        }

        private void CycleWeapon(int direction)
        {
            _weapons[_currentWeaponIndex].gameObject.SetActive(false);

            _currentWeaponIndex += direction;

            if (_currentWeaponIndex >= _weapons.Count)
            {
                _currentWeaponIndex = 0;
            }
            else if (_currentWeaponIndex < 0)
            {
                _currentWeaponIndex = _weapons.Count - 1;
            }

            _weapons[_currentWeaponIndex].gameObject.SetActive(true);

            UpdatePlayerGunController();
        }

        private void SelectWeapon(int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex >= _weapons.Count || weaponIndex == _currentWeaponIndex)
            {
                return;
            }

            _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            _currentWeaponIndex = weaponIndex;
            _weapons[_currentWeaponIndex].gameObject.SetActive(true);

            UpdatePlayerGunController();
        }

        private void UpdatePlayerGunController()
        {
            BoxfightController playerController = GetComponent<BoxfightController>();
            if (playerController != null)
            {
                playerController.UpdateGunController(_weapons[_currentWeaponIndex]);
            }
        }
    }
}
