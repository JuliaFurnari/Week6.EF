using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.Core.Models;
using Week6.EF.EF;

namespace Week6.EF
{
    class DisconnectedScenario
    {
        //Scenario disconnesso
        //Si usano istanze di contesto diverse, una per recuperare i dati e una per salvare i cambiamenti sul db.
        //Gli scenari disconnessi sono più comuni nelle applicazioni Web, in cui il contesto viene rinnovato a ogni
        //richiesta (http) e gli oggetti vengono passati al contesto dall'esterno. In questi scenari, bisogna impostare
        // lo stato di un'entità in modo che il contesto sappia se deve essere aggiunto, aggiornato ecc...

        //DbContext non è a conoscenza delle entità 'disconnesse' perché le entità sono state aggiunte
        //o modificate ecc. nell'ambito di un contesto diverso da quello usato per recuperare i dati.

        //=> le entità disconnesse devono essere 'agganciate' a un contesto con un certo stato, 
        //(EntityState) in base alle operazioni(Aggiungi, Modifica, Elimina) che
        //si vogliono fare sul database.

        //In base a questo stato, quando si chiama il SaveChanges(), viene fatta una query diversa
        //sul database.
        //- Added -> nel database viene fatta una insert
        //- Modified -> nel database viene fatta una update
        //- Deleted -> nel database viene fatta una delete

        //Add:
        //Aggancia un'entità al contesto con EntityState = Added (=> si traduce in insert sul db)

        //Attach:
        //'Allega' un'entità al contesto.
        //Se l'entità esiste già (quindi EF Core riconosce che ha già l'id per esempio) allora setta lo stato su Unchanged
        //Se invece non ha una chiave primaria 'popolata', capisce che è una nuova entità e fissa il suo EntityState su Added (=> che diventa una insert sul db)

        //Update:
        //Aggancia un'entità al contesto con EntityState = Modified (=> si traduce in update sul db)

        // Esempio: Aggiungere un'entità

        public static void AddNewKnight()
        {
            var knight = new Knight
            {
                Name = "Valfred"
            };

            using (var context = new KnightsContext())
            {
                //Opzione 1
                context.Knights.Add(knight);

                //Altre opzioni valide (alternative alla 1)
                // context.Add<Knight>(std);
                // context.Entry<Knight>(std).State = EntityState.Added;
                // context.Attach<Knight>(std);

                //-> viene 'agganciata' al contesto un'entity con EntityState = Added

                //Chiamando il SaveChanges, viene eseguita una insert sul db
                context.SaveChanges();
            }
        }

        // Esempio: Modificare un'entità

        public static void UpdateNewKnight()
        {

            Knight knight = new Knight();

            using (var context = new KnightsContext())
            {
                knight = context.Knights.Find(3);
            }

            knight.Name = "Arard";

            using (var context = new KnightsContext())
            {
                context.Knights.Update(knight);

                // Altre opzioni valide
                //context.Update<Knight>(knight);
                //context.Attach<Knight>(knight).State = EntityState.Modified;
                // context.Entry<Knight>(knight).State = EntityState.Modified; 

                //-> qua capisce che knight è un'entità esistente perchè ha un id valido.
                //-> quando chiama update collega l'entità al contesto e imposta lo stato su Modified => Update sul db.
                context.SaveChanges();
            }
        }
        //Metodo per recuperare tutte le battaglie e modificare le date
        public static void RetrieveAndUpdateBattles()
        {
            List<Battle> disconnectedBattles = new List<Battle>();

            using (var context = new KnightsContext())
            {
                //recupero le battaglie e chiudo contesto
                disconnectedBattles = context.Battles.ToList();
            } //context è disposed

            //modificare le date delle battaglie
            disconnectedBattles.ForEach(b =>
            {
                b.StartDate = new DateTime(780, 01, 01);
                b.EndDate = new DateTime(780, 12, 31);
            });

            //salvare i cambiamenti sul database creando un nuovo contesto
            using (var ctx = new KnightsContext())
            {
                ctx.UpdateRange(disconnectedBattles);

                ctx.SaveChanges();
            }
        }
        //Update scatena due azioni:
        //1. Il context prima non tracciava ancora l’oggetto, ora inizia a tracciarlo.
        //2. Una volta che l’oggetto è conosciuto dal contesto, allora viene marcato come Modified.
        //=> Chiamando il metodo SaveChanges, vengono praticamente fatte una query di select e una di update, per ogni battaglia



        //Metodo per aggiungere aggiungere un'arma a un cavaliere esistente (disconnesso)
        public static void AddNewWeaponToExistingKnigth_Disconnected()
        {
            Knight knight = new Knight();

            using (var context = new KnightsContext())
            {
                knight = context.Knights.Find(3);
            }

            knight.Weapons.Add(new Weapon
            {
                Description = "Mazza"
            });

            using (var ctx = new KnightsContext())
            {
                ctx.Knights.Update(knight);
                ctx.SaveChanges();
            }
        }

        //il contesto che si usa per salvare la nuova arma nel db non ha idea della storia di questi oggetti.
        //Quando chiamiamo update, inizia il tracking.
        //Ef Core capisce che knight ha già un ID e determina che la arma deve essere nuova perché non ha un’ID.
        //Poiché il valore chiave dell'arma non è settato, imposta lo stato su ‘Added’.
        //Inoltre, assume che la FK dell’arma dovrebbe essere il valore dell’id del cavaliere. => La setta

        //In background, per il cavaliere, viene fatta una update anche se non viene realmente modificata
        //direttamente una proprietà.
        //Ma viene fatta una update proprio perchè il metodo Update setta l'entity state su modificato => nel db diventa 
        //una query di Update

        public static void AddNewWeaponToExistingKnigth_Disconnected_Attach()
        {

            Knight knight = new Knight();

            using (var context = new KnightsContext())
            {
                knight = context.Knights.Find(3);
            }

            knight.Weapons.Add(new Weapon
            {
                Description = "Lancia"
            });

            using (var ctx = new KnightsContext())
            {
                ctx.Knights.Attach(knight);
                ctx.SaveChanges();
            }
        }

        //Chiamando Attach il contesto inizia a tracciare il cavaliere con stato su ‘Unchanged’.

        //EF Core vedrà la FK mancante nell’arma e la setta.
        //Dietro le quinte, ci sarà ancora una INSERT per la arma ma non ci sono UPDATE.
    }
}
