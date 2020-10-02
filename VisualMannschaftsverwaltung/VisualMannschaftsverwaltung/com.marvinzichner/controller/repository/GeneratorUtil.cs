using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class GeneratorUtil
    {
        ApplicationController app;
        private int COUNT_PERSONEN = 15;
        private int COUNT_MANNSCHAFTEN = 30;
        private List<string> sessions;
        private string[] forename;
        private string[] surname;
        private string[] birthdate;
        private string[] mannschaften;


        public GeneratorUtil()
        {
            this.app = new ApplicationController();
            this.sessions = new List<string>();
        }

        private void fillMocks()
        {
            this.sessions.Add("352db51c3ff06159d380d3d9935ec814");
            this.sessions.Add("d5ac2a52f8e075588bff7fa473efecc5");

            this.forename = new string[] {
                "Henry",
                "Markus",
                "Marvin",
                "Tom",
                "Thomas",
                "Peter",
                "Harald",
                "Yi",
                "Werner",
                "John",
            };
            this.surname = new string[] {
                "Schmitz",
                "Lu",
                "Johnson",
                "Peters",
                "Müller",
                "Schreiner",
                "Altenburg",
                "Siemens",
                "Mania",
                "Feldmann",
                "Bert",
            };
            this.birthdate = new string[] {
                "12.01.1971",
                "23.06.1998",
                "09.08.1999",
                "15.04.1989",
                "16.11.1900",
                "11.11.1911",
                "04.06.2003",
            };
            this.mannschaften = new string[] {
                "FC Augsburg",
                "1. FC Union Berlin",
                "Hertha BSC",
                "Arminia Bielefeld",
                "Werder Bremen",
                "Borussia Dortmund",
                "Eintracht Frankfurt",
                "SC Freiburg",
                "TSG 1899 Hoffenheim",
                "1. FC Köln",
                "RB Leipzig",
                "1. FSV Mainz 05",
                "Borussia Mönchengladbach",
                "FC Bayern München",
                "FC Schalke 04",
                "VfB Stuttgart",
                "VfL Wolfsburg",
                "VfL Bochum",
                "Hannover 96",
                "Fortuna Düsseldorf",
                "Holstein Kiel",
            };
        }

        public void generate()
        {
            fillMocks();

            this.sessions.ForEach(session =>
            {
                for (int i = 0; i <= this.COUNT_PERSONEN; i++)
                {
                    int rand = RandomUtils.asInteger(0, 4);

                    if(rand == 0) { 
                        FussballSpieler fussballSpieler = new FussballSpieler();
                        fussballSpieler
                            .name(RandomUtils.fromCollection(forename))
                            .nachname(RandomUtils.fromCollection(surname))
                            .birthdate(RandomUtils.fromCollection(birthdate))
                            .sportArt(SportArt.FUSSBALL)
                                .toFussballSpieler()
                                .isLeftFeet(RandomUtils.asBoolean())
                                .spielSiege(RandomUtils.asInteger(0, 3));
                        app.addPerson(fussballSpieler, session);
                    }

                    if (rand == 1)
                    {
                        HandballSpieler handballSpieler = new HandballSpieler();
                        handballSpieler
                            .name(RandomUtils.fromCollection(forename))
                            .nachname(RandomUtils.fromCollection(surname))
                            .birthdate(RandomUtils.fromCollection(birthdate))
                            .sportArt(SportArt.HANDBALL)
                                .toHandballSpieler()
                                .isLeftHand(RandomUtils.asBoolean())
                                .spielSiege(RandomUtils.asInteger(0, 3));
                        app.addPerson(handballSpieler, session);
                    }

                    if (rand == 2)
                    {
                        TennisSpieler tennisSpieler = new TennisSpieler();
                        tennisSpieler
                            .name(RandomUtils.fromCollection(forename))
                            .nachname(RandomUtils.fromCollection(surname))
                            .birthdate(RandomUtils.fromCollection(birthdate))
                            .sportArt(SportArt.TENNIS)
                                .toTennisSpieler()
                                .isLeftHand(RandomUtils.asBoolean())
                                .spielSiege(RandomUtils.asInteger(0, 3));
                        app.addPerson(tennisSpieler, session);
                    }

                    if (rand == 3)
                    {
                        Trainer trainer = new Trainer();
                        trainer
                            .name(RandomUtils.fromCollection(forename))
                            .nachname(RandomUtils.fromCollection(surname))
                            .birthdate(RandomUtils.fromCollection(birthdate))
                            .sportArt(SportArt.KEINE)
                                .toTrainer()
                                .hasLicense(RandomUtils.asBoolean());
                        app.addPerson(trainer, session);
                    }

                    if (rand == 4)
                    {
                        Physiotherapeut physiotherapeut = new Physiotherapeut();
                        physiotherapeut
                            .name(RandomUtils.fromCollection(forename))
                            .nachname(RandomUtils.fromCollection(surname))
                            .birthdate(RandomUtils.fromCollection(birthdate))
                            .sportArt(SportArt.KEINE)
                                .toPhysiotherapeut()
                                .hasLicense(RandomUtils.asBoolean());
                        app.addPerson(physiotherapeut, session);
                    }

                }

                // mannschaften
                for (int j = 0; j < this.mannschaften.Length; j++)
                {
                    Mannschaft mannschaft = new Mannschaft();
                    mannschaft
                        .name(this.mannschaften[j])
                        .sportArt(SportArt.FUSSBALL);

                    app.addMannschaftIfNotExists(mannschaft, session);
                }

                //Turnier
                app.createNewTurnier("1. Bundesliga", SportArt.FUSSBALL, session);
                app.createNewTurnier("2. Bundesliga", SportArt.FUSSBALL, session);

                app.getTurniere(session).ForEach(turnier =>
                {
                    int count = 0;
                    app.getMannschaften(session).ForEach(mannschaft =>
                    {
                        if (count < 5 || RandomUtils.asBoolean())
                        {
                            app.addMappingOfTurnierAndMannschaft(
                                mannschaft.ID.ToString(), turnier.getId().ToString());
                            count++;
                        }
                    });
                });

                //Spiele
                app.getTurniere(session).ForEach(turnier =>
                {
                    int day = 1;
                    List<Mannschaft> turnierMannschaften = 
                        turnier.getMannschaften();

                    turnierMannschaften.ForEach(mannschaft =>
                    {
                        turnierMannschaften.ForEach(mannschaft2 =>
                        {
                            if (mannschaft.ID != mannschaft2.ID)
                            {
                                app.createNewSpielOfTurnier(
                                    "generated.fwd",
                                    mannschaft.ID,
                                    mannschaft2.ID,
                                    day.ToString(),
                                    turnier.getId(),
                                    session);
                                day++;
                            }
                        });
                    });

                    app.generateRandomResults(turnier.getId().ToString(), session);
                });
            });
        }
    }
}