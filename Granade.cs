using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    #region Variable

    [SerializeField] private Rigidbody2D _rig;

    private Skill _skill;

    private float _damage;

    #endregion



    #region Method

    public void Init(Skill skill)
    {
        _skill = skill;

        _damage = 400;
    }

    public void Spawn(Vector2 pos)
    {
        this.transform.position = pos;

        _rig.AddForce(Vector2.right * Random.Range(0.4f, 0.7f), ForceMode2D.Impulse);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);

        Bomb();

        this.gameObject.SetActive(false);
    }

    private void Bomb()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);

        _skill.Effect(this.transform.position);

        foreach(Collider2D col in collider)
        {
            if (col.tag == "Zombie")
            {
                col.GetComponent<ZombieHp>().GetDamage(_damage);
            }
        }
    }

    #endregion
}
