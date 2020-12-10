using System;
using System.Collections.Generic;

namespace TemperatureConverterMain.model.Scales
{
    public class ScalesList
    {
        public event Action<string> AddScale;
        public event Action<string> RemoveScale;

        public int Count { get; private set; }

        private readonly List<IScale> scales;

        public ScalesList()
        {
            scales = new List<IScale>();
        }

        public void Add(IScale scale)
        {
            scales.Add(scale);
            Count++;
            AddScale?.Invoke(scale.Name);
        }

        public void Remove(IScale scale)
        {
            scales.Remove(scale);
            Count--;
            RemoveScale?.Invoke(scale.Name);
        }

        public string[] GetScalesRange()
        {
            string[] scalesName = new string[Count];

            for (int i = 0; i < Count; i++)
            {
                scalesName[i] = scales[i].Name;
            }

            return scalesName;
        }

        public IScale Get(string ScaleName)
        {
            return scales.Find(s => s.Name == ScaleName);
        }
    }
}
