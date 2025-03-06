using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieManager : MonoBehaviour
{
    #region Varaible

    [SerializeField] private GameObject _canvas;

    [SerializeField] private Zombie _zombie;

    private List<Zombie> _zombies = new List<Zombie>();

    [SerializeField] private List<GameObject> _lines;

    #endregion



    #region Method

    private void Init()
    {
        for(int i = 0; i < 20; i++)
        {
            _zombies.Add(Instantiate(_zombie));

            _zombies[i].gameObject.SetActive(false);

            _zombies[i].GetComponent<ZombieHp>().Init(_canvas, this);
        }
    }

    private void SpawnZombie()
    {
        int line = Random.Range(0, _lines.Count);

        for(int i = 0; i < _zombies.Count; i++)
        {
            if(!_zombies[i].gameObject.activeSelf)
            {
                _zombies[i].gameObject.SetActive(true);

                _zombies[i].Init(_lines[line].transform.position, line);

                _zombies[i].transform.SetParent(_lines[line].transform);

                _zombies[i].GetComponent<ZombieHp>().Spawn();

                return;
            }
        }

        _zombies.Add(Instantiate(_zombie));

        _zombies[_zombies.Count - 1].GetComponent<ZombieHp>().Init(_canvas, this);

        _zombies[_zombies.Count - 1].GetComponent<ZombieHp>().Spawn();

        _zombies[_zombies.Count - 1].Init(_lines[line].transform.position, line);
    }

    private IEnumerator SpawnTimer()
    {
        SpawnZombie();

        yield return new WaitForSeconds(2f);

        StartCoroutine(SpawnTimer());
    }

    public Vector2 GetZombie(Vector2 pos)
    {
        float distance = float.MaxValue;

        Vector2 zombiePos = Vector2.zero;
        
        foreach(Zombie zom in _zombies)
        {
            if(zom.gameObject.activeSelf == true)
            {
                if(Vector2.Distance(zom.transform.position, pos) < distance)
                {
                    distance = Vector2.Distance(zom.transform.position, pos);

                    zombiePos = zom.transform.position;
                }
            }
        }

        return zombiePos;
    }

    private void Start()
    {
        Init();

        InitDamageText();

        StartCoroutine(SpawnTimer());
    }

    #endregion



    #region DamageText

    [SerializeField] private DamageText _damageText;

    private List<DamageText> _damageTexts = new List<DamageText>();

    private void InitDamageText()
    {
        for(int i = 0; i < 5; i++)
        {
            _damageTexts.Add(Instantiate(_damageText, _canvas.transform));

            _damageTexts[i].gameObject.SetActive(false);
        }
    }

    public void DamageTextSpawn(Vector3 pos, float damage)
    {
        for(int i = 0; i < _damageTexts.Count; i++)
        {
            if(!_damageTexts[i].gameObject.activeSelf)
            {
                _damageTexts[i].gameObject.SetActive(true);

                _damageTexts[i].Spawn(pos, damage);

                return;
            }
        }

        _damageTexts.Add(Instantiate(_damageText, _canvas.transform));

        _damageTexts[_damageTexts.Count - 1].Spawn(pos, damage);
    }

    #endregion
}
