using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    public Image indicator;
    public Upgrade upgrade;

    private WhipWeapon weapon;
    private Character character; 
    private PlayerController cntrl; 
    private RandomSetUp rndSetUp;
    private ThrowingDagger dagger;

    private Image img;
    
    void Start()
    {
        img = GetComponent<Image>();
        weapon = FindObjectOfType<WhipWeapon>();
        character = FindObjectOfType<Character>();
        cntrl = FindObjectOfType<PlayerController>();
        rndSetUp = FindObjectOfType<RandomSetUp>();
        dagger = FindObjectOfType<ThrowingDagger>();
    }
    
    void Update()
    {
        if (upgrade != null)
        {
            img.sprite = upgrade.icon;
        }
    }

    public void Action()
    {
        if (upgrade.damageProyectil == 0)
        {
            upgrade.firstTime = false;
            cntrl.velocidadMovimiento += upgrade.speed;
            dagger.speed += upgrade.speed;
            character.healAmount += upgrade.lifeReg;
            weapon.whipDamage += upgrade.damage;
            weapon.timeToAttack -= upgrade.reload;
        }
        else
        {
            Debug.Log("Entro");
            
            if (upgrade.firstTime)
            {
                dagger.usable = true;
                upgrade.firstTime = false;
            }
            else
            {
                if(indicator.sprite == rndSetUp.Icon2) dagger.damage += upgrade.damageProyectil;
                else
                {
                    dagger.swordCount++;
                }
            }
        }

        rndSetUp.OnOff(false);
    }
}
