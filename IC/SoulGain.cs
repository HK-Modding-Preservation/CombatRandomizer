using CombatRandomizer.Modules;
using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;

namespace CombatRandomizer.IC;

public class SoulGain : AbstractItem
{
    public SoulGain()
    {
        name = "Soul_Gain";
        UIDef = new MsgUIDef {
            name = new BoxedString("Soul Gain"),
            shopDesc = new BoxedString("Don't just defeat your enemies. Take their very essence."),
            sprite = new CombatSprite("SoulGain")
        };
        tags = [SoulGainItemTag()];
    }

    private static InteropTag SoulGainItemTag()
    {
        InteropTag tag = new();
        tag.Properties["ModSource"] = "CombatRandomizer";
        tag.Properties["PinSprite"] = new ItemChangerSprite("ShopIcons.Soul");
        tag.Properties["PoolGroup"] = "Soul Upgrades";
        tag.Message = "RandoSupplementalMetadata";
        return tag;
    }

    public override bool Redundant()
    {
        return CombatModule.Instance.SoulGainItems + CombatModule.Instance.Settings.SoulGain.SoulGainSettings.BaseGain >= 11;
    }
    public override void GiveImmediate(GiveInfo info)
    {
        CombatModule.Instance.SoulGainItems += 1;
    }
}