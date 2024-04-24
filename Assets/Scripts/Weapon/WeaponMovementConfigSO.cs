using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigWeaponMovementSO", menuName = "Config/Config Weapon Movement")]
public class WeaponMovementConfigSO : ScriptableObject
{
    [Title("Sway")]
    public float SwayAmount;
    public float SwaySmoothing;
    public float SwayResetSmoothing;
    public float SwayClampX;
    public float SwayClampY;
    
    [Title("Movement Sway")]
    public float MovementSwayAmountX;
    public float MovementSwayAmountY;
    public float MovementSwaySmoothing;
    
    [Title("Idle Sway")]
    public float swayAmountA;
    public float swayAmountB;
    public float swayScale;
    public float swayLerpSpeed;
}
