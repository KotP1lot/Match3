using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UICaffeStat : MonoBehaviour
{
    [SerializeField] db_Interior db;
    [SerializeField] BonusSO bonuses;
    [SerializeField] PlayerWalletSO wallet;

    [SerializeField] LocalizedString localized;
    [SerializeField] LocalizeStringEvent loc_nameTxt;
    [SerializeField] LocalizeStringEvent loc_infoTxt;
    [SerializeField] LocalizeStringEvent loc_newLvlTxt;

    [SerializeField] TextMeshProUGUI lvlTxt;
    [SerializeField] TextMeshProUGUI newLvlTxt;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;
    [SerializeField] TextMeshProUGUI costTxt;
    [SerializeField] UICustomise customise;

    [SerializeField] Button newLvlBtn;

    [SerializeField] Image currFurniture;
    private InteriorSO currInt;
    private PlayerInteriorData data;
    private int cost;

    private void Start()
    {
        db.Load();
        localized.StringChanged += (string t) => newLvlTxt.text = t;
        loc_infoTxt.StringReference.StringChanged += (string t) => infoTxt.text = t;
    }
    private void OnEnable()
    {
       
    }
    public void Setup(PlayerInteriorData data, InteriorSO so)
    {
        this.data = data;

        currInt = so;
        gameObject.SetActive(true);
        InteriorLvlInfo currInfo = so.GetLvlInfo(data.lvl);
        if (data.lvl < 2)
        {
            if (wallet.Stars.Amount >= currInt.GetLvlInfo(data.lvl + 1).cost)
            {
                newLvlBtn.interactable = true;
            }
            else
            {
                newLvlBtn.interactable = false;
            }
            InteriorLvlInfo nextLVlinfo = so.GetLvlInfo(data.lvl + 1);
            localized.Arguments = new object[] { nextLVlinfo.bonus};
            localized.RefreshString();
            costTxt.text = nextLVlinfo.cost.ToString();
            cost = nextLVlinfo.cost;
        }
        else
        {
            loc_newLvlTxt.SetEntry("maxlvl");
            costTxt.text = "---";
            newLvlBtn.interactable = false;
        }

        loc_infoTxt.SetEntry(data.type switch
        {
            InteriorType.stil => "tableInfo",
            InteriorType.stul => "chairInfo",
            InteriorType.light => "lightInfo",
        });
        loc_infoTxt.StringReference.Arguments = new object[] { so.GetLvlBonus(data.lvl) };
        loc_infoTxt.RefreshString();


        lvlTxt.text = data.lvl.ToString();


        currFurniture.sprite = currInfo.sprites[data.currSprite];


        loc_nameTxt.SetEntry(data.type switch
        {
            InteriorType.stul => "chair",
            InteriorType.stil => "table",
            InteriorType.light => "light",
            _ => "chair"
        });
        customise.Setup(currInfo, data.currSprite);
    }
    public void ChangeCurr(int curr)
    {
        db.ChangeCurrSpriteByType(data.type, curr);
        Setup(db.GetPlayerDataByType(data.type), currInt);
    }
    public void Upgrade()
    {
        wallet.Stars.Spend(cost);
        db.UpgradeType(data.type);
        Setup(db.GetPlayerDataByType(data.type), currInt);
    }
}
