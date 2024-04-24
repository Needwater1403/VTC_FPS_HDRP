using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Parameters

    // ----------SWAY----------
    private Vector3 weaponRotation;
    private Vector3 weaponRotationVelocity;
    
    private Vector3 targetWeaponRotation;
    private Vector3 targetWeaponRotationVelocity;
    
    // ----------MOVEMENT SWAY----------
    private Vector3 weaponMovementRotation;
    private Vector3 weaponMovementRotationVelocity;
    
    private Vector3 targetWeaponMovementRotation;
    private Vector3 targetWeaponMovementRotationVelocity;
    
    // ----------ANIMATION----------
    [SerializeField] private Animator weaponAnimator;
    private float animatorSpeed;
    private static readonly int Sprint = Animator.StringToHash("IsSprint");
    
    // ----------IDLE SWAY----------
    [SerializeField] private Transform weaponSway;
    private float swayTime;
    private Vector3 swayPos;
    
    #endregion
    
    private void Update()
    {
        if (ReceiveInput.Instance.SprintInputValue == false)
        {
            animatorSpeed = GameManager.Instance.Player._characterController.velocity.magnitude/ ConfigCenter.Instance.GetConfigMovement().walkFowardSpeed;
            if(animatorSpeed > 1)
            {
                animatorSpeed = 1;
            }
            weaponAnimator.speed = animatorSpeed;
        }
        else
        {
            animatorSpeed = GameManager.Instance.Player._characterController.velocity.magnitude/ ConfigCenter.Instance.GetConfigMovement().sprintSpeed;
            if(animatorSpeed > 1)
            {
                animatorSpeed = 1;
            }
            weaponAnimator.speed = animatorSpeed;
        }
        
        weaponSway.localPosition = IdleSway();
        weaponAnimator.SetBool(Sprint,ReceiveInput.Instance.SprintInputValue);
        transform.localRotation = Quaternion.Euler(Sway() + MovementSway());
    }

    private Vector3 IdleSway()
    {
        swayTime += Time.deltaTime;
        if (swayTime > 6.3f)
        {
            swayTime = 0;
        }
        var targetPos = LissajousCurve(swayTime, ConfigCenter.Instance.GetConfigMovementWeapon().swayAmountA, 
            ConfigCenter.Instance.GetConfigMovementWeapon().swayAmountB) / ConfigCenter.Instance.GetConfigMovementWeapon().swayScale;
        return swayPos = Vector3.Lerp(swayPos, targetPos, Time.smoothDeltaTime * ConfigCenter.Instance.GetConfigMovementWeapon().swayLerpSpeed);
    }

    private Vector3 Sway()
    {
        targetWeaponRotation.x -= ConfigCenter.Instance.GetConfigMovementWeapon().SwayAmount * ReceiveInput.Instance.LookInputValue.y * Time.deltaTime;
        targetWeaponRotation.y += ConfigCenter.Instance.GetConfigMovementWeapon().SwayAmount * ReceiveInput.Instance.LookInputValue.x  * Time.deltaTime;
        targetWeaponRotation.x = Mathf.Clamp(targetWeaponRotation.x, -ConfigCenter.Instance.GetConfigMovementWeapon().SwayClampX, ConfigCenter.Instance.GetConfigMovementWeapon().SwayClampX);
        targetWeaponRotation.y = Mathf.Clamp(targetWeaponRotation.x, -ConfigCenter.Instance.GetConfigMovementWeapon().SwayClampY, ConfigCenter.Instance.GetConfigMovementWeapon().SwayClampY);
        targetWeaponRotation = Vector3.SmoothDamp(targetWeaponRotation, Vector3.zero, ref targetWeaponRotationVelocity,
            ConfigCenter.Instance.GetConfigMovementWeapon().SwayResetSmoothing);
        return weaponRotation = Vector3.SmoothDamp(weaponRotation, targetWeaponRotation, ref weaponRotationVelocity,
            ConfigCenter.Instance.GetConfigMovementWeapon().SwaySmoothing);
    }

    private Vector3 MovementSway()
    {
        targetWeaponMovementRotation.z = ConfigCenter.Instance.GetConfigMovementWeapon().MovementSwayAmountX * ReceiveInput.Instance.MovementInputValue.x;
        targetWeaponMovementRotation.y = ConfigCenter.Instance.GetConfigMovementWeapon().MovementSwayAmountY * ReceiveInput.Instance.MovementInputValue.y;
        targetWeaponMovementRotation = Vector3.SmoothDamp(targetWeaponMovementRotation, Vector3.zero, ref targetWeaponMovementRotationVelocity,
            ConfigCenter.Instance.GetConfigMovementWeapon().MovementSwaySmoothing);
        return weaponMovementRotation = Vector3.SmoothDamp(weaponMovementRotation, targetWeaponMovementRotation, ref weaponMovementRotationVelocity,
            ConfigCenter.Instance.GetConfigMovementWeapon().MovementSwaySmoothing);
    }
    
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }
}
