using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashController : Interactable
{
    [SerializeField] private Vector2 _offset;
    [SerializeField] private TextMeshProUGUI _textObject;

    private bool _isChecked = false;

    public override IEnumerator Interact()
    {
        if (!_isChecked)
        {
            _textObject.text = "别翻垃圾桶，说真的。";
            _isChecked = true;
        }
        else
        {
            _textObject.text = "叫你别翻你还翻？";
        }

        _textObject.enabled = true;

        yield return new WaitForSeconds(3);

        _textObject.enabled = false;
    }
}