//Name          Marvin Zichner
//Datum         12.03.2020
//Datei         ApplicationContext.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class ApplicationContext
    {
        #region Eigenschaften
        #endregion

        #region Accessoren / Modifier
        #endregion

        #region Konstruktoren
        public ApplicationContext()
        {
           
        }
        #endregion

        #region Worker
        public static List<Mannschaft> createApplicationContext()
        {
            List<Mannschaft> returnableList = new List<Mannschaft>();
            Mannschaft mannschaft = new Mannschaft("Köln");

            mannschaft
                .sportArt(SportArt.FUSSBALL)
                .flushPersonen();

            returnableList.Add(mannschaft);

            return returnableList;
        }

        public static List<Person> createPersonData()
        {
            List<Person> personen = new List<Person>();
            FussballSpieler Marvin = new FussballSpieler();
            HandballSpieler Henry = new HandballSpieler();
            TennisSpieler Lars = new TennisSpieler();
            Trainer John = new Trainer();
            Physiotherapeut Thomas = new Physiotherapeut();
            FussballSpieler Zili = new FussballSpieler();
            TennisSpieler Jeff = new TennisSpieler();

            Marvin
                .name("Marvin")
                .nachname("Zichner")
                .sportArt(SportArt.FUSSBALL)
                .toFussballSpieler()
                .isLeftFeet(true)
                .spielerNummer(12)
                .spielSiege(66)
                .spielerRolle(SpielerRolle.STUERMER);

            Henry
                .name("Henry C.")
                .nachname("Johnson")
                .sportArt(SportArt.HANDBALL)
                .toHandballSpieler()
                .isLeftHand(false)
                .spielerNummer(2)
                .spielSiege(6)
                .spielerRolle(SpielerRolle.KEINE);

            Lars
                .name("Lars T.-J.")
                .nachname("Smith")
                .sportArt(SportArt.TENNIS)
                .toTennisSpieler()
                .isLeftHand(true)
                .spielerNummer(1)
                .spielSiege(11)
                .spielerRolle(SpielerRolle.KEINE);

            Jeff
                .name("Jeff")
                .nachname("Miller")
                .sportArt(SportArt.TENNIS)
                .toTennisSpieler()
                .isLeftHand(true)
                .spielerNummer(2)
                .spielSiege(2)
                .spielerRolle(SpielerRolle.KEINE);

            John
                .name("John")
                .nachname("de Reginald")
                .sportArt(SportArt.KEINE)
                .toTrainer()
                .hasLicense(true)
                .sportArt(SportArt.FUSSBALL);

            Zili
                .name("Carsten")
                .nachname("Stahl-Grubers")
                .sportArt(SportArt.FUSSBALL)
                .toFussballSpieler()
                .isLeftFeet(true)
                .spielerNummer(12)
                .spielSiege(23)
                .spielerRolle(SpielerRolle.STUERMER);

            Thomas
                .name("Thomas")
                .nachname("Britt")
                .sportArt(SportArt.KEINE)
                .toPhysiotherapeut()
                .hasLicense(true);

            personen.Add(Marvin);
            personen.Add(Henry);
            personen.Add(Lars);
            personen.Add(John);
            personen.Add(Zili);
            personen.Add(Thomas);
            personen.Add(Jeff);

            return personen;
        }
        #endregion
    }
}
