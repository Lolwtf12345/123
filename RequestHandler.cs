using robotManager;
using robotManager.Helpful;
using robotManager.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wManager;
using wManager.Wow;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

public class Main : wManager.Plugin.IPlugin
{
    private WoWPlayer Inviznik;
    private WoWPlayer Attacker;
    private Vector3 InviznikPosition;
    private List<string> HatersNames = new List<string>();
    private Timer _FlyBaitTimer = new Timer();
    private Timer _dispelltimer = new Timer();
    private Timer _holylighttimer = new Timer();
    private Timer _heroicstriketimer = new Timer();
    private Timer _saytimer = new Timer();
    private Timer _chektargettimer = new Timer();
    private Timer _WaitGankTimer = new Timer();
    //дк
    public Spell Lichborne;
    public Spell EveryManForHimself;
    public Spell MindFreeze;
    public Spell FrostPresence;
    public Spell DarkCommand;
    public Spell RaiseDead;
    public Spell ArmyoftheDead;
    public Spell SummonGargoyle;
    public Spell AntiMagicShell;
    public Spell Hysteria;
    public Spell DancingRuneWeapon;
    public Spell Gnaw;
    public Spell Strangulate;
    public Spell DeathPact;
    public Spell Pestilence;
    public Spell EmpowerRuneWeapon;
    public Spell DeathGrip;
    public Spell BloodTap;
    public Spell IceboundFortitude;
    public Spell VampiricBlood;
    public Spell GiftoftheNaaru;
    public Spell RuneTap;
    public Spell MarkofBlood;
    public Spell ChainsofIce;
    public Spell PlagueStrike;
    public Spell IcyTouch;
    public Spell DeathStrike;
    public Spell RuneStrike;
    public Spell DeathCoil;
    public Spell HeartStrike;
    public Spell ScourgeStrike;
    public Spell BloodStrike;
    public Spell HornofWinter;
    public Spell GhoulFrenzy;
    public Spell DeathandDecay;
    public Spell BloodBoil;
    public Spell BoneShield;
    public Spell BloodPresence;
    public Spell PathofFrost;
    public Spell FrostStrike;
    public Spell Obliterate;
    public Spell UnholyPresence;
    public Spell AntiMagicZone;
    public Spell CorpseExplosion;
    public Spell BloodFury;
    public Spell ArcaneTorrent;
    public Spell Lifeblood;
    public Spell HowlingBlast;
    //пал

    public Spell SealofRighteousness;
    public Spell SealofWisdom;
    public Spell SealofCommand;
    public Spell DivineShield;
    public Spell LayonHands;
    public Spell DivineProtection;
    public Spell HolyLight;
    public Spell SealofLight;
    public Spell HandofReckoning;
    public Spell JudgementofWisdom;
    public Spell JudgementofLight;
    public Spell JudgementofJustice;
    public Spell FlashofLight;
    public Spell AvengingWrath;
    public Spell Repentance;
    public Spell DivineStorm;
    public Spell HammerofWrath;
    public Spell ShieldofRighteousness;
    public Spell CrusaderStrike;
    public Spell Consecration;
    public Spell Exorcism;
    public Spell SacredShield;
    public Spell HandofFreedom;
    public Spell DivinePlea;
    public Spell Cleanse;
    public Spell BlessingofMight;
    public Spell BlessingofWisdom;
    public Spell DevotionAura;
    public Spell RighteousFury;
    public Spell HammerofJustice;
    public Spell HolyWrath;
    public Spell RetributionAura;
    public Spell BlessingofKings;
    //прист
    public Spell ShadowWordPain;
    public Spell VampiricTouch;
    public Spell DevouringPlague;
    public Spell MindBlast;
    public Spell MindFlay;
    public Spell DispelMagic;
    public Spell AbolishDisease;
    public Spell CureDisease;
    public Spell PsychicScream;
    public Spell VampiricEmbrace;
    public Spell InnerFire;
    public Spell PowerWordShield;
    public Spell FlashHeal;
    public Spell Renew;
    public Spell Shadowform;
    public Spell Shadowfiend;
    public Spell HymnofHope;
    public Spell Dispersion;
    public Spell ShadowWordDeath;
    public Spell PowerWordFortitude;
    public Spell DivineSpirit;
    public Spell MindSear;
    public Spell DivineHymn;
    public Spell PrayerofMending;
    public Spell GreaterHeal;
    public Spell HolyNova;
    public Spell Silence;
    public Spell PsychicHorror;
    //маг
    public Spell FrostNova;
    public Spell LivingBomb;
    public Spell Scorch;
    public Spell FireBlast;
    public Spell Pyroblast;
    public Spell MoltenArmor;
    public Spell ArcaneIntellect;
    public Spell DragonsBreath;
    public Spell Flamestrike;
    public Spell Havchik;
    public Spell CreateManaGem;
    public Spell BlastWave;
    public Spell Evocation;
    public Spell Mirrors;
    public Spell ManaShield;
    public Spell Blink;
    public Spell Combustion;
    public Spell IceBlock;
    public Spell FireBall;
    public Spell FrostfireBolt;
    public Spell Ship;
    public Spell Inviz;
    public Spell ConeofCold;
    //шаманы
    public Spell WindfuryWeapon;
    public Spell FlametongueWeapon;
    public Spell LightningShield;
    public Spell WaterShield;
    public Spell HealingWave;
    public Spell LesserHealingWave;
    public Spell EarthShock;
    public Spell FlameShock;
    public Spell FrostShock;
    public Spell lavaBurst;
    public Spell ChainLightning;
    public Spell FireNova;
    public Spell LightningBolt;
    public Spell Thunderstorm;
    public Spell ElementalMastery;
    public Spell Hex;
    public Spell Bloodlust;
    public Spell Heroism;
    public Spell Stormstrike;
    public Spell Lavalash;
    public Spell FireElementalTotem;
    public Spell EarthElementalTotem;
    public Spell WindShear;
    public Spell GhostWolf;
    public Spell Berserk;
    public Spell FeralSpirit;
    public Spell ShamanisticRage;
    //вар
    public Spell Intercept;
    public Spell DeathWish;
    public Spell BerserkerRage;
    public Spell EnragedRegeneration;
    public Spell WarStomp;
    public Spell Bloodthirst;
    public Spell Whirlwind;
    public Spell Cleave;
    public Spell VictoryRush;
    public Spell Slam;
    public Spell HeroicStrike;
    public Spell Execute;
    public Spell Retaliation;
    public Spell HeroicThrow;
    public Spell Pummel;
    public Spell Bloodrage;
    public Spell BattleShout;
    public Spell CommandingShout;
    public Spell BerserkerStance;
    public Spell Recklessness;
    public Spell DemoralizingShout;
    public Spell Giftofnaaru;
    public Spell Hamstring;
    public Spell Fear;
    public Spell Charge;
    public Spell BattleStance;
    public Spell DefensiveStance;
    public Spell Devastate;
    public Spell Revenge;
    public Spell ShieldSlam;
    public Spell Intervene;
    public Spell Disarm;
    public Spell ShieldBash;
    public Spell Shockwave;
    public Spell Taunt;
    public Spell ThunderClap;
    public Spell ShieldBlock;
    public Spell LastStand;
    public Spell ConcussionBlow;
    public Spell ShieldWall;
    public Spell SpellReflection;
    public Spell InnerRage;
    public Spell RagingBlow;
    public Spell RallyingCry;
    public Spell HeroicLeap;
    public Spell HeroicFury;
    public Spell InnerFury;

    //дру
    public Spell Maul;
    public Spell Bash;
    public Spell SwipeBear;
    public Spell SwipeCat;
    public Spell Claw;
    public Spell FeralChargeBear;
    public Spell FeralChargeCat;
    public Spell Rip;
    public Spell Rake;
    public Spell FerociousBite;
    public Spell Ravage;
    public Spell Pounce;
    public Spell MangleBear;
    public Spell MangleCat;
    public Spell Shred;
    public Spell Maim;
    public Spell Lacerate;
    public Spell MarkoftheWild;
    public Spell Thorns;
    public Spell Enrage;
    public Spell TigersFury;
    public Spell Dash;
    public Spell NaturesSwiftness;
    public Spell FrenziedRegeneration;
    public Spell LeaderofthePack;
    public Spell Barkskin;
    public Spell SurvivalInstincts;
    public Spell AquaticForm;
    public Spell TravelForm;
    public Spell CatForm;
    public Spell DireBearForm;
    public Spell TreeofLife;
    public Spell HealingTouch;
    public Spell Rejuvenation;
    public Spell Regrowth;
    public Spell Rebirth;
    public Spell Tranquility;
    public Spell Swiftmend;
    public Spell WildGrowth;
    public Spell Lifebloom;
    public Spell Nourish;
    public Spell Growl;
    public Spell FaerieFireFeral;
    public Spell FaerieFire;
    public Spell Prowl;
    public Spell Cower;
    public Spell Innervate;
    public Spell EntanglingRoots;
    public Spell NaturesGrasp;
    public Spell Hibernate;
    public Spell Typhoon;
    public Spell DemoralizingRoar;
    public Spell ChallengingRoar;
    public Spell CurePoison;
    public Spell RemoveCurse;
    public Spell AbolishPoison;
    public Spell SavageRoar;
    public Spell MoonkinForm;
    public Spell MoonFire;
    public Spell StarFall;
    public Spell Wrath;
    public Spell Pni;
    public Spell Hurricane;
    public Spell Cyclone;
    public Spell Starfire;
    //варлок
    public Spell Immolate;
    public Spell Conflagrate;
    public Spell Incinerete;
    public Spell ChaosBolt;
    public Spell ShadowFury;
    public Spell ShadowFlame;
    public Spell Imp;
    public Spell Void;
    public Spell FelArmor;
    public Spell WarlockFear;
    public Spell SoulLink;
    public Spell HowlofTerror;
    public Spell DrainSoul;
    public Spell CreateHealthstone;
    public Spell SearingPain;
    public Spell LifeTap;
    public Spell Rain;
    public Spell Shitok;
    public Spell ConsumeShadows;
    public Spell Corruption;
    public Spell DrainLife;
    public Spell UnstableAffliction;
    public Spell Haunt;
    public Spell ShadowBolt;
    public Spell CurseofAgony;
    public Spell CurseofElements;
    public Spell Sacrifice;
    public Spell DemonArmor;
    //хант
    public Spell HuntersMark;
    public Spell SerpentSting;
    public Spell ArcaneShot;
    public Spell ConcussiveShot;
    public Spell MultiShot;
    public Spell SteadyShot;
    public Spell KillShot;
    public Spell RaptorStrike;
    public Spell MongooseBite;
    public Spell AspecoftheMonkey;
    public Spell AspecoftheHawk;
    public Spell AspecoftheCheetah;
    public Spell AspecoftheViper;
    public Spell AspecoftheDragonhawk;
    public Spell RapidFire;
    public Spell KillCommand;
    public Spell RevivePet;
    public Spell CallPet;
    public Spell MendPet;
    public Spell Intimidation;
    public Spell BestialWrath;
    public Spell Disengage;
    public Spell FrostTrap;
    public Spell SnakeTrap;
    public Spell ExplosiveTrap;
    public Spell FeignDeath;
    public Spell Topori;
    public Spell Volley;
    public Spell Blizzard;
    public bool MeMeleek = false;
    public void Dispose()
    {
        wManager.Events.FightEvents.OnFightLoop -= VarManager;
        Attacker = null;
        EventsLuaWithArgs.OnEventsLuaWithArgs -= CheckLuaEvents;
        log("Disposed.");
    }

    public void Initialize()
    {

        Attacker = null;
        initializeHatersNamesList();
        while (!Conditions.InGameAndConnected && Products.IsStarted)
        {
            log("Started in Logount Thread.Sleep()");
            Thread.Sleep(10000);
        }
        int count = 0;
        while (!Var.Exist("HatersNamesList") && Products.IsStarted && count < 5)
        {
            count++;
            log("Плагин старотовал, но переменная робота HatersNamesList еще не объявлена, ожидаем.");
            Thread.Sleep(5000);
        }
        if (Var.Exist("HatersNamesList"))
        {
            foreach (var name in Var.GetVar<List<string>>("HatersNamesList"))
            {
                if (!HatersNames.Contains(name))
                    HatersNames.Add(name);
            }
            log("обновлен список имен хейтеров");
        }
        InitializeClassSpells();
        _saytimer.ForceReady();
        _chektargettimer.ForceReady();
        _WaitGankTimer.ForceReady();
        if (ObjectManager.Me.WowClass == WoWClass.DeathKnight || ObjectManager.Me.WowClass == WoWClass.Paladin || ObjectManager.Me.WowClass == WoWClass.Rogue || ObjectManager.Me.WowClass == WoWClass.DeathKnight || (ObjectManager.Me.WowClass == WoWClass.Druid && SpellManager.KnowSpell(33876)) || (ObjectManager.Me.WowClass == WoWClass.Shaman && SpellManager.KnowSpell(17364)))
        {
            MeMeleek = true;
        }
        CapchaTimer.ForceReady();
        //wManager.Events.FightEvents.OnFightEnd += FightEventsOnOnFightEnd;
        RequestHandlerSettings.Load();
        EventsLuaWithArgs.OnEventsLuaWithArgs += CheckLuaEvents;
        wManager.Events.FightEvents.OnFightLoop += VarManager;
        log("Initialized... Tracking LUA events.");
    }

    private void log(string msg)
    {
        Logging.Write("[RequestHandler]: " + msg, Logging.LogType.Normal, System.Drawing.Color.DarkCyan);
    }
    private void RandomWait()
    {
        int ms = new System.Random().Next(RequestHandlerSettings.CurrentSetting.WaitMin, RequestHandlerSettings.CurrentSetting.WaitMax);
        System.Threading.Thread.Sleep(ms);
    }
    private void InitializeClassSpells()
    {
        //Thread.Sleep(10000);
        if (ObjectManager.Me.WowClass == WoWClass.DeathKnight)
        {
            Lichborne = new Spell("Lichborne");
            EveryManForHimself = new Spell("Every Man For Himself");
            MindFreeze = new Spell("Mind Freeze");
            FrostPresence = new Spell("Frost Presence");
            DarkCommand = new Spell("Dark Command");
            RaiseDead = new Spell("Raise Dead");
            ArmyoftheDead = new Spell("Army of the Dead");
            SummonGargoyle = new Spell("Summon Gargoyle");
            AntiMagicShell = new Spell("Anti-Magic Shell");
            Hysteria = new Spell("Hysteria");
            DancingRuneWeapon = new Spell("Dancing Rune Weapon");
            Gnaw = new Spell("Gnaw");
            Strangulate = new Spell("Strangulate");
            DeathPact = new Spell("Death Pact");
            Pestilence = new Spell("Pestilence");
            EmpowerRuneWeapon = new Spell("Empower Rune Weapon");
            DeathGrip = new Spell("Death Grip");
            BloodTap = new Spell("Blood Tap");
            IceboundFortitude = new Spell("Icebound Fortitude");
            VampiricBlood = new Spell("Vampiric Blood");
            GiftoftheNaaru = new Spell("Gift of the Naaru");
            RuneTap = new Spell("Rune Tap");
            MarkofBlood = new Spell("Mark of Blood");
            ChainsofIce = new Spell("Chains of Ice");
            PlagueStrike = new Spell("Plague Strike");
            IcyTouch = new Spell("Icy Touch");
            DeathStrike = new Spell("Death Strike");
            RuneStrike = new Spell("Rune Strike");
            DeathCoil = new Spell("Death Coil");
            HeartStrike = new Spell("Heart Strike");
            ScourgeStrike = new Spell("Scourge Strike");
            BloodStrike = new Spell("Blood Strike");
            HornofWinter = new Spell("Horn of Winter");
            GhoulFrenzy = new Spell("Ghoul Frenzy");
            DeathandDecay = new Spell("Death and Decay");
            BloodBoil = new Spell("Blood Boil");
            BoneShield = new Spell("Bone Shield");
            BloodPresence = new Spell("Blood Presence");
            PathofFrost = new Spell("Path of Frost");
            FrostStrike = new Spell("Frost Strike");
            Obliterate = new Spell("Obliterate");
            UnholyPresence = new Spell("Unholy Presence");
            AntiMagicZone = new Spell("Anti-Magic Zone");
            CorpseExplosion = new Spell("Corpse Explosion");
            BloodFury = new Spell("Blood Fury");
            HowlingBlast = new Spell("Howling Blast");

        }
        if (ObjectManager.Me.WowClass == WoWClass.Paladin)
        {
            SealofRighteousness = new Spell("Seal of Righteousness");
            SealofWisdom = new Spell("Seal of Wisdom");
            SealofCommand = new Spell("Seal of Command");
            DivineShield = new Spell("Divine Shield");
            LayonHands = new Spell("Lay on Hands");
            DivineProtection = new Spell("Divine Protection");
            HolyLight = new Spell("Holy Light");
            SealofLight = new Spell("Seal of Light");
            HandofReckoning = new Spell("Hand of Reckoning");
            JudgementofWisdom = new Spell("Judgement of Wisdom");
            JudgementofLight = new Spell("Judgement of Light");
            JudgementofJustice = new Spell("Judgement of Justice");
            FlashofLight = new Spell("Flash of Light");
            AvengingWrath = new Spell("Avenging Wrath");
            Repentance = new Spell("Repentance");
            DivineStorm = new Spell("Divine Storm");
            HammerofWrath = new Spell("Hammer of Wrath");
            ShieldofRighteousness = new Spell("Shield of Righteousness");
            CrusaderStrike = new Spell("Crusader Strike");
            Consecration = new Spell("Consecration");
            Exorcism = new Spell("Exorcism");
            SacredShield = new Spell("Sacred Shield");
            HandofFreedom = new Spell("Hand of Freedom");
            DivinePlea = new Spell("Divine Plea");
            Cleanse = new Spell("Cleanse");
            BlessingofMight = new Spell("Blessing of Might");
            BlessingofWisdom = new Spell("Blessing of Wisdom");
            DevotionAura = new Spell("Devotion Aura");
            RighteousFury = new Spell("Righteous Fury");
            HammerofJustice = new Spell("Hammer of Justice");
            HolyWrath = new Spell("Holy Wrath");
            RetributionAura = new Spell("Retribution Aura");
            BlessingofKings = new Spell("Blessing of Kings");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Priest)
        {
            ShadowWordPain = new Spell("Shadow Word: Pain");
            VampiricTouch = new Spell("Vampiric Touch");
            DevouringPlague = new Spell("Devouring Plague");
            MindBlast = new Spell("Mind Blast");
            MindFlay = new Spell("Mind Flay");
            DispelMagic = new Spell("Dispel Magic");
            AbolishDisease = new Spell("Abolish Disease");
            CureDisease = new Spell("Cure Disease");
            PsychicScream = new Spell("Psychic Scream");
            VampiricEmbrace = new Spell("Vampiric Embrace");
            InnerFire = new Spell("Inner Fire");
            PowerWordShield = new Spell("Power Word: Shield");
            FlashHeal = new Spell("Flash Heal");
            Renew = new Spell("Renew");
            Shadowform = new Spell("Shadowform");
            Shadowfiend = new Spell("Shadowfiend");
            HymnofHope = new Spell("Hymn of Hope");
            Dispersion = new Spell("Dispersion");
            ShadowWordDeath = new Spell("Shadow Word: Death");
            PowerWordFortitude = new Spell("Power Word: Fortitude");
            DivineSpirit = new Spell("Divine Spirit");
            MindSear = new Spell("Mind Sear");
            DivineHymn = new Spell("Divine Hymn");
            PrayerofMending = new Spell("Prayer of Mending");
            GreaterHeal = new Spell("Greater Heal");
            HolyNova = new Spell("Holy Nova");
            Silence = new Spell("Silence");
            PsychicHorror = new Spell("Psychic Horror");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Mage)
        {
            FrostNova = new Spell("Frost Nova");
            LivingBomb = new Spell("Living Bomb");
            Scorch = new Spell("Scorch");
            FireBlast = new Spell("Fire Blast");
            Pyroblast = new Spell("Pyroblast");
            MoltenArmor = new Spell("Molten Armor");
            ArcaneIntellect = new Spell("Arcane Intellect");
            DragonsBreath = new Spell("Dragon's Breath");
            Flamestrike = new Spell("Flamestrike");
            Havchik = new Spell("Conjure Refreshment");
            CreateManaGem = new Spell("Conjure Mana Gem");
            BlastWave = new Spell("Blast Wave");
            Evocation = new Spell("Evocation");
            Mirrors = new Spell("Mirror Image");
            ManaShield = new Spell("Mana Shield");
            Blink = new Spell("Blink");
            Combustion = new Spell("Combustion");
            IceBlock = new Spell("Ice Block");
            FireBall = new Spell("Fireball");
            FrostfireBolt = new Spell("Frostfire Bolt");
            Ship = new Spell("Polymorph");
            Inviz = new Spell("Invisibitity");
            ConeofCold = new Spell("Cone of Cold");
            Blizzard = new Spell("Blizzard");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Shaman)
        {
            WindfuryWeapon = new Spell("Windfury Weapon");
            FlametongueWeapon = new Spell("Flametongue Weapon");
            LightningShield = new Spell("Lightning Shield");
            WaterShield = new Spell("Water Shield");
            HealingWave = new Spell("Healing Wave");
            LesserHealingWave = new Spell("Lesser Healing Wave");
            EarthShock = new Spell("Earth Shock");
            FlameShock = new Spell("Flame Shock");
            FrostShock = new Spell("Frost Shock");
            lavaBurst = new Spell("Lava Burst");
            ChainLightning = new Spell("Chain Lightning");
            FireNova = new Spell("Fire Nova");
            LightningBolt = new Spell("Lightning Bolt");
            Thunderstorm = new Spell("Thunderstorm");
            ElementalMastery = new Spell("Elemental Mastery");
            Hex = new Spell("Hex");
            Bloodlust = new Spell("Bloodlust");
            Heroism = new Spell("Heroism");
            Stormstrike = new Spell("Stormstrike");
            Lavalash = new Spell("Lava Lash");
            FireElementalTotem = new Spell("Fire Elemental Totem");
            EarthElementalTotem = new Spell("Earth Elemental Totem");
            WindShear = new Spell("Wind Shear");
            GhostWolf = new Spell("Ghost Wolf");
            Berserk = new Spell("Berserk");
            FeralSpirit = new Spell("Feral Spirit");
            ShamanisticRage = new Spell("Shamanistic Rage");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Warrior)
        {
            Intercept = new Spell("Intercept");
            DeathWish = new Spell("Death Wish");
            BerserkerRage = new Spell("Berserker Rage");
            EnragedRegeneration = new Spell("Enraged Regeneration");
            WarStomp = new Spell("War Stomp");
            Bloodthirst = new Spell("Bloodthirst");
            Whirlwind = new Spell("Whirlwind");
            Cleave = new Spell("Cleave");
            VictoryRush = new Spell("Victory Rush");
            Slam = new Spell("Slam");
            HeroicStrike = new Spell("Heroic Strike");
            Execute = new Spell("Execute");
            Retaliation = new Spell("Retaliation");
            HeroicThrow = new Spell("Heroic Throw");
            Pummel = new Spell("Pummel");
            Bloodrage = new Spell("Bloodrage");
            BattleShout = new Spell("Battle Shout");
            CommandingShout = new Spell("Commanding Shout");
            BerserkerStance = new Spell("Berserker Stance");
            Recklessness = new Spell("Recklessness");
            DemoralizingShout = new Spell("Demoralizing Shout");
            Giftofnaaru = new Spell("Gift of the Naaru");
            Hamstring = new Spell("Hamstring");
            Fear = new Spell("Intimidating Shout");
            Charge = new Spell("Charge");
            BattleStance = new Spell("Battle Stance");
            DefensiveStance = new Spell("Defensive Stance");
            Devastate = new Spell("Devastate");
            Revenge = new Spell("Revenge");
            ShieldSlam = new Spell("Shield Slam");
            Intervene = new Spell("Intervene");
            Disarm = new Spell("Disarm");
            ShieldBash = new Spell("Shield Bash");
            Shockwave = new Spell("Shockwave");
            Taunt = new Spell("Taunt");
            ThunderClap = new Spell("Thunder Clap");
            ShieldBlock = new Spell("Shield Block");
            LastStand = new Spell("Last Stand");
            ConcussionBlow = new Spell("Concussion Blow");
            ShieldWall = new Spell("Shield Wall");
            SpellReflection = new Spell("Spell Reflection");
            InnerRage = new Spell("Inner Rage");
            RagingBlow = new Spell("Raging Blow");
            RallyingCry = new Spell("Rallying Cry");
            HeroicLeap = new Spell("Heroic Leap");
            HeroicFury = new Spell("Heroic Fury");
            InnerFury = new Spell("Inner Fury");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Druid)
        {
            Maul = new Spell("Maul");
            Bash = new Spell("Bash");
            SwipeBear = new Spell("Swipe (Bear)");
            SwipeCat = new Spell("Swipe (Cat)");
            Claw = new Spell("Claw");
            FeralChargeBear = new Spell("Feral Charge - Bear");
            FeralChargeCat = new Spell("Feral Charge - Cat");
            Rip = new Spell("Rip");
            Rake = new Spell("Rake");
            FerociousBite = new Spell("Ferocious Bite");
            Ravage = new Spell("Ravage");
            Pounce = new Spell("Pounce");
            MangleBear = new Spell("Mangle (Bear)");
            MangleCat = new Spell("Mangle (Cat)");
            Shred = new Spell("Shred");
            Maim = new Spell("Maim");
            Lacerate = new Spell("Lacerate");
            MarkoftheWild = new Spell("Mark of the Wild");
            Thorns = new Spell("Thorns");
            Enrage = new Spell("Enrage");
            TigersFury = new Spell("Tiger's Fury");
            Dash = new Spell("Dash");
            NaturesSwiftness = new Spell("Nature's Swiftness");
            FrenziedRegeneration = new Spell("Frenzied Regeneration");
            LeaderofthePack = new Spell("Leader of the Pack");
            Barkskin = new Spell("Barkskin");
            SurvivalInstincts = new Spell("Survival Instincts");
            AquaticForm = new Spell("Aquatic Form");
            TravelForm = new Spell("Travel Form");
            CatForm = new Spell("Cat Form");
            DireBearForm = new Spell("Dire Bear Form");
            TreeofLife = new Spell("Tree of Life");
            HealingTouch = new Spell("Healing Touch");
            Rejuvenation = new Spell("Rejuvenation");
            Regrowth = new Spell("Regrowth");
            Rebirth = new Spell("Rebirth");
            Tranquility = new Spell("Tranquility");
            Swiftmend = new Spell("Swiftmend");
            WildGrowth = new Spell("Wild Growth");
            Lifebloom = new Spell("Lifebloom");
            Nourish = new Spell("Nourish");
            Growl = new Spell("Growl");
            FaerieFireFeral = new Spell("Faerie Fire (Feral)");
            FaerieFire = new Spell("Faerie Fire");
            Prowl = new Spell("Prowl");
            Cower = new Spell("Cower");
            Innervate = new Spell("Innervate");
            EntanglingRoots = new Spell("Entangling Roots");
            NaturesGrasp = new Spell("Nature's Grasp");
            Hibernate = new Spell("Hibernate");
            Typhoon = new Spell("Typhoon");
            DemoralizingRoar = new Spell("Demoralizing Roar");
            ChallengingRoar = new Spell("Challenging Roar");
            CurePoison = new Spell("Cure Poison");
            RemoveCurse = new Spell("Remove Curse");
            AbolishPoison = new Spell("Abolish Poison");
            SavageRoar = new Spell("Savage Roar");
            MoonkinForm = new Spell("Moonkin Form");
            MoonFire = new Spell("Moonfire");
            StarFall = new Spell("StarFall");
            Wrath = new Spell("Wrath");
            Pni = new Spell("Force of Nature");
            Hurricane = new Spell("Hurricane");
            Cyclone = new Spell("Cyclone");
            Starfire = new Spell("Starfire");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Warlock)
        {
            Immolate = new Spell("Immolate");
            Conflagrate = new Spell("Conflagrate");
            Incinerete = new Spell("Incinerate");
            ShadowFlame = new Spell("Shadowflame");
            Imp = new Spell("Summon Imp");
            Void = new Spell("Summon Voidwalker");
            FelArmor = new Spell("Fel Armor");
            WarlockFear = new Spell("Fear");
            SoulLink = new Spell("Soul Link");
            HowlofTerror = new Spell("Howl of Terror");
            DrainSoul = new Spell("Drain Soul");
            CreateHealthstone = new Spell("Create Healthstone");
            SearingPain = new Spell("Searing Pain");
            LifeTap = new Spell("Life Tap");
            Rain = new Spell("Rain of Fire");
            Shitok = new Spell("Sacrifice");
            ConsumeShadows = new Spell("Consume Shadows");
            Corruption = new Spell("Corruption");
            DrainLife = new Spell("Drain Life");
            UnstableAffliction = new Spell("Unstable Affliction");
            Haunt = new Spell("Haunt");
            ShadowBolt = new Spell("Shadow Bolt");
            CurseofAgony = new Spell("Curse of Agony");
            CurseofElements = new Spell("Curse of the Elements");
            Sacrifice = new Spell("Sacrifice");
            DemonArmor = new Spell("Demon Armor");
        }
        if (ObjectManager.Me.WowClass == WoWClass.Hunter)
        {
            HuntersMark = new Spell("Hunter's Mark");
            SerpentSting = new Spell("Serpent Sting");
            ArcaneShot = new Spell("Arcane Shot");
            ConcussiveShot = new Spell("Concussive Shot");
            MultiShot = new Spell("Multi-Shot");
            SteadyShot = new Spell("Steady Shot");
            KillShot = new Spell("Kill Shot");
            RaptorStrike = new Spell("Raptor Strike");
            MongooseBite = new Spell("Mongoose Bite");
            AspecoftheMonkey = new Spell("Aspect of the Monkey");
            AspecoftheHawk = new Spell("Aspect of the Hawk");
            AspecoftheCheetah = new Spell("Aspect of the Cheetah");
            AspecoftheViper = new Spell("Aspect of the Viper");
            AspecoftheDragonhawk = new Spell("Aspect of the Dragonhawk");
            RapidFire = new Spell("Rapid Fire");
            KillCommand = new Spell("Kill Command");
            RevivePet = new Spell("Revive Pet");
            CallPet = new Spell("Call Pet");
            MendPet = new Spell("Mend Pet");
            Intimidation = new Spell("Intimidation");
            BestialWrath = new Spell("Bestial Wrath");
            Disengage = new Spell("Disengage");
            FrostTrap = new Spell("Frost Trap");
            SnakeTrap = new Spell("Snake Trap");
            ExplosiveTrap = new Spell("Explosive Trap");
            FeignDeath = new Spell("Feign Death");
            Topori = new Spell("Deterrence");
            Volley = new Spell("Volley");
        }
    }
    public List<string> ReplyPartyYasno = new List<string>()
        {
            "понятно, удачи",
            "ясно",
            "ясн",
            "понял",
            "))",
            ")",
            "...",
            "мда",
            "facepalm",
            "ок",
            "what?",
            "что?)",
            "а, понял",
            "понятно",
            "yasn",
            "пфф",
            "лол",
            "кек",
            "lol",
            "лул",
            "kek)",
            "давай",
            "всего плохого",
            "удачи",
            "пиздец)",
            "гений...",
            "кек)",
            "yasnо))",
            "пфф",
            "лол)",
            "кек)",
            "lol)",
            "лул",
            "яснопонятно",
            "мне пора",
            "дурачек",
            "ретард",
            "retard",
            "пока.",
            "пака",
            "пакеда",
            "удачки)",
            "ну тебя)",
            "да ну тебя",
            "ну нах",
            "ну нахуй",
            "в пизду",
            "к черту",
            "без обид",
            "бывает.",
            "писец",
            "Писос",
            "пиздец)",
            "ну нах",
            "чего блядь?)",
            "чиво бля?",
            "да пошел ты",
            "Нахуй послан",
            "ну нах",
            "гениально,а я здесь при чем?",
            "отсекайся",
            "а ты смешной",
            "клоун..",
            "досвидания",
            "ниче не понял",
            "хуй тебе",
            "хуй тебе)",
            "хуйня",
            "хуйня это всё",
            "Хуйня все это",
            "нахуй надо",
            "гуляй",
            "гуляй вася",
            "странный ты тип",
            "странный ты",
            "держись от меня по дальше",
            "лапшу мне на уши вешаешь",
            "пиздабол",
            "не пизди",
            "пиздишь дохуя",
            "дохуя умный",
            "дохуя умный?",
            "ну нах",
            "нах такое делать?",
            "жаль конечно",
            "жаль тебя",
            "дичь",
            "втираешь дичь",
            "ты втираешь мне какую-то дичь)",
            "ебобо",
            "та я ебал",
            "хуй пизда джигурда",
            "всего хорошего",
            "всего наихудшего" +
        "да уж",
            "мне от тебя тошнит",
            "нюхай плинтус",
            "нюхай писю",
            "мне похуй",
            "похуй",
            "пахую",
            "срать вообще",
            "ваще тупо пох",
            "поебать",
        };
    public List<string> PartyQuestion = new List<string>()
        {
            "?",
            "??",
            "???",
            "что?",
            "нахуя пати?",
            "че хотел?",
            "че хочешь?",
            "че надо?",
            "зачем пати?",
            "зачем инвайт?",
            "шо?",
            "чего?",
            "эм?",
            "мм?",
            "что нужно?",
            "ку, пати зачем кинул?",
            "пати зачем кинул?",
            "?????????",
            "что еще?",
            "тут?",
        };
    public List<string> ReplyPartyInviteWhisper = new List<string>()
        {
            "?",
            "??",
            "???",
            "?????????",
            "что?",
            "что?)",
            "чё?",
            "че?",
            "че?)",
            "чё?))",
            "what?",
            "что?)",
            "что?",
            "что хочешь?",
            "че те надо?",
            "че те надо?))",
            "чего тебе?)",
            "че тебе?",
            "чё хотел?",
            "че хочешь?",
            "че хочеш?)",
            "чево тебе?",
            "шо?",
            "ну и шо?",
            "шо тебе надо?",
            "ну и шо?)",
            "шо тебе надо?)",
            "шо забыл?",
            "чё забыл?)",
            "привет",
            "какого лысово тебе надо?",
            "какого лысово тебе надо?)",
            "что тебе нужно?",
            "мм?",
            "м?",
            "делать нех?",
            "че пристал ко мне?",
            "че пристал ко мне?)",
            "тебе нечем заняться?)",
            "слыш гусь, ты че?",
            "ты офигел?",
            "че за прикол?",
            "в чём рофл?",
            "и в чем прикол?",
            "ты кто?",
            "ты кто такой?",
            "хто ты такой?",
            "ты хто",
            "ну?",
            "и?",
            "и что ?",
            "ну и че?" +
            "ты норм?",
            "кек",
            "лол",
            "ты меня хочешь?",
            "хули пристал?",
            "хули прицыпился?",
            "хули нада?",
            "как оно весело?",
            "много времени свободного?",
            "ты шо дурко?",
            "ты че сдурел?",
            "дурачек?",
            "додик?",
            "даун?",
            "петушок?",
            "соси",
            "клоун",
            "алеша?",
            "снова ты?",
            "лох?",
            "нах ты нужен?",
            "ты гей?",
            "ты мало говна поел?",
            "зачем пати?",
            "нах пати кидаешь?",
            "привет",
            "привет, че хотел?",
            "зачем пати?",
            "нах пати?",
            "зачем инвайт кинул?",
            "зачем пати?",
            "шо еще?",
            "и?",
            "че каво?",
            "ку, че надо?",
            "зачем пати?",
            "не проси помочь мне некогда",
            "отвали",
            "ебобо?",
            "умри",
            "отцепись",
            "удали вов",
            "еблан",
            "отввянь",
            "отвали)",
            "отвянь)",
            "отстань)",
            "отстань",
            "отъебись",
            "отебисб",
            "атыбысь",
            "отстань ты от меня",
            "ты заебла меня",
            "ты заебал",
            "заебал",
            "Заипал",
            "ну что еще",
            "что опять?",
            "нужна помощь?",
            "помочь?",
            "нид хелп?",
            "бля",
            "не видишь я занят?",
            "занят",
            "я занят",
            "pfyzn",
            "xnj nfrjt",
            "blb yf[eq gbljh t,fysq",
            "[eq cjcb",
            "gjitk yf[eq",
            "blb yf[eq gbljh t,fysq",
            "нахуй послан",
            "от геев инвайты не принимаю",
            "хули доебался?",
            "хули тебе блядь надо?",
            "хуй пизда джигурда",
            "ну и что ты хочешь?",
            "капец",
        };
    private bool MeIsMeleeClass()
    {
        return (ObjectManager.Me.WowClass == WoWClass.DeathKnight || ObjectManager.Me.WowClass == WoWClass.Paladin || ObjectManager.Me.WowClass == WoWClass.Rogue || ObjectManager.Me.WowClass == WoWClass.DeathKnight || (ObjectManager.Me.WowClass == WoWClass.Druid && SpellManager.KnowSpell(33876)) || (ObjectManager.Me.WowClass == WoWClass.Shaman && SpellManager.KnowSpell(17364)));
    }
    private void RandomEmote()
    {
        int direction = Others.Random(1, 21);
        if (direction == 1)
            Lua.LuaDoString("DoEmote('rofl')");
        if (direction == 2)
            Lua.LuaDoString("DoEmote('guffaw')");
        if (direction == 3)
            Lua.LuaDoString("DoEmote('rasp')");
        if (direction == 4)
            Lua.LuaDoString("DoEmote('wait')");
        if (direction == 5)
            Lua.LuaDoString("DoEmote('no')");
        if (direction == 6)
            Lua.LuaDoString("DoEmote('laugh')");
        if (direction == 7)
            Lua.LuaDoString("DoEmote('burp')");
        if (direction == 8)
            Lua.LuaDoString("DoEmote('cackle')");
        if (direction == 9)
            Lua.LuaDoString("DoEmote('confused')");
        if (direction == 10)
            Lua.LuaDoString("DoEmote('shoo')");
        if (direction == 11)
            Lua.LuaDoString("DoEmote('puzzled')");
        if (direction == 12)
            Lua.LuaDoString("DoEmote('slap')");
        if (direction == 13)
            Lua.LuaDoString("DoEmote('snicker')");
        if (direction == 14)
            Lua.LuaDoString("DoEmote('sniff')");
        if (direction == 15)
            Lua.LuaDoString("DoEmote('spit')");
        if (direction == 16)
            Lua.LuaDoString("DoEmote('surrender')");
        if (direction == 17)
            Lua.LuaDoString("DoEmote('surprised')");
        if (direction == 18)
            Lua.LuaDoString("DoEmote('tired')");
        if (direction == 19)
            Lua.LuaDoString("DoEmote('hello')");
        if (direction == 20)
            Lua.LuaDoString("DoEmote('flex')");
        if (direction == 21)
            Lua.LuaDoString("DoEmote('disappointed')");
        //System.Threading.Thread.Sleep(240000);
    }
    private void CheckLuaEvents(LuaEventsId id, List<string> args)
    {
        if (Conditions.InGameAndConnectedAndProductStartedNotInPause)
        {
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "START_LOOT_ROLL"))
            {
                if (RequestHandlerSettings.CurrentSetting.LootR)
                {
                    RandomWait();
                    int rollType;
                    switch (RequestHandlerSettings.CurrentSetting.LootRSetting)
                    {
                        case "Need":
                            rollType = 1;
                            log("Rolling Need.");
                            break;
                        case "Greed":
                            rollType = 2;
                            log("Rolling Greed.");
                            break;
                        case "Pass":
                            rollType = 0;
                            log("Rolling Pass.");
                            break;
                        default:
                            rollType = 2;
                            log("Rolling Greed.");
                            break;
                    }
                    Lua.LuaDoString("for i=1,4 do if _G['GroupLootFrame'..i]:IsShown() then _G['GroupLootFrame'..i..'RollButton']:Click() end end");
                }
                //return;
            }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "PLAYER_DEAD")) //ганк чек
            {
                //чекаем ганк ли это
                log("PLAYER_DEAD check gank");
                if (Attacker != null && Attacker.IsValid)
                {
                    Var.SetVar("Ganker", Attacker);
                    Lua.LuaDoString("print('Ганк от " + Attacker.Name + "')");
                    log("Ганк от " + Attacker.Name + "");
                    //string message = ObjectManager.Me.Name + " server: [" + Usefuls.RealmName + "] gank from " + Attacker.Name + " zone: [" + Usefuls.MapZoneName + "] subzone: [" + Usefuls.SubMapZoneName + "] position: [" + ObjectManager.Me.Position + "]";
                    //TGSMAlert(LetsGoldBotToken, GankInfoChannelID, message);
                }
                if ((Attacker == null || !Attacker.IsValid) && ObjectManager.GetObjectWoWPlayer().Count(p => p.IsAttackable && p.GetDistance <= 30) > 0)
                {
                    Var.SetVar("Ganker", ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer().FindAll(p => p.IsAttackable && p.GetDistance <= 40)));
                    Lua.LuaDoString("print('Возможный Ганк от игрока " + ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer().FindAll(p => p.IsAttackable)).Name + "')");
                    log("Возможный Ганк от игрока " + ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer().FindAll(p => p.IsAttackable)).Name + "");
                }
            }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "CHAT_MSG_MONSTER_EMOTE")) //убегание мобав
            {

                if ((args[0].Contains("бежать") || args[0].Contains("run away")) && ObjectManager.Me.InCombatFlagOnly && ObjectManager.Target.Name == args[1] && ObjectManager.Target.GetMove)
                {
                    log(args[1] + " убегает");
                    Lua.LuaDoString("print('" + args[1] + " убегает')");
                    if (ObjectManager.Me.WowClass == WoWClass.Paladin)
                    {
                        while ((/*(!HammerofJustice.KnownSpell && !Repentance.KnownSpell) || */((ObjectManager.Me.CooldownTimeLeft(HammerofJustice.Id) > 1500 && HammerofJustice.KnownSpell) && (ObjectManager.Me.CooldownTimeLeft(Repentance.Id) > 1500 && Repentance.KnownSpell)) || ObjectManager.Me.ManaPercentage < 10) && !ObjectManager.Target.HasTarget && ObjectManager.Target.GetMove)
                        {
                            Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(true) end");
                            Products.InPause = true;
                            log("Пауза пока " + args[1] + " убегает");
                            Thread.Sleep(1000);
                            if (ObjectManager.Target.IsTargetingMeOrMyPet || !ObjectManager.Me.HasTarget)
                            {
                                Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                                Products.InPause = false;
                                if (ObjectManager.Target.IsValid && ObjectManager.Target.IsAlive)
                                    Fight.StartFight(ObjectManager.Target.Guid);
                            }
                        }
                        while (ObjectManager.Me.WowClass == WoWClass.Paladin && ObjectManager.Me.HasTarget && HammerofJustice.KnownSpell && ObjectManager.Me.CooldownTimeLeft(HammerofJustice.Id) < 1500 && ObjectManager.Me.ManaPercentage > 10)
                        {
                            if (HammerofJustice.IsSpellUsable)
                            {
                                HammerofJustice.Launch();
                            }
                            Thread.Sleep(50);
                        }
                        while (ObjectManager.Me.WowClass == WoWClass.Paladin && ObjectManager.Me.HasTarget && Repentance.KnownSpell && ObjectManager.Me.CooldownTimeLeft(Repentance.Id) < 1500 && ObjectManager.Me.ManaPercentage > 10 && !HammerofJustice.TargetHaveBuff)
                        {
                            if (Repentance.IsSpellUsable && Repentance.IsDistanceGood)
                            {
                                Repentance.Launch();
                            }
                            Thread.Sleep(50);
                        }
                        Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                        Products.InPause = false;
                        return;
                    }
                }
            }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "CORPSE_IN_RANGE")) //тело в радиусе 40
            {
                //log("CORPSE_IN_RANGE check gank");
                if (Var.Exist("Ganker") && ObjectManager.GetObjectWoWPlayer().Count(p => p.Name == Var.GetVar<WoWPlayer>("Ganker").Name) > 0)
                {
                    Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(true) end");
                    robotManager.Products.Products.InPause = true;
                    log("Ганк от " + Var.GetVar<WoWPlayer>("Ganker").Name + "");
                    _WaitGankTimer = new Timer(Others.Random(600000, 900000));
                    while (Var.GetVar<WoWPlayer>("Ganker").IsValid && !_WaitGankTimer.IsReady && Conditions.InGameAndConnected && ObjectManager.Me.IsDead)
                    {
                        int sleep = Others.Random(10000, 30000);
                        Lua.LuaDoString("print('Ганкер " + Var.GetVar<WoWPlayer>("Ganker").Name + " ждет нашего реса ждем возле тела еще " + (sleep / 1000) + " секунд')");
                        log("Ганкер " + Var.GetVar<WoWPlayer>("Ganker").Name + " ждет нашего реса, ждем пока он свалит еще " + (sleep / 1000) + " секунд");
                        Thread.Sleep(sleep);
                        if (!Var.GetVar<WoWPlayer>("Ganker").IsValid || Var.GetVar<WoWPlayer>("Ganker").InCombat || _WaitGankTimer.IsReady)
                        {
                            Lua.LuaDoString("if StaticPopup1Button1:IsEnabled() then StaticPopup1Button1:Click() end");
                            Thread.Sleep(200);
                            while (ObjectManager.Me.IsAlive && ObjectManager.Me.IsOutdoors && !wManager.Wow.Bot.Tasks.MountTask.OnFlyMount() && !ObjectManager.Me.InCombat) //взлетаем
                            {
                                if (!ObjectManager.Me.HaveBuff(wManager.wManagerSetting.CurrentSetting.FlyingMountName))
                                    SpellManager.CastSpellByNameLUA(wManager.wManagerSetting.CurrentSetting.FlyingMountName);
                                if (ObjectManager.Me.IsCast)
                                {
                                    Usefuls.WaitIsCasting();
                                    Thread.Sleep(100);
                                }
                                if (wManager.Wow.Bot.Tasks.MountTask.OnFlyMount())
                                {
                                    Vector3 point = null;
                                    wManager.Wow.Helpers.Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.JUMP, Others.Random(1500, 5000));
                                    for (int i = 0; i >= 50; i++)
                                    {
                                        point = robotManager.Helpful.Math.GetRandomPointInCircle(ObjectManager.Me.Position, 100);
                                        if (!TraceLine.TraceLineGo(point))
                                            break;
                                    }
                                    if (point != null)
                                    {
                                        ClickToMove.CGPlayer_C__ClickToMove(point.X, point.Y, point.Z, 0, (int)wManager.Wow.Enums.ClickToMoveType.Move, 2f);
                                        Thread.Sleep(sleep);
                                    }
                                }
                            }
                            if (Var.GetVar<WoWPlayer>("Ganker").IsValid)
                            {
                                MovementManager.StopMove();
                                Lua.LuaDoString("print('Кил процесса т.к ганкер " + Var.GetVar<WoWPlayer>("Ganker").Name + " исчез и тут же появился а мы думали что он исчез и сели на маунта')");
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }

                        }
                    }
                    //Thread.Sleep(Others.Random(600000, 900000));
                    Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                    robotManager.Products.Products.InPause = false;
                }

            }
            /*            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "READY_CHECK"))
                        {
                            log("Ready check started by '" + args[0] + "'.");
                            if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.Rcheck)
                            {
                                var Inviter = ObjectManager.GetObjectWoWPlayer().FirstOrDefault(p => p.Name == args[0]);
                                RandomWait();
                                if (RequestHandlerSettings.CurrentSetting.PartyRAccept)
                                {
                                    Lua.LuaDoString("StaticPopup1Button1:Click()");
                                    log("Ready check accepted.");
                                }
                                else
                                {
                                    Lua.LuaDoString("StaticPopup1Button2:Click()");
                                    log("Ready check declined.");
                                }
                            }
                        }*/
            /*            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "RESURRECT_REQUEST"))
                        {
                            log("Rezz request from '" + args[0] + "'.");
                            if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.RezzR)
                            {
                                System.Threading.Thread.Sleep(2000);
                                if (RequestHandlerSettings.CurrentSetting.RezzRAccept)
                                {
                                    Lua.LuaDoString("StaticPopup1Button1:Click()");
                                    log("Rezz request accepted.");
                                }
                                else
                                {
                                    Lua.LuaDoString("StaticPopup1Button2:Click()");
                                    log("Rezz request declined.");
                                    RandomEmote();
                                }
                            }
                        }*/
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "DUEL_REQUESTED"))
            {
                log("Duel request from '" + args[0] + "'.");
                if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.DuelR)
                {
                    //RandomWait();
                    System.Threading.Thread.Sleep(Others.Random(1000, 2000));
                    Products.InPause = true;
                    System.Threading.Thread.Sleep(Others.Random(2000, 5000));
                    Products.InPause = false;
                    if (RequestHandlerSettings.CurrentSetting.DuelRAccept)
                    {
                        Lua.LuaDoString("StaticPopup1Button1:Click()");
                        log("Duel request accepted.");
                        Products.InPause = true;
                        System.Threading.Thread.Sleep(Others.Random(10000, 30000));
                        Products.InPause = false;
                    }
                    else
                    {
                        Lua.LuaDoString("StaticPopup1Button2:Click()");
                        log("Duel request declined.");
                        ObjectManager.Me.Target = ObjectManager.GetObjectWoWPlayer().Where(p => p.Name == args[0]).FirstOrDefault().Guid;
                        RandomEmote();
                    }
                }
            }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "COMBAT_LOG_EVENT_UNFILTERED"))
            {

                if (Var.Exist("Hunter") && args[1] == "UNIT_DIED" && args[6] == Var.GetVar<WoWPlayer>("Hunter").Name && ObjectManager.GetObjectWoWPlayer().Count(p => p.WowClass == WoWClass.Hunter && p.IsAttackable && p.HaveBuff("Feign Death")) > 0)
                {
                    WoWPlayer Hunt = ObjectManager.GetObjectWoWPlayer().Where(p => p.WowClass == WoWClass.Hunter && p.IsValid && p.IsAttackable && p.HaveBuff("Feign Death")).FirstOrDefault();
                    log("Feign Death hunter detected - attack dead hunter");
                    Thread.Sleep(500);
                    while (Hunt.HaveBuff("Feign Death") && !Fight.InFight && Products.IsStarted)
                    {
                        if (MeMeleek)
                            wManager.Wow.Bot.Tasks.GoToTask.ToPosition(Hunt.Position);
                        Interact.InteractGameObject(Hunt.GetBaseAddress);
                        FightClass();
                        if (!Hunt.HaveBuff("Feign Death"))
                        {
                            Logging.Write("Хант встал запускаем файтклас");
                            Fight.StartFight(Hunt.Guid);
                        }
                        Thread.Sleep(10);
                    }
                }
                if (Inviznik != null && args[1] == "SPELL_CAST_SUCCESS" && (args[9] == "Shadowmeld" || args[9] == "Prowl" || args[9] == "Stealth") && args[3] == Inviznik.Name)
                {
                    log("" + args[3] + " casts " + args[9] + "  Casting Aoe spell");
                    AoeSpell();
                    if (ObjectManager.GetObjectWoWPlayer().Count(p => (p.PlayerRace == PlayerFactions.NightElf || p.WowClass == WoWClass.Rogue || (p.WowClass == WoWClass.Druid && p.HaveBuff("Cat Form")))) > 0)
                    {
                        /*WoWPlayer Inviznik = ObjectManager.GetObjectWoWPlayer().FindAll(p => (p.PlayerRace == PlayerFactions.NightElf || p.WowClass == WoWClass.Rogue || (p.WowClass == WoWClass.Druid && p.HaveBuff("Cat Form"))) && p.IsAttackable && p.InCombat && p.GetDistance <= 50).FirstOrDefault();*/
                        Inviznik = ObjectManager.GetObjectWoWPlayer().FindAll(p => p.Name == args[3]).FirstOrDefault();
                        while (Inviznik.IsValid && !Fight.InFight)
                        {
                            log("" + args[3] + " был выбит из инвиза начинаем бой");
                            Fight.StartFight(Var.GetVar<WoWPlayer>("Invizer").Guid);
                        }
                    }
                }
                if (args[1] == "SPELL_AURA_APPLIED")
                {
                    if (args[9] == "Sap" && args[6] == ObjectManager.Me.Name)
                    {
                        var SpyRogueGuid = ObjectManager.Me.BuffCastedByAll("Sap").FirstOrDefault();
                        var SpyRogue = ObjectManager.GetObjectWoWPlayer().FirstOrDefault(p => p.Guid == SpyRogueGuid);
                        log("Sap");
                        //PlaySound("dog1.wav");

                        if (Var.GetVar<List<ulong>>("HatersGuidsList").Contains(SpyRogueGuid))
                        {
                            Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(true) end");
                            robotManager.Products.Products.InPause = true;
                            PlaySound("DwarfHorn.wav");
                            Var.SetVar("HaterNear", true);
                            while (ObjectManager.Me.HaveBuff("Sap"))
                            {
                                Thread.Sleep(50);
                            }
                            InviznikPosition = ObjectManager.Me.Position;
                            AoeSpell();
                            /*                            if(ObjectManager.Me.InCombat)
                                                        {
                                                            Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                                                            robotManager.Products.Products.InPause = false;
                                                        }*/
                            Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                            robotManager.Products.Products.InPause = false;

                            //SDMDebug1("" + info1 + " Sap from faggot " + SpyRogue.Name + " " + SpyRogueGuid + ", " + info2 + "");
                            //SetCenterText(text: "Sap from faggot " + SpyRogue.Name + " " + SpyRogueGuid + "", frameflash: true, CenterTextFrameUpdateTimeCD: 30, textcolor: "ff5500", force: true);
                        }
                    }

                }
                //if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "CORPSE_IN_INSTANCE")) //для инстов
                //{
                //    log("Duel request from '" + args[0] + "'.");
                //    if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.DuelR)
                //    {
                //        RandomWait();
                //        wManager.wManagerSetting.CurrentSetting.UseSpiritHealer = true;
                //        wManager.Wow.Helpers.Lua.LuaDoString(@"Stuck() BasicScriptErrors:SetScale(6) if BasicScriptErrors:IsShown() then BasicScriptErrors:Hide() end message('застревание, каст анстака') ");
                //        log("Cast Unstack cause go to corpse, which is in the dungeon");
                //        wManager.wManagerSetting.CurrentSetting.UseSpiritHealer = true;
                //    }
                //}
                /*            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "TRADE_REQUEST"))
                            {
                                log("Trade request from '" + args[0] + "'.");
                                if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.TradeR)
                                {
                                    RandomWait();
                                    if (RequestHandlerSettings.CurrentSetting.TradeRAccept)
                                    {
                                        Lua.LuaDoString("TradeFrameTradeButton:Click()");
                                        log("Trade request accepted.");
                                    }
                                    else
                                    {
                                        Lua.LuaDoString("TradeFrameTradeButton:Click()");
                                        log("Trade request declined.");
                                    }
                                }
                            }*/

            }
                //You have entered too many instances recently CHAT_MSG_SYSTEM
                if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "CHAT_MSG_SYSTEM"))
                {
                //Thread.Sleep(500);
                /*if (Lua.LuaDoString<bool>("if GossipFrame:IsVisible() then return true end") && (args[0].Contains("88") || args[0].Contains("__")))
                {
                    log("КНТ: " + args[0]);
                    //robotManager.Products.Products.InPause = true;
                    //ShowWindow();
                    //robotManager.Helpful.Win32.Native.ShowWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, 4); // развернуть окно робота
                    //robotManager.Helpful.Win32.Native.SetForegroundWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle); // сделать окно робота поверх
                    //Logging.Write(ObjectManager.Me.Name + " КНТ " + args[0]);
                    var linetext = ObjectManager.Me.Name + ": " + args[0];
                    if (CapchaTimer.IsReady)
                    {
                        CapchaLines.Clear();
                        CapchaTimer = new Timer(15000);
                        System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            linetext = "";
                            Thread.Sleep((int)CapchaTimer.TimeLeft());
                            foreach (var str in CapchaLines)
                            {
                                Logging.Write("Добавили сторку " + args[0] + " в list CapchaLines");
                                linetext = linetext + Environment.NewLine + str;
                            }
                            TGSMDebug(TOKEN, "-1001641102456", linetext);
                        });

                    }
                    if (!CapchaTimer.IsReady)
                    {
                        CapchaLines.Add(linetext);
                    }
                    //var linetext = ObjectManager.Me.Name + ": " + args[0];

                    //SendDiscordMessageSystemCaptcha("Капча нового типа " + args[0]);

                    return;
                }*/
                if (args[0] == "You have entered too many instances recently.")
                    {
                        var instancecooldownpause = new System.Random().Next(500000, 1000000);
                        log(" Логаут и пауза т.к. инст на кд");
                        Lua.LuaDoString("Logout()");
                        Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(true) end");
                        robotManager.Products.Products.InPause = true;
                        Thread.Sleep(instancecooldownpause);
                        Lua.LuaDoString("if " + Var.GetVar<string>("PauseButtonFrameName") + ":IsShown() then " + Var.GetVar<string>("PauseButtonFrameName") + ":SetChecked(false) end");
                        robotManager.Products.Products.InPause = false;
                        Thread.Sleep(30000);
                    }
                }
                if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "GUILD_INVITE_REQUEST"))
                {
                    log("Guild request from '" + args[0] + "'.");
                    if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.GuildR)
                    {
                        RandomWait();
                        if (RequestHandlerSettings.CurrentSetting.GuildRAccept)
                        {
                            Lua.LuaDoString("StaticPopup1Button1:Click()");
                            log("Guild request accepted.");
                        }
                        else
                        {
                            Lua.LuaDoString("StaticPopup1Button2:Click()");
                            log("Guild request declined.");
                        }
                    }
                }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "LOOT_BIND_CONFIRM"))
            {
                if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();"))
                {
                    Thread.Sleep(100);
                    Lua.LuaDoString("StaticPopup1Button1:Click()");
                    log("Loot confirmed");
                }
            }
            if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "PARTY_INVITE_REQUEST"))
                {

                    log("Party request from '" + args[0] + "'.");
                    if (Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();") && RequestHandlerSettings.CurrentSetting.PartyR)
                    {
                        var name = "";
                        var zone = "";
                        var Inviter = ObjectManager.GetObjectWoWPlayer().FirstOrDefault(p => p.Name == args[0]);
                        /*                    if (!Lua.LuaDoString<bool>("if FriendsFrame:IsShown() then return true end;"))
                                            {
                                                Lua.RunMacroText("/who");
                                                System.Threading.Thread.Sleep(5000);
                                            }*/
                        Lua.RunMacroText("/who " + args[0] + "");
                        //RandomWait();
                        System.Threading.Thread.Sleep(500 + Usefuls.LatencyReal);
                        int m = Lua.LuaDoString<int>("numResults, totalCount = GetNumWhoResults(); return numResults");
                        //var name = Lua.LuaDoString<string>(string.Format("name, guild, level, race, class, zone, classFileName = GetWhoInfo({0}); if name=="+ args[0] +" and zone~='' then return name end", i));
                        Dictionary<string, string> PlayerZoneList = new Dictionary<string, string>();
                        for (int i = 0; i <= m; i++)
                        {
                            name = Lua.LuaDoString<string>(string.Format("name, guild, level, race, class, zone, classFileName = GetWhoInfo({0}); return name", i));
                            zone = Lua.LuaDoString<string>(string.Format("name, guild, level, race, class, zone, classFileName = GetWhoInfo({0}); return zone", i));
                            if (name == args[0] && DungeonNames.Contains(zone))
                            {
                                PlayerZoneList.Add(name, zone);
                                log("Добавили в PlayerZoneList игрока " + name);
                            }
                            if (((name == args[0] && DungeonNames.Contains(zone)) || Inviter != null) && (Usefuls.ContinentId == (int)ContinentId.Expansion01 || Usefuls.ContinentId == (int)ContinentId.Northrend || Usefuls.ContinentId == (int)ContinentId.Kalimdor || Usefuls.ContinentId == (int)ContinentId.Azeroth) && !HatersNames.Contains(name))
                            {

                                RequestHandlerSettings.CurrentSetting.PartyRAccept = true;
                                break;
                            }
                            else
                                RequestHandlerSettings.CurrentSetting.PartyRAccept = false;
                        }
                        /*for (int i = 0; i <= m; i++)
                        {
                            name = Lua.LuaDoString<string>(string.Format("name, guild, level, race, class, zone, classFileName = GetWhoInfo({0});if name~='' and (zone == 'The Botanica' or zone == 'Shadow Labyrinth' or zone == 'The Steamvault' or zone == 'The Shattered Halls' or zone == 'Sethekk Halls' or zone == 'Hellfire Ramparts' or zone == 'The Blood Furnace' or zone == 'Mana-Tombs' or zone == 'Auchenai Crypts' or zone == 'The Slave Pens' or zone == 'The Mechanar' or zone == 'Stratholme' or zone == 'The Nexus' or zone == 'Utgarde Keep' or string.find(zone, 'Old Kingdom') or string.find(zone, 'Keep')) then return name end", i));
                            if ((name == args[0] || Inviter != null) && (Usefuls.ContinentId == (int)ContinentId.Expansion01 || Usefuls.ContinentId == (int)ContinentId.Northrend || Usefuls.ContinentId == (int)ContinentId.Kalimdor || Usefuls.ContinentId == (int)ContinentId.Azeroth) && !HatersNames.Contains(name))
                            {

                                RequestHandlerSettings.CurrentSetting.PartyRAccept = true;
                                break;
                            }
                            else
                                RequestHandlerSettings.CurrentSetting.PartyRAccept = false;
                        }*/
                        /*                    if (Usefuls.ContinentId != (int)ContinentId.Expansion01 && Usefuls.ContinentId != (int)ContinentId.Northrend && Usefuls.ContinentId != (int)ContinentId.Kalimdor && Usefuls.ContinentId != (int)ContinentId.Azeroth)
                                            {
                                                RequestHandlerSettings.CurrentSetting.PartyRAccept = false;
                                            }
                                            else
                                            {
                                                RequestHandlerSettings.CurrentSetting.PartyRAccept = true;
                                            }*/
                        if (HatersNames.Contains(args[0]))
                        {
                            //Skill.ha
                            var random = Others.Random(1, 10);
                            if (random == 1)
                            {
                                log("Random whisper reply for hater " + args[0] + "");
                                log("Hater " + args[0] + " кинул инвайт, рандомное сообщение в ответ");
                                RandomWhisper(ReplyPartyInviteWhisper, args[0]);
                            }
                            if (random == 2)
                            {
                                log("Hater " + args[0] + " кинул инвайт, игнорим без деклайна");
                                return;
                            }
                            /*                        if (random == 3)
                                                    {
                                                        log("Hater " + args[0] + " кинул инвайт, Деклайн");
                                                        return;
                                                    }*/
                            if (random == 4)
                            {
                                log("Hater " + args[0] + " кинул инвайт, кидаем в чс");
                                Lua.LuaDoString("AddIgnore('" + args[0] + "')");
                                return;
                            }
                            if (random == 5)
                            {

                                log("Hater " + args[0] + " кинул инвайт, принимаем ");
                                Var.SetVar("HaterNameWhisper", args[0]);
                                Lua.LuaDoString("StaticPopup1Button1:Click()");
                                if (Others.Random(1, 2) == 1)
                                {
                                    log("Random PartyQuestion for hater " + args[0] + "");
                                    Thread.Sleep(Others.Random(4000, 15000));
                                    RandomReply(PartyQuestion, ChatTypeId.BN_WHISPER);
                                }
                                if (Others.Random(1, 2) == 2)
                                {
                                    log("Random ReplyPartyYasno reply for hater " + args[0] + "");
                                    Thread.Sleep(Others.Random(4000, 15000));
                                    RandomReply(ReplyPartyYasno, ChatTypeId.BN_WHISPER);
                                }
                                Thread.Sleep(Others.Random(1000, 4000));
                                Lua.LuaDoString("LeaveParty()");
                                return;
                            }
                        }
                        if (RequestHandlerSettings.CurrentSetting.PartyRAccept)
                        {
                            Lua.LuaDoString("StaticPopup1Button1:Click()");
                            log("Party request accepted.");
                        }
                        else
                        {
                            Lua.LuaDoString("StaticPopup1Button2:Click()");
                            log("Party request declined.");
                        }
                        //Lua.LuaDoString("if FriendsFrame:IsShown() then FriendsFrame:Hide() end;"); //закрываем вху фрейм
                    }
                }
                if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "UPDATE_BATTLEFIELD_STATUS"))
                {
                    /*if (Var.Exist("Battlegounder") && Var.GetVar<bool>("Battlegounder"))
                        return;*/
                    RandomWait();
                    var text1 = Lua.LuaDoString<string>("return StaticPopup1Text:GetText()");
                    var text2 = Lua.LuaDoString<string>("return StaticPopup2Text:GetText()");
                    if (Products.ProductName == "Battlegrounder" && !Battleground.IsInBattleground())
                    {
                        if (Battleground.BattlefieldStatusQueued())
                        {

                            if (Lua.LuaDoString<bool>("return StaticPopup1Button2:IsVisible()"))
                                Lua.LuaDoString("StaticPopup1Button2:Click()");
                            if (Lua.LuaDoString<bool>("return StaticPopup2Button2:IsVisible()"))
                                Lua.LuaDoString("StaticPopup2Button2:Click()");
                            Lua.RunMacroText("/click MiniMapBattlefieldFrame \\n/click DropDownList1Button3");
                            log("Leave BG Quique");

                            /*                        else
                                                    {
                                                        Lua.RunMacroText("/click MiniMapBattlefieldFrame \\n/click DropDownList1Button3");
                                                    }*/
                        }
                        var t = System.Threading.Tasks.Task.Run(async delegate
                        {
                            await System.Threading.Tasks.Task.Delay(500);
                            //Products.DisposeProduct();
                            if (Products.ProductName != "Quester")
                            {
                                log("stop & load product Quester");
                                robotManager.Products.Products.ProductStop();
                                robotManager.Products.Products.LoadProducts("Quester");
                                robotManager.Products.Products.ProductStart();
                                Thread.Sleep(10000);
                                if (Battleground.BattlefieldStatusQueued())
                                    Lua.RunMacroText("/click MiniMapBattlefieldFrame \\n/click DropDownList1Button3");
                            }
                            Logging.Write("product start");
                            System.Threading.Thread.Sleep(1500);
                            if (Battleground.BattlefieldStatusQueued())
                                Lua.RunMacroText("/click MiniMapBattlefieldFrame \\n/click DropDownList1Button3");
                        });
                        t.Wait();
                        log("Тред Завершился. Текущий продукт - " + Products.ProductName + "");
                        if (Battleground.BattlefieldStatusQueued())
                            Lua.RunMacroText("/click MiniMapBattlefieldFrame \\n/click DropDownList1Button3");


                    }
                    if ((text1.Contains("Battleground") || text2.Contains("Battleground")) && Products.ProductName == "Quester" && (ObjectManager.GetObjectWoWPlayer().Count(p => HatersNames.Contains(p.Name)) > 0 || (Var.Exist("HaterNear") && Var.GetVar<bool>("HaterNear")) || (Var.Exist("NeedtoLeave") && Var.GetVar<bool>("NeedtoLeave"))))
                    {
                        System.Threading.Tasks.Task.Factory.StartNew(() =>
                        {
                            while (text1.Contains("Battleground") || text2.Contains("Battleground"))
                            {
                                if (!ObjectManager.Me.InCombat && ObjectManager.Me.IsAlive && !Battleground.IsInBattleground())
                                {
                                    log("Bg request accepted.");
                                    Battleground.AcceptBattlefieldPortAll();
                                    RandomWait();
                                }
                                Thread.Sleep(2000);
                            }

                        });

                    }
                    if (Battleground.IsInBattleground() && Products.ProductName != "Battlegrounder" && Products.IsStarted)
                    {
                        var t = System.Threading.Tasks.Task.Run(async delegate
                        {
                            await System.Threading.Tasks.Task.Delay(500);
                            //Products.DisposeProduct();
                            if (Products.ProductName != "Battlegrounder")
                            {
                                robotManager.Products.Products.ProductStop();
                                robotManager.Products.Products.LoadProducts("Battlegrounder");
                                robotManager.Products.Products.ProductStart();
                                log("stop & load product Battlegrounder");
                            }
                            Logging.Write("product start");
                            System.Threading.Thread.Sleep(1500);
                        });
                        t.Wait();
                        if (t.IsCompleted)
                            log("Тред Завершился текущий продукт " + Products.ProductName + "");
                    }
                }
            
        }
    }
    /*private void Screenshot(string msg = null, bool UseWowApi = false, bool serverinfo = false, bool clearchat = true, bool hideframes = true, bool restoredown = true)
    {
        Task.Factory.StartNew(() =>
        {

                if (clearchat && ShowLogsInWowChat && ShowLogsInWowChat2)
                {
                    logs("[Screenshot] чистим чат если были логи");
                    runlua("ChatFrame1:Clear()");
                }
                if (UseWowApi)
                {
                    logs("[Screenshot] скриншот сохранен в папке вов");
                    runlua("Screenshot()");
                    sleep(1000);
                }
                else
                {
                    var path = Application.StartupPath + @"\Data\Screenshots Debug\";

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string filename;

                    if (msg != null)
                        filename = "" + msg + " - " + DateTime.Now.ToString("yyyy-MM-dd - hh-mm-ss") + " - " + MyRealName.ToUpper() + " - " + RealmShortName.ToUpper() + ".jpg";
                    else
                        filename = "" + DateTime.Now.ToString("yyyy-MM-dd - hh-mm-ss") + " - " + MyRealName.ToUpper() + " - " + RealmShortName.ToUpper() + ".jpg";

                    var pathandname = "" + path + "" + filename + "";
                    Display.ScreenshotWindow(WowWindow(), pathandname, System.Drawing.Imaging.ImageFormat.Jpeg);
                    sleep(100);
                    runlua("if " + CenterTextFrameName + " then " + CenterTextFrameName + ":Show() end if " + ButtonsFrameName + " then " + ButtonsFrameName + ":Show() end");
                    logs("[Screenshot] скриншот сохранен в " + pathandname + "");
                    NoTopmostWowWindow();
                }
                sleep(10);
            
        });
    }*/
    public static string TOKEN = "2125234557:AAF6RdLTsAqQdWGrLo20uZIZqiiX0b3503I";

    public static List<string> CapchaLines = new List<string>();
    public static robotManager.Helpful.Timer CapchaTimer = new robotManager.Helpful.Timer();
    public static void TGSMDebug(string token, string destID, string text, string info = null)
    {
        try
        {
            //https://api.telegram.org/bot1818250514:AAFraJjCaC9f0FRFpruTygg6YMO_0omj8-8/sendMessage?chat_id=1416112614&text=➟ ➤ ⛏:herb:🦌
            var URL = "https://api.telegram.org/bot" + token + "/sendMessage?chat_id=" + destID + "&text=" + text.Replace(Environment.NewLine, "%0A") + "";

            //var webReq = System.Net.WebRequest.Create(URL) as System.Net.HttpWebRequest;
            System.Net.HttpWebRequest webReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);

            webReq.ContentType = "application/json";
            webReq.Method = "POST";
            using (var streamWriter = new System.IO.StreamWriter(webReq.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (System.Net.HttpWebResponse)webReq.GetResponse();
            using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            Thread.Sleep(1000);
        }
        catch (Exception e)
        {
            if (info == null)
            {
                Logging.Write("TGSM Exception: " + e.ToString());
            }
            else
            {
                Logging.Write("TGSM Exception: " + e.ToString() + ", инфо: " + info + ", длина строки: " + text.Length + "");
            }
        }
    }
    public static void ShowWindow()
    {

        if (Display.GetWindowHeight(wManager.Wow.Memory.WowMemory.Memory.WindowHandle) < 600)
        {
            //robotManager.Helpful.Win32.Native.SetForegroundWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
            wManager.Wow.Memory.WowMemory.Memory.ToForegroundWindow();
            robotManager.Helpful.Win32.Native.ShowWindow(Memory.WowMemory.Memory.WindowHandle, 3);
        }
    }
    public static string LetsGoldBotToken = "1649810276:AAHU_XXpEbpTKcUsrlwo7XHvzdH0ByfXveE";
    //public string GankAlertBotToken = "5332210965:AAEH_AsW9zoW5Rqy5EKZIxcsO98k2VkzP0Q";
    public string GankInfoChannelID = "-1001645584259";
    private void TGSMAlert(string token, string destID, string text)
    {
        try
        {
            var URL = "https://api.telegram.org/bot" + token + "/sendMessage?chat_id=" + destID + "&text=" + text + "";
            //https://api.telegram.org/bot1818250514:AAFraJjCaC9f0FRFpruTygg6YMO_0omj8-8/sendMessage?chat_id=-1001390449192&text=fdfdfd
            var webReq = System.Net.WebRequest.Create(URL) as System.Net.HttpWebRequest;
            webReq.ContentType = "application/json";
            webReq.Method = "POST";
            using (var streamWriter = new System.IO.StreamWriter(webReq.GetRequestStream()))
            {
                streamWriter.Write("");
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (System.Net.HttpWebResponse)webReq.GetResponse();
            using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            //_OrderAlertTimer = new robotManager.Helpful.Timer(60 * 10000); //10 min cd

        }
        catch (Exception) { }
    }
    private void SendDiscordMessageSystemCaptcha(String contentBody)
    {
        try
        {
            var discordWebhookURL = "https://discord.com/api/webhooks/907710066935873576/rAMrTBmu54TmPhJ7iTMey4FzEiq0osxmsVy_GI3dGVhiI3cLmDPugblgp9fK6yJE5WOg";
            var webReq = System.Net.WebRequest.Create(discordWebhookURL) as System.Net.HttpWebRequest;
            webReq.ContentType = "application/json";
            webReq.Method = "POST";
            using (var streamWriter = new StreamWriter(webReq.GetRequestStream()))
            {
                string json = "{\"content\":\"" + contentBody + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (System.Net.HttpWebResponse)webReq.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        catch (Exception) { }
    }
    public string GossipText
    {
        get
        {
            string oldString = Lua.LuaDoString<string>("return GossipGreetingText:GetText()").ToLower();
            System.Text.StringBuilder newString = new System.Text.StringBuilder();
            foreach (var ch in oldString)
                if (!Char.IsControl(ch))
                    newString.Append(ch);
            string text = newString.ToString();
            return text;
        }
    }

    public List<string> DungeonNames = new List<string>
    {
        "The Botanica",
        "Shadow Labyrinth",
        "The Steamvault",
        "The Shattered Halls",
        "Sethekk Halls",
        "Hellfire Ramparts",
        "The Blood Furnace",
        "Mana-Tombs",
        "Auchenai Crypts",
        "The Slave Pens",
        "The Mechanar",
        "Stratholme",
        "Gundrak",
        "The Nexus",
        "Ahn'kahet: The Old Kingdom",
        "Drak'Tharon Keep",
        "Utgarde Keep",
        "Magisters' Terrace",
        "Blackrock Depths",
        "Utgarde Keep",
        "Blackrock Spire",
        "Blackrock Depths",
    };
    private void PlaySound(string FileNameOrPath, bool RobotDataFolder = true)
    {
        string path;

        if (RobotDataFolder)
            path = System.Windows.Forms.Application.StartupPath + @"\Data\" + FileNameOrPath + "";
        else
            path = FileNameOrPath;

        if (File.Exists(path))
        {
            Logging.Write("[PlaySound] " + path + "");
            new System.Media.SoundPlayer(path).Play();
        }
    }
    private void FightClass()
    {
        if (ObjectManager.Me.WowClass == WoWClass.DeathKnight && !SpellManager.KnowSpell("Howling Blast"))
            PvpCombatRotationDKgibrid();
        if (ObjectManager.Me.WowClass == WoWClass.DeathKnight && SpellManager.KnowSpell("Howling Blast"))
            PvpCombatRotationDKgibrid();
        if (ObjectManager.Me.WowClass == WoWClass.Paladin)
            PvpCombatRotationRetPal();
        if (ObjectManager.Me.WowClass == WoWClass.Priest)
            ShadowPriestPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Mage)
            FireMagePvP();
        if (ObjectManager.Me.WowClass == WoWClass.Shaman && SpellManager.KnowSpell("Feral Spirit"))
            ShamanEnchPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Shaman && SpellManager.KnowSpell("Thunderstorm"))
            ShamElemPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Warrior && SpellManager.KnowSpell("Blast Wave"))
            ProtWarPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Warrior && SpellManager.KnowSpell("Bloodthirst"))
            FuryWarPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Druid && SpellManager.KnowSpell("Mangle (Cat)"))
            CombatRotationFeral();
        if (ObjectManager.Me.WowClass == WoWClass.Druid && SpellManager.KnowSpell("StarFall"))
            SovaPvp();
        if (ObjectManager.Me.WowClass == WoWClass.Warlock && SpellManager.KnowSpell("Haunt"))
            WarlockAflicPvP();
        if (ObjectManager.Me.WowClass == WoWClass.Hunter)
            HunterPvp();
    }
    private void initializeHatersNamesList()
    {
        HatersNames = new List<string>(new string[]
        {

              "Тамби"
            ,  "Darksuln"
            ,  "Определённо"
            ,   "Raidho"
            ,   "Якрысычь"
            ,   "Jeemee"
            ,   "Анчоусик"
            ,   "Arefin"
            ,   "Приистус"
            ,  "Automute"
            ,   "Sonicmaster"
            ,   "Trunembra"
            ,   "Kosolapaya"
            ,   "Miliena"
            ,  "Xalerka"
            ,  "Безжалостная"
            ,   "Бенхарт"
            ,   "Тянучька"
            ,    "Dizerxd"
            ,  "Dizerxnova"
            ,  "Dizermod"
            ,   "Dizermoder"
            ,   "Druidicha"
            ,  "Juciewrld"
            ,   "Dreammage"
            ,   "Evilbun"
            ,  "Goozert"
            ,  "Acstepko"
            ,  "Торапышкаа"
            ,   "Трепещите"
            ,   "Хитрыйдруу"
            ,  "Сепфисто"
            ,  "Желтыйгриб"
            , "Signetic"
            ,  "Acstep"
            , "Bloodypalad"
            ,   "Krivedka"
            ,  "Quickley"
            ,   "Аббадонис"
            ,   "Скорпирон"
            ,   "Necromancer"
            ,   "Siege"
            ,   "Kybot"
            ,   "Payn"
            ,   "Tkeyah"
            ,   "Беатрич"
            ,   "Gilliona"
            ,  "Anrolik"
            ,   "Raxaji"
            ,   "Gylve"
            ,   "Линси"
            ,   "Pidje"
            ,   "Призраквойны"
            ,   "Обнаженная"
            ,   "Обнажённая"
            ,   "Грохотуля"
            ,   "Милашкаприст"
            ,   "Rozochka"
            ,   "Шампа"
            ,   "Суфле"
            ,   "Sequoya"
            ,   "Krisvon"
            ,   "Qollgate"
            ,   "Tianero"
            ,   "Шайтана"
            ,   "Корелла"
            ,   "Эдарби"
            ,   "Милашкамайя"
            ,   "Маюша"
            ,   "Maya"
            ,   "Елизаздра"
            ,   "Hesenberg"
            ,   "Милашкадру"
            ,   "Леснойсон"
            ,   "Tanazal"
            ,   "Euphoriagm"
            ,  "Пиффия"
            ,   "Прихлоп"
            ,  "Рабыня"
            ,   "Конецнастал"
            ,  "Идзайка"
            ,   "Баблдинша"
 /*           ,  "Злойбычарра"
             ,"Ыщаырвзщрыдп"
            ,   "Dllink"*/
            ,   "Тролодин"
            ,   "Девачка"
            ,   "Леми"
            //,  "Musorniy"
            ,   "Провинился"
            ,   "Каккух"
            ,   "Чучурындра"
            ,   "Шалунишо"
            ,   "Деска"
            ,   "Prinujdenie"
            ,   "Бешкэтнык"
            ,   "Леснойапух"
            ,  "Определённо"
            ,   "Raidho"
            ,   "Якрысычь"
            ,   "Jeemee"
            ,   "Анчоусик"
            ,   "Arefin"
            ,   "Мирфеа"
            ,   "Соне"
            ,   "Срустоя"
            ,   "Йохен"
            ,   "Prinujdenie"
            ,   "Иксфактор"
            ,   "мэрилинка"
            ,   "sonora"
            ,   "soulfiery"
            ,   "Аатвинта"
            ,   "Милики"
            ,   "supergeil"
            ,   "ispandora"
            ,   "Шамашамашан"
            ,  "Likkili"
            ,   "Иксфактор"
            ,   "javadonna"
            ,   "заябулочка"
            ,   "nemidora"
            ,   "rishalo"
            ,   "Larcen"
            ,   "безжалостная"
            ,   "Dizerbad"
            ,  "непобедимая"
            ,   "vusalegm"
            ,   "kastelo"
            ,   "Эльдриаса"
            ,   "Жииваая"
            ,   "Salanne"
            ,   "Newer"
            ,   "Недлядпс"
            ,   "Altela"
            ,   "Эльброхо"
            ,   "Besthakc"
            ,   "Grinexx"
            ,   "Samaara"
            ,   "Ландор"
            ,   "Настенько"
            ,   "neolog"
            ,   "mungalova"
            ,   "Ariala"
            ,   "Sinsaint"
            ,   "Янебарыга"
            ,   "Shapely"
            ,   "Костеро"
            ,   "Wintage"
            ,   "thighgap"
            ,   "Дратутиня"
            ,   "Вее"
            ,   "Wishkillr"
            ,   "Напишимне"
            //,   "Qwgfghjh" //перм 
            //,   "Khambir" //перм
            ,   "Дворфпристт"
            ,   "Druidichko"
            ,   "Coolsindra"
            ,   "Qq"//лузернейм
              , "Каталина"
              ,"Прихлопбил"
              ,"Предсмертная"
              ,"Lesly"
              ,"Lisa"
              ,"Byrevesnica"
            ,"Чара"
            ,"Beby"
            ,"Buntarka"
            ,"Катястрофа"
            ,"Zzajka"
            ,"Frostmigera"
            ,"Byntarka"
            ,"Meowkissme"
            ,"Патрикеевна"
            ,"Саломка"
            ,"Дворфоломон"
        });

    }
    private void RandomWhisper(List<string> spisok, string playername)
    {
        if (_saytimer.IsReady)
        {

            string RandomChatMessage = spisok[Others.Random(0, spisok.Count - 1)];
            Chat.SendChatMessageWhisper(RandomChatMessage, playername);
            _saytimer = new Timer(50000);
        }
    }
    private void RandomReply(List<string> spisok, ChatTypeId chatType)
    {
        if (_saytimer.IsReady)
        {

            string RandomChatMessage = spisok[Others.Random(0, spisok.Count - 1)];
            Chat.SendChatMessage(RandomChatMessage, chatType);
            _saytimer = new Timer(5000);
        }
    }
    private void VarManager(WoWUnit unit, CancelEventArgs cancel)
    {
        if (ObjectManager.Target.Type == WoWObjectType.Player && _chektargettimer.IsReady)
        {
            //return;
            _chektargettimer = new Timer(100);
            if (Attacker == null || Attacker.Name != ObjectManager.Target.Name)
                Attacker = ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer());
            if (ObjectManager.Target.WowClass == WoWClass.Hunter)
            {
                if (!Var.Exist("Hunter") || (Var.Exist("Hunter") && Var.GetVar<WoWPlayer>("Hunter").Name != ObjectManager.Target.Name))
                    Var.SetVar("Hunter", ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer()));
            }
            if (Inviznik == null || !Inviznik.IsValid)
                Inviznik = ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer().FindAll(p => (p.PlayerRace == PlayerFactions.NightElf || p.WowClass == WoWClass.Rogue || (p.WowClass == WoWClass.Druid && p.HaveBuff("Cat Form")))));
            if (Inviznik.IsValid)
            {
                InviznikPosition = Inviznik.Position;
            }
            /*if (!Var.Exist("Invizer") || !Var.GetVar<WoWPlayer>("Invizer").IsValid)
                Var.SetVar("Invizer", ObjectManager.GetNearestWoWPlayer(ObjectManager.GetObjectWoWPlayer().FindAll(p => (p.PlayerRace == PlayerFactions.NightElf || p.WowClass == WoWClass.Rogue || (p.WowClass == WoWClass.Druid && p.HaveBuff("Cat Form"))))));

            if (Var.GetVar<WoWPlayer>("Invizer").IsValid)
                Var.SetVar("InvizerPos", Var.GetVar<WoWPlayer>("Invizer").Position);*/
        }
    }
    private void AoeSpell()
    {


        MovementManager.StopMove();
        if (ObjectManager.Me.WowClass == WoWClass.DeathKnight)
        {
            if (SpellManager.GetSpellCooldownTimeLeft("Death and Decay") <= 1500 && ObjectManager.Me.RunesReadyCount(RuneTypes.Blood) >= 1 && ObjectManager.Me.RunesReadyCount(RuneTypes.Blood) >= 1 && ObjectManager.Me.RunesReadyCount(RuneTypes.Blood) >= 1)
            {
                while (ObjectManager.Me.GlobalCooldownEnabled)
                    Thread.Sleep(10);
                DeathandDecay.Launch();
                Thread.Sleep(10);
                ClickOnTerrain.Pulse(InviznikPosition);
            }
        }
        if (ObjectManager.Me.WowClass == WoWClass.Mage)
        {
            if (SpellManager.GetSpellCooldownTimeLeft("Blizzard") <= 1500)
            {
                int i = 0;
                while (ObjectManager.Me.Position.DistanceTo(InviznikPosition) > 30 && !TraceLine.TraceLineGo(InviznikPosition))
                {
                    log("target is too far go to her position");
                    i++;
                    MovementManager.MoveTo(InviznikPosition);
                    Thread.Sleep(500);
                    if (ObjectManager.Me.Position.DistanceTo(InviznikPosition) <= 30 || i > 20)
                    {
                        MovementManager.StopMove();
                        break;
                    }
                    //MovementManager.MoveTo(InviznikPosition);
                }
                i = 0;
                //MovementManager.MoveTo(InviznikPosition);
                while (ObjectManager.Me.GlobalCooldownEnabled)
                    Thread.Sleep(100);
                MovementManager.StopMove();
                new Spell("Blizzard").Launch(true, true, false);
                Thread.Sleep(10);
                //ClickOnTerrain.Pulse(new Vector3(ObjectManager.Me.Position));
                ClickOnTerrain.Pulse(InviznikPosition);
            }
        }
    }
    private void HunterPvp()
    {
        Lua.LuaDoString("PetAttack();", false);
        if (ObjectManager.Target.GetDistance >= 7.0)
        {
            if (ObjectManager.Target.HealthPercent < 20 && KillShot.IsSpellUsable && KillShot.IsDistanceGood)
            {
                KillShot.Launch();
                //return;
            }
            if (HuntersMark.IsSpellUsable && !HuntersMark.TargetHaveBuff && HuntersMark.IsDistanceGood)
            {
                HuntersMark.Launch();
                //return;
            }
            if (ArcaneShot.IsSpellUsable && ArcaneShot.IsDistanceGood)
            {
                ArcaneShot.Launch();
                //return;
            }
            if (Volley.IsDistanceGood && Volley.IsSpellUsable)
            {
                Volley.Launch();
                ClickOnTerrain.Pulse(new Vector3(ObjectManager.Target.Position));
                Usefuls.WaitIsCasting();
                //return;
            }

        }
    }
    private void WarlockAflicPvP()
    {
        if (WarlockFear.IsSpellUsable && !Fear.TargetHaveBuff && ObjectManager.Target.GetDistance <= 20 && !ObjectManager.Me.GetMove)
        {
            WarlockFear.Launch(true, true, false);
            //return;
        }
        else if (DeathCoil.IsSpellUsable && DeathCoil.IsDistanceGood && wManager.Wow.ObjectManager.ObjectManager.Me.HealthPercent <= 70.0 || wManager.Wow.ObjectManager.ObjectManager.Target.IsCast)
        {
            DeathCoil.Launch();
            //return;
        }
        else if (Corruption.IsSpellUsable && Corruption.IsDistanceGood && !Corruption.TargetHaveBuff)
        {
            Corruption.Launch();
            //return;
        }
        else if (!CurseofElements.TargetHaveBuff && CurseofElements.IsSpellUsable && CurseofElements.IsDistanceGood)
        {
            CurseofElements.Launch();
            //return;
        }
        else if (Haunt.IsSpellUsable && Haunt.IsDistanceGood && !wManager.Wow.ObjectManager.ObjectManager.Me.GetMove)
        {
            Haunt.Launch(true, true, false);
            //return;
        }
        else if (UnstableAffliction.IsSpellUsable && UnstableAffliction.IsDistanceGood && !UnstableAffliction.TargetHaveBuff)
        {
            UnstableAffliction.Launch(true, true, false);
            //return;
        }
        else if (ShadowBolt.IsSpellUsable && ShadowBolt.IsDistanceGood && wManager.Wow.ObjectManager.ObjectManager.Me.HaveBuff("Shadow Trance"))
        {
            if (wManager.Wow.ObjectManager.ObjectManager.Me.IsCast)
            {
                Lua.LuaDoString("print('stopcasting " + wManager.Wow.ObjectManager.ObjectManager.Me.CastingSpell.Name + " - прокнула стрела и хп больше 70%')", false);
                Lua.LuaDoString("SpellStopCasting();", false);
                if (wManager.Wow.ObjectManager.ObjectManager.Me.IsCast)
                    Move.Backward(Move.MoveAction.PressKey, new Random().Next(50, 150));
            }
            ShadowBolt.Launch();
            //return;
        }
        else if (DrainLife.IsSpellUsable && DrainLife.IsDistanceGood)
        {
            DrainLife.Launch(true, true, false);
            //return;
        }
        else if (ShadowFlame.IsSpellUsable && (double)wManager.Wow.ObjectManager.ObjectManager.Target.GetDistance <= 6.0 && wManager.Wow.ObjectManager.ObjectManager.Me.IsFacing(wManager.Wow.ObjectManager.ObjectManager.Target.Position, 2.2f))
        {
            ShadowFlame.Launch();
            //return;
        }
        else if (HowlofTerror.IsSpellUsable && wManager.Wow.ObjectManager.ObjectManager.GetUnitAttackPlayer().Count<WoWUnit>((Func<WoWUnit, bool>)(u => (double)u.GetDistance <= 10.0)) >= 2 && !wManager.Wow.ObjectManager.ObjectManager.Me.GetMove)
        {
            HowlofTerror.Launch(true, true, false);
            //return;
        }
        else
        {
            if (!_FlyBaitTimer.IsReady || !wManager.Wow.ObjectManager.ObjectManager.Target.IsFlying || wManager.Wow.ObjectManager.ObjectManager.Target.Type != WoWObjectType.Player || wManager.Wow.ObjectManager.ObjectManager.Me.IsMounted)
                //return;
                robotManager.Products.Products.InPause = true;
            Thread.Sleep(3000);
            robotManager.Products.Products.InPause = false;
            _FlyBaitTimer = new robotManager.Helpful.Timer(10000);
        }
    }
    private void SovaPvp()
    {
        if (ObjectManager.Target.GetDistance > 40 && TravelForm.IsSpellUsable && !TravelForm.HaveBuff)
        {
            TravelForm.Launch();
            //return;
        }

        if (ObjectManager.Me.HealthPercent > 40 && ObjectManager.Target.GetDistance < 35)
        {
            if ((ObjectManager.Target.IsCast || ObjectManager.Target.GetDistance < 10) && Typhoon.IsSpellUsable && Typhoon.IsDistanceGood && ObjectManager.Me.IsFacing(ObjectManager.Target.Position, 2.20f))
            {
                Typhoon.Launch();
                //return;
            }
            if (NaturesGrasp.IsSpellUsable)
            {
                NaturesGrasp.Launch();
                //return;
            }
            if (ObjectManager.Me.Health < 80)
            {
                if (Barkskin.IsSpellUsable)
                {
                    Barkskin.Launch();
                    //return;
                }
                if (!Rejuvenation.HaveBuff && Rejuvenation.IsSpellUsable)
                {
                    Rejuvenation.Launch();
                    //return;
                }
            }
            if (ObjectManager.Me.HaveBuff("Eclipse (Lunar)") && Starfire.IsSpellUsable && Starfire.IsDistanceGood)
            {
                Starfire.Launch(true, true, false);
                //return;
            }
            if (StarFall.IsSpellUsable && ObjectManager.Target.GetDistance <= 30)
            {
                StarFall.Launch();
                //return;
            }
            if (ObjectManager.Target.GetMove && !ObjectManager.Target.HaveBuff("Entangling Roots") && EntanglingRoots.IsSpellUsable && EntanglingRoots.IsDistanceGood)
            {
                EntanglingRoots.Launch(true, true, false);
                //return;
            }
            if (Pni.KnownSpell && Pni.IsSpellUsable)
            {
                SpellManager.CastSpellByIDAndPosition(33831, ObjectManager.Me.Position);
                //return;
            }
            if (!MoonFire.TargetHaveBuff && MoonFire.IsSpellUsable)
            {
                MoonFire.Launch();
                //return;
            }
            if (Wrath.KnownSpell && Wrath.IsDistanceGood && Wrath.IsSpellUsable)
            {
                Wrath.Launch(true, true, false);
                //return;
            }
            if (WarStomp.KnownSpell && WarStomp.IsSpellUsable && ObjectManager.Target.GetDistance <= 8)
            {
                WarStomp.Launch();
                //return;
            }

        }
        else
        {
            if (!Cyclone.TargetHaveBuff && Cyclone.IsSpellUsable && Cyclone.IsDistanceGood)
            {
                Cyclone.Launch(true, true, false);
                //return;
            }
            if (ObjectManager.Target.HaveBuff("Cyclone"))
            {
                if (!Rejuvenation.HaveBuff && Rejuvenation.IsSpellUsable)
                {
                    Rejuvenation.Launch();
                    //return;
                }
                if (!ObjectManager.Me.HaveBuff("Regrowtn") && Regrowth.IsSpellUsable)
                {
                    Regrowth.Launch(true, true, false);
                    //return;
                }
                if (HealingTouch.IsSpellUsable)
                {
                    HealingTouch.Launch(true, true, false);
                    //return;
                }
            }
        }
    }
    private void CombatRotationFeral()
    {
        if (!CatForm.HaveBuff && CatForm.IsSpellUsable)
        {
            CatForm.Launch();
            //return;
        }
        if (CatForm.HaveBuff)
        {
            if (Prowl.HaveBuff)
            {
                if (Ravage.IsSpellUsable && Ravage.IsDistanceGood && !Ravage.TargetHaveBuff)
                {
                    Ravage.Launch();
                    //return;
                }
                if (Shred.IsSpellUsable && Shred.IsDistanceGood)
                {
                    Shred.Launch();
                    //return;
                }
                if (Pounce.IsSpellUsable && Pounce.IsDistanceGood)
                {
                    Pounce.Launch();
                    //return;
                }
            }
            if (MangleCat.IsSpellUsable && MangleCat.IsDistanceGood)
            {
                MangleCat.Launch();
                //return;
            }
            if (TigersFury.IsSpellUsable)
            {
                TigersFury.Launch();
                //return;
            }
            if (ObjectManager.Me.ComboPoint >= 4 && ObjectManager.Target.HealthPercent > 50.0 && Rip.IsSpellUsable && Rip.IsDistanceGood)
            {
                Rip.Launch();
                //return;
            }
            //if (FeralSettings.CurrentSetting.Dash)
            //    BuffSpell(Dash, false, false);
        }

    }
    private void FuryWarPvP()
    {
        if (ObjectManager.Target.GetDistance <= 8)
        {
            if (InnerRage.IsSpellUsable)
            {
                InnerRage.Launch();
                //return;
            }
            if (BerserkerRage.IsSpellUsable && (ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Howl of Terror")))
            {
                BerserkerRage.Launch();
                //return;
            }
            if (!BerserkerStance.HaveBuff)
            {
                BerserkerStance.Launch();
                //return;
            }
            if (DeathWish.IsSpellUsable)
            {
                DeathWish.Launch();
                //return;
            }
            if (Recklessness.IsSpellUsable)
            {
                Recklessness.Launch();
                //return;
            }
            if (ObjectManager.Target.IsCast && Pummel.IsDistanceGood && Pummel.IsSpellUsable)
            {
                Pummel.Launch();
                //return;
            }
            if (ObjectManager.Target.IsCast && Fear.IsDistanceGood && Fear.IsSpellUsable)
            {
                Fear.Launch();
                //return;
            }
            if (!Hamstring.TargetHaveBuff && Hamstring.IsDistanceGood)
            {
                Hamstring.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 20 && Bloodthirst.IsDistanceGood && Bloodthirst.IsSpellUsable)
            {
                Bloodthirst.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 25 && Whirlwind.IsDistanceGood && Whirlwind.IsSpellUsable)
            {
                Whirlwind.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 15 && Execute.IsSpellUsable && Execute.IsDistanceGood && ObjectManager.Target.HealthPercent <= 20)
            {
                Execute.Launch();
                //return;
            }
            if (HeroicStrike.IsDistanceGood && ObjectManager.Me.Rage >= 20)
            {
                HeroicStrike.Launch();
                //return;
            }
            if (ObjectManager.Me.HaveBuff("Bloodsurge") && ObjectManager.Me.Rage >= 15 && Slam.IsDistanceGood && Slam.IsSpellUsable)
            {
                Slam.Launch();
                //return;
            }
        }
        else if (ObjectManager.Target.GetDistance > 8)
        {
            if (ObjectManager.Me.Rage < 20 && Bloodrage.IsSpellUsable)
            {
                Bloodrage.Launch();
                //return;
            }
            if (InnerRage.IsSpellUsable)
            {
                InnerRage.Launch();
                //return;
            }
            if (HeroicFury.IsSpellUsable && !ObjectManager.Me.CooldownEnabled("Intercept"))
            {
                HeroicFury.Launch();
                //return;
            }
            if (HeroicThrow.IsDistanceGood && HeroicThrow.IsSpellUsable)
            {
                HeroicThrow.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 10 && Intercept.IsSpellUsable && Intercept.IsDistanceGood)
            {
                Intercept.Launch();
                //return;
            }
            if (ObjectManager.Me.CooldownEnabled("Intercept") && !ObjectManager.Me.CooldownEnabled("Charge"))
            {
                if (Lua.LuaDoString<bool>("if GetShapeshiftForm() ~= 1 then return true end"))
                {
                    BattleStance.Launch();
                    //return;
                }

                if (Charge.IsSpellUsable && Charge.IsDistanceGood)
                    Charge.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 10 && !ObjectManager.Me.CooldownEnabled("Intercept"))
            {
                if (Lua.LuaDoString<bool>("if GetShapeshiftForm() ~= 3 then return true end"))
                {
                    BerserkerStance.Launch();
                    //return;
                }
                Intercept.Launch();
                //return;
            }
        }
    }
    private void ProtWarPvP()
    {

        if (ObjectManager.Target.GetDistance <= 8)
        {
            if (BerserkerRage.IsSpellUsable && (ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Howl of Terror")))
            {
                BerserkerRage.Launch();
                //return;
            }
            if (Lua.LuaDoString<bool>("if GetShapeshiftForm() ~= 2 then return true end"))
            {
                DefensiveStance.Launch();
                //return;
            }
            if (DeathWish.IsSpellUsable)
            {
                DeathWish.Launch();
                //return;
            }
            if (ObjectManager.Target.IsCast)
            {
                if (SpellReflection.IsSpellUsable)
                {
                    SpellReflection.Launch();
                    //return;
                }
                if (ShieldBash.IsSpellUsable && ShieldBash.IsDistanceGood)
                {
                    ShieldBash.Launch();
                    //return;
                }
                if (Fear.IsDistanceGood && Fear.IsSpellUsable)
                {
                    Fear.Launch();
                    //return;
                }
            }
            if (ShieldBlock.IsSpellUsable && (ObjectManager.Target.WowClass == WoWClass.Warrior || ObjectManager.Target.WowClass == WoWClass.Paladin || ObjectManager.Target.WowClass == WoWClass.DeathKnight || ObjectManager.Target.WowClass == WoWClass.Hunter))
            {
                ShieldBlock.Launch();
                //return;
            }
            if (ObjectManager.Me.HealthPercent < 50 && ShieldWall.IsSpellUsable)
            {
                ShieldWall.Launch();
                //return;
            }
            if (LastStand.IsSpellUsable && ObjectManager.Me.HealthPercent < 20)
            {
                LastStand.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 12 && Shockwave.IsSpellUsable && Shockwave.IsDistanceGood && ObjectManager.Me.IsFacing(ObjectManager.Target.Position, 2f))
            {
                Shockwave.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 12 && ThunderClap.IsSpellUsable && ObjectManager.Target.GetDistance <= 6)
            {
                ThunderClap.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 20 && ShieldSlam.IsDistanceGood && ShieldSlam.IsSpellUsable)
            {
                ShieldSlam.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 9 && Devastate.IsSpellUsable && Devastate.IsDistanceGood)
            {
                Devastate.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 20 && HeroicStrike.IsDistanceGood && _heroicstriketimer.IsReady)
            {
                HeroicStrike.Launch();
                _heroicstriketimer = new Timer(1000);
                //return;
            }
            if (_FlyBaitTimer.IsReady && (ObjectManager.Target.GetDistanceZ >= (ObjectManager.Me.GetDistanceZ + 10) && ObjectManager.Target.GetDistance < 10))
            {
                Products.InPause = true;
                Logging.WriteDebug("Байтфлаем пауза 3 сек");
                Thread.Sleep(3 * 1000);
                Products.InPause = false;
                _FlyBaitTimer = new Timer(5000);
                //return;
            }
        }
        else if (ObjectManager.Target.GetDistance > 8)
        {
            if (ObjectManager.Me.Rage < 20 && Bloodrage.IsSpellUsable)
            {
                Bloodrage.Launch();
                //return;
            }
            if (HeroicThrow.IsDistanceGood && HeroicThrow.IsSpellUsable)
            {
                HeroicThrow.Launch();
                //return;
            }
            if (ObjectManager.Me.Rage >= 10 && Intercept.IsSpellUsable && Intercept.IsDistanceGood)
            {
                Intercept.Launch();
                //return;
            }
            if (!Intercept.TargetHaveBuff && Charge.IsSpellUsable && Charge.IsDistanceGood)
            {
                Charge.Launch();
                //return;
            }
        }
    }
    private void ShamElemPvP()
    {
        if (ObjectManager.Target.GetDistance > 50 && ObjectManager.Target.GetMove && !GhostWolf.HaveBuff)
        {
            GhostWolf.Launch(true, true, false);
            //return;
        }
        if (ObjectManager.Target.GetDistance <= 40)
        {
            if (ElementalMastery.IsSpellUsable)
            {
                ElementalMastery.Launch();
                //return;
            }
            if (Heroism.IsSpellUsable)
            {
                Heroism.Launch();
                //return;
            }
            if (FireElementalTotem.IsSpellUsable)
            {
                FireElementalTotem.Launch();
                //return;
            }
            if (ObjectManager.Target.IsCast && WindShear.IsSpellUsable && WindShear.IsDistanceGood)
            {
                WindShear.Launch();
                //return;
            }
            if (Thunderstorm.IsSpellUsable && ObjectManager.GetObjectWoWPlayer().Count(p => p.IsAttackable && p.GetDistance <= 7) > 0)
            {
                Thunderstorm.Launch();
                //return;
            }
            if (FrostShock.IsSpellUsable && ObjectManager.Target.GetDistance <= 25 && ObjectManager.Target.GetMove && !FrostShock.TargetHaveBuff)
            {
                FrostShock.Launch();
                //return;
            }
            if (FlameShock.IsSpellUsable && ObjectManager.Target.GetDistance <= 25 && !FlameShock.TargetHaveBuff)
            {
                FlameShock.Launch();
                //return;
            }
            if (lavaBurst.IsSpellUsable && lavaBurst.IsDistanceGood && FlameShock.TargetHaveBuff)
            {
                lavaBurst.Launch(true, true, false);
                //return;
            }
            if (ChainLightning.IsSpellUsable && ChainLightning.IsDistanceGood)
            {
                ChainLightning.Launch(true, true, false);
                //return;
            }
            if (LightningBolt.IsSpellUsable && LightningBolt.KnownSpell && LightningBolt.IsDistanceGood)
            {
                LightningBolt.Launch(true, true, false);
                //return;
            }
            if (ObjectManager.Me.HealthPercent < 50)
            {
                if (Hex.IsSpellUsable && Hex.IsDistanceGood)
                {
                    Hex.Launch();
                    //return;
                }
                if (Hex.TargetHaveBuff)
                {
                    HealingWave.Launch(true, true, false);
                    //return;
                }
                else
                {
                    LesserHealingWave.Launch(true, true, false);
                    //return;
                }
            }
            if (ItemsManager.GetItemCountByIdLUA(33312) == 1 && ObjectManager.Me.ManaPercentage <= 80 && Lua.LuaDoString<bool>("local _, _, enable = GetItemCooldown('33312'); return enable;"))
            {
                ItemsManager.UseItem(33312);
                //return;
            }
        }
    }
    private void ShamanEnchPvP()
    {
        if (ObjectManager.Target.GetDistance > 40 && ObjectManager.Target.GetMove)
        {
            GhostWolf.Launch(true, true, false);
            //return;
        }
        if (ObjectManager.Target.GetDistance <= 40)
        {
            if (ObjectManager.Me.HealthPercent < 50)
            {
                if (Hex.IsSpellUsable && Hex.IsDistanceGood)
                {
                    Hex.Launch();
                    //return;
                }
                if (Hex.TargetHaveBuff)
                {
                    HealingWave.Launch(true, true, false, true);
                    //return;
                }
                else
                {
                    LesserHealingWave.Launch(true, true, false, true);
                    //return;
                }
            }
            if (ElementalMastery.IsSpellUsable)
            {
                ElementalMastery.Launch();
                //return;
            }
            if (Heroism.IsSpellUsable)
            {
                Heroism.Launch();
                //return;
            }
            if (FireElementalTotem.IsSpellUsable)
            {
                FireElementalTotem.Launch();
                //return;
            }
            if (FeralSpirit.IsSpellUsable)
            {
                FeralSpirit.Launch();
                //return;
            }
            if (ObjectManager.Target.IsCast && WindShear.IsSpellUsable && WindShear.IsDistanceGood)
            {
                WindShear.Launch();
                //return;
            }
            if (FrostShock.IsSpellUsable && ObjectManager.Target.GetDistance <= 25 && ObjectManager.Target.GetMove && !FrostShock.TargetHaveBuff)
            {
                FrostShock.Launch();
                //return;
            }
            if (ObjectManager.Me.BuffStack("Maelstrom Weapon") >= 5)
            {
                if (ChainLightning.IsSpellUsable && ChainLightning.IsDistanceGood && ObjectManager.GetUnitAttackPlayer().Count(u => u.GetDistance <= 7) >= 2)
                {
                    ChainLightning.Launch();
                    //return;
                }
                if (LightningBolt.IsSpellUsable && ChainLightning.IsDistanceGood)
                {
                    LightningBolt.Launch();
                    //return;
                }
            }
            if (Stormstrike.IsSpellUsable && Stormstrike.IsDistanceGood)
            {
                Stormstrike.Launch();
                //return;
            }
            if (EarthShock.IsSpellUsable && EarthShock.IsDistanceGood)
            {
                EarthShock.Launch();
                //return;
            }
            if (Lavalash.IsSpellUsable && Lavalash.IsDistanceGood)
            {
                Lavalash.Launch();
                //return;
            }
            if (ItemsManager.GetItemCountByIdLUA(33312) == 1 && ObjectManager.Me.ManaPercentage <= 80 && Lua.LuaDoString<bool>("local _, _, enable = GetItemCooldown('33312'); return enable;"))
            {
                ItemsManager.UseItem(33312);
                //return;
            }
            if (_FlyBaitTimer.IsReady && (ObjectManager.Target.GetDistanceZ >= 5 && ObjectManager.Target.GetDistance < 10))
            {
                Products.InPause = true;
                Thread.Sleep(3 * 1000);
                Products.InPause = false;
                _FlyBaitTimer = new Timer(10000);
                //return;
            }
        }
    }
    private void FireMagePvP()
    {

        //атак спелы
        if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Bladestorm") || ObjectManager.Target.HaveBuff("Dispersion")))
        {
            if (ObjectManager.Target.GetDistance >= 8)
            {
                if (Pyroblast.IsSpellUsable && Pyroblast.IsDistanceGood && ObjectManager.Me.HaveBuff("Hot Streak"))
                {
                    Pyroblast.Launch();
                    //return;
                }
                if (LivingBomb.IsSpellUsable && LivingBomb.IsDistanceGood && !ObjectManager.Target.HaveBuff("living Bomb") && ObjectManager.Target.Health > 5000)
                {
                    LivingBomb.Launch();
                    //return;
                }
                if (Flamestrike.IsSpellUsable && Flamestrike.IsDistanceGood && ObjectManager.Me.HaveBuff("Firestarter"))
                {
                    Flamestrike.Launch();
                    ClickOnTerrain.Pulse(new Vector3(ObjectManager.Target.Position));
                    //return;
                }
                if (DragonsBreath.IsSpellUsable && ObjectManager.Target.GetDistance <= 6 && ObjectManager.Me.IsFacing(ObjectManager.Target.Position, 2.20f))
                {
                    DragonsBreath.Launch();
                    //return;
                }
                if (FrostfireBolt.IsSpellUsable && FrostfireBolt.IsDistanceGood && !FrostfireBolt.TargetHaveBuff)
                {
                    FrostfireBolt.Launch(true, true, false);
                    //return;
                }
                if (Scorch.IsSpellUsable && Scorch.IsDistanceGood)
                {
                    Scorch.Launch();
                    //return;
                }
                if (FireBlast.IsSpellUsable && FireBlast.IsDistanceGood)
                {
                    FireBlast.Launch();
                    //return;
                }
                if (FireBall.IsSpellUsable && FireBall.IsDistanceGood && (ObjectManager.Target.GetDistance <= 40))
                {
                    FireBall.Launch();
                    //return;
                }
            }
            if (ObjectManager.Target.GetDistance < 8)
            {
                if (Pyroblast.IsSpellUsable && Pyroblast.IsDistanceGood && ObjectManager.Me.HaveBuff("Hot Streak"))
                {
                    Pyroblast.Launch();
                    //return;
                }
                if (FrostNova.IsSpellUsable && FrostNova.KnownSpell && !(ObjectManager.Target.HaveBuff("Hand of Freedom") || ObjectManager.Target.HaveBuff("Free Action")))
                {
                    FrostNova.Launch();
                    //return;
                }
                if (BlastWave.IsSpellUsable && !FrostNova.TargetHaveBuff)
                {
                    BlastWave.Launch();
                    //return;
                }
                if (DragonsBreath.IsSpellUsable && ObjectManager.Me.IsFacing(ObjectManager.Target.Position, 2.20f))
                {
                    DragonsBreath.Launch();
                    //return;
                }
                if (FireBlast.IsSpellUsable && FireBlast.IsDistanceGood)
                {
                    FireBlast.Launch();
                    //return;
                }
            }
        }
    }
    private void ShadowPriestPvP()
    {

        if (ObjectManager.Target.Type == WoWObjectType.Player && ObjectManager.Target.IsAttackable && Fight.InFight)
        {

            if (!ObjectManager.Me.Silenced && !ObjectManager.Me.IsStunned)
            {

                if (ObjectManager.Me.HealthPercent >= 56)
                {

                    // облик тьмы //
                    if (!ObjectManager.Me.HaveBuff("Shadowform"))
                    {
                        if (Shadowform.IsSpellUsable && Shadowform.KnownSpell)
                        {
                            Shadowform.Launch();
                            //return;
                        }
                    }


                    if (ObjectManager.Me.HaveBuff("Shadowform"))
                    {

                        // вампирик //
                        if (!ObjectManager.Me.GetMove && !ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid))
                        {
                            if (VampiricTouch.IsSpellUsable && ObjectManager.Target.GetDistance <= 36 && ObjectManager.Target.Health >= 5000 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && VampiricTouch.KnownSpell)
                            {
                                MovementManager.StopMove();
                                VampiricTouch.Launch();
                                Usefuls.WaitIsCasting();
                                //return;
                            }
                        }

                        // слово тьмы боль //
                        if (!ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid))
                        {
                            if (ShadowWordPain.IsSpellUsable && ObjectManager.Target.GetDistance <= 36 && ObjectManager.Target.Health >= 5000 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && ShadowWordPain.KnownSpell)
                            {
                                ShadowWordPain.Launch();
                                //return;
                            }
                        }

                        // чума //
                        if (!ObjectManager.Target.BuffCastedByAll("Devouring Plague").Contains(ObjectManager.Me.Guid))
                        {
                            if (DevouringPlague.IsSpellUsable && ObjectManager.Target.GetDistance <= 36 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && DevouringPlague.KnownSpell)
                            {
                                DevouringPlague.Launch();
                                //return;
                            }
                        }

                        // диспел болезни //
                        if (AbolishDisease.IsSpellUsable && !AbolishDisease.HaveBuff && (ObjectManager.Me.HaveBuff("Frost Fever") || ObjectManager.Me.HaveBuff("Blood Plague") || ObjectManager.Me.HaveBuff("Frost Fever") || ObjectManager.Me.HaveBuff("Devouring Plague") || ObjectManager.Me.HaveBuff("Infected Wounds")) && (ObjectManager.Target.BuffCastedByAll("Devouring Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid)) && AbolishDisease.KnownSpell)
                        {
                            AbolishDisease.Launch(false, false, false, true);
                            //return;
                        }

                        // диспел магии с себя //
                        if (DispelMagic.IsSpellUsable && (ObjectManager.Me.HaveBuff("Chains of Ice") || ObjectManager.Me.HaveBuff("Mark of Blood") || ObjectManager.Me.HaveBuff("Moonfire") || ObjectManager.Me.HaveBuff("Entangling Roots") || ObjectManager.Me.HaveBuff("Entangling Roots") || ObjectManager.Me.HaveBuff("Faerie Fire") || ObjectManager.Me.HaveBuff("Insect Swarm") || ObjectManager.Me.HaveBuff("Black Arrow") || ObjectManager.Me.HaveBuff("Serpent Sting") || ObjectManager.Me.HaveBuff("Scorpid Sting") || ObjectManager.Me.HaveBuff("Viper Sting") || ObjectManager.Me.HaveBuff("Hunter's Mark") || ObjectManager.Me.HaveBuff("Slow") || ObjectManager.Me.HaveBuff("Frost Nova") || ObjectManager.Me.HaveBuff("Cone of Cold") || ObjectManager.Me.HaveBuff("Shadow Word: Pain") || ObjectManager.Me.HaveBuff("Flame Shock") || ObjectManager.Me.HaveBuff("Frost Shock") || ObjectManager.Me.HaveBuff("Earthbind") || ObjectManager.Me.HaveBuff("Earthgrab") || ObjectManager.Me.HaveBuff("Corruption") || ObjectManager.Me.HaveBuff("Immolate") || ObjectManager.Me.HaveBuff("Shadowflame")) && (ObjectManager.Target.BuffCastedByAll("Devouring Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid)) && DispelMagic.KnownSpell)
                        {
                            DispelMagic.Launch(false, false, false, true);
                            //return;
                        }

                        // диспел магии с цели //
                        if (DispelMagic.IsSpellUsable && (ObjectManager.Target.BuffCastedByAll("Devouring Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid)) && (ObjectManager.Target.HaveBuff("Avenging Wrath") || ObjectManager.Target.HaveBuff("Sacred Shield") || ObjectManager.Me.HaveBuff("Blessing of Kings") || ObjectManager.Target.HaveBuff("Blessing of Might") || ObjectManager.Target.HaveBuff("Power Word: Shield") || ObjectManager.Target.HaveBuff("Noggenfogger Elixir") || ObjectManager.Target.HaveBuff("Ice Barrier") || ObjectManager.Target.HaveBuff("Power Word: Fortitude") || ObjectManager.Target.HaveBuff("Prayer of Fortitude") || ObjectManager.Target.HaveBuff("Divine Protection") || ObjectManager.Target.HaveBuff("Runescroll of Fortitude") || ObjectManager.Target.HaveBuff("Blessing of Forgotten Kings") || ObjectManager.Target.HaveBuff("Divine Plea") || ObjectManager.Target.HaveBuff("Heroism") || ObjectManager.Target.HaveBuff("Bloodlust") || ObjectManager.Target.HaveBuff("Sacrifice")) && DispelMagic.KnownSpell)
                        {
                            SpellManager.CastSpellByNameOn("Dispel Magic", "target");
                            //return;
                        }

                        // финишер //
                        if (ObjectManager.Target.Health < 4000 && ShadowWordDeath.IsSpellUsable)
                        {
                            if (ObjectManager.Target.GetDistance <= 36 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && ShadowWordDeath.KnownSpell)
                            {
                                ShadowWordDeath.Launch();
                                //return;
                            }
                        }

                        // взрыв разума //
                        if (!ObjectManager.Me.GetMove && MindBlast.IsSpellUsable)
                        {
                            if (ObjectManager.Target.GetDistance <= 36 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && MindBlast.KnownSpell)
                            {
                                MovementManager.StopMove();
                                MindBlast.Launch();
                                Usefuls.WaitIsCasting();
                                //return;
                            }
                        }

                        // пытка разума //
                        if (!ObjectManager.Me.GetMove && !ObjectManager.Target.BuffCastedByAll("Mind Flay").Contains(ObjectManager.Me.Guid))
                        {
                            if (MindFlay.IsSpellUsable && ObjectManager.Target.GetDistance <= 36 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) && MindFlay.KnownSpell)
                            {
                                MovementManager.StopMove();
                                MindFlay.Launch();
                                //return;
                            }
                        }

                    }
                }
            }

            // байт флаем (пвп) //
            if (ObjectManager.Target.IsFlying || ObjectManager.Target.GetDistanceZ >= 5)
            {
                if (_FlyBaitTimer.IsReady && !ObjectManager.Me.IsMounted)
                {
                    Logging.Write("[Fightclass shadow priest]: [" + ObjectManager.Me.Name + "]: байт флаем", Logging.LogType.Fight, System.Drawing.Color.Lime);
                    Products.InPause = true;
                    Thread.Sleep(new System.Random().Next(2000, 5000));
                    Products.InPause = false;
                    _FlyBaitTimer = new Timer(5000);
                    //return;
                }
            }

        }
    }
    private void PvpCombatRotationRetPal()
    {

        // покаяние (пвп) //
        if (Repentance.IsSpellUsable && Repentance.KnownSpell)
        {
            if (!ObjectManager.Target.HaveBuff("Hammer of Justice") && !ObjectManager.Me.HaveBuff("Divine Shield") && ObjectManager.Me.HealthPercent <= 50 && ObjectManager.Target.GetDistance <= 20 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")))
            {
                Repentance.Launch();
                //return;
            }
        }

        // стан (пвп) //
        if (HammerofJustice.IsSpellUsable && HammerofJustice.KnownSpell)
        {
            if (!ObjectManager.Target.HaveBuff("Repentance") && !ObjectManager.Me.HaveBuff("Divine Shield") && ObjectManager.Me.HealthPercent <= 50 && ObjectManager.Target.GetDistance <= 10 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")))
            {
                HammerofJustice.Launch();
                //return;
            }
        }

        // холи лайт (пвп) //
        if (ObjectManager.Me.HealthPercent <= 50 && HolyLight.KnownSpell && HolyLight.IsSpellUsable)
        {
            if (((ObjectManager.Target.HaveBuff("Hammer of Justice") || ObjectManager.Target.HaveBuff("Repentance")) || ObjectManager.Target.GetDistance >= 15 || ObjectManager.Me.HaveBuff("Divine Shield")) && _holylighttimer.IsReady)
            {
                MovementManager.StopMove();
                HolyLight.Launch(true, true, false, true);
                Usefuls.WaitIsCasting();
                _holylighttimer = new Timer(3000);
                //return;
            }
        }

        // гнев карателя (пвп) //
        if (AvengingWrath.IsSpellUsable && AvengingWrath.KnownSpell)
        {
            if (((ObjectManager.Target.HealthPercent <= 70 && ObjectManager.Me.HaveBuff("Forbearance")) || ObjectManager.Target.HealthPercent <= 30) && ObjectManager.Me.HealthPercent >= 90)
            {
                AvengingWrath.Launch();
                //return;
            }
        }

        // добивалка (пвп) //
        if (HammerofWrath.IsSpellUsable && HammerofWrath.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")))
            {
                HammerofWrath.Launch();
                //return;
            }
        }

        // красная джага (пвп) //
        if (JudgementofJustice.IsSpellUsable && JudgementofJustice.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")) && ObjectManager.Target.GetDistance <= 10)
            {
                JudgementofJustice.Launch();
                //return;
            }
        }

        // флешка под прок (пвп) //
        if (FlashofLight.IsSpellUsable && FlashofLight.KnownSpell)
        {
            if (ObjectManager.Me.HaveBuff("The Art of War") && ObjectManager.Me.HealthPercent <= 90)
            {
                FlashofLight.Launch(false, false, false, true);
                //return;
            }
        }

        // буря (пвп) //
        if (DivineStorm.IsSpellUsable && DivineStorm.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Dispersion")) && ObjectManager.Target.GetDistance <= 8)
            {
                DivineStorm.Launch();
                //return;
            }
        }

        // удар щитом (пвп) //
        if (ShieldofRighteousness.IsSpellUsable && ShieldofRighteousness.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")) && ObjectManager.Target.GetDistance <= 5)
            {
                ShieldofRighteousness.Launch();
                //return;
            }
        }

        // крусадер (пвп) //
        if (CrusaderStrike.IsSpellUsable && CrusaderStrike.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence")) && ObjectManager.Target.GetDistance <= 5)
            {
                CrusaderStrike.Launch();
                //return;
            }
        }

        // очищение (диспел) (пвп) //
        if (_dispelltimer.IsReady && Cleanse.KnownSpell)
        {
            if (ObjectManager.Me.ManaPercentage >= 20 && (ObjectManager.Me.HaveBuff("Chains of Ice") || ((ObjectManager.Me.HaveBuff("Frost Fever") || ObjectManager.Me.HaveBuff("Blood Plague")) && !ObjectManager.Me.HaveBuff("Unholy Blight")) || ObjectManager.Me.HaveBuff("Mark of Blood") || ObjectManager.Me.HaveBuff("Moonfire") || ObjectManager.Me.HaveBuff("Entangling Roots") || ObjectManager.Me.HaveBuff("Entangling Roots") || ObjectManager.Me.HaveBuff("Faerie Fire") || ObjectManager.Me.HaveBuff("Insect Swarm") || ObjectManager.Me.HaveBuff("Infected Wounds") || ObjectManager.Me.HaveBuff("Infected Wounds") || ObjectManager.Me.HaveBuff("Black Arrow") || ObjectManager.Me.HaveBuff("Serpent Sting") || ObjectManager.Me.HaveBuff("Scorpid Sting") || ObjectManager.Me.HaveBuff("Viper Sting") || ObjectManager.Me.HaveBuff("Hunter's Mark") || ObjectManager.Me.HaveBuff("Slow") || ObjectManager.Me.HaveBuff("Frost Nova") || ObjectManager.Me.HaveBuff("Cone of Cold") || ObjectManager.Me.HaveBuff("Shadow Word: Pain") || ObjectManager.Me.HaveBuff("Devouring Plague") || ObjectManager.Me.HaveBuff("Mind Blast") || ObjectManager.Me.HaveBuff("Wound Poison") || ObjectManager.Me.HaveBuff("Flame Shock") || ObjectManager.Me.HaveBuff("Frost Shock") || ObjectManager.Me.HaveBuff("Earthbind") || ObjectManager.Me.HaveBuff("Earthgrab") || ObjectManager.Me.HaveBuff("Corruption") || ObjectManager.Me.HaveBuff("Immolate") || ObjectManager.Me.HaveBuff("Shadowflame")) && Cleanse.IsSpellUsable)
            {
                Cleanse.Launch(false, false, false, true);
                _dispelltimer = new Timer(2000);
                //return;
            }
        }

        // длань свободы (пвп) //
        if (HandofFreedom.IsSpellUsable && HandofFreedom.KnownSpell)
        {
            if (ObjectManager.Target.GetDistance >= 7 && (ObjectManager.Me.HaveBuff("Curse of Exhaustion") || ObjectManager.Me.HaveBuff("Hamstring") || ObjectManager.Me.HaveBuff("Piercing Howl") || ObjectManager.Me.HaveBuff("Crippling Poison") || ObjectManager.Me.HaveBuff("Waylay") || ObjectManager.Me.HaveBuff("Icy Clutch") || ObjectManager.Me.HaveBuff("Mind Flay") || ObjectManager.Me.HaveBuff("Desecration") || ObjectManager.Me.HaveBuff("Frost Nova") || ObjectManager.Me.HaveBuff("Frostbite") || ObjectManager.Me.HaveBuff("Chilled") || ObjectManager.Me.HaveBuff("Shattered Barrier") || ObjectManager.Me.HaveBuff("Freeze") || ObjectManager.Me.HaveBuff("Cone of Cold") || ObjectManager.Me.HaveBuff("Frostfire Bolt") || ObjectManager.Me.HaveBuff("Frostbite") || ObjectManager.Me.HaveBuff("Typhoon") || ObjectManager.Me.HaveBuff("Frostbite") || ObjectManager.Me.HaveBuff("Shadowflame") || ObjectManager.Me.HaveBuff("Earthgrab") || ObjectManager.Me.HaveBuff("Frost Trap Aura") || ObjectManager.Me.HaveBuff("Chains of Ice") || ObjectManager.Me.HaveBuff("Entrapment")))
            {
                HandofFreedom.Launch(false, false, false, true);
                //return;
            }
        }

        // каждый за себя (хуман) (пвп) //
        if (EveryManForHimself.KnownSpell && EveryManForHimself.IsSpellUsable)
        {
            if ((ObjectManager.Me.HealthPercent <= 65 || ObjectManager.Target.HealthPercent <= 30) && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                EveryManForHimself.Launch();
                //return;
            }
        }

        /*
        // пвп тринкет если одет (орда) //
        if (EquippedItems.GetEquippedItems().Where(x => x.Entry == 51378).Count() == 1 && _trinkettimer.IsReady)
        {
            if ((ObjectManager.Me.HealthPercent <= 65 || ObjectManager.Target.HealthPercent <= 30) && wManager.Wow.Helpers.Bag.GetContainerItemCooldown(51378) == 0 && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                ItemsManager.UseItem(51378);
                _trinkettimer = new Timer(120000);
                //return;
            }
        }
        */

        // сало блад эльфов (пвп) //
        if (ArcaneTorrent.IsSpellUsable && ArcaneTorrent.KnownSpell)
        {
            if (ObjectManager.Target.IsCast && !ObjectManager.Target.HaveBuff("Divine Shield") && !(ObjectManager.Target.WowClass == WoWClass.Warrior || ObjectManager.Target.WowClass == WoWClass.Rogue) && ObjectManager.Target.GetDistance <= 8)
            {
                ArcaneTorrent.Launch();
                //return;
            }
        }

        // экзорциус под прок (пвп) //
        if (Exorcism.IsSpellUsable && Exorcism.KnownSpell)
        {
            if (!(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")) && ObjectManager.Me.HaveBuff("The Art of War") && ObjectManager.Me.HealthPercent >= 91)
            {
                Exorcism.Launch();
                //return;
            }
        }

        // лужа (пвп) //
        if (Consecration.IsSpellUsable && Consecration.KnownSpell)
        {
            if (ObjectManager.Me.ManaPercentage > 50 && ObjectManager.GetUnitAttackPlayer().Count(u => u.GetDistance <= 7) >= 2)
            {
                Consecration.Launch();
                //return;
            }
        }

        /*      
		// попытка уйти из боя если цель 80 (на каче) //
		if (ObjectManager.Me.Level <= 79)
		{
		   if (ObjectManager.Target.MaxHealth >= 12999 && ObjectManager.Target.Level >= 79 && ObjectManager.Target.IsValid)
		   {
			   Lua.RunMacroText("/stopattack");
			   Products.InPause = true;
			   Thread.Sleep(new System.Random().Next(3000, 10000));
			   Products.InPause = false;
			   Lua.RunMacroText("/stopattack");
			   //return;
		   }
		}
        */

        // байт флаем (пвп) //
        if (ObjectManager.Target.IsFlying || ObjectManager.Target.GetDistanceZ >= 5)
        {
            if (_FlyBaitTimer.IsReady && !ObjectManager.Me.IsMounted)
            {
                Logging.Write("[FightClass ret pal]: [" + ObjectManager.Me.Name + "]: байт флаем", Logging.LogType.Fight, System.Drawing.Color.SteelBlue);
                Products.InPause = true;
                Thread.Sleep(new System.Random().Next(2000, 5000));
                Products.InPause = false;
                _FlyBaitTimer = new Timer(5000);
                //return;
            }
        }

    }
    public void PvpCombatRotationFrost()
    {
        // оковы (пвп) //
        if (ObjectManager.Target.BuffTimeLeft("Chains of Ice") <= 5000 || !ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (ChainsofIce.IsSpellUsable && ObjectManager.Target.GetDistance >= 8 && !ObjectManager.Target.HaveBuff("Divine Shield") && !ObjectManager.Target.HaveBuff("Ice Block") && !ObjectManager.Target.HaveBuff("Deterrence") && !ObjectManager.Target.HaveBuff("Spell Reflection") && !ObjectManager.Target.HaveBuff("Anti-Magic Shell") && !ObjectManager.Target.HaveBuff("Hand of Freedom") && !ObjectManager.Target.HaveBuff("Free Action Potion") && !ObjectManager.Target.HaveBuff("Bladestorm"))
            {
                ChainsofIce.Launch();
                //return;
            }
        }

        // айси тач //
        if (!ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")))
        {
            if (IcyTouch.IsSpellUsable)
            {
                IcyTouch.Launch();
                //return;
            }
        }

        // метка (пвп) //
        if (ObjectManager.Me.HealthPercent <= 55)
        {
            if (MarkofBlood.IsSpellUsable && ObjectManager.Target.IsTargetingMe && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) || (ObjectManager.Target.CastingSpell.Name == "Mind Control" && ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Me.CooldownEnabled("Strangulate")))
            {
                MarkofBlood.Launch();
                //return;
            }
        }

        // детстрайк, хп < 85 отхил //
        if (ObjectManager.Me.HealthPercent <= 85)
        {
            if (DeathStrike.IsSpellUsable && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.GetDistance <= 5)
            {
                DeathStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // личборн //
        if (Lichborne.IsSpellUsable)
        {
            if (((ObjectManager.Target.CastingSpell.Name == "Polymorph" && ObjectManager.Target.IsCast) || (ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Psychic Scream"))))
            {
                Lichborne.Launch();
                //return;
            }
        }

        // зеленка //
        if (AntiMagicShell.IsSpellUsable)
        {
            if ((ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8) || (ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Target.IsCast) || (ObjectManager.Me.HealthPercent <= 70 && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue))
            {
                AntiMagicShell.Launch();
                //return;
            }
        }

        // сьедание пета //
        if (ObjectManager.Me.HealthPercent <= 30)
        {
            if (DeathPact.IsSpellUsable && ObjectManager.Pet.IsValid && ObjectManager.Pet.IsAlive)
            {
                DeathPact.Launch();
                //return;
            }
        }

        // армия //
        if (ObjectManager.Me.HealthPercent <= 80)
        {
            if (ArmyoftheDead.IsSpellUsable)
            {
                ArmyoftheDead.Launch(true, true, false);
                Usefuls.WaitIsCasting();
                //return;
            }
        }

        // каждый за себя (хуман, пвп) //
        if (ObjectManager.Me.PlayerRace == PlayerFactions.Human)
        {
            if (EveryManForHimself.IsSpellUsable && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                EveryManForHimself.Launch();
                //return;
            }
        }

        /*
        // пвп тринкет если одет (орда) //
        if (EquippedItems.GetEquippedItems().Where(x => x.Entry == 51378).Count() == 1 && _trinkettimer.IsReady && ObjectManager.Me.IsHorde)
        {
            if ((ObjectManager.Me.HealthPercent <= 65 || ObjectManager.Target.HealthPercent <= 30) && wManager.Wow.Helpers.Bag.GetContainerItemCooldown(51378) == 0 && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                ItemsManager.UseItem(51378);
                _trinkettimer = new Timer(120000);
                //return;
            }
        }
		*/

        // цель пытается выйти из боя (пвп) //
        if (!ObjectManager.Target.InCombat)
        {
            if (DarkCommand.IsSpellUsable && ObjectManager.Target.GetDistance >= 10)
            {
                DarkCommand.Launch();
                //return;
            }
        }

        // сало //
        if (Strangulate.IsSpellUsable)
        {
            if ((ObjectManager.Target.HealthPercent <= 25 && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue) || (ObjectManager.Target.IsCast && ObjectManager.Me.CooldownEnabled("Mind Freeze")) || (ObjectManager.Target.IsCast && ObjectManager.Me.CooldownEnabled("Death Grip") && ObjectManager.Target.GetDistance >= 8 && ObjectManager.Target.Position.DistanceTo(ObjectManager.Pet.Position) > 5) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) || (ObjectManager.Target.BuffCastedByAll("Mark of Blood").Contains(ObjectManager.Me.Guid) && (ObjectManager.Target.WowClass == WoWClass.Priest || ObjectManager.Target.WowClass == WoWClass.Paladin)))
            {
                Strangulate.Launch();
                //return;
            }
        }

        // хватка (пвп) //
        if (DeathGrip.IsSpellUsable)
        {
            if (ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8 && !(ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Divine Shield")))
            {
                DeathGrip.Launch();
                //return;
            }
        }

        // сало блад эльфов //
        if (ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance <= 8)
        {
            if (ArcaneTorrent.IsSpellUsable && !ObjectManager.Target.HaveBuff("Divine Shield") && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue && ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Me.CooldownEnabled("Death Grip") && ObjectManager.Me.CooldownEnabled("Strangulate"))
            {
                ArcaneTorrent.Launch();
                //return;
            }
        }

        // ледяной удар, прок машины //
        if (ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (FrostStrike.IsSpellUsable && ObjectManager.Me.HaveBuff("Killing Machine") && ObjectManager.Target.GetDistance <= 5 && !(ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Anti-Magic Zone") || ObjectManager.Target.HaveBuff("Dispersion")))
            {
                FrostStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // воющий ветер (прок) //
        if (ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Me.HaveBuff("Freezing Fog") && !(ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Anti-Magic Zone") || ObjectManager.Target.HaveBuff("Dispersion")))
        {
            if (HowlingBlast.IsSpellUsable)
            {
                HowlingBlast.Launch();
                //return;
            }
        }

        // ледяной удар, много рп //
        if (ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (ObjectManager.Me.GetPowerByPowerType(PowerType.RunicPower) >= 100 && FrostStrike.IsSpellUsable && ObjectManager.Target.GetDistance <= 5 && !(ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Anti-Magic Zone") || ObjectManager.Target.HaveBuff("Dispersion")))
            {
                FrostStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // удар чумы //
        if (!ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid))
        {
            if (PlagueStrike.IsSpellUsable && ObjectManager.Target.GetDistance <= 5)
            {
                PlagueStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // уничтожение //
        if (ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (Obliterate.IsSpellUsable && ObjectManager.Target.GetDistance <= 5 && !ObjectManager.Target.HaveBuff("Dispersion"))
            {
                Obliterate.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // кровавий удар //
        if (ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (BloodStrike.IsSpellUsable && ObjectManager.Target.GetDistance <= 5)
            {
                BloodStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // лужа //
        if (ObjectManager.GetWoWUnitHostile().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 6 && u.IsAttackable) >= 3)
        {
            if (DeathandDecay.IsSpellUsable && ObjectManager.Me.HealthPercent > 85 && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid))
            {
                DeathandDecay.Launch();
                ClickOnTerrain.Pulse(new Vector3(ObjectManager.Target.Position));
                //return;
            }
        }

        // вскипание //
        if (ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 6 && u.IsAttackable) >= 3)
        {
            if (BloodBoil.IsSpellUsable && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
            {
                BloodBoil.Launch();
                //return;
            }
        }

        // попытка уйти из боя если цель 80 (на каче) //
        //if (ObjectManager.Me.Level <= 79)
        //{
        //    if (ObjectManager.Target.Health >= 12999 && ObjectManager.Target.Level >= 79 && ObjectManager.Target.IsValid)
        //    {
        //        Lua.RunMacroText("/stopattack");
        //        Products.InPause = true;
        //        Thread.Sleep(Others.Random(3000, 10000));
        //        Products.InPause = false;
        //        Lua.RunMacroText("/stopattack");
        //        //return;
        //    }
        //}

        // байт флаем //
        if (ObjectManager.Target.IsFlying)
        {
            if (_FlyBaitTimer.IsReady && !ObjectManager.Me.IsMounted)
            {
                Products.InPause = true;
                Thread.Sleep(3 * 1000);
                Products.InPause = false;
                _FlyBaitTimer = new Timer(5000);
                //return;
            }
        }

        // не лупить своих //
        //if ((ObjectManager.Target.Name == "Nick" || ObjectManager.Target.Name == "Cold") && _xtimer.IsReady)
        //{
        //    Lua.LuaDoString("PetDismiss()");
        //    Lua.RunMacroText("/stopattack");
        //    Products.InPause = true;
        //    Thread.Sleep(Others.Random(10000, 15000));
        //    Products.InPause = false;
        //    Lua.RunMacroText("/stopattack");
        //    _xtimer = new Timer(5000);
        //    //return;
        //}

    }
    private void PvpCombatRotationDKgibrid()
    {

        // оковы //
        if (ObjectManager.Target.BuffTimeLeft("Chains of Ice") <= 5000)
        {
            if (ChainsofIce.KnownSpell && ChainsofIce.IsSpellUsable && ObjectManager.Target.GetDistance >= 6 && ObjectManager.Target.GetDistance <= 30 && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Hand of Freedom") || ObjectManager.Target.HaveBuff("Free Action") || ObjectManager.Target.HaveBuff("Bladestorm") || ObjectManager.Target.HaveBuff("Dispersion")))
            {
                ChainsofIce.Launch();
                //return;
            }
        }

        // айси тач //
        if (!ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")))
        {
            if (IcyTouch.KnownSpell && IcyTouch.IsSpellUsable && ObjectManager.Target.GetDistance >= 6 && ObjectManager.Target.GetDistance <= 30)
            {
                IcyTouch.Launch();
                //return;
            }
        }

        // удар чумы //
        if (!ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid))
        {
            if (PlagueStrike.KnownSpell && PlagueStrike.IsSpellUsable && ObjectManager.Target.GetDistance <= 5)
            {
                PlagueStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // метка //
        if (ObjectManager.Me.HealthPercent <= 40 && ObjectManager.Target.GetDistance <= 30)
        {
            if (MarkofBlood.KnownSpell && MarkofBlood.IsSpellUsable && ObjectManager.Target.IsTargetingMe && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) || (ObjectManager.Target.CastingSpell.Name == "Mind Control" && ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Me.CooldownEnabled("Strangulate")))
            {
                MarkofBlood.Launch();
                //return;
            }
        }

        // койл на расстоянии //
        if (ObjectManager.Target.GetDistance >= 6 && ObjectManager.Target.GetDistance <= 30 && (ObjectManager.Target.BuffCastedByAll("Ebon Plague").Contains(ObjectManager.Me.Guid) || ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid)) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")))
        {
            if (DeathCoil.KnownSpell && DeathCoil.IsSpellUsable)
            {
                DeathCoil.Launch();
                //return;
            }
        }

        // детстрайк, хп < 85 отхил //
        if (ObjectManager.Me.HealthPercent <= 85)
        {
            if (DeathStrike.KnownSpell && DeathStrike.IsSpellUsable && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.GetDistance <= 5)
            {
                DeathStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // гарга //
        if (SummonGargoyle.IsSpellUsable && SummonGargoyle.KnownSpell && ObjectManager.Target.GetDistance <= 30)
        {
            SummonGargoyle.Launch();
            //return;
        }

        // личборн //
        if (Lichborne.IsSpellUsable && Lichborne.KnownSpell)
        {
            if (((ObjectManager.Target.CastingSpell.Name == "Polymorph" && ObjectManager.Target.IsCast) || (ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Psychic Scream"))))
            {
                Lichborne.Launch();
                //return;
            }
        }

        // зеленка //
        if (AntiMagicShell.IsSpellUsable && AntiMagicShell.KnownSpell)
        {
            if ((ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8) || (ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Target.IsCast) || (ObjectManager.Me.HealthPercent <= 70 && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue))
            {
                AntiMagicShell.Launch();
                //return;
            }
        }

        // зона //
        if (AntiMagicZone.IsSpellUsable && AntiMagicZone.KnownSpell)
        {
            if (((ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8) || (ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Target.IsCast) || (ObjectManager.Me.HealthPercent <= 40 && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue)) && ObjectManager.Me.CooldownEnabled("Anti-Magic Shell"))
            {
                AntiMagicZone.Launch();
                //return;
            }
        }

        // сало //
        if (Strangulate.IsSpellUsable && Strangulate.KnownSpell && ObjectManager.Target.GetDistance <= 30)
        {
            if ((ObjectManager.Target.HealthPercent <= 25 && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue) || (ObjectManager.Target.IsCast && ObjectManager.Me.CooldownEnabled("Mind Freeze")) || (ObjectManager.Target.IsCast && ObjectManager.Me.CooldownEnabled("Death Grip") && ObjectManager.Target.GetDistance >= 8 && ObjectManager.Target.Position.DistanceTo(ObjectManager.Pet.Position) > 5) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell")) || (ObjectManager.Target.BuffCastedByAll("Mark of Blood").Contains(ObjectManager.Me.Guid) && (ObjectManager.Target.WowClass == WoWClass.Priest || ObjectManager.Target.WowClass == WoWClass.Paladin)))
            {
                Strangulate.Launch();
                //return;
            }
        }

        // хватка (пвп) //
        if (DeathGrip.IsSpellUsable && DeathGrip.KnownSpell)
        {
            if (ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8 && ObjectManager.Target.GetDistance <= 30 && !(ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Divine Shield")))
            {
                DeathGrip.Launch();
                //return;
            }
        }

        // стан пета (пвп) //
        if (ObjectManager.Pet.IsValid && SpellManager.SpellUsableLUA("Gnaw"))
        {
            if (ObjectManager.Pet.IsAlive && ((ObjectManager.Target.HealthPercent <= 25 && ObjectManager.Target.Position.DistanceTo(ObjectManager.Pet.Position) <= 5 && ObjectManager.Target.HaveBuff("Strangulate")) || (ObjectManager.Target.IsCast && ObjectManager.Target.Position.DistanceTo(ObjectManager.Pet.Position) <= 5 && ObjectManager.Me.CooldownEnabled("Mind Freeze")) || (ObjectManager.Target.Position.DistanceTo(ObjectManager.Pet.Position) <= 5 && ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance >= 8)))

            {
                SpellManager.CastSpellByNameLUA("Gnaw");
                //return;
            }
        }

        // сало блад эльфов //
        if (ObjectManager.Target.IsCast && ObjectManager.Target.GetDistance <= 8)
        {
            if (ArcaneTorrent.KnownSpell && ArcaneTorrent.IsSpellUsable && !ObjectManager.Target.HaveBuff("Divine Shield") && ObjectManager.Target.WowClass != WoWClass.Warrior && ObjectManager.Target.WowClass != WoWClass.Rogue && ObjectManager.Me.CooldownEnabled("Mind Freeze") && ObjectManager.Me.CooldownEnabled("Death Grip") && ObjectManager.Me.CooldownEnabled("Strangulate") && ObjectManager.Me.CooldownEnabled("Gnaw"))
            {
                ArcaneTorrent.Launch();
                //return;
            }
        }

        // сьедание пета //
        if (ObjectManager.Me.HealthPercent <= 30)
        {
            if (ArcaneTorrent.KnownSpell && DeathPact.IsSpellUsable && ObjectManager.Pet.IsValid && ObjectManager.Pet.IsAlive)
            {
                DeathPact.Launch();
                //return;
            }
        }

        // взрыв //
        if (CorpseExplosion.IsSpellUsable && CorpseExplosion.KnownSpell)
        {
            if ((ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) || ObjectManager.Target.BuffCastedByAll("Ebon Plague").Contains(ObjectManager.Me.Guid)) && (ObjectManager.Target.HaveBuff("Gnaw") || ObjectManager.Target.HaveBuff("Strangulate")) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")) && ObjectManager.Me.HealthPercent > 50)
            {
                SpellManager.CastSpellByNameOn("Corpse Explosion", "pet");
                //return;
            }
        }

        // армия //
        if (ArmyoftheDead.IsSpellUsable && ArmyoftheDead.KnownSpell)
        {
            if (ObjectManager.Me.HealthPercent <= 80 && !ObjectManager.Me.GetMove)
            {
                MovementManager.StopMove();
                ArmyoftheDead.Launch(true, true, true);
                Usefuls.WaitIsCasting();
                //return;
            }
        }

        // каждый за себя (хуман, пвп) //
        if (ObjectManager.Me.PlayerRace == PlayerFactions.Human && EveryManForHimself.KnownSpell)
        {
            if ((ObjectManager.Me.HealthPercent <= 65 || ObjectManager.Target.HealthPercent <= 30) && EveryManForHimself.IsSpellUsable && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                EveryManForHimself.Launch();
                //return;
            }
        }

        /*
        // пвп тринкет если одет (орда) //
        if (EquippedItems.GetEquippedItems().Where(x => x.Entry == 51378).Count() == 1 && _trinkettimer.IsReady && ObjectManager.Me.IsHorde)
        {
            if ((ObjectManager.Me.HealthPercent <= 65 || ObjectManager.Target.HealthPercent <= 30) && wManager.Wow.Helpers.Bag.GetContainerItemCooldown(51378) == 0 && (ObjectManager.Me.HaveBuff("Cyclone") || ObjectManager.Me.HaveBuff("Blind") || ObjectManager.Me.HaveBuff("Repentance") || ObjectManager.Me.HaveBuff("Hammer of Justice") || ObjectManager.Me.HaveBuff("Death Coil") || ObjectManager.Me.HaveBuff("Freezing Arrow Effect") && ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Psychic Scream") || ObjectManager.Me.HaveBuff("Fear") || ObjectManager.Me.HaveBuff("Howl of Terror") || ObjectManager.Me.HaveBuff("Intimidating Shout") || ObjectManager.Me.HaveBuff("Mind Control") || ObjectManager.Me.HaveBuff("Hex") || ObjectManager.Me.HaveBuff("Kidney Shot") || ObjectManager.Me.HaveBuff("Dragon's Breath") || ObjectManager.Me.HaveBuff("Freezing Trap Effect")))
            {
                ItemsManager.UseItem(51378);
                _trinkettimer = new Timer(120000);
                //return;
            }
        }
		*/

        // цель пытается выйти из боя (пвп) //
        if (!ObjectManager.Target.InCombat && ObjectManager.Target.GetDistance <= 30)
        {
            if (DarkCommand.IsSpellUsable && DarkCommand.KnownSpell)
            {
                DarkCommand.Launch();
                //return;
            }
        }

        // койл, много рп //
        if (ObjectManager.Target.GetDistance <= 30 && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) || ObjectManager.Target.BuffCastedByAll("Ebon Plague").Contains(ObjectManager.Me.Guid))
        {
            if (DeathCoil.KnownSpell && ObjectManager.Me.GetPowerByPowerType(PowerType.RunicPower) >= 100 && DeathCoil.IsSpellUsable && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")))
            {
                DeathCoil.Launch();
                //return;
            }
        }

        /* 
                // мор //
                if (_mortimer.IsReady && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
                {
                    if (Pestilence.IsSpellUsable && ObjectManager.Target.GetDistance <= 5 && ((ObjectManager.GetUnitAttackPlayer().Count(u => u.GetDistance <= 15) >= 2) || ((ObjectManager.Target.BuffTimeLeft("Blood Plague") <= 7000 || ObjectManager.Target.BuffTimeLeft("Frost Fever") <= 7000) && _GlyphOfDisease)))
                    {
                        Pestilence.Launch();
                        _mortimer = new Timer(10000);
                        //return;
                    }
                }
         */

        // удар плети //
        if (ObjectManager.Me.HealthPercent >= 86 && ScourgeStrike.KnownSpell)
        {
            if (ScourgeStrike.IsSpellUsable && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.GetDistance <= 5 && !ObjectManager.Target.HaveBuff("Dispersion"))
            {
                ScourgeStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // кровавий удар //
        if (ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
        {
            if (BloodStrike.IsSpellUsable && ObjectManager.Target.GetDistance <= 5 && BloodStrike.KnownSpell)
            {
                BloodStrike.Launch();
                RuneStrike.Launch();
                //return;
            }
        }

        // койл (мили ренж) //
        if (ObjectManager.Target.GetDistance <= 30 && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && !(ObjectManager.Target.HaveBuff("Divine Shield") || ObjectManager.Target.HaveBuff("Ice Block") || ObjectManager.Target.HaveBuff("Deterrence") || ObjectManager.Target.HaveBuff("Spell Reflection") || ObjectManager.Target.HaveBuff("Anti-Magic Shell") || ObjectManager.Target.HaveBuff("Dispersion")))
        {
            if (DeathCoil.IsSpellUsable && DeathCoil.KnownSpell)
            {
                DeathCoil.Launch();
                //return;
            }
        }

        // лужа //
        if (ObjectManager.GetWoWUnitHostile().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 10 && u.IsAttackable) >= 3 && DeathandDecay.KnownSpell)
        {
            if (DeathandDecay.IsSpellUsable && ObjectManager.Me.HealthPercent > 85 && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid))
            {
                DeathandDecay.Launch();
                ClickOnTerrain.Pulse(new Vector3(ObjectManager.Target.Position));
                //return;
            }
        }

        // вскипание //
        if (ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 6 && u.IsAttackable) >= 3 && BloodBoil.KnownSpell)
        {
            if (BloodBoil.IsSpellUsable && ObjectManager.Target.BuffCastedByAll("Blood Plague").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Frost Fever").Contains(ObjectManager.Me.Guid))
            {
                BloodBoil.Launch();
                //return;
            }
        }

        // попытка уйти из боя если цель 80 (на каче) //
        //if (ObjectManager.Me.Level <= 79)
        //{
        //    if (ObjectManager.Target.Health >= 12999 && ObjectManager.Target.Level >= 79 && ObjectManager.Target.IsValid)
        //    {
        //        Lua.RunMacroText("/stopattack");
        //        Products.InPause = true;
        //        Thread.Sleep(new System.Random().Next(3000, 10000));
        //        Products.InPause = false;
        //        Lua.RunMacroText("/stopattack");
        //        //return;
        //    }
        //}

        // байт флаем //
        if (ObjectManager.Target.IsFlying)
        {
            if (_FlyBaitTimer.IsReady && !ObjectManager.Me.IsMounted)
            {
                Logging.Write("[Fightclass gibrid dk]: [" + ObjectManager.Me.Name + "]: байт флаем", Logging.LogType.Fight, System.Drawing.Color.Lime);
                Products.InPause = true;
                System.Threading.Thread.Sleep(3 * 1000);
                Products.InPause = false;
                _FlyBaitTimer = new Timer(5000);
                //return;
            }
        }


    }

    public void Settings()
    {
        //Var.SetVar("Ganker", Attacker);
        //Lua.LuaDoString("print('Ганк от " + Attacker.Name + "')");
        //log("Ганк от " + Attacker.Name + "");
        string message = ObjectManager.Me.Name + " server: [" + Usefuls.RealmName + "] gank from TestName zone: [" + Usefuls.MapZoneName + "] subzone: [" + Usefuls.SubMapZoneName + "] position: [" + ObjectManager.Me.Position + "]";
        //string message = "123";
        TGSMAlert(LetsGoldBotToken, GankInfoChannelID, message);
        //RequestHandlerSettings.Load();
        //RequestHandlerSettings.CurrentSetting.ToForm();
        //RequestHandlerSettings.CurrentSetting.Save();
    }

    [Serializable]
    public class RequestHandlerSettings : Settings
    {
        [Setting]
        [DefaultValue(500)]
        [Category("1. Config")]
        [DisplayName("Min wait time in MS")]
        [Description("")]
        public int WaitMin { get; set; }

        [Setting]
        [DefaultValue(6500)]
        [Category("1. Config")]
        [DisplayName("Max wait time in MS")]
        [Description("")]
        public int WaitMax { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("2. Party")]
        [DisplayName("a. Handle Party Requests")]
        [Description("")]
        public bool PartyR { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("2. Party")]
        [DisplayName("b. Accept request?")]
        [Description("If true it will accept, if false it will decline")]
        public bool PartyRAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("3. Guild")]
        [DisplayName("a. Handle Guild Requests")]
        [Description("")]
        public bool GuildR { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("3. Guild")]
        [DisplayName("b. Accept request?")]
        [Description("If true it will accept, if false it will decline")]
        public bool GuildRAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("4. Trade")]
        [DisplayName("a. Handle Trade Requests")]
        [Description("")]
        public bool TradeR { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("4. Trade")]
        [DisplayName("b. Accept request?")]
        [Description("If true it will accept, if false it will decline")]
        public bool TradeRAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("5. Duel")]
        [DisplayName("a. Handle Duel Requests")]
        [Description("")]
        public bool DuelR { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("5. Duel")]
        [DisplayName("b. Accept request?")]
        [Description("If true it will accept, if false it will decline")]
        public bool DuelRAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("6. Rezz")]
        [DisplayName("a. Handle Rezz Requests")]
        [Description("")]
        public bool RezzR { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("6. Rezz")]
        [DisplayName("b. Accept request?")]
        [Description("If true it will accept, if false it will decline")]
        public bool RezzRAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("7. RCheck")]
        [DisplayName("a. Handle Ready Checks")]
        [Description("")]
        public bool Rcheck { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("7. RCheck")]
        [DisplayName("b. Accept check?")]
        [Description("If true it will accept, if false it will decline")]
        public bool RcheckAccept { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("8. LootRolls")]
        [DisplayName("a. Handle Loot Rolls")]
        [Description("This will select the below option when loot roll windows appear")]
        public bool LootR { get; set; }

        [Setting]
        [DefaultValue("Greed")]
        [Category("8. LootRolls")]
        [DisplayName("b. Need, Greed or Pass?")]
        [Description("Type in exactly Need, Greed or Pass to choose what option to select when loot rolls open up")]
        public string LootRSetting { get; set; }


        private RequestHandlerSettings()
        {
            PartyR = true;
            PartyRAccept = true;
            GuildR = true;
            GuildRAccept = true;
            TradeR = true;
            TradeRAccept = true;
            DuelR = true;
            DuelRAccept = false;
            RezzR = true;
            RezzRAccept = true;
            Rcheck = true;
            RcheckAccept = false;
            LootR = true;
            LootRSetting = "Greed";
            WaitMin = 500;
            WaitMax = 4500;
            ConfigWinForm(new System.Drawing.Point(300, 600), "RequestHandler " + Translate.Get("Settings"));
        }

        public static RequestHandlerSettings CurrentSetting { get; set; }

        public bool Save()
        {
            try
            {
                //wManager.wManagerSetting.CurrentSetting.BlackListZoneWhereDead = t
                return Save(AdviserFilePathAndName("RequestHandler", ObjectManager.Me.Name + "." + Usefuls.RealmName));
            }
            catch (Exception e)
            {
                Logging.WriteError("RequestHandlerSettings > Save(): " + e);
                return false;
            }
        }

        public static bool Load()
        {
            try
            {
                if (File.Exists(AdviserFilePathAndName("RequestHandler", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
                {
                    CurrentSetting =
                        Load<RequestHandlerSettings>(AdviserFilePathAndName("RequestHandler", ObjectManager.Me.Name + "." + Usefuls.RealmName));
                    return true;
                }
                CurrentSetting = new RequestHandlerSettings();
            }
            catch (Exception e)
            {
                Logging.WriteError("RequestHandlerSettings > Load(): " + e);
            }
            return false;
        }
    }
}
