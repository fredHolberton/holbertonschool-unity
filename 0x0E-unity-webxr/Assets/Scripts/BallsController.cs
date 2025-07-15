using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallsController : MonoBehaviour
{
    [SerializeField] private InputActionReference mouseClick;

    [SerializeField] private InputActionReference mouseMove;

    [SerializeField] private InputActionReference lateralMove;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject xrOrigin;

    [SerializeField] private float ballStrength = 100f;

    [SerializeField] private GameObject alleyFloor;

    [SerializeField] private GameObject firstBall;

    private GameObject bowlingBall;
    private Vector2 _mouseCursor;
    private bool aBallIsMoving = false;
    private bool aBallIsGrabbed = false;
    private Bounds bndFloor;
    private Vector2 direction = Vector2.zero;
    private Vector3 firstBallposition;
    private Plane dragPlane;

    private void OnEnable()
    {
        mouseClick.action.started += MousePress;
        mouseClick.action.canceled += MouseRelease;

        lateralMove.action.performed += LateralMovePerformed;
        lateralMove.action.canceled += LateralMoveCanceled;
    }

    private void OnDisable()
    {
        mouseClick.action.started -= MousePress;
        mouseClick.action.canceled -= MouseRelease;

        lateralMove.action.performed -= LateralMovePerformed;
        lateralMove.action.canceled -= LateralMoveCanceled;
    }

    private void MousePress(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(_mouseCursor);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 3);

            if (hit.collider != null)
            {
                if (hit.collider.transform.gameObject.tag == "Interactable")
                {
                    bowlingBall = hit.collider.transform.gameObject;

                    aBallIsGrabbed = true;
                    aBallIsMoving = false;
                }

            }

        }
    }

    private void MouseRelease(InputAction.CallbackContext context)
    {
        if (aBallIsGrabbed)
        {
            aBallIsGrabbed = false;
            aBallIsMoving = true;
            bowlingBall.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * ballStrength);
        }

    }

    private void LateralMovePerformed(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void LateralMoveCanceled(InputAction.CallbackContext context)
    {
        direction = Vector2.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bndFloor = alleyFloor.GetComponent<Renderer>().bounds;
        firstBallposition = firstBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseCursor = mouseMove.action.ReadValue<Vector2>();

        Vector3 ballPos;
        if ((aBallIsGrabbed && !aBallIsMoving) || aBallIsMoving)
        {
            if (aBallIsGrabbed && !aBallIsMoving)
            {
                dragPlane = new Plane(Vector3.forward, bowlingBall.transform.position);
                Ray ray = mainCamera.ScreenPointToRay(_mouseCursor);
                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    ballPos = hitPoint;
                    //ballPos = new Vector3(mousePos.x, mousePos.y, bowlingBall.transform.position.z);
                    bowlingBall.transform.position = ballPos;
                }
            }
            else if (aBallIsMoving
                    && bowlingBall.transform.position.x >= bndFloor.min.x
                    && bowlingBall.transform.position.x <= bndFloor.max.x
                    && bowlingBall.transform.position.z >= bndFloor.min.z)
            {
                float x = bowlingBall.transform.position.x + (direction.x * 0.01f);
                ballPos = new Vector3(x, bowlingBall.transform.position.y, bowlingBall.transform.position.z);
                bowlingBall.transform.position = ballPos;
            }
            else if (aBallIsMoving && (bowlingBall.transform.position.z > bndFloor.max.z || bowlingBall.transform.position.z < bndFloor.min.z))
            {
                aBallIsMoving = false;
                bowlingBall.transform.position = firstBallposition;
                bowlingBall.GetComponent<BallTrigger>().Reset();
            }

        }
    }
}