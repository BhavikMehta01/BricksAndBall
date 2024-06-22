using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public static ShootManager instance;
    public bool isFire = true;
    public float waitForShoot;
    public int score;

    public GameObject bulletPrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    GameObject spwanBullate;
    bool isRight;
    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            if (!PlayerObjectManager.instance.isPlayerDouble)
            {
                spwanBullate = Instantiate(bulletPrefab);
                spwanBullate.transform.position = transform.position;
                isFire = false;
                if (!PlayerObjectManager.instance.isPlayerSpeedUp) StartCoroutine(WaitForShooting(waitForShoot));
                else StartCoroutine(WaitForShooting(waitForShoot / 1.5f));
            }else
            {
                if (isRight)
                {
                    Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                    bullet.transform.position = transform.position;
                    isRight = false;
                    bullet.enabled = false;
                    bullet.gameObject.AddComponent<BulletMoveRight>();
                }
                else
                {
                    Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                    bullet.transform.position = transform.position;
                    isRight = true;
                    bullet.enabled = false;
                    bullet.gameObject.AddComponent<BulletMoveLeft>();
                }
                isFire = false;
                
                StartCoroutine(WaitForShooting(waitForShoot / 5f));
            }
        }
    }

    public IEnumerator WaitForShooting(float _waitForShoot)
    {
        yield return new WaitForSeconds(_waitForShoot);
        isFire = true;
    }

}
