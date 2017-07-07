using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Structs
{
    public class NGSortedObjectsCollection<K,V,P> where P : Type
    {
        private Dictionary<K, V> _CollectionOfData = new Dictionary<K, V>();
        private SortedDictionary<P, K> _CollectionSortedKeys = new SortedDictionary<P, K>();

        public void AddAtPosition(K Key, V Value, P Position)
        {
            if (_CollectionOfData.ContainsKey(Key))
            {
                _CollectionOfData[Key] = Value;
                var _Position = _CollectionSortedKeys.Where(x => x.Value.ToString() == Key.ToString()).Select(x=>x.Key).First();

                _CollectionSortedKeys.Remove(_Position);
                _CollectionSortedKeys.Add(Position, Key);
            }
        }

        public void AddToEnd(K Key, V Value)
        {

        }

        public void AddToBeginning(K Key, V Value)
        {

        }

        public KeyValuePair<K,V> GetAtPosition(P Position)
        {
            KeyValuePair<K, V> _TmpReturn = new KeyValuePair<K, V>();

            if (_CollectionSortedKeys.ContainsKey(Position) == false) { return _TmpReturn; }

            var _TmpKey = _CollectionSortedKeys[Position];

            return _TmpReturn;
        }
    }
}
