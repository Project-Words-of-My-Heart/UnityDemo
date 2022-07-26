using System.Collections;
using UnityEngine;
using TMPro;

public class ColliderMessenger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProGui;
    [SerializeField] private string _message;

    // Start is called before the first frame update
    private void Start()
    {
        _textMeshProGui.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        _textMeshProGui.text = _message;
        _textMeshProGui.enabled = true;

        yield return new WaitForSeconds(3);
        _textMeshProGui.enabled = false;
    }
}