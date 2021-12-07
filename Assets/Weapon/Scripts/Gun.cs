using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{ 
    AK47,
}
public class Gun : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

    //weapon information

    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private float bulletInterval;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletMaxDistance;

    [SerializeField] private int totalAmmoCount;
    [SerializeField] private int ammoCount;
    [SerializeField] private int clipSize;
    [SerializeField] private int weaponNumber;
    [SerializeField] private bool allowHoldShoot;
    //bullet prefab
    [SerializeField] private ParticleSystem hitPlayerEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem muzzleFlash;
    //public TrailRenderer trail;
    //Bullet date
    class Bullet
    {
        private GameObject prefab;
        private Vector3 direction;
        private float speed;
        private float accumulatedDistance;
        private LayerMask layer;
        private Vector3 Position { get { return prefab.transform.position; } set { prefab.transform.position = value; } }

        public void Init(GameObject prefab, Vector3 direction, float speed, LayerMask layer)
        {
            this.prefab = prefab;
            this.direction = direction;
            this.speed = speed;
            this.layer = layer;
        }
        public Collider2D Move(float time)
        {
            Vector3 displacement = direction * speed * time;
            float distance = displacement.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(Position, direction, distance, layer);

            Position += displacement;
            accumulatedDistance += distance;
            return hit.collider;
        }
        public bool IsMaxDistance(float maxDistance)
        {
            return accumulatedDistance >= maxDistance;
        }
        public void DestroyBullet()
        {
            Destroy(prefab);
        }
    }
    private List<Bullet> bullets = new List<Bullet>();
    private float accumulatedTime;


    private void Update()
    {
        accumulatedTime += Time.deltaTime;
        UpdateBullet(Time.deltaTime);
    }
    private bool CanFire()
    {
        if (accumulatedTime >= bulletInterval)
        {
            accumulatedTime = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Fire()
    {
        if (!CanFire()) return;
        SoundManager.instance.PlayAttack();
        Debug.Log("StartFire");
        Bullet bullet = new Bullet();

        GameObject prefab = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.Init(prefab, firePoint.up, bulletSpeed, hitLayer);

        bullets.Add(bullet);
    }
    void UpdateBullet(float deltaTime)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            Collider2D coll = bullets[i].Move(deltaTime);
            if (coll != null)
            {
                if (coll.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(bulletDamage);
                }
                bullets[i].DestroyBullet();
                bullets.RemoveAt(i);
                i--;
            }
            else if (bullets[i].IsMaxDistance(bulletMaxDistance))
            {
                bullets[i].DestroyBullet();
                bullets.RemoveAt(i);
                i--;
            }
        }

    }


}
