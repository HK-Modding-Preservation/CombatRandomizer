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
        tags = [NailItemTag()];
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

    public override void GiveImmediate(GiveInfo info)
    {
        CombatModule.Instance.NailItems += 1;

        // We call this to refresh the damage
        PlayerData.instance.GetInt("nailDamage");
    }
}