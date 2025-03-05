using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    #region Variable

    [SerializeField] private ZombieManager _zombies;

    [SerializeField] private GameObject _muzzle;

    private float _coolTime = 2f;

    private float _currentTime = 0f;

    [SerializeField] private Bullet _bullet;

    private List<Bullet> _bullets = new List<Bullet>();

    #endregion



    #region Method

    private void Init()
    {
        for(int i = 0; i < 5; i++)
        {
            _bullets.Add(Instantiate(_bullet));

            _bullets[i].gameObject.SetActive(false);
        }
    }

    private void Aim()
    {
        Vector2 target = _zombies.GetZombie(this.transform.position);

        if(target == Vector2.zero)
        {
            return;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y - this.transform.position.y, target.x - this.transform.position.x) * Mathf.Rad2Deg - 32f);
        }
    }

    private void Fire(float rot)
    {
        for(int i = 0; i <_bullets.Count; i++)
        {
            if(!_bullets[i].gameObject.activeSelf)
            {
                _bullets[i].gameObject.SetActive(true);

                _bullets[i].Init(_muzzle.transform.position, rot);

                return;
            }
        }

        _bullets.Add(Instantiate(_bullet));

        _bullets[_bullets.Count - 1].Init(_muzzle.transform.position, rot);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Aim();

        _currentTime += Time.deltaTime;

        if(_currentTime >= _coolTime)
        {
            _currentTime = 0f;

            for(int i = 0; i < 3; i++)
            {
                Fire(this.transform.eulerAngles.z + 24f + 8 * i);
            }
        }
    }

    #endregion
}
