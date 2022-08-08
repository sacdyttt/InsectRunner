using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float LimitOfMovment;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    private void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        if (transform.position.x <= LimitOfMovment && transform.position.x >= -LimitOfMovment)
        { 
            transform.Translate(swerveAmount, 0, 0); 
            if(transform.position.x > LimitOfMovment)
            {
                transform.position = new Vector3(LimitOfMovment, transform.position.y,transform.position.z);
            }
            if(transform.position.x < -LimitOfMovment)
            {
                transform.position = new Vector3(-LimitOfMovment, transform.position.y, transform.position.z);
            }
        }

    }
}
