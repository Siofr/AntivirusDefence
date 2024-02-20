using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public CPUScript cpu;
    public EconomySystem economy;
    // Declare Script that holds wave number

    public GameObject target;
    private TowerScript targetTower;
    private MalwareScript targetMalware;
    private TileScript targetTile;

    public GameObject pauseScreen, infoBox;
    public Slider CPUHealthBar, targetHealthBar;
    public TMP_Text CPUHealth, cryptocoins, waveNo; // Variables Displaying CPU current health, current cryptocoins and the wave number
    public TMP_Text targetName, targetHealth, targetDamage, targetROF, targetSpeed, targetRange, targetEffect; // Variables Displaying the target enemy/tower's name, total health, current health, average damage, average Rate of Fire, Speed and Range

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCPUHealth();
        UpdateCryptocoins();
        UpdateWaveNumber();
        if (target != null)
        {
            UpdateTargetInfo();
        }
    }

    public void PauseGame(bool pauseBool)
    {
        pauseScreen.SetActive(pauseBool);
    }

    public void UpdateCPUHealth()
    {
        CPUHealth.text = cpu.CPUTotalHealth.ToString() + " / " + cpu.CPUCurrentHealth.ToString();
        CPUHealthBar.maxValue = cpu.CPUTotalHealth;
        CPUHealthBar.value = cpu.CPUCurrentHealth;
    }

    public void UpdateCryptocoins()
    {
        cryptocoins.text = economy.cryptocoins.ToString();
    }

    public void UpdateWaveNumber()
    {

    }

    public void UpdateTargetInfo()
    {
        switch (target.tag)
        {
            case "Tile":
            {
                if(target.GetComponent<TileScript>())
                {
                    targetTile = target.GetComponent<TileScript>();
                    targetTile.tileUI.SetActive(true);
                }
                break;
            }
            case "Tower":
            {
                if(target.GetComponent<TowerScript>())
                {
                    infoBox.SetActive(true);
                    targetTower = target.GetComponent<TowerScript>();
                    targetName.text = targetTower.towerName;
                    targetHealth.text = targetTower.towerTotalHealth.ToString() + " / " + targetTower.towerCurrentHealth.ToString();
                    targetDamage.text = "Deals " + targetTower.towerDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetTower.towerROF.ToString() + " Seconds";
                    targetRange.text = "Radius: " + targetTower.towerRange.ToString() + " cm";
                    targetEffect.text = targetTower.towerEffect;
                    targetHealthBar.maxValue = targetTower.towerTotalHealth;
                    targetHealthBar.value = targetTower.towerCurrentHealth;
                }
                else
                {
                    infoBox.SetActive(false);
                }
                break;
            }
            case "Malware":
            {
                if(target.GetComponent<MalwareScript>())
                {
                    infoBox.SetActive(true);
                    targetMalware = target.GetComponent<MalwareScript>();
                    targetName.text = targetMalware.malwareName;
                    targetHealth.text = targetMalware.malwareCurrentHealth.ToString() + " / " + targetMalware.malwareTotalHealth.ToString();
                    targetDamage.text = "Deals " + targetMalware.malwareDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetMalware.malwareROF.ToString() + " Seconds";
                    targetSpeed.text = "Moves " + targetMalware.malwareSpeed.ToString() + " cm/s";
                    targetEffect.text = targetMalware.malwareEffect;
                    targetHealthBar.maxValue = targetMalware.malwareTotalHealth;
                    targetHealthBar.value = targetMalware.malwareCurrentHealth;
                }
                else
                {
                    infoBox.SetActive(false);
                }
                break;
            }
            default:
            {
                infoBox.SetActive(false);
                break;
            }
        }
    }
}
