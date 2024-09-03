# Combat Randomizer

This connection will not provide any new locations.

It will, however, affect your combatting experience when battling anything on Hallownest. Depending on chosen difficulty tier, this can be from mildly inconvenient to a very cursed fighting experience.

## Properties

### Charm Notches

Removes Charm Notches from the game and replaces them with Charm Notch Fragments. Every N fragment will grant you a Charm Notch, where N is dependant on difficulty. The mod accepts selecting 2 to 4 fragments per notch, and allows to set the maximum total Charm Notches you can get.

### Nail Damage

Nail Upgrades are made entirely cosmetic, and base nail damage is determined by the Nail Damage objects. Dreamer health will be set by standard nail upgrades, as will Lost Artifact available slots, so for a more fun use of Nail Upgrades, having them be automatically be applied when obtaining an item may or may not make this setting more fun.

The base damage and total nail items available will define your power, with the base damage being capped at 21 (Pure Nail damage).

There is also the option to cap your Nail Damage based on obtained progression with the "Limit Nail Damage" setting. If enabled:
 - If neither Wings or Claws are obtained, Nail Damage is capped at 9.
 - If any Split Claw is obtained but no Wings, Nail Damage is capped at 13.
 - If Wings is obtained but no Claw, Nail Damage is capped at 17.
 - If Wings and any claw or full claw are obtained, there's no damage cap.

 The Limit Nail Damage setting will automatically be activated if Enemy Pogo skip is active to prevent impossible seeds.


### Soul Gain

You will gain less soul from hitting enemies with your nail by default, and the amount will increase back to normal with obtained items. Each item grants +1 soul per nail swing.

Vanilla soul gain is 11, the settings allow to customize what is the base gain you obtain and how many items will be in the pool that remedy this.

### Soul Drain

You will periodically lose soul over time until you obtain items that prevent the loss, much like if you had an evil King's Soul permanently equipped. You will lose N soul every two seconds, N being the BaseDrain you define, and each plug you obtain will reduce that value by 1, so if you lose 3 soul per check, whenever you obtain 3 Soul Plugs the effect will be completely negated.

## Logic effects

Activating this mod will cause you to periodically lose soul, which can become extremely painful to track logic-wise with state logic. Therefore, if any soul drain is enabled, this mod will disable all spell-based skips. Furthermore, this mod will likely have spell usage access be more restricted, so tags such as Baldur killing or boss fighting will have a few additional requirements by default.

If any other impossible spell-based issues come up, please let me know to handle them.