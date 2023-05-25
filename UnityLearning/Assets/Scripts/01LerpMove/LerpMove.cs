using UnityEngine;

public class LerpMove : MonoBehaviour
{
    public enum TChange
    {
        None,
        Change0,
        Change1,
        Change2,
    }
    [SerializeField]TChange change;
    [SerializeField] float moveTime = 1f;
    float animTime;
    bool isAnimating;
    Vector3 posStart;
    Vector3 posTarget;
    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
            Animate();
        else
            CheckInput();
    }


    void Animate()
    {
        animTime += Time.deltaTime;
        float t = Mathf.Clamp01(animTime / moveTime);

        ChangeT(ref t);

        transform.position = Vector3.Lerp(posStart, posTarget, t);
        if (t >= 1f)
            isAnimating = false;
    }

    void ChangeT(ref float t)
    {
        switch (change)
        {
            case TChange.None:
                break;
            case TChange.Change0:
                t = QuaEaseIn(t);
                break;
            case TChange.Change1:
                t = QuaEaseOut(t);
                break;
            case TChange.Change2:
                t = CubicInOut(t);
                break;
            default:
                break;
        }
    }

    float QuaEaseIn(float t) => t * t;
    float QuaEaseOut(float t) => 1 - QuaEaseIn(1f - t);
    float CubicInOut(float t) => t * t * (3 - 2 * t);

    void CheckInput()
    {
        void Move(KeyCode keyCode, Vector3 dir)
        {
            if (Input.GetKeyDown(keyCode))
            {
                posStart = transform.position;
                posTarget = posStart + dir;
                isAnimating = true;
                animTime = 0;
            }
        }
        Move(KeyCode.W, Vector3.up);
        Move(KeyCode.S, Vector3.down);
        Move(KeyCode.A, Vector3.left);
        Move(KeyCode.D, Vector3.right);
    }
}
