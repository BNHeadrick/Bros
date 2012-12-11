/*
***************************************************************************
* Copyright notice removed by a creator for anonymity, please don't sue   *
*                                                                         *
* Licensed under the Apache License, Version 2.0 (the "License");         *
* you may not use this file except in compliance with the License.        *
* You may obtain a copy of the License at                                 *
*                                                                         *
* http://www.apache.org/licenses/LICENSE-2.0                              *
*                                                                         *
* Unless required by applicable law or agreed to in writing, software     *
* distributed under the License is distributed on an "AS IS" BASIS,       *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.*
* See the License for the specific language governing permissions and     *
* limitations under the License.                                          *
***************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA
{
    public class Constants
    {
        // TODO: Also support loading constants from a file

        // important characters
        public const int PLAYER = 261;
        public const int COMPANION = 55;
        public const int LIQUERMERCH = 132;
        public const int STOLEN_BREW = 338;
        public const int BREW_THIEF1 = 197;
        public const int BREW_THIEF2 = 292;
        public const int BREW_THIEF3 = 312;

        public const int BREW_MAIDEN = 137;
        public const int COOK = 101;

        public const int PARTY_PEOPLE1 = 373;
        public const int PARTY_PEOPLE2 = 267;
        public const int PARTY_PEOPLE3 = 258;
        public const int PARTY_PEOPLE4 = 223;
        public const int PARTY_PEOPLE5 = 198;
        public const int PARTY_PEOPLE6 = 32;
        public const int PARTY_PEOPLE7 = 173;
        public const int PARTY_PEOPLE8 = 175;
        public const int PARTY_PEOPLE9 = 64;
        public const int PARTY_PEOPLE10 = 27;

        public const int DEBUG_PARTY_DIALOG = 999;


        public const float DepthDebugLines = 1.0f;

        public const float DepthGameplayTiles = 0.19f;
        public const float DepthBaseGameplay = 0.2f;
        public const float DepthMaxGameplay = 0.3f;
        public const float DepthRangeGameplay = DepthMaxGameplay - DepthBaseGameplay;

        public const float DepthMainMenuText = 0.5f;

        public const float DEPTH_LOW = 0.2f;

        public const float DepthDialoguePage = .95f;
        public const float DepthDialogueText = .951f;

        public enum CharType : int
        {
            PLAYERCHAR = 0,
            NPCHAR = 1,
            COMPANIONCHAR = 2
        }

        /**
         * Converts a doodad's integer index to the string index
         *@param doodad the int index
         *@return the string index
         */
        public static string doodadIntToString(int doodad)
        {
            string[] doodadIndex = {// graveyard
                                    /*0 => */ "NONE",      /*1 => */"tree1",      /*2 => */"tree2", 
                                    /*3 => */"tombstone1", /*4 => */"tombstone2", /*5 => */"tombstone3", 
                                    /*6 => */"tombstone4", /*7 => */"obelisk",    /*8 => */ "bigstone1", 
                                    /*9 => */"bigstone2",
                                    // town
                                    /*10=> */"house1",
                                    // trees
                                    /*11=> */"tree1",     /*12=> */"tree2",
                                    // characters
                                    /*13=> */"DarkKnight",/*14=> */"Jason",       /*15=> */"Ness",
                                    /*16=> */"Salsa",
                                    "NONE", "NONE", "NONE", "NONE", 
                                    //ADULT
                                    /*21=> */"Adult1", /*22=> */"Adult2", /*23=> */"Adult3", /*24=> */"Adult4", 
                                    /*25=> */"Adult5", /*26=> */"Adult6", /*27=> */"Adult7", /*28=> */"Adult8", 
                                    "NONE", "NONE",
                                    //ANIMAL
                                    /*31=> */"Animal1", /*32=> */"Animal2", /*33=> */"Animal3", /*34=> */"Animal4", 
                                    /*35=> */"Animal5", /*36=> */"Animal6", /*37=> */"Animal7", /*38=> */"Animal8", 
                                    "NONE", "NONE", 
                                    //Animal2
                                    /*41=> */"Animal21", /*42=> */"Animal22", /*43=> */"Animal23", /*44=> */"Animal24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Boss
                                    /*51=> */"Boss1", /*52=> */"Boss2", /*53=> */"Boss3", /*54=> */"Boss4", 
                                    /*55=> */"Boss5", /*56=> */"Boss6", /*57=> */"Boss7", /*58=> */"Boss8", 
                                    "NONE", "NONE", 
                                    //Boss2
                                    /*61=> */"Boss21", /*62=> */"Boss22", /*63=> */"Boss23", /*64=> */"Boss24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //BunnyGirl2
                                    /*71=> */"BunnyGirl1", /*72=> */"BunnyGirl2", /*73=> */"BunnyGirl3", /*74=> */"BunnyGirl24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Child
                                    /*81=> */"Child1", /*82=> */"Child2", /*83=> */"Child3", /*84=> */"Child4", 
                                    /*85=> */"Child5", /*86=> */"Child6", /*87=> */"Child7", /*88=> */"Child8", 
                                    "NONE", "NONE", 
                                    //Church
                                    /*91=> */"Church1", /*92=> */"Church2", /*93=> */"Church3", /*94=> */"Church4", 
                                    /*95=> */"Church5", /*96=> */"Church6", /*97=> */"Church7", /*98=> */"Church8", 
                                    "NONE", "NONE", 
                                    //Cook2
                                    /*101=> */"Cook1", /*102=> */"Cook2", /*103=> */"Cook3", /*104=> */"Cook4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Dragon
                                    /*111=> */"Dragon1", /*112=> */"Dragon2", /*113=> */"Dragon3", /*114=> */"Dragon4", 
                                    /*115=> */"Dragon5", /*116=> */"Dragon6", /*117=> */"Dragon7", /*118=> */"Dragon8", 
                                    "NONE", "NONE", 
                                    //Elve
                                    /*121=> */"Elve1", /*122=> */"Elve2", /*123=> */"Elve3", /*124=> */"Elve4", 
                                    /*125=> */"Elve5", /*126=> */"Elve6", /*127=> */"Elve7", /*128=> */"Elve8", 
                                    "NONE", "NONE", 
                                    //Employee
                                    /*131=> */"Employee1", /*132=> */"Employee2", /*133=> */"Employee3", /*134=> */"Employee4", 
                                    /*135=> */"Employee5", /*136=> */"Employee6", /*137=> */"Employee7", /*138=> */"Employee8", 
                                    "NONE", "NONE", 
                                    //Fairy2
                                    /*141=> */"Fairy1", /*142=> */"Fairy2", /*143=> */"Fairy3", /*144=> */"Fairy4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Fighter
                                    /*151=> */"Fighter1", /*152=> */"Fighter2", /*153=> */"Fighter3", /*154=> */"Fighter4", 
                                    /*155=> */"Fighter5", /*156=> */"Fighter6", /*157=> */"Fighter7", /*158=> */"Fighter8", 
                                    "NONE", "NONE", 
                                    //Flame2
                                    /*161=> */"Flame1", /*162=> */"Flame2", /*163=> */"Flame3", /*164=> */"Flame4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Hero
                                    /*171=> */"Hero1", /*172=> */"Hero2", /*173=> */"Hero3", /*174=> */"Hero4", 
                                    /*175=> */"Hero5", /*176=> */"Hero6", /*177=> */"Hero7", /*178=> */"Hero8", 
                                    "NONE", "NONE",
                                    //Machine
                                    /*181=> */"Machine1", /*182=> */"Machine2", /*183=> */"Machine3", /*184=> */"Machine4", 
                                    /*185=> */"Machine5", /*186=> */"Machine6", /*187=> */"Machine7", /*188=> */"Machine8", 
                                    "NONE", "NONE",
                                    //Mage
                                    /*191=> */"Mage1", /*192=> */"Mage2", /*193=> */"Mage3", /*194=> */"Mage4", 
                                    /*195=> */"Mage5", /*196=> */"Mage6", /*197=> */"Mage7", /*198=> */"Mage8", 
                                    "NONE", "NONE",
                                    //Merchant2
                                    /*201=> */"Merchant1", /*202=> */"Merchant2", /*203=> */"Merchant3", /*204=> */"Merchant4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Monster
                                    /*211=> */"Monster1", /*212=> */"Monster2", /*213=> */"Monster3", /*214=> */"Monster4", 
                                    /*215=> */"Monster5", /*216=> */"Monster6", /*217=> */"Monster7", /*218=> */"Monster8", 
                                    "NONE", "NONE",
                                    //Monster2
                                    /*221=> */"Monster21", /*222=> */"Monster22", /*223=> */"Monster23", /*224=> */"Monster24", 
                                    /*225=> */"Monster25", /*226=> */"Monster26", /*227=> */"Monster27", /*228=> */"Monster28", 
                                    "NONE", "NONE",
                                    //Monster3
                                    /*231=> */"Monster31", /*232=> */"Monster32", /*233=> */"Monster33", /*234=> */"Monster34", 
                                    /*235=> */"Monster35", /*236=> */"Monster36", /*237=> */"Monster37", /*238=> */"Monster38", 
                                    "NONE", "NONE",
                                    //NekoGirl2
                                    /*241=> */"NekoGirl1", /*242=> */"NekoGirl2", /*243=> */"NekoGirl3", /*244=> */"NekoGirl4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Old
                                    /*251=> */"Old1", /*252=> */"Old2", /*253=> */"Old3", /*254=> */"Old4", 
                                    /*255=> */"Old5", /*256=> */"Old6", /*257=> */"Old7", /*258=> */"Old8", 
                                    "NONE", "NONE",
                                    //Pirate
                                    /*261=> */"Pirate1", /*262=> */"Pirate2", /*263=> */"Pirate3", /*264=> */"Pirate4", 
                                    /*265=> */"Pirate5", /*266=> */"Pirate6", /*267=> */"Pirate7", /*268=> */"Pirate8", 
                                    "NONE", "NONE",
                                    //Royal
                                    /*271=> */"Royal1", /*272=> */"Royal2", /*273=> */"Royal3", /*274=> */"Royal4", 
                                    /*275=> */"Royal5", /*276=> */"Royal6", /*277=> */"Royal7", /*278=> */"Royal8", 
                                    "NONE", "NONE",
                                    //Royal2
                                    /*281=> */"Royal21", /*282=> */"Royal22", /*283=> */"Royal23", /*284=> */"Royal24", 
                                    /*285=> */"Royal25", /*286=> */"Royal26", /*287=> */"Royal27", /*288=> */"Royal28", 
                                    "NONE", "NONE",
                                    //Seer2
                                    /*291=> */"Seer1", /*292=> */"Seer2", /*293=> */"Seer3", /*294=> */"Seer4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Soldier
                                    /*301=> */"Soldier1", /*302=> */"Soldier2", /*303=> */"Soldier3", /*304=> */"Soldier4", 
                                    /*305=> */"Soldier5", /*306=> */"Soldier6", /*307=> */"Soldier7", /*308=> */"Soldier8", 
                                    "NONE", "NONE",
                                    //Student
                                    /*311=> */"Student1", /*312=> */"Student2", /*313=> */"Student3", /*314=> */"Student4", 
                                    /*315=> */"Student5", /*316=> */"Student6", /*317=> */"Student7", /*318=> */"Student8", 
                                    "NONE", "NONE",
                                    //Template
                                    /*321=> */"Template1", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE", "NONE", "NONE",  
                                    "NONE", "NONE",
                                    //Thief
                                    /*331=> */"Thief1", /*332=> */"Thief2", /*333=> */"Thief3", /*334=> */"Thief4", 
                                    /*335=> */"Thief5", /*336=> */"Thief6", /*337=> */"Thief7", /*338=> */"Thief8", 
                                    "NONE", "NONE",
                                    //Vehicle
                                    /*341=> */"Vehicle1", /*342=> */"Vehicle2", /*343=> */"Vehicle3", /*344=> */"Vehicle4", 
                                    /*345=> */"Vehicle5", /*346=> */"Vehicle6", /*347=> */"Vehicle7", /*348=> */"Vehicle8", 
                                    "NONE", "NONE",
                                    //Vehicle2
                                    /*351=> */"Vehicle21", /*352=> */"Vehicle22", /*353=> */"Vehicle23", /*354=> */"Vehicle24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Warrior
                                    /*361=> */"Warrior1", /*362=> */"Warrior2", /*363=> */"Warrior3", /*364=> */"Warrior4", 
                                    /*365=> */"Warrior5", /*366=> */"Warrior6", /*367=> */"Warrior7", /*368=> */"Warrior8", 
                                    "NONE", "NONE",
                                    //Young
                                    /*371=> */"Young1", /*372=> */"Young2", /*373=> */"Young3", /*374=> */"Young4", 
                                    /*375=> */"Young5", /*376=> */"Young6", /*377=> */"Young7", /*378=> */"Young8", 
                                    "NONE", "NONE",
                                   };
            return doodadIndex[doodad];
        }

    }
}
