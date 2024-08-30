using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

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

    [JsonPropertyName("personidentifikator")]
    public string PersonIdentifikator { get; set; }

    [JsonPropertyName("inntektsaar")]
    public string InntektsAar { get; set; }

    [JsonPropertyName("skjermet")]
    public bool Skjermet { get; set; }

    [JsonPropertyName("grunnlag")]
    public List<GrunnlagDto> Grunnlag { get; set; }

    [JsonPropertyName("skatteoppgjoersdato")]
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

    [JsonPropertyName("tekniskNavn")]
    public string TekniskNavn { get; set; }

    [JsonPropertyName("beloep")]
    public int Beloep { get; set; }

    [JsonPropertyName("kategori")]
    public string[] Kategori { get; set; }
}
