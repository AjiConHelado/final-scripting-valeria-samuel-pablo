using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSetUp : MonoBehaviour
{
    [SerializeField] private Container bullet;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<Upgrade> upgrades;
    [SerializeField] private List<Sprite> icons;

    private List<Upgrade> used = new List<Upgrade>();
    private CanvasGroup cnv;
    private Dictionary<int, Upgrade> shuffled = new Dictionary<int, Upgrade>();

    public Sprite Icon1
    {
        get
        {
            return icons[0];
        }
    }
    
    public Sprite Icon2
    {
        get
        {
            return icons[1];
        }
    }

    private void Awake()
    {
        Init();
        cnv = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadRandom();
        OnOff(false);
    }

    public void LoadRandom()
    {
        Reset();

        System.Random rnd = new System.Random();
        
        shuffled = shuffled.OrderBy(x => rnd.Next()).ToDictionary(item => item.Key, item => item.Value);
        
        foreach (GameObject element in objects)
        {
            Upgrade selected = shuffled[Random.Range(0, upgrades.Count)];
            Container cont = element.GetComponent<Container>();
            
            cont.upgrade = selected;
            used.Add(selected);
            upgrades.Remove(selected);

            if (selected.firstTime)
            {
                cont.indicator.sprite = Icon1;
            }
            else
            {
                cont.indicator.sprite = Icon2;
            }
        }
        
        IconChange(bullet.upgrade, bullet);
    }

    void IconChange(Upgrade selected, Container cont)
    {
        if (selected.firstTime)
        {
            cont.indicator.sprite = Icon1;
        }
        else
        {
            cont.indicator.sprite = icons[Random.Range(0, icons.Count)];
        }
    }
    
    void Reset()
    {
        foreach (Upgrade element in used)
        {
            upgrades.Add(element);
        }
        
        used.Clear();
    }

    void Init()
    {
        int count = 0;
        foreach (Upgrade element in upgrades)
        {
            element.firstTime = true;
            shuffled.Add(count, element);
            count++;
        }

        bullet.upgrade.firstTime = true;
    }

    public void OnOff(bool switchVar)
    {
        if (switchVar)
        {
            cnv.interactable = true;
            cnv.alpha = 1;
        }
        else
        {
            cnv.interactable = false;
            cnv.alpha = 0;
            Time.timeScale = 1;
            LoadRandom();
        }
    }
}
