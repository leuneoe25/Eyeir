using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectArea : MonoBehaviour
{
    public void OnCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void OffCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
