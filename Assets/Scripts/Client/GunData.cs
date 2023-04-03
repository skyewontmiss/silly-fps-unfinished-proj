// GunData.cs

using UnityEngine;

namespace Boxfight2.Client.Weapons
{
    [CreateAssetMenu(fileName = "New Take 2 Gun", menuName = "Boxfight 2 Gun")]
    public class GunData : ScriptableObject
    {
        [Header("Metadata")]
        [SerializeField] private Sprite gunImage;
        [SerializeField] private string gunName;

        [Header("Game Data")]
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private int maxAmmo = 10;
        [SerializeField] private float reloadTime = 2f;

        public string GunName => gunName;
        public Sprite GunImage => gunImage;
        public float FireRate => fireRate;
        public float BulletDamage => damage;
        public int MagazineSize => maxAmmo;
        public float ReloadTime => reloadTime;
    }
}
