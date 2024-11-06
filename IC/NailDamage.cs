using CombatRandomizer.Modules;
using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;

namespace CombatRandomizer.IC;

public class NailDamage : AbstractItem
{
    public NailDamage()
    {
        name = "Nail_Damage";
        UIDef = new MsgUIDef {
            name = new BoxedString("Nail Damage"),
            shopDesc = new BoxedString("Do you even lift bro?"),
            sprite = new ItemChangerSprite("ShopIcons.Upslash")
        };
        tags = [NailItemTag(), CurseTag()];
    }

    private static InteropTag NailItemTag()
    {
        InteropTag tag = new();
        tag.Properties["ModSource"] = "CombatRandomizer";
        tag.Properties["PinSprite"] = new ItemChangerSprite("ShopIcons.Upslash");
        tag.Properties["PoolGroup"] = "Nail Upgrades";
        tag.Message = "RandoSupplementalMetadata";
        return tag;
    }
    private InteropTag CurseTag()
    {
        InteropTag tag = new();
        tag.Properties["CanMimic"] = new BoxedBool(true);
        tag.Properties["MimicNames"] = new string[] {"Neil Damage", "Nail Damaged", "Nai1 Damage"};
        tag.Message = "CurseData";
        return tag;
    }
    public override bool Redundant()
    {
        CombatModule.Instance.SetNailDamage();
        return PlayerData.instance.nailDamage >= 21;
    }

    public override void GiveImmediate(GiveInfo info)
    {
        CombatModule.Instance.NailItems += 1;
        CombatModule.Instance.SetNailDamage();  
    }
}