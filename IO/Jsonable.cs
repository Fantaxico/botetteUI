using BotetteUI.Helper;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotetteUI.IO
{
    public abstract class Jsonable<T>
    {
        public static T Read(string filePath)
        {
            string json = File.ReadAllText(filePath);
            T? data = JsonConvert.DeserializeObject<T>(json);
            if (App_Helper.IsNullOrEmpty(data)) Com_Helper.Error($"Could not read from JSON.\n({filePath})");
            return data!;
        }

        public static T Write(T obj, string filePath)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, json);
            return Read(filePath);
        }
    }
}
