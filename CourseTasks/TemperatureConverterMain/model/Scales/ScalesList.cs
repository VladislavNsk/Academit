using System;
using System.Linq;
using System.Collections.Generic;

namespace TemperatureConverterMain.Model.Scales
{
    public class ScalesList
    {
        public event Action<string> AddScale;
        public event Action<string> RemoveScale;

        public int Count => scales.Count;
        private readonly List<IScale> scales;

        public ScalesList()
        {
            scales = new List<IScale>();
        }

        public void Add(IScale scale)
        {
            scales.Add(scale);
            AddScale?.Invoke(scale.Name);
        }

        public void Remove(IScale scale)
        {
            scales.Remove(scale);
            RemoveScale?.Invoke(scale.Name);
        }

        public string[] GetNames()
        {
            return scales.Select(scale => scale.Name).ToArray();
        }

        public IScale GetScale(string scaleName)
        {
            return scales.Find(s => s.Name == scaleName);
        }
    }
}
