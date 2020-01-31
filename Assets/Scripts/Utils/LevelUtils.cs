using System.Collections.Generic;
using UnityEngine;

namespace RaidHealer {
    public class LevelUtils {

        public static T GetLeveledValue<T>(List<T> list, int level) {
            var index = Mathf.Clamp(level - 1, 0, list.Count - 1);
            return list[index];
        }
    }
}
