using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHp : MonoBehaviour
{
    #region Variable

    [SerializeField] private Animator _anim;

    [SerializeField] private HPBar _hp;

    private float _maxHP;

    private float _currentHP;

    private ZombieManager _manager;

    #endregion



    #region Method

    public void Init(GameObject canvas, ZombieManager manager)
    {
        _manager = manager;

        _hp = Instantiate(_hp, canvas.transform);

        _maxHP = 300f;

        _hp.gameObject.SetActive(false);
    }

    public void Spawn()
    {
        _currentHP = _maxHP;

        _hp.gameObject.SetActive(false);
    }

    public void GetDamage(float damage)
    {
        _manager.DamageTextSpawn(this.transform.position, damage);

        if(!_hp.gameObject.activeSelf)
        {
            _hp.gameObject.SetActive(true);
        }

        _currentHP -= damage;

        _hp.GaugeAmount(_currentHP / _maxHP);

        if(_currentHP < 0f)
        {
            _hp.gameObject.SetActive(false);

            _anim.SetBool("IsDead", true);

            StartCoroutine(DeadTimer());
        }
    }

    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_hp.gameObject.activeSelf)
        {
            _hp.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 1.5f, 0));
        }
    }

    #endregion
}
