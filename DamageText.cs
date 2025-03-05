using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Vector3 _force;

    public void Spawn(Vector3 pos, float damage)
    {
        this.transform.position = Camera.main.WorldToScreenPoint(pos + new Vector3(0f, 1f, 0f));

        _text.text = damage.ToString("n0");

        StartCoroutine(LifeTimer());

        _force.x = Random.Range(-100f, 100f);

        _force.y = 400f;
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        _force.y -= 700f * Time.deltaTime;

        this.transform.position += _force * Time.deltaTime;
    }
}
