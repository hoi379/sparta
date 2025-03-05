using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    public void Init(Vector2 pos)
    {
        this.transform.position = pos;

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }
}
