using UnityEngine;
using UnityEngine.InputSystem;

public class Ozisan : MonoBehaviour
{
    public Vector2 Velocity;
    public Animator Animator;
    public SpriteRenderer Renderer;
    public bool isWalk;
    public Vector2 mousePoint;
    public Vector3 targetPoint;
    public GameObject weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetFloat("Velocity", Mathf.Abs(Velocity.x) * (isWalk ? 0.5f : 1));

        if (Velocity.x <= -0.1f) Renderer.flipX = true;
        else if (0.1f <= Velocity.x) Renderer.flipX = false;

        float speed = Velocity.x * 5 * (isWalk ? 0.5f : 1) * Time.deltaTime;
        transform.Translate(speed, 0, 0);

        targetPoint = GetMousePoint();

        float diffX = targetPoint.x - transform.position.x;
        float diffY = targetPoint.x - transform.position.y;
        float radian = Mathf.Atan2(diffY, diffX);

        float dx = Mathf.Cos(radian);
        float dy = Mathf.Sin(radian);

        weapon.transform.eulerAngles = new Vector3(0, 0, radian * 180 / Mathf.PI);
    }

    void OnMove(InputValue value)
    {
        Velocity = value.Get<Vector2>();
    }
    void OnClick(InputValue value)
    {
        isWalk = value.Get<float>() > 0.5;
    }
    void OnMousePoint(InputValue value)
    {
        mousePoint = value.Get<Vector2>();
            }

    Vector2 GetMousePoint()
    {
        Vector2 mousePoint = this.mousePoint;

        Camera camera = Camera.main;

        Vector3 worldPoint = camera.ScreenToWorldPoint(mousePoint);

        return worldPoint;
    }
 
}
