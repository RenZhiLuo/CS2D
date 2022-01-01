using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum GunType
{ 
    AK47,
    Shotgun,
}
public class Gun : MonoBehaviour
{
    [SerializeField] private GunType type;
    public GunType Type { get { return type; } }

    [SerializeField] private Collider2D gunColl;

    [SerializeField] private Transform[] firePoints;
    [SerializeField] private LayerMask hitLayer;

    [Header("Gun Sprites")]
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite handSprite;
    [SerializeField] private Sprite groundSprite;

    [Header("Bullet Info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletInterval = 0.1f;
    [SerializeField] private float bulletSpeed = 40;
    [SerializeField] private float bulletDamage = 10;
    [SerializeField] private float bulletMaxDistance = 50;

    [SerializeField] private float recoil = 0.1f;

    [SerializeField] private int totalAmmoCount = 10000;
    [SerializeField] private int ammoCount = 100;
    [SerializeField] private int clipSize = 100;

    [Header("Reload Info")]
    [SerializeField] private float reloadDuration = 1;
    private bool isReloading;


    [Header("Effect")]
    [SerializeField] private ParticleSystem hitPlayerEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private Sound shootSound;
    [SerializeField] private Sound reloadSound;

    public event Action<int, int, int> bulletUpdateHandler;


    private float shootAccumulatedTime;


    public class Bullet
    {
        private GameObject prefab;
        private Vector3 direction;
        private float damage;
        private float speed;
        private float maxDistance;
        private float accumulatedDistance;
        private LayerMask layer;
        private Vector3 Position { get { return prefab.transform.position; } set { prefab.transform.position = value; } }
        public void Init(GameObject prefab, Vector3 direction, float speed, float damage, float maxDistance, LayerMask layer)
        {
            this.prefab = prefab;
            this.direction = direction;
            this.speed = speed;
            this.damage = damage;
            this.maxDistance = maxDistance;
            this.layer = layer;
        }
        public RaycastHit2D Move(float time)
        {
            Vector3 displacement = direction * speed * time;
            float distance = displacement.magnitude;
            
            RaycastHit2D hit = Physics2D.Raycast(Position, direction, distance, layer);
            Position += displacement;
            accumulatedDistance += distance;
            return hit;
        }
        public bool IsMaxDistance()
        {
            return accumulatedDistance >= maxDistance;
        }
        public void DestroyBullet()
        {
            Destroy(prefab);
        }
    }

    private List<Bullet> bullets = new List<Bullet>();
    private void Update()
    {
        shootAccumulatedTime += Time.deltaTime;
        UpdateBullet(Time.deltaTime);
    }
    private bool CanShoot()
    {
        if (shootAccumulatedTime >= bulletInterval && ammoCount > 0 && !isReloading)
        {
            shootAccumulatedTime = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Shoot()
    {
        if (!CanShoot()) return;

        SoundManager.instance.PlayOneShot(shootSound);
        muzzleFlash.Emit(1);

        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject prefab = Instantiate(bulletPrefab, firePoints[i].position, Quaternion.identity);

            Bullet bullet = new Bullet();

            bullet.Init(prefab, firePoints[i].up, bulletSpeed, bulletDamage, bulletMaxDistance, hitLayer);

            bullets.Add(bullet);

            float recoilX = UnityEngine.Random.Range(-this.recoil, this.recoil);

            bullet.Init(prefab, firePoints[i].up + firePoints[i].right * recoilX, bulletSpeed, bulletDamage, bulletMaxDistance, hitLayer);

        }

        ammoCount--;

        bulletUpdateHandler?.Invoke(clipSize, ammoCount, totalAmmoCount);

        CheckReload();
    }
    private void UpdateBullet(float deltaTime)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            RaycastHit2D hit = bullets[i].Move(deltaTime);

            if (hit)
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(bulletDamage);
                }
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = hit.normal;
                hitEffect.Emit(1);
                bullets[i].DestroyBullet();
                bullets.RemoveAt(i);
                i--;
            }
            else if (bullets[i].IsMaxDistance())
            {
                bullets[i].DestroyBullet();
                bullets.RemoveAt(i);
                i--;
            }
        }

    }
    private void CheckReload()
    {
        if (ammoCount <= 0 && totalAmmoCount > 0)
        {
            Reload();
        }
    }

    public bool Reload()
    {
        if (ammoCount < clipSize && totalAmmoCount > 0 && !isReloading)
        {
            StartCoroutine(IEnum_Reload());
            return true;
        }
        return false;
        IEnumerator IEnum_Reload()
        {
            SoundManager.instance.PlayOneShot(reloadSound);
            isReloading = true;
            int ammo = clipSize - ammoCount;
            if (totalAmmoCount < ammo) ammo = totalAmmoCount;
            yield return new WaitForSeconds(reloadDuration);
            totalAmmoCount -= ammo;
            ammoCount += ammo;
            bulletUpdateHandler?.Invoke(clipSize, ammoCount, totalAmmoCount);
            isReloading = false;
            SoundManager.instance.PlayOneShot(reloadSound);
        }
    }

    public void SetOnHand()
    {
        bulletUpdateHandler?.Invoke(clipSize, ammoCount, totalAmmoCount);
        CheckReload();
        enabled = true;
        sp.sprite = handSprite;
        gunColl.enabled = false;
    }
    public void SetOnGround()
    {
        StopAllCoroutines();
        isReloading = false;
        enabled = false;
        sp.sprite = groundSprite;
        gunColl.enabled = true;
    }
}
