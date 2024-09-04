
using System.Collections.Generic;

namespace Dan.Plugin.Skatteetaten.Models
{
    public class SummertSkattegrunnlagModel
    {
        public string personidentifikator { get; set; }
        public string inntektsaar { get; set; }
        public bool skjermet { get; set; }
        public List<Grunnlag> grunnlag { get; set; }
        public string skatteoppgjoersdato { get; set; }
    }

    public class Grunnlag
    {
        public string tekniskNavn { get; set; }
        public int beloep { get; set; }
        public string kategori { get; set; }
    }
}
