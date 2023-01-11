using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dan.Common.Enums;
using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Plugin.Skatteetaten.Models;
using Dan.Plugin.Skatteetaten.Models.Arbeidsgiveravgift;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Constants = Dan.Common.Constants;
using JsonSchema = NJsonSchema.JsonSchema;
namespace Dan.Plugin.DATASOURCENAME;

/// <summary>
/// All plugins must implement IEvidenceSourceMetadata, which describes that datasets returned by this plugin. An example is implemented below.
/// </summary>
public class Metadata : IEvidenceSourceMetadata
{

    private const string SourceTaxDepartment = "Skatteetaten";
    private const string ServiceContextCompliance = "Seriøsitetsinformasjon";
    private const string ServiceContextEbevis = "eBevis";
    private const string ServiceContextTaxiPermit = "Drosjeloyve";
    private const string ServiceContextOed = "OED";
    private const string ServiceContextDihe = "DigitaleHelgeland";
    private const string ServiceContextReelle = "Reelle rettighetshavere";
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<EvidenceCode> GetEvidenceCodes()
    {
        return new List<EvidenceCode>
            {
                new EvidenceCode()
                {
                    EvidenceCodeName = "OppdragUtenlandskeVirksomheter",
                    Description = "Return information about abroad contracts",
                    EvidenceSource = SourceTaxDepartment,
                    MaxValidDays = 90,
                    RequiredScopes = "skatteetaten:oppdragutenlandskevirksomheter",
                    BelongsToServiceContexts = new List<string> { ServiceContextCompliance, ServiceContextEbevis },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 2,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextCompliance },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PrivateEnterprise)
                            }
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PublicAgency)
                            }
                        },
                        new LegalBasisRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            FailureAction = FailureAction.Skip
                        }
                    },
                    Values = new List<EvidenceValue>()
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "organisasjonsnavn",
                            ValueType = EvidenceValueType.String,
                            Source = SourceTaxDepartment
                        },
                         new EvidenceValue()
                        {
                            EvidenceValueName = "organisasjonsnummer",
                            ValueType = EvidenceValueType.String,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "antallAktiveOppdragSomArbeidsgiver",
                            ValueType = EvidenceValueType.Number,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "antallAktiveArbeidstakere",
                            ValueType = EvidenceValueType.Number,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "antallRegistrerteOppdragSomOppdragsgiver",
                            ValueType = EvidenceValueType.Number,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "levert",
                            ValueType = EvidenceValueType.DateTime,
                            Source = SourceTaxDepartment
                        },
                    }
                },
                new EvidenceCode()
                {
                    EvidenceCodeName = "RestanserV2",
                    Description = "Return the arrears for the subject company - new format",
                    MaxValidDays =  90,
                    RequiredScopes = "skatteetaten:restanser",
                    BelongsToServiceContexts = new List<string> { ServiceContextCompliance, ServiceContextEbevis, ServiceContextTaxiPermit },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 5,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextCompliance },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PrivateEnterprise)
                            }
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis, ServiceContextTaxiPermit },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PublicAgency)
                            }
                        }
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "levert",
                            ValueType = EvidenceValueType.DateTime,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "forespurteOrganisasjon",
                            ValueType = EvidenceValueType.String,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "arbeidsgiveravgiftForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        },

                        new EvidenceValue()
                        {
                            EvidenceValueName = "forskuddstrekkForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        },

                        new EvidenceValue()
                        {
                            EvidenceValueName = "forskuddsskattForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        },

                        new EvidenceValue()
                        {
                            EvidenceValueName = "restskattForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        },

                        new EvidenceValue()
                        {
                            EvidenceValueName = "gebyrForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "merverdiavgiftForfaltOgUbetalt",
                            ValueType = EvidenceValueType.Amount,
                            Source = SourceTaxDepartment
                        }
                    }
                },
                new EvidenceCode()
                {
                    EvidenceCodeName = "Arbeidsgiveravgift",
                    Description = "Return the payroll taxes for the subject company",
                    MaxValidDays =  90,
                    RequiredScopes = "skatteetaten:arbeidsgiveravgift",
                    BelongsToServiceContexts = new List<string> { ServiceContextCompliance, ServiceContextEbevis },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 3,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextCompliance },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PrivateEnterprise)
                            }
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PublicAgency)
                            }
                        },
                        new LegalBasisRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            FailureAction = FailureAction.Skip
                        }
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "levert",
                            ValueType = EvidenceValueType.DateTime,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "forespurteOrganisasjon",
                            ValueType = EvidenceValueType.String,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "arbeidsgiveravgifter",
                            ValueType = EvidenceValueType.JsonSchema,
                            JsonSchemaDefintion = JsonSchema.FromType<PayrollTaxModel>().ToJson(Newtonsoft.Json.Formatting.Indented),
                            Source = SourceTaxDepartment
                        }
                    }
                },
                new EvidenceCode()
                {
                    EvidenceCodeName = "MvaMeldingsOpplysning",
                    Description = "Return information about VAT reports submitted from the subject company",
                    MaxValidDays =  90,
                    RequiredScopes = "skatteetaten:mvameldingsopplysning",
                    BelongsToServiceContexts = new List<string> { ServiceContextCompliance, ServiceContextEbevis },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 4,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextCompliance },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PrivateEnterprise)
                            }
                        },
                        new PartyTypeRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Requestor, PartyTypeConstraint.PublicAgency)
                            }
                        },
                        new LegalBasisRequirement()
                        {
                            AppliesToServiceContext = new List<string> { ServiceContextEbevis },
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            FailureAction = FailureAction.Skip
                        }
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "levert",
                            ValueType = EvidenceValueType.DateTime,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "forespurteOrganisasjon",
                            ValueType = EvidenceValueType.String,
                            Source = SourceTaxDepartment
                        },
                        new EvidenceValue()
                        {
                            EvidenceValueName = "mvaAlminneligNaering",
                            ValueType = EvidenceValueType.JsonSchema,
                            Source = SourceTaxDepartment,
                            JsonSchemaDefintion = JsonSchema.FromType<MvaAlminneligNaering>().ToJson(Newtonsoft.Json.Formatting.Indented)
                        }
                    }
                }, new EvidenceCode()
                {
                    EvidenceCodeName = "SummertSkattegrunnlagOED",
                    Description = "Informasjon om formue og gjeld",
                    MaxValidDays =  90,
                    RequiredScopes = "skatteetaten:summertskattegrunnlag",
                    BelongsToServiceContexts = new List<string> { ServiceContextOed },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new PartyTypeRequirement()
                        {
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Subject, PartyTypeConstraint.PrivatePerson)
                            }
                        },
                        new MaskinportenScopeRequirement()
                        {
                            RequiredScopes = new List<string>() { "altinn:dataaltinnno/oed" }
                        }
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "default",
                            ValueType = EvidenceValueType.JsonSchema,
                            Source = SourceTaxDepartment,
                            JsonSchemaDefintion =  JsonSchema.FromType<SkattItemResponse>().ToJson(Newtonsoft.Json.Formatting.Indented)
                        }
                    }
                }, new EvidenceCode()
                {
                    EvidenceCodeName = "SummertSkattegrunnlag",
                    Description = "Informasjon om formue og gjeld",
                    MaxValidDays =  90,
                    RequiredScopes = "skatteetaten:summertskattegrunnlag",
                    BelongsToServiceContexts = new List<string> { ServiceContextDihe },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new PartyTypeRequirement()
                        {
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Subject, PartyTypeConstraint.PrivatePerson)
                            }
                        },
                        new MaskinportenScopeRequirement()
                        {
                            RequiredScopes = new List<string>() { "altinn:dataaltinnno/dihe" }
                        },
                        new ProvideOwnTokenRequirement(),
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "default",
                            ValueType = EvidenceValueType.JsonSchema,
                            Source = SourceTaxDepartment,
                            JsonSchemaDefintion =  ""
                        }
                    },
                    Parameters = new List<EvidenceParameter>
                    {
                        new EvidenceParameter()
                        {
                            EvidenceParamName = "stadie",
                            ParamType = EvidenceParamType.String,
                            Required = false
                        }
                    }
                }, new EvidenceCode()
                {
                    EvidenceCodeName = "FregPerson",
                    Description = "Informasjon fra folkeregisteret",
                    MaxValidDays =  90,
                    RequiredScopes = "folkeregister:deling/offentligmedhjemmel",
                    BelongsToServiceContexts = new List<string> { ServiceContextDihe, ServiceContextReelle },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new PartyTypeRequirement()
                        {
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Subject, PartyTypeConstraint.PrivatePerson)
                            }
                        },
                        new MaskinportenScopeRequirement()
                        {
                            RequiredScopes = new List<string>() { "altinn:dataaltinnno/dihe" },
                            AppliesToServiceContext = new List<string>() { ServiceContextDihe }
                        },
                        new ProvideOwnTokenRequirement(),
                        new MaskinportenScopeRequirement()
                        {
                            RequiredScopes = new List<string>() { "altinn:dataaltinnno/reelle" },
                            AppliesToServiceContext = new List<string>() { ServiceContextReelle }
                        },
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "default",
                            ValueType = EvidenceValueType.JsonSchema,
                            Source = SourceTaxDepartment,
                            JsonSchemaDefintion =  ""
                        }
                    }
                }
                , new EvidenceCode()
                {
                    EvidenceCodeName = "FregPersonRelasjonUtvidet",
                    Description = "Informasjon fra folkeregisteret",
                    MaxValidDays =  90,
                    RequiredScopes = "folkeregister:deling/offentligmedhjemmel",
                    BelongsToServiceContexts = new List<string> { ServiceContextDihe },
                    AuthorizationRequirements = new List<Requirement>()
                    {
                        new PartyTypeRequirement()
                        {
                            AllowedPartyTypes = new AllowedPartyTypesList()
                            {
                                new KeyValuePair<AccreditationPartyTypes, PartyTypeConstraint>(
                                    AccreditationPartyTypes.Subject, PartyTypeConstraint.PrivatePerson)
                            }
                        },
                        new MaskinportenScopeRequirement()
                        {
                            RequiredScopes = new List<string>() { "altinn:dataaltinnno/dihe" }
                        },
                        new ProvideOwnTokenRequirement()
                    },
                    Values = new List<EvidenceValue>
                    {
                        new EvidenceValue()
                        {
                            EvidenceValueName = "default",
                            ValueType = EvidenceValueType.JsonSchema,
                            Source = SourceTaxDepartment,
                            JsonSchemaDefintion =  ""
                        }
                    }
                }
            };
    }



    /// <summary>
    /// This function must be defined in all DAN plugins, and is used by core to enumerate the available datasets across all plugins.
    /// Normally this should not be changed.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [Function(Constants.EvidenceSourceMetadataFunctionName)]
    public async Task<HttpResponseData> GetMetadataAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req,
        FunctionContext context)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(GetEvidenceCodes());
        return response;
    }

}
