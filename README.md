# UnityLearning
一些学习项目

# 1.平滑移动
> **优化t值的函数**

```cs
using UnityEngine;

public class LerpMove : MonoBehaviour
{
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

        t = CubicInOut(t);

        transform.position = Vector3.Lerp(posStart, posTarget, t);
        if (t >= 1f)
            isAnimating = false;
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

```