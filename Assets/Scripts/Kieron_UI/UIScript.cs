using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public CPUBehaviour cpu;
    //public EconomySystem economy;
    // Declare Script that holds wave number

    public GameObject target;

    //private TowerScript targetTower;
    //private MalwareScript targetMalware;
    public TurretStats targetTower;
    private EnemyStats targetMalware;

    private TileScript targetTile;

    public GameObject pauseScreen, infoBox;
    public Slider CPUHealthBar, targetHealthBar;
    public TMP_Text CPUHealth, cryptocoins, waveNo; // Variables Displaying CPU current health, current cryptocoins and the wave number
    public TMP_Text targetName, targetHealth, targetCost, targetDamage, targetROF, targetSpeed, targetRange, targetEffect; // Variables Displaying the target enemy/tower's name, total health, current health, average damage, average Rate of Fire, Speed and Range
    public int waveNumber = 1;
    public int maxWave = 10;

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
        CPUHealth.text = cpu.maxHealth.ToString() + " / " + cpu.health.ToString();
        CPUHealthBar.maxValue = cpu.maxHealth;
        CPUHealthBar.value = cpu.health;

        if(cpu.health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void UpdateCryptocoins()
    {
        cryptocoins.text = EconomySystem.cryptocoins.ToString();
    }

    public void UpdateWaveNumber()
    {

    }

    public void ArenaPlaced()
    {
        cpu = GameObject.FindGameObjectsWithTag("CPU")[0].GetComponent<CPUBehaviour>();
        targetEffect.text = "Tap Tiles to Place Towers, Destroy Malware, Protect your CPU!";
        waveNumber = 1;
        waveNo.text = waveNumber.ToString() + " / " + maxWave.ToString();
    }

    public void NextWave()
    {
        if(waveNumber < maxWave)
        {
            waveNumber ++;
            waveNo.text = waveNumber.ToString()  + " / " + maxWave.ToString();
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void UnselectTarget()
    {
        target = null;
        targetTile = null;
        targetTower = null;
        targetMalware = null;
    }

    public void UpdateTargetInfo()
    {
        switch (target.tag)
        {
            case "Tile":
            {
                if(target.GetComponent<TileScript>())
                {
                    if(targetTile != null)
                    {
                        targetTile.tileUI.SetActive(false);
                    }
                    targetTile = target.GetComponent<TileScript>();
                    targetTile.tileUI.SetActive(true);

                    Debug.Log("Target = Tile");
                }
                break;
            }
            case "Turret":
            {
                
                if(target.GetComponentInParent</*TowerScript*/DefenceStats>())
                {
                    infoBox.SetActive(true);
                    /*targetTower = target.GetComponent<TowerScript>();
                    targetName.text = targetTower.towerName;
                    targetHealth.text = targetTower.towerTotalHealth.ToString() + " / " + targetTower.towerCurrentHealth.ToString();
                    targetDamage.text = "Deals " + targetTower.towerDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetTower.towerROF.ToString() + " Seconds";
                    targetRange.text = "Radius: " + targetTower.towerRange.ToString() + " cm";
                    targetEffect.text = targetTower.towerEffect;
                    targetHealthBar.maxValue = targetTower.towerTotalHealth;
                    targetHealthBar.value = targetTower.towerCurrentHealth;
                    targetSpeed.gameObject.SetActive(false);*/

                    //target.GetComponentInParent</*TowerScript*/DefenceStats>().upgradeUI.SetActive(true);
                    targetTower = target.GetComponentInParent<DefenceStats>().defenceStats;
                    targetName.text = targetTower.defenceName;
                    targetCost.text = targetTower.defenceCost.ToString();
                    //targetHealth.text = target.GetComponentInParent<DefenceStats>().health.ToString() + " / " + targetTower.defenceHealth.ToString();
                    targetDamage.text = "Deals " + targetTower.defenceDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetTower.defenceFireRate.ToString() + " Seconds";
                    targetRange.text = "Radius: " + targetTower.defenceRange.ToString() + " cm";
                    targetEffect.text = targetTower.defenceDescription;
                    targetHealthBar.maxValue = targetTower.defenceHealth;
                    targetHealthBar.value = target.GetComponentInParent<DefenceStats>().health;
                    targetHealth.gameObject.SetActive(false);
                    targetSpeed.gameObject.SetActive(false);
                    targetRange.gameObject.SetActive(true);

                    target.GetComponentInParent<DefenceStats>().upgradeUI.SetActive(true);

                    Debug.Log("Target = turret");
                }
                else
                {
                    infoBox.SetActive(false);
                }
                break;
            }
            case "Enemy":
            {
                if(target.GetComponent</*MalwareScript*/EnemyBehaviour>())
                {
                    infoBox.SetActive(true);
                    /*targetMalware = target.GetComponent<MalwareScript>();
                    targetName.text = targetMalware.malwareName;
                    targetHealth.text = targetMalware.malwareCurrentHealth.ToString() + " / " + targetMalware.malwareTotalHealth.ToString();
                    targetDamage.text = "Deals " + targetMalware.malwareDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetMalware.malwareROF.ToString() + " Seconds";
                    targetSpeed.text = "Moves " + targetMalware.malwareSpeed.ToString() + " cm/s";
                    targetEffect.text = targetMalware.malwareEffect;
                    targetHealthBar.maxValue = targetMalware.malwareTotalHealth;
                    targetHealthBar.value = targetMalware.malwareCurrentHealth;
                    targetRange.gameObject.SetActive(false);*/

                    targetMalware = target.GetComponent<EnemyBehaviour>().enemyStats;
                    targetName.text = targetMalware.enemyName;
                    targetHealth.text = target.GetComponent<EnemyBehaviour>().health.ToString() + " / " + targetMalware.health.ToString();
                    targetDamage.text = "Deals " + targetMalware.damage.ToString() + " Damage";
                    targetROF.text = "Every " + targetMalware.enemyFireRate.ToString() + " Seconds";
                    targetSpeed.text = "Moves " + targetMalware.moveSpeed.ToString() + " cm/s";
                    targetEffect.text = targetMalware.enemyDescription;
                    targetHealthBar.maxValue = targetMalware.health;
                    targetHealthBar.value = target.GetComponent<EnemyBehaviour>().health;
                    targetCost.gameObject.SetActive(false);
                    targetRange.gameObject.SetActive(false);
                    targetSpeed.gameObject.SetActive(true);

                    Debug.Log("Target = Enemy");
                }
                else
                {
                    infoBox.SetActive(false);
                }
                break;
            }
            default:
            {
                if(target.GetComponent<DefenceStats>())
                {
                    infoBox.SetActive(true);
                    //target.GetComponent</*TowerScript*/DefenceStats>().upgradeUI.SetActive(true);
                    targetTower = target.GetComponent<DefenceStats>().defenceStats;
                    targetName.text = targetTower.defenceName;
                    targetCost.text = targetTower.defenceCost.ToString();
                    targetDamage.text = "Deals " + targetTower.defenceDamage.ToString() + " Damage";
                    targetROF.text = "Every " + targetTower.defenceFireRate.ToString() + " Seconds";
                    targetRange.text = "Radius: " + targetTower.defenceRange.ToString() + " cm";
                    targetEffect.text = targetTower.defenceDescription;
                    targetHealthBar.maxValue = targetTower.defenceHealth;
                    targetHealthBar.value = target.GetComponent<DefenceStats>().health;
                    targetHealth.gameObject.SetActive(false);
                    targetSpeed.gameObject.SetActive(false);
                    targetRange.gameObject.SetActive(true);

                    Debug.Log("Target = turret 2");
                }
                else
                {
                    infoBox.SetActive(false);
                }
                break;
            }
        }
    }
}
