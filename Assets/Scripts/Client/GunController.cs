// GunController.cs

using System.Collections;
using UnityEngine;

namespace Boxfight2.Client.Weapons
{
    public class GunController : MonoBehaviour
    {
        [Header("Gun Controller")]
        [SerializeField] private GunData _gunData = default;
        [SerializeField] private Transform _muzzle = default;
        [SerializeField] private GameObject _bulletPrefab = default;
        [SerializeField] private AudioSource _audioSource = default;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject Model;

        [SerializeField] public bool isBomb;
        [SerializeField] public BombController b;

        [Header("Animation")]
        [SerializeField] private Animator anims;

        private float _nextFireTime = 0f;
        private int _bulletsInMagazine = 0;
        private bool _isReloading = false;

        [Header("ADS")]
        [SerializeField] private Transform Normal;
        [SerializeField] private Transform ADS;
        [SerializeField] private float ADSPeed;
        [SerializeField] private Camera cam;


        private void Start()
        {
            _bulletsInMagazine = _gunData.MagazineSize;
        }

        private void Update()
        {
            if (_isReloading)
            {
                return;
            }

            if (_bulletsInMagazine <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
            {
                _nextFireTime = Time.time + _gunData.FireRate;
                Fire();
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Model.transform.position = Vector3.Lerp(Model.transform.position, ADS.position, ADSPeed);
                Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, ADS.rotation, ADSPeed);
                Model.transform.localScale = Vector3.Lerp(Model.transform.localScale, ADS.localScale, ADSPeed);

            }
            else
            {
                Model.transform.position = Vector3.Lerp(Model.transform.position, Normal.position, ADSPeed);
                Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, Normal.rotation, ADSPeed);
                Model.transform.localScale = Vector3.Lerp(Model.transform.localScale, Normal.localScale, ADSPeed);
            }
        }

        private void Fire()
        {
            if (!isBomb)
            {
                anims.StopPlayback();
                anims.Play(_gunData.GunName + " Fire");
                _bulletsInMagazine--;
                GameObject bullet = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
                bullet.name = "New Client Bullet";
                var bulletController = bullet.GetComponent<BulletController>();
                bulletController.SetDamage(_gunData.BulletDamage);
                bulletController.player = player;
            }

            if (isBomb)
            {
                b.ThrowBomb(cam.transform.forward);
                // Disable this gun controller
                enabled = false;
            }

            if (_audioSource != null)
                _audioSource.PlayOneShot(_audioSource.clip);
        }


        private IEnumerator Reload()
        {
            _isReloading = true;
            yield return new WaitForSeconds(_gunData.ReloadTime);

            _bulletsInMagazine = _gunData.MagazineSize;
            _isReloading = false;
        }
    }
}
