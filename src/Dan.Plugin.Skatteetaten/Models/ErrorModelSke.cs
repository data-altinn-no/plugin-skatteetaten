using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dan.Plugin.Skatteetaten.Models
{
    [DataContract]
    public class ErrorModelSke
    {
        [DataMember(Name = "kode")]
        public string kode { get; set; }

        [DataMember(Name = "melding")]
        public string melding { get; set; }

        [DataMember(Name = "korrelasjonsid")]
        public string korrelasjonsId { get; set; }


        public override string ToString()
        {
            return $"Kode: {this.kode}, korrelasjonsid: {this.korrelasjonsId}, melding: {this.melding}";
        }
    }
}
