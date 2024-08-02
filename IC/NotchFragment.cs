using System;
using CombatRandomizer.Modules;
using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;

namespace CombatRandomizer.IC;

public class NotchFragment : AbstractItem
{
    public NotchFragment()
    {
        name = "Notch_Fragment";
        UIDef = new MsgUIDef {
            name = new BoxedString("Notch Fragment"),
            shopDesc = new BoxedString("Maybe if we put them together..."),
            sprite = new ItemChangerSprite("ShopIcons.CharmNotch")
        };
        tags = [NotchFragmentItemTag()];
    }

    private static InteropTag NotchFragmentItemTag()
    {
        InteropTag tag = new();
        tag.Properties["ModSource"] = "CombatRandomizer";
        tag.Properties["PinSprite"] = new ItemChangerSprite("ShopIcons.CharmNotch");
        tag.Properties["PoolGroup"] = "Charm Notches";
        tag.Message = "RandoSupplementalMetadata";
        return tag;
    }

    public override void GiveImmediate(GiveInfo info)
    {
        int fragmentsPerNotch = Math.Min((int)CombatModule.Instance.Settings.Difficulty + 2, 4);
        CombatModule.Instance.NotchFragments += 1;
        if (UIDef is MsgUIDef ui && !Redundant())
            ui.name = new BoxedString($"{ui.name.Value} ({CombatModule.Instance.NotchFragments} / {fragmentsPerNotch})");
        if (CombatModule.Instance.NotchFragments == fragmentsPerNotch)
        {
            // Grant all Charm Notch vanilla properties
            PlayerData.instance.charmSlots += 1;
            HeroController.instance.CharmUpdate();
            EventRegister.SendEvent("UPDATE BLUE HEALTH");
            GameManager.instance.RefreshOvercharm();

            // Reset notch fragments variable
            CombatModule.Instance.NotchFragments = 0;
        }

    }
}