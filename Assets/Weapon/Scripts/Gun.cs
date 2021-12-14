using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum GunType
{ 
    AK47,
}
public class Gun : MonoBehaviour
{
    [SerializeField] private GunType type;
    public GunType Type { get { return type; } }

    //weapon information

    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitLayer;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite handSprite;
    [SerializeField] private Sprite groundSprite;

    [Header("Bullet Info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletInterval;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletMaxDistance;

    [SerializeField] private int totalAmmoCount;
    [SerializeField] private int ammoCount;
    [SerializeField] private int clipSize;

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

    [Header("Reload Info")]
    [SerializeField] private float reloadDuration = 1;



    [Header("Effect")]
    [SerializeField] private ParticleSystem hitPlayerEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem muzzleFlash;

    
    public event Action<int, int> bulletUpdateHandler;


    private float accumulatedTime;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Reload();
        }
        accumulatedTime += Time.deltaTime;
        UpdateBullet(Time.deltaTime);
    }
    private bool CanFire()
    {
        if (accumulatedTime >= bulletInterval && ammoCount > 0)
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
        Bullet bullet = new Bullet();

        GameObject prefab = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.Init(prefab, firePoint.up, bulletSpeed, hitLayer);

        bullets.Add(bullet);

        ammoCount--;
        bulletUpdateHandler?.Invoke(ammoCount, totalAmmoCount);

        CheckReload();

    }
    private void CheckReload()
    {
        if (ammoCount <= 0 && totalAmmoCount > 0)
        {
            Reload();
        }
    }
    private void Reload()
    {
        StartCoroutine(IEnum_Reload());
    }
    private IEnumerator IEnum_Reload()
    {
        int ammo = clipSize - ammoCount;
        if (totalAmmoCount < ammo) ammo = totalAmmoCount;
        yield return new WaitForSeconds(reloadDuration);
        totalAmmoCount -= ammo;
        ammoCount += ammo;
        bulletUpdateHandler?.Invoke(ammoCount, totalAmmoCount);
    }
    private void UpdateBullet(float deltaTime)
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

    public void SetOnHand()
    {
        CheckReload();
        enabled = true;
        sp.sprite = handSprite;
    }
    public void SetOnGround()
    {
        StopAllCoroutines();
        enabled = false;
        sp.sprite = groundSprite;
    }
}
