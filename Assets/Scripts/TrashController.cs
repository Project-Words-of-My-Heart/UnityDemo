using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashController : Interactable
{
    [SerializeField] private Vector2 m_Offset;
    [SerializeField] private TextMeshProUGUI m_TextObject;

    private bool m_isChecked = false;

    public override IEnumerator Interact()
    {
        if (!m_isChecked) { m_TextObject.text = "别翻垃圾桶，说真的。"; m_isChecked = true; }
        else { m_TextObject.text = "叫你别翻你还翻？"; }
        m_TextObject.enabled = true;

        yield return new WaitForSeconds(3);

        m_TextObject.enabled = false;
    }
}
