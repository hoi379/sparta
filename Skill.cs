using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    #region Variable

    [SerializeField] private Image _mpGauge;

    [SerializeField] private Text _mpText;

    [SerializeField] private GameObject _hand;

    [SerializeField] private Granade _granade;

    private List<Granade> _granades = new List<Granade>();

    [SerializeField] private EffectTimer _effect;

    private List<EffectTimer> _effects = new List<EffectTimer>();

    private float _coolTime;

    private float _currentTime;

    private int _mp;

    #endregion



    #region Method

    private void Init()
    {
        _coolTime = 8f;

        _currentTime = 0f;

        _mp = 0;

        _mpText.text = _mp.ToString();

        for(int i = 0; i < 3; i++)
        {
            _granades.Add(Instantiate(_granade));

            _granades[i].Init(this);

            _granades[i].gameObject.SetActive(false);



            _effects.Add(Instantiate(_effect));

            _effects[i].gameObject.SetActive(false);
        }
    }

    public void Granade()
    {
        if(_mp >= 2)
        {
            _mp -= 2;

            for (int i = 0; i < _granades.Count; i++)
            {
                if (!_granades[i].gameObject.activeSelf)
                {
                    _granades[i].gameObject.SetActive(true);

                    _granades[i].Spawn(_hand.transform.position);

                    return;
                }
            }

            _granades.Add(Instantiate(_granade));

            _granades[_granades.Count - 1].Init(this);

            _granades[_granades.Count - 1].Spawn(_hand.transform.position);
        }
    }

    public void Effect(Vector2 pos)
    {
        for(int i = 0; i < _effects.Count; i++)
        {
            if(!_effects[i].gameObject.activeSelf)
            {
                _effects[i].gameObject.SetActive(true);

                _effects[i].Init(pos);

                return;
            }
        }

        _effects.Add(Instantiate(_effect));

        _effects[_effects.Count - 1].Init(pos);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        _mpGauge.fillAmount = _currentTime / _coolTime;

        if(_currentTime >= _coolTime)
        {
            _currentTime = 0f;

            _mp++;

            _mpText.text = _mp.ToString();
        }
    }

    #endregion
}
