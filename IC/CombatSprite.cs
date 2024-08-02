using ItemChanger;
using ItemChanger.Internal;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace CombatRandomizer.IC
{
    [Serializable]
    public class CombatSprite : ISprite
    {
        private static SpriteManager EmbeddedSpriteManager = new(typeof(CombatSprite).Assembly, "CombatRandomizer.Resources.Sprites.");
        public string Key { get; set; }
        public CombatSprite(string key)
        {
            if (!string.IsNullOrEmpty(key))
                Key = key;
        }

        [JsonIgnore]
        public Sprite Value => EmbeddedSpriteManager.GetSprite(Key);
        public ISprite Clone() => (ISprite)MemberwiseClone();
    }
}