using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Week6.EF.Core.Models;
using Week6.EF.EF;

namespace Week6.EF
{
    class Program
    {
        //Istanza di KnightsContext:
        private static KnightsContext _knightsContext = new KnightsContext();
        static void Main(string[] args)
        {
            _knightsContext.Database.EnsureCreated(); //per assicurarsi dell'esistenza del db.
                                                      //Se non esiste, lo crea.
                                                      //(questo metodo si usa nei test)

            //Recuperiamo tutti i cavalieri 
            //Console.WriteLine("Prima dell'aggiunta:");
            //FetchKnights();

            //Aggiungiamo un cavaliere 
            //AddKnight();

            //Recuperiamo tutti i cavalieri dopo l'aggiunta
            //Console.WriteLine("Dopo dell'aggiunta:");
            //FetchKnights();

            //Aggiunta tipi diversi
            //AddVariousTypes();

            //Filtrare tutti i cavalieri con nome Telman (2 modi)

            //Recuperare il primo con nome Telman

            //Recuperare un cavaliere con un certo id (2 modi)

            //Update cavaliere
            //GetAndUpdateKnight();

            //Delete cavaliere
            //GetAndDeleteKnight();

            //Recuperare tutte le battaglie e modificare le date
            //   DisconnectedScenario.RetrieveAndUpdateBattles();

            //Aggiungere un cavaliere con una o più armi
            //AddKnightWithWeapons();

            //Aggiungere un'arma a un cavaliere esistente
            //AddNewWeaponToExistingKnigth();

            //  DisconnectedScenario.AddNewWeaponToExistingKnigth_Disconnected();

            //   DisconnectedScenario.AddNewWeaponToExistingKnigth_Disconnected_Attach();

            //EagerLoaginKnightsWithWeapons();

            //Recuperare i cavalieri e solo le armi con descrizione Ascia
            //EagerLoaginKnightsWithWeapons_Filter();

        }

        private static void FetchKnights()
        {
            //Query LINQ
            var knights = _knightsContext.Knights.ToList();

            //var query = _knightsContext.Knights; //spezzando in due parti
            //var knights = query.ToList();


            //Stampiamo il numero di records cavalieri nella tabella db
            Console.WriteLine($"Il numero di cavalieri è: {knights.Count}");

            //Stampiamo i nomi dei cavalieri nel db
            foreach (var k in knights)
                Console.WriteLine(k.Name);

            //foreach (var k in _knightsContext.Knights)
            //{
            //    Console.WriteLine(k.Name);
            //} //POCO PERFORMANTE
        }
        private static void AddKnight()
        {
            var newKnight = new Knight { Name = "Bober" }; //avrà una lista di armi vuota

            _knightsContext.Knights.Add(newKnight);

            _knightsContext.SaveChanges();
        }

        private static void AddVariousTypes()
        {
            _knightsContext.AddRange(
                new Knight { Name = "Driacco" },
                new Battle { Name = "Cassel" }
                );

            _knightsContext.SaveChanges();
        }

        private static void GetByName()
        {
            var knights = _knightsContext.Knights.Where(k => k.Name == "Telman").ToList();
        }

        private static void FilterByName()
        {
            var name = "Driacco";
            var knights = _knightsContext.Knights.Where(k => k.Name == name).ToList();
        }

        private static void GetFirstByName() //Recuperare il primo che trova con un certo nome
        {
            var name = "Telman";
            //var knight = _knightsContext.Knights.Where(k => k.Name == name).First();
            //var knight = _knightsContext.Knights.Where(k => k.Name == name).FirstOrDefault(); //Se non trova niente, da null4
            var knight = _knightsContext.Knights.FirstOrDefault(k => k.Name == name);
        }

        public static void GetKnightById()
        {
            var knight = _knightsContext.Knights.FirstOrDefault(k => k.Id == 3);
        }

        public static void GetKnightById_Find()
        {
            var knight = _knightsContext.Knights.Find(3);
        }

        public static void GetAndUpdateKnight()
        {
            var knight = _knightsContext.Knights.FirstOrDefault();

            knight.Name = "Valfred";

            _knightsContext.SaveChanges();
        }

        public static void GetAndDeleteKnight()
        {
            //voglio cancellare un cavaliere tramite il suo id
            var knight = _knightsContext.Knights.Find(4);

            _knightsContext.Knights.Remove(knight);

            _knightsContext.SaveChanges();
        }

        //Metodo per aggiungere un cavaliere con una o più armi
        private static void AddKnightWithWeapons()
        {
            var knight = new Knight
            {
                Name = "Tusman",
                Weapons = new List<Weapon>
                {
                    new Weapon
                    {
                        Description = "Scimitarra"
                    }
                }

            };

            _knightsContext.Knights.Add(knight);
            _knightsContext.SaveChanges();
        }

        //Metodo per aggiungere un'arma a un cavaliere esistente
        private static void AddNewWeaponToExistingKnigth()
        {
            var knight = _knightsContext.Knights.FirstOrDefault();
            knight.Weapons.Add(new Weapon
            {
                Description = "Ascia"
            });
            _knightsContext.SaveChanges();
        }

        private static void EagerLoaginKnightsWithWeapons()
        {
            //  var knights = _knightsContext.Knights.ToList();
            //Il metodo precedente non si porta dietro le armi
            var knightsWithWeapons = _knightsContext.Knights.Include(k => k.Weapons).ToList();
        }

        //Recuperare i cavalieri e solo le armi con descrizione Ascia

        private static void EagerLoaginKnightsWithWeapons_Filter() //Da sistemare
        {
            //Prova a partire dalle armi e includi i cavalieri
            var weap = _knightsContext.Knights.Include(k => k.Weapons
                                      .Where(w => w.Description == "Ascia")).ToList();
        }

        //Query projections

        //Solo id e nome del cavaliere

        private static void IdAndName_AnonimousType()
        {
            var knights = _knightsContext.Knights
                .Select(k => new { k.Id, k.Name }).ToList();
        }

        public struct IAndName
        {
            public int Id;
            public string Name;
            public IAndName (int id, string name)
            {
               Id = id;
                Name = name;
            }
        }

        public static void IdAndName_Struct()
        {
            var idAndName = _knightsContext.Knights
            .Select(k => new IAndName(k.Id, k.Name)).ToList();
        }

        //Explicit loading

        private static void ExplicitLoading()
        {
            var knight = _knightsContext.Knights.Find(1);
            _knightsContext.Entry(knight).Collection(k => k.Weapons).Load();
            _knightsContext.Entry(knight).Reference(k => k.Horse).Load();
        }

    }
}
