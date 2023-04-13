using System.Collections.Generic;
using UnityEngine;
using Boxfight2.Client.Player;

namespace Boxfight2.Client.Weapons
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<GunController> _weapons;
        private int _currentWeaponIndex;
        [SerializeField] private int whereIsBomb;

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

        public void AddBomb()
        {
            Debug.Log("Adding bomb");
            _weapons[whereIsBomb].b.CanBeHeld = true;
            _weapons[whereIsBomb].b.amountOfThrows++;

        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                
                // Scroll up
                CycleWeapon(1, false);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                // Scroll down
                CycleWeapon(-1, false);
            }

            for (int i = 0; i < 9; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    SelectWeapon(i, false);
                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("will ad bom");
                AddBomb();
            }
        }

        private void CycleWeapon(int direction, bool ManageBomb)
        {
            if (!_weapons[whereIsBomb].b.CanBeHeld) //if so, can the bomb be held?
            {
                //if it can't then check...

                if (_weapons[whereIsBomb].b.IsBeingThrown) //if it's being thrown, just let it stay active but scroll right past it.
                    ManageBomb = true;
                else if (!_weapons[whereIsBomb].b.IsBeingThrown) //if it's not being thrown, make it unactive. damn.
                    ManageBomb = false;
            }




            if (ManageBomb)
            {
                if (_weapons[_currentWeaponIndex] != _weapons[whereIsBomb])
                    _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            }
            else
            {
                _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            }

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

            if (_weapons[_currentWeaponIndex] == _weapons[whereIsBomb]) //check if the current GO is the bomb.
            {
                if (!_weapons[whereIsBomb].b.CanBeHeld) //if so, can the bomb be held?
                {
                    //if it can't then check...
                        CycleWeapon(direction, _weapons[whereIsBomb].b.IsBeingThrown);
                }
            }


            UpdatePlayerGunController();
        }
        
        public void HideBomb()
        {
            if (_weapons[_currentWeaponIndex] != _weapons[whereIsBomb])
                _weapons[whereIsBomb].gameObject.SetActive(false);

            if (_weapons[whereIsBomb].b.amountOfThrows < 1 && _weapons[whereIsBomb].gameObject.active)
            {
                _weapons[whereIsBomb].gameObject.SetActive(false);
                _currentWeaponIndex = whereIsBomb - 1;
                _weapons[_currentWeaponIndex].gameObject.SetActive(true);
                
            }


        }

        private void SelectWeapon(int weaponIndex, bool ManageBomb)
        {
            if (!_weapons[whereIsBomb].b.CanBeHeld) //if so, can the bomb be held?
            {
                //if it can't then check...

                if (_weapons[whereIsBomb].b.IsBeingThrown) //if it's being thrown, just let it stay active but scroll right past it.
                    ManageBomb = true;
                else if (!_weapons[whereIsBomb].b.IsBeingThrown) //if it's not being thrown, make it unactive. damn.
                    ManageBomb = false;
            }

            if (weaponIndex < 0 || weaponIndex >= _weapons.Count || weaponIndex == _currentWeaponIndex)
            {
                return;
            }


            if (ManageBomb)
            {
                if (_weapons[_currentWeaponIndex] != _weapons[whereIsBomb])
                    _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            }
            else
            {
                _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            }

            _currentWeaponIndex = weaponIndex;

            _weapons[_currentWeaponIndex].gameObject.SetActive(true);

            if (_weapons[_currentWeaponIndex] == _weapons[whereIsBomb]) //check if the current GO is the bomb.
            {
                if (!_weapons[whereIsBomb].b.CanBeHeld) //if so, can the bomb be held?
                {
                    //if it can't then check...
                        CycleWeapon(_currentWeaponIndex + 1, _weapons[whereIsBomb].b.IsBeingThrown);
                }
            }

            UpdatePlayerGunController();
        }

        private void UpdatePlayerGunController()
        {
            BoxfightController playerController = GetComponent<BoxfightController>();
            GetComponent<Animator>().Play("OpenGun");
            if (playerController != null)
            {
                playerController.UpdateGunController(_weapons[_currentWeaponIndex]);
            }
        }
    }
}
