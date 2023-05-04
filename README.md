<html>
    <head>
        <meta charset="utf-8">
        <title>My Markdown File</title>
        <link rel="stylesheet" href="BackgroundColor.css">
    </head>
    <body>
        <!-- 在这里添加Markdown文本 -->
    </body>
</html>
# UnityLearning
一些学习项目

# 1.平滑移动
> **优化t值的函数**
> 可以在QuaEaseIn、QuaEaseOut、CubicInOut三个函数之间切换看看效果，以及不执行这三个函数的效果

```csharp
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