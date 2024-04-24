using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCenter : MonoBehaviour
{
    public static ConfigCenter Instance;
    [SerializeField] private PlayerSettingConfig playerSetting;
    [SerializeField] private ConfigMovementSO configMovement;
    [SerializeField] private WeaponMovementConfigSO configWeaponMovement;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public PlayerSettingConfig GetPlayerSetting()
    {
        return playerSetting;
    }
    public ConfigMovementSO GetConfigMovement()
    {
        return configMovement;
    }

    public WeaponMovementConfigSO GetConfigMovementWeapon()
    {
        return configWeaponMovement;
    }
}
