using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatbotNext.Core
{
    public interface IDataContext
    {
        string ConnectionString { get; set; }

        string GetName<T>();

        List<T> GetCollection<T>(string name = null, bool useCache = true) where T : IDataCollection;

        void SetItem<T>(T item, string name = null, bool useCache = true) where T : IDataCollection;

        void DeleteItem<T>(T item, string name = null, bool useCache = true) where T : IDataCollection;
        void DeleteItems<T>(IEnumerable<T> items, string name = null, bool useCache = true) where T : IDataCollection;

        void Set<T>(string key, T value, bool useCache = true) where T : IDataCollection;

        T Get<T>(string key, bool useCache = true) where T : IDataCollection;

        T GetOrDefault<T>(string key, bool useCache = true) where T : IDataCollection;

        event EventHandler<DataContextCollectionChangedEventArgs> CollectionChanged;
    }

    public interface IDataCollection
    {
        public string ID { get; set; }
    }

    public enum DataContextCollectionChangedAction
    {
        Add,
        Update,
        Delete,
    }
}
