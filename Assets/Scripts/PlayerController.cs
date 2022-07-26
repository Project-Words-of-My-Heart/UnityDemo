using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Interactable")] 
    [SerializeField] private GameObject _interactableText;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector2 _textOffset;

    private float _horizontalSpeed, _verticalSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _interactableText.SetActive(false);
        _interactableText.GetComponent<TextMesh>().text = "按F交互";
    }

    // Update is called once per frame
    private void Update()
    {
        SetAnimation();

        if (Input.GetKeyDown(KeyCode.F)) CheckInteraction();
        _interactableText.transform.position =
            new Vector2(transform.position.x + _textOffset.x, transform.position.y + _textOffset.y);
    }

    private void FixedUpdate()
    {
        _horizontalSpeed = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        _verticalSpeed = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;

        if (_horizontalSpeed > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontalSpeed < 0) _spriteRenderer.flipX = true;

        transform.position =
            new Vector2(transform.position.x + _horizontalSpeed, transform.position.y + _verticalSpeed);
    }

    private void SetAnimation()
    {
        if ((_horizontalSpeed != 0 || _verticalSpeed != 0) && !_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            _animator.SetBool("isRunning", true);
        else
            _animator.SetBool("isRunning", false);

        if (Input.GetButtonDown("Jump") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            _animator.SetBool("isRunning", false);
            _animator.SetTrigger("jump");
        }
    }

    public void ShowInteractableMessage()
    {
        _interactableText.SetActive(true);
    }

    public void UnshowInteractableMessage()
    {
        _interactableText.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _boxSize, 0, Vector2.zero); 
        Debug.Log("Check!" + hits.Length.ToString());

        if (hits.Length > 0)
            foreach (RaycastHit2D raycastHit2D in hits)
                if (raycastHit2D.transform.GetComponent<Interactable>())
                {
                    StartCoroutine(raycastHit2D.transform.GetComponent<Interactable>().Interact());
                    return;
                }
    }
}