using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    [Header("Interactable")]
    [SerializeField] private GameObject m_InteractableText;
    [SerializeField] private Vector2 m_BoxSize;
    [SerializeField] private Vector2 m_TextOffset;

    private float horizontalSpeed, verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_InteractableText.SetActive(false);
        m_InteractableText.GetComponent<TextMesh>().text = "°´F½»»¥";
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();

        if (Input.GetKeyDown(KeyCode.F)) { CheckInteraction(); }
        m_InteractableText.transform.position = new Vector2(transform.position.x + m_TextOffset.x, transform.position.y + m_TextOffset.y);
    }

    void FixedUpdate()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * m_MoveSpeed * Time.deltaTime;
        verticalSpeed = Input.GetAxisRaw("Vertical") * m_MoveSpeed * Time.deltaTime;

        if (horizontalSpeed > 0) { m_SpriteRenderer.flipX = false; }
        else if (horizontalSpeed < 0) { m_SpriteRenderer.flipX = true; }

        transform.position = new Vector2(transform.position.x + horizontalSpeed, transform.position.y + verticalSpeed);
    }

    private void setAnimation()
    {
        if ((horizontalSpeed != 0 || verticalSpeed != 0) && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("jump")) { m_Animator.SetBool("isRunning", true); }
        else { m_Animator.SetBool("isRunning", false); }

        if (Input.GetButtonDown("Jump") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetTrigger("jump");
        }
    }

    public void ShowInteractableMessage()
    {
        m_InteractableText.SetActive(true);
    }

    public void UnshowInteractableMessage()
    {
        m_InteractableText.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, m_BoxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D raycastHit2D in hits)
            {
                if (raycastHit2D.transform.GetComponent<Interactable>()) { StartCoroutine(raycastHit2D.transform.GetComponent<Interactable>().Interact()); return; }
            }
        }
    }
}
