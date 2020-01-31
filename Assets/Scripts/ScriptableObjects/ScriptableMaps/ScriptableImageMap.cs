using DeRibura;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {

    [CreateAssetMenu(fileName = "ImageMap", menuName = "ScriptableObjects/Maps/ImageMap")]
    public class ScriptableImageMap : ScriptableMap<string, Sprite> {

        [SerializeField] private List<Tuple> collection;

        protected override IEnumerable<SerializableTuple<string, Sprite>> items => collection;

        [Serializable]
        private class Tuple : SerializableTuple<string, Sprite> { }
    }
}
