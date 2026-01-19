using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private Camera cam;
    [SerializeField] private List<Transform> PivotTransforms;
    [SerializeField] private Transform CurPivotTransforms;
    [SerializeField] private int CurPivot;
    private float PivotDelay;
    private float CurAngle;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
    }

    private void Start()
    {
        CurPivot = 0;
        CurPivotTransforms = PivotTransforms[CurPivot];
        cam = Camera.main;
        cam.transform.position = new Vector3(
    CurPivotTransforms.position.x + Mathf.Cos(CurAngle * Mathf.Deg2Rad) * 5f,
    5f,
    CurPivotTransforms.position.z + Mathf.Sin(CurAngle * Mathf.Deg2Rad) * 5f);
    }

    private void Update()
    {

        TargetChange();
    }

    private void LateUpdate()
    {
        cam.transform.rotation = Quaternion.Euler(new Vector3(60f, -CurAngle + 270f, 0f));
        TargetWheel();
    }

    private void TargetWheel()
    {
        bool ChangeCheck = InputManager.Instance.MiddleClickInput;
        Vector2 MoveCheck = InputManager.Instance.PointInput;

        if (ChangeCheck == false) return;

        if (MoveCheck.x < 0f)
        {
            CurAngle -= 60f * Time.deltaTime;
        }
        else if (MoveCheck.x > 0f)
        {
            CurAngle += 60f * Time.deltaTime;
        }

        cam.transform.position = new Vector3(
            CurPivotTransforms.position.x + Mathf.Cos(CurAngle * Mathf.Deg2Rad) * 5f,
            5f,
            CurPivotTransforms.position.z + Mathf.Sin(CurAngle * Mathf.Deg2Rad) * 5f);

    }

    private void TargetChange()
    {
        PivotDelay -= Time.deltaTime;
        if (PivotDelay <= 0f) PivotDelay = 0f;
        if (PivotDelay > 0f) return;


        if (PivotTransforms.Count == 0)
        {
            Debug.LogWarning("Pivot Target is null");
            return;
        }

        bool ChangeCheck = InputManager.Instance.LeftClickInput;

        if (ChangeCheck)
        {
            PivotDelay = 1.25f;
            InputManager.Instance.LeftClickInput = false;
            ChangeCheck = false;

            CurPivot++;
            if (CurPivot >= PivotTransforms.Count) CurPivot = 0;

            CurPivotTransforms = PivotTransforms[CurPivot];
            cam.transform.position = new Vector3(
    CurPivotTransforms.position.x + Mathf.Cos(CurAngle * Mathf.Deg2Rad) * 5f,
    5f,
    CurPivotTransforms.position.z + Mathf.Sin(CurAngle * Mathf.Deg2Rad) * 5f);
        }
    }

}