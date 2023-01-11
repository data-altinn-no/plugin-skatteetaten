using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dan.Plugin.Skatteetaten.Models
{
    public class SkattItemResponse
    {
        [DataMember(Name = "aar")]
        public int Aar { get; set; }

        [DataMember(Name = "utkast")]
        public bool Utkast { get; set; }

        [DataMember(Name = "bruttoformue")]
        public int Bruttoformue { get; set; }

        [DataMember(Name = "samletGjeld")]
        public int SamletGjeld { get; set; }
    }
}
