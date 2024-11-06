using CombatRandomizer.Modules;
using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;
using RandomizerMod.Settings;

namespace CombatRandomizer.IC;

public class SoulPlug : AbstractItem
{
    public SoulPlug()
    {
        name = "Soul_Plug";
        UIDef = new MsgUIDef {
            name = new BoxedString("Soul Plug"),
            shopDesc = new BoxedString("Stop giving away your soul!"),
            sprite = new CombatSprite("SoulPlug")
        };
        tags = [SoulPlugItemTag(), CurseTag()];
    }

    private static InteropTag SoulPlugItemTag()
    {
        InteropTag tag = new();
        tag.Properties["ModSource"] = "CombatRandomizer";
        tag.Properties["PinSprite"] = new CombatSprite("SoulPlug");
        tag.Properties["PoolGroup"] = "Soul Upgrades";
        tag.Message = "RandoSupplementalMetadata";
        return tag;
    }

    private InteropTag CurseTag()
    {
        InteropTag tag = new();
        tag.Properties["CanMimic"] = new BoxedBool(true);
        tag.Properties["MimicNames"] = new string[] {"Soul Pug", "Sou1 Plug", "Soul P1ug", "Sol Plug"};
        tag.Message = "CurseData";
        return tag;
    }
    public override bool Redundant()
    {
        return CombatModule.Instance.SoulPlugItems >= CombatModule.Instance.Settings.SoulPlugs.SoulDrainSettings.BaseDrain;
    }
    public override void GiveImmediate(GiveInfo info)
    {
        CombatModule.Instance.SoulPlugItems += 1;
    }
}