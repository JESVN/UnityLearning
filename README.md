## UnityLearning
## 一些学习项目

## 目录

* [1.平滑移动](#1.平滑移动)
* [2.移动的线段](#2.移动的线段)

## 1.平滑移动
> **t值变化呈现出的不同效果**
> 在面板上切换枚举，移动将呈现不同的效果

```csharp
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

```

## 2.移动的线段

```csharp
using System.Collections.Generic;
using UnityEngine;

public struct DrawLinePos
{
    public Vector3 start;
    public Vector3 end;
}

public class SampleVectorMove : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// 线段数量
    /// </summary>
    public int count = 10;

    /// <summary>
    /// 每条线段长度
    /// </summary>
    public int length = 1;

    /// <summary>
    /// 线段间隔
    /// </summary>
    public float interval=1;

    private List<DrawLinePos> drawLinePosList=new List<DrawLinePos>();
    private void Start()
    {
        //Init();       
    }

    // Update is called once per frame
    private void Update()
    {
        DrawLineTriangle();
        DrawLineMove();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        drawLinePosList.Clear();
        for (int i = 0; i < count*2; i++)
        {
            if (i % 2 == 0)
            {
                var start = transform.position + transform.forward * i * length;
                var end = start + transform.forward * length;
                DrawLinePos drawLinePos;
                drawLinePos.start = start;
                drawLinePos.end = end;
                drawLinePosList.Add(drawLinePos);
                //Debug.DrawLine(drawLinePos.start, drawLinePos.end, Color.green,3f);
            }
        }
    }

    /// <summary>
    /// 三角形
    /// </summary>
    private void DrawLineTriangle()
    {
        var right = transform.position + (transform.forward + transform.right) * 10;
        var left = transform.position + (transform.forward - transform.right) * 10;
        Debug.DrawLine(transform.position, right, Color.green);
        Debug.DrawLine(transform.position, left, Color.green);
        Debug.DrawLine(right, left, Color.green);
    }

    float move;
    /// <summary>
    /// 线移动——直线
    /// </summary>
    private void DrawLineMove()
    {
        float nextStartLength = 0;
        for (int i = 0; i < count; i++)
        {
            var start = transform.position + transform.forward* (nextStartLength+ move);
            var end= start + transform.forward * length;
            nextStartLength += interval+length;
            move += Time.deltaTime * speed * 1/count;
            Debug.DrawLine(start , end, Color.green);
            if (i==0&&Vector3.Distance(transform.position,start)>= interval + length)
            {
                move = 0;
            }
        }
    }
}

```