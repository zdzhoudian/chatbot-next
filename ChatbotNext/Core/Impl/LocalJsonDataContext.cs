using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatbotNext.Core
{

    /// <summary>
    /// 数据量太少了，本地json存储够用了
    /// </summary>
    public class LocalJsonDataContext : IDataContext
    {
        private readonly ConcurrentDictionary<string, object> _caches = new ConcurrentDictionary<string, object>();

        public string ConnectionString { get; set; }

        public string GetName<T>()
        {
            return typeof(T).Name;
        }

        public List<T> GetCollection<T>(string name = null, bool useCache = true)
            where T : IDataCollection
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GetName<T>();
            }
            if (useCache && _caches.TryGetValue(name, out var obj) && obj is List<T> collection)
            {
                return collection;
            }
            Directory.CreateDirectory(ConnectionString);
            var path = Path.Combine(ConnectionString, $"{name}.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                collection = JsonSerializer.Deserialize<List<T>>(json);
            }
            else
            {
                collection = new List<T>();
            }

            if (useCache)
            {
                _caches[name] = collection;
            }
            return collection.Select(p => p).ToList();//copy
        }

        public void SetItem<T>(T item, string name = null, bool useCache = true)
            where T : IDataCollection
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Directory.CreateDirectory(ConnectionString);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GetName<T>();
            }
            var path = Path.Combine(ConnectionString, $"{name}.json");
            var collection = GetCollection<T>(name, useCache);
            var index = collection.FindIndex(p => p.ID == item.ID);
            var isAdd = index == -1;
            if (isAdd)
            {
                collection.Add(item);
            }
            else
            {
                collection.RemoveAt(index);
                collection.Insert(index, item);
            }
            var json = JsonSerializer.Serialize(collection);
            File.WriteAllText(path, json);
            if (useCache)
            {
                _caches[name] = collection;
            }
            OnCollectionChanged(typeof(T), name, new List<T> { item }, isAdd ? DataContextCollectionChangedAction.Add : DataContextCollectionChangedAction.Update);
        }

        public void DeleteItem<T>(T item, string name = null, bool useCache = true)
            where T : IDataCollection
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Directory.CreateDirectory(ConnectionString);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GetName<T>();
            }
            var path = Path.Combine(ConnectionString, $"{name}.json");
            var collection = GetCollection<T>(name, useCache);
            var deletedItem = collection.FirstOrDefault(p => p.ID == item.ID);
            if (deletedItem != null)
            {
                collection.Remove(deletedItem);
                var json = JsonSerializer.Serialize(collection);
                File.WriteAllText(path, json);
                if (useCache)
                {
                    _caches[name] = collection;
                }
                OnCollectionChanged(typeof(T), name, new List<T> { deletedItem }, DataContextCollectionChangedAction.Delete);
            }
        }

        public void DeleteItems<T>(IEnumerable<T> items, string name = null, bool useCache = true)
            where T : IDataCollection
        {
            if (items == null || items.Count() == 0)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Directory.CreateDirectory(ConnectionString);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = GetName<T>();
            }
            var path = Path.Combine(ConnectionString, $"{name}.json");
            var collection = GetCollection<T>(name, useCache);
            foreach (var item in items)
            {
                var deletedItem = collection.FirstOrDefault(p => p.ID == item.ID);
                if (deletedItem != null)
                {
                    collection.Remove(deletedItem);
                }
            }
            var json = JsonSerializer.Serialize(collection);
            File.WriteAllText(path, json);
            if (useCache)
            {
                _caches[name] = collection;
            }
            OnCollectionChanged(typeof(T), name, items, DataContextCollectionChangedAction.Delete);
        }

        public void Set<T>(string key, T value, bool useCache = true)
            where T : IDataCollection
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"invalid {nameof(key)}");
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            Directory.CreateDirectory(ConnectionString);
            var name = "KV";
            var path = Path.Combine(ConnectionString, $"{name}.json");
            var valueJson = JsonSerializer.Serialize(value);
            var kvs = GetCollection<KVItem>(name, useCache);
            var kv = kvs.FirstOrDefault(p => p.ID.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (kv == null)
            {
                kv = new KVItem()
                {
                    ID = key,
                    Value = valueJson
                };
            }
            else
            {
                kv.Value = valueJson;
            }
            SetItem(kv, name, useCache);
        }

        public T Get<T>(string key, bool useCache = true)
            where T : IDataCollection
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"invalid {nameof(key)}");
            }
            var name = "KV";
            var path = Path.Combine(ConnectionString, $"{name}.json");
            var kvs = GetCollection<KVItem>(name, useCache);
            var kv = kvs.FirstOrDefault(p => p.ID.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (kv == null)
            {
                throw new KeyNotFoundException($"{key} not found");
            }
            if (kv.Value == null)
            {
                return default;
            }
            var value = JsonSerializer.Deserialize<T>(kv.Value);
            return value;
        }

        public T GetOrDefault<T>(string key, bool useCache = true)
            where T : IDataCollection
        {
            try
            {
                return Get<T>(key, useCache);
            }
            catch
            {
                return default;
            }
        }

        public event EventHandler<DataContextCollectionChangedEventArgs> CollectionChanged;

        private void OnCollectionChanged(Type dataType, string name, IEnumerable items, DataContextCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new DataContextCollectionChangedEventArgs(dataType, name, items, action));
        }

        private class KVItem : IDataCollection
        {
            public string ID { get; set; }

            public string Value { get; set; }
        }
    }

    public class DataContextCollectionChangedEventArgs : EventArgs
    {
        public Type DataType { get; }

        public string Name { get; }

        public IEnumerable Items { get; }

        public DataContextCollectionChangedAction Action { get; }


        public DataContextCollectionChangedEventArgs(Type dataType, string name, IEnumerable items, DataContextCollectionChangedAction action)
        {
            DataType = dataType;
            Name = name;
            Items = items;
            Action = action;
        }
    }

}
