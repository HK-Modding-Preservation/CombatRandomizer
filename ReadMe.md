# Combat Randomizer

This connection will not provide any new locations.

It will, however, affect your combatting experience when battling anything on Hallownest. Depending on chosen difficulty tier, this can be from mildly inconvenient to a very cursed fighting experience.

## Properties

### Charm Notches

Removes Charm Notches from the game and replaces them with Charm Notch Fragments. Every N fragment will grant you a Charm Notch, where N is dependant on difficulty.

- Easy: 2 fragments per notch.
- Standard: 3 fragments per notch.
- Intermediate: 4 fragments per notch.
- Hard: 4 fragments per notch. Max 9 notches.
- Extreme: 4 fragments per notch. Max 6 notches.

### Nail Damage

Nail Upgrades are made entirely cosmetic, and base nail damage is determined by the Nail Damage objects. Each Nail Damage object will grant +1 damage.

- Easy: You can earn up to 20 damage, making you deal 4 more damage than in vanilla when fully geared.
- Standard: You can earn up to 16 damage, equivalent to all 4 vanilla upgrades.
- Intermediate: You can earn up to 18 damage, but start dealing 3 instead of the standard 5.
- Hard: You can earn up to 15 damage, but start dealing 2 instead of the standard 5, leading to a maximum of 17 damage (equivalent to 3 nail upgrades).
- Extreme: You can earn up to 12 damage, but start dealing 1 instead of the standard 5, leading to a maximum of 13 damage (equivalent to 2 nail upgrades).

### Soul Gain

You will gain less soul from hitting enemies with your nail by default, and the amount will increase back to normal with obtained items. Each item grants +1 soul.

- Easy: You start obtaining 8 soul instead of 11 per swing. 5 items are randomized, making you gain up to 2 extra soul
- Standard: You start obtaining 6 soul instead of 11 per swing. 5 items are randomized, equivalent to vanilla soul gain.
- Intermediate: You start obtaining 4 soul instead of 11 per swing. 7 items are randomized, equivalent to vanilla soul gain.
- Hard: You start obtaining 3 soul per swing, and 5 items are randomized, making you earn up to 8 soul per swing.
- Extreme: You start obtaining 1 soul per swing, and only 5 items are randomized, making you earn up to 8 soul per swing.

### Soul Drain

You will periodically lose soul over time until you obtain items that prevent the loss, much like if you had an evil King's Soul permanently equipped.

- Easy: You lose 1 soul every 2 seconds. An item that stops the draining is randomized.
- Standard: You lose 2 soul every 2 seconds. 2 items that stop the draining are randomized.
- Intermediate: You lose 3 soul every 2 seconds. 3 items that stop the draining are randomized.
- Hard: You lose 5 soul every 2 seconds. 4 items that stop the draining are randomized.
- Extreme: You lose 7 soul every 2 seconds. 5 items that stop the draining are randomized.

## Settings

The settings are pretty straightforward.
- Enabled: Defines if this mod does anything at all.
- Each obtainable has its own difficulty tier, where leaving it as Disabled ignores that entirely.
- Limit Nail Damage: This will cap your nail damage based on obtained progression.
 - If neither Wings or Claws are obtained, Nail Damage is capped at 9.
 - If any Split Claw is obtained but no Wings, Nail Damage is capped at 13.
 - If Wings is obtained but no Claw, Nail Damage is capped at 17.
 - If Wings and any claw or full claw are obtained, there's no damage limit.

The Limit Nail Damage setting will automatically be activated if Enemy Pogo skip is active to prevent impossible seeds.

## Logic effects

Activating this mod will cause you to periodically lose soul, which can become extremely painful to track logic-wise with state logic. Therefore, if any soul drain is enabled, this mod will disable all spell-based skips. Furthermore, this mod will likely have spell usage access be more restricted, so tags such as Baldur killing or boss fighting will have a few additional requirements by default.

If any other impossible spell-based issues come up, please let me know to handle them.