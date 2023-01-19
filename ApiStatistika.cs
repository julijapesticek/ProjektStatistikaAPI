using DataAccess.Data;
using System.Runtime.CompilerServices;
using DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ProjektStatistikaAPI
{
    public static class ApiStatistika
    {
        public static void ConfigureApi(this WebApplication app)
        {
            //mapiranje metod 
            app.MapGet("/stat/zadnjiKlicanEP", NaloziZadnjeKlicanEP);
            app.MapGet("/stat/najpogostejeKlicanEP", NaloziNajpogostejeKlicanEP);
            app.MapGet("/stat/steviloEPKlicev", SteviloEPKlicev);
            app.MapPost("/stat/noviKlicEP", NoviKlicEP);
        }

        /// <summary>
        /// Izpis zadnje klicano storitev.
        /// </summary>
        //[Authorize]
        private static async Task<IResult> NaloziZadnjeKlicanEP(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result= Results.Ok(await data.NaloziZadjeKlicanEP());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);                            
            }
        }

        /// <summary>
        /// Izpise najpogosteje klicano storitev.
        /// </summary>
        //[Authorize]
        private static async Task<IResult> NaloziNajpogostejeKlicanEP(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.NaloziNajpogostejeKlicanEP());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Izpise stevilo klicev posamezne storitve.
        /// </summary>
        //[Authorize]
        private static async Task<IResult> SteviloEPKlicev(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.SteviloEPKlicev());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Zapise klic storitve v bazo.
        /// </summary>
        //[Authorize]
        private static async Task<IResult> NoviKlicEP(StatistikaModel statistikaModel, IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("POST klic za dodajanje zahtevka ");
                await data.ShraniNoviKlicEP(statistikaModel);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);

            }
        }

    }
}
