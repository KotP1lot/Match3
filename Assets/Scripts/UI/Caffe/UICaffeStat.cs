using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UICaffeStat : MonoBehaviour
{
    [SerializeField] db_Interior db;
    [SerializeField] BonusSO bonuses;
    [SerializeField] PlayerWalletSO wallet;
    [SerializeField] EnergyManager energyManager;

    [SerializeField] TextMeshProUGUI lvlTxt;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI infoTxt;
    [SerializeField] TextMeshProUGUI newLvlTxt;
    [SerializeField] TextMeshProUGUI costTxt;
    [SerializeField] UICustomise customise;

    [SerializeField] Button newLvlBtn;

    [SerializeField] Image currFurniture;
    private InteriorSO currInt;
    private PlayerInteriorData data;

    private void Start()
    {
        db.Load();
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
            newLvlTxt.text = $"+ {nextLVlinfo.bonus} ������!";
            costTxt.text = nextLVlinfo.cost.ToString();
        }
        else
        {
            newLvlTxt.text = "������������ ������!";
            costTxt.text = "---";
            newLvlBtn.interactable = false;
        }
        infoTxt.text = (data.type) switch
        {
            InteriorType.stil => $"����� ���� ��������� {bonuses.yummyBonus}% �� ����������� �볺����",
            InteriorType.stul => $"����� ���� ��������� {bonuses.moneyBonus}% ��������� ����� �� �볺����",
            InteriorType.light => $"����� ������ �������� ����㳿 �� {bonuses.energyBonus}",
            _=>"���, ��������� �����"
        };
        lvlTxt.text = data.lvl.ToString();
        currFurniture.sprite = currInfo.sprites[data.currSprite];
        nameTxt.text = data.type.ToString();
        customise.Setup(currInfo, data.currSprite);
    }
    public void ChangeCurr(int curr)
    {
        db.ChangeCurrSpriteByType(data.type, curr);
        Setup(db.GetPlayerDataByType(data.type), currInt);
    }
    public void Upgrade()
    {
        db.UpgradeType(data.type);
        Setup(db.GetPlayerDataByType(data.type), currInt);
    }
}
