using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform characterTransform;
    private Animator animator;
    public float moveSpeed = 5f;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private bool isInteracting = false;
    public CanvasGroup shopUI;
    public Animator sellerButtonAnimator;
    public GameObject characterUI;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        movement.Normalize();

        Vector3 clampedMovement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 newPosition = transform.position + clampedMovement * moveSpeed * Time.deltaTime;
        
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;

        bool isMoving = clampedMovement.magnitude > 0.1f;
        animator.SetBool("Walk", isMoving);

        if (isMoving)
        {
            FlipCharacter(clampedMovement.x);
        }
        else
        {
            characterTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (isInteracting && Input.GetKeyDown(KeyCode.B))
        {
            InteractWithShop();
        }
    }

    private void FlipCharacter(float horizontalInput)
    {
        float rotationY = horizontalInput < 0 ? 180 : 0f;
        characterTransform.localEulerAngles = new Vector3(0f, rotationY, 0f);
    }

    private void InteractWithShop()
    {
        shopUI.alpha = 1;
        shopUI.blocksRaycasts = true;
        AudioManager.Instance.PlaySFX("OpenUI");
        characterUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.alpha = 0;
        shopUI.blocksRaycasts = false;
        AudioManager.Instance.PlaySFX("CloseUI");
        characterUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Seller"))
        {
            isInteracting = true;
            sellerButtonAnimator.SetTrigger("Show");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Seller"))
        {
            isInteracting = false;
            sellerButtonAnimator.SetTrigger("Show");
        }
    }
}
