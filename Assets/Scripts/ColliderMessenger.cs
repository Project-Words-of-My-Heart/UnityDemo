using System.Collections;
using UnityEngine;
using TMPro;

public class ColliderMessenger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProGUI;
    [SerializeField] private string m_Message;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProGUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        m_TextMeshProGUI.text = m_Message;
        m_TextMeshProGUI.enabled = true;

        yield return new WaitForSeconds(3);
        m_TextMeshProGUI.enabled = false;
    }
}
