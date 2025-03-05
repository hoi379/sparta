using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHp : MonoBehaviour
{
    #region Variable

    [SerializeField] private float _maxHp;

    private float _currentHp;

    [SerializeField] private GameObject _hpCanvas;

    [SerializeField] private Slider _hpBar;

    [SerializeField] private SpriteRenderer _effect;

    private bool _isEffect;

    private float _alpha = 0f;

    #endregion



    #region Method

    private void Init()
    {
        _currentHp = _maxHp;

        _isEffect = false;
    }

    public void GetDamage(float damage)
    {
        if(!_hpCanvas.activeSelf)
        {
            _hpCanvas.SetActive(true);
        }

        _currentHp -= damage;

        _hpBar.value = _currentHp / _maxHp;

        if(!_isEffect)
        {
            StartCoroutine(EffectTimer());
        }
    }

    private IEnumerator EffectTimer()
    {
        _alpha = 0.7f;

        _isEffect = true;

        yield return new WaitForSeconds(0.7f);

        _isEffect = false;

        _alpha = 0f;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if(_isEffect)
        {
            if(_alpha >= 0f)
            {
                _alpha -= Time.deltaTime;
            }

            _effect.color = new Color(1, 1, 1, _alpha);
        }
    }

    #endregion
}
