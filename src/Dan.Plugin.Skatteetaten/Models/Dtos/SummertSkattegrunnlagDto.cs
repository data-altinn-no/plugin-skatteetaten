using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Dan.Plugin.Skatteetaten.Models.Dtos;

[Serializable]
public class SummertSkattegrunnlagDto
{
    public SummertSkattegrunnlagDto(SummertSkattegrunnlagModel summertSkattegrunnlag)
    {
        PersonIdentifikator = summertSkattegrunnlag.personidentifikator;
        InntektsAar = summertSkattegrunnlag.inntektsaar;
        Skjermet = summertSkattegrunnlag.skjermet;
        Grunnlag = summertSkattegrunnlag.grunnlag.Select(g => new GrunnlagDto(g)).ToList();
        Skatteoppgjoersdato = summertSkattegrunnlag.skatteoppgjoersdato;
    }

    [JsonProperty("personidentifikator")]
    public string PersonIdentifikator { get; set; }

    [JsonProperty("inntektsaar")]
    public string InntektsAar { get; set; }

    [JsonProperty("skjermet")]
    public bool Skjermet { get; set; }

    [JsonProperty("grunnlag")]
    public List<GrunnlagDto> Grunnlag { get; set; }

    [JsonProperty("skatteoppgjoersdato")]
    public string Skatteoppgjoersdato { get; set; }
}

[Serializable]
public class GrunnlagDto
{
    public GrunnlagDto(Grunnlag grunnlag)
    {
        TekniskNavn = grunnlag.tekniskNavn;
        Beloep = grunnlag.beloep;
        Kategori = new[] { grunnlag.kategori };
    }

    [JsonProperty("tekniskNavn")]
    public string TekniskNavn { get; set; }

    [JsonProperty("beloep")]
    public int Beloep { get; set; }

    [JsonProperty("kategori")]
    public string[] Kategori { get; set; }
}
