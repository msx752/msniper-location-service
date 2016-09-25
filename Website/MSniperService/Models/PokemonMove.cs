using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MSniperService.Models
{

    public enum PokemonMove : short
    {
        [JsonProperty("MOVE_UNSET")]
        MoveUnset = 0,
        [JsonProperty("THUNDER_SHOCK")]
        ThunderShock = 1,
        [JsonProperty("QUICK_ATTACK")]
        QuickAttack = 2,
        [JsonProperty("SCRATCH")]
        Scratch = 3,
        [JsonProperty("EMBER")]
        Ember = 4,
        [JsonProperty("VINE_WHIP")]
        VineWhip = 5,
        [JsonProperty("TACKLE")]
        Tackle = 6,
        [JsonProperty("RAZOR_LEAF")]
        RazorLeaf = 7,
        [JsonProperty("TAKE_DOWN")]
        TakeDown = 8,
        [JsonProperty("WATER_GUN")]
        WaterGun = 9,
        [JsonProperty("BITE")]
        Bite = 10,
        [JsonProperty("POUND")]
        Pound = 11,
        [JsonProperty("DOUBLE_SLAP")]
        DoubleSlap = 12,
        [JsonProperty("WRAP")]
        Wrap = 13,
        [JsonProperty("HYPER_BEAM")]
        HyperBeam = 14,
        [JsonProperty("LICK")]
        Lick = 15,
        [JsonProperty("DARK_PULSE")]
        DarkPulse = 16,
        [JsonProperty("SMOG")]
        Smog = 17,
        [JsonProperty("SLUDGE")]
        Sludge = 18,
        [JsonProperty("METAL_CLAW")]
        MetalClaw = 19,
        [JsonProperty("VICE_GRIP")]
        ViceGrip = 20,
        [JsonProperty("FLAME_WHEEL")]
        FlameWheel = 21,
        [JsonProperty("MEGAHORN")]
        Megahorn = 22,
        [JsonProperty("WING_ATTACK")]
        WingAttack = 23,
        [JsonProperty("FLAMETHROWER")]
        Flamethrower = 24,
        [JsonProperty("SUCKER_PUNCH")]
        SuckerPunch = 25,
        [JsonProperty("DIG")]
        Dig = 26,
        [JsonProperty("LOW_KICK")]
        LowKick = 27,
        [JsonProperty("CROSS_CHOP")]
        CrossChop = 28,
        [JsonProperty("PSYCHO_CUT")]
        PsychoCut = 29,
        [JsonProperty("PSYBEAM")]
        Psybeam = 30,
        [JsonProperty("EARTHQUAKE")]
        Earthquake = 31,
        [JsonProperty("STONE_EDGE")]
        StoneEdge = 32,
        [JsonProperty("ICE_PUNCH")]
        IcePunch = 33,
        [JsonProperty("HEART_STAMP")]
        HeartStamp = 34,
        [JsonProperty("DISCHARGE")]
        Discharge = 35,
        [JsonProperty("FLASH_CANNON")]
        FlashCannon = 36,
        [JsonProperty("PECK")]
        Peck = 37,
        [JsonProperty("DRILL_PECK")]
        DrillPeck = 38,
        [JsonProperty("ICE_BEAM")]
        IceBeam = 39,
        [JsonProperty("BLIZZARD")]
        Blizzard = 40,
        [JsonProperty("AIR_SLASH")]
        AirSlash = 41,
        [JsonProperty("HEAT_WAVE")]
        HeatWave = 42,
        [JsonProperty("TWINEEDLE")]
        Twineedle = 43,
        [JsonProperty("POISON_JAB")]
        PoisonJab = 44,
        [JsonProperty("AERIAL_ACE")]
        AerialAce = 45,
        [JsonProperty("DRILL_RUN")]
        DrillRun = 46,
        [JsonProperty("PETAL_BLIZZARD")]
        PetalBlizzard = 47,
        [JsonProperty("MEGA_DRAIN")]
        MegaDrain = 48,
        [JsonProperty("BUG_BUZZ")]
        BugBuzz = 49,
        [JsonProperty("POISON_FANG")]
        PoisonFang = 50,
        [JsonProperty("NIGHT_SLASH")]
        NightSlash = 51,
        [JsonProperty("SLASH")]
        Slash = 52,
        [JsonProperty("BUBBLE_BEAM")]
        BubbleBeam = 53,
        [JsonProperty("SUBMISSION")]
        Submission = 54,
        [JsonProperty("KARATE_CHOP")]
        KarateChop = 55,
        [JsonProperty("LOW_SWEEP")]
        LowSweep = 56,
        [JsonProperty("AQUA_JET")]
        AquaJet = 57,
        [JsonProperty("AQUA_TAIL")]
        AquaTail = 58,
        [JsonProperty("SEED_BOMB")]
        SeedBomb = 59,
        [JsonProperty("PSYSHOCK")]
        Psyshock = 60,
        [JsonProperty("ROCK_THROW")]
        RockThrow = 61,
        [JsonProperty("ANCIENT_POWER")]
        AncientPower = 62,
        [JsonProperty("ROCK_TOMB")]
        RockTomb = 63,
        [JsonProperty("ROCK_SLIDE")]
        RockSlide = 64,
        [JsonProperty("POWER_GEM")]
        PowerGem = 65,
        [JsonProperty("SHADOW_SNEAK")]
        ShadowSneak = 66,
        [JsonProperty("SHADOW_PUNCH")]
        ShadowPunch = 67,
        [JsonProperty("SHADOW_CLAW")]
        ShadowClaw = 68,
        [JsonProperty("OMINOUS_WIND")]
        OminousWind = 69,
        [JsonProperty("SHADOW_BALL")]
        ShadowBall = 70,
        [JsonProperty("BULLET_PUNCH")]
        BulletPunch = 71,
        [JsonProperty("MAGNET_BOMB")]
        MagnetBomb = 72,
        [JsonProperty("STEEL_WING")]
        SteelWing = 73,
        [JsonProperty("IRON_HEAD")]
        IronHead = 74,
        [JsonProperty("PARABOLIC_CHARGE")]
        ParabolicCharge = 75,
        [JsonProperty("SPARK")]
        Spark = 76,
        [JsonProperty("THUNDER_PUNCH")]
        ThunderPunch = 77,
        [JsonProperty("THUNDER")]
        Thunder = 78,
        [JsonProperty("THUNDERBOLT")]
        Thunderbolt = 79,
        [JsonProperty("TWISTER")]
        Twister = 80,
        [JsonProperty("DRAGON_BREATH")]
        DragonBreath = 81,
        [JsonProperty("DRAGON_PULSE")]
        DragonPulse = 82,
        [JsonProperty("DRAGON_CLAW")]
        DragonClaw = 83,
        [JsonProperty("DISARMING_VOICE")]
        DisarmingVoice = 84,
        [JsonProperty("DRAINING_KISS")]
        DrainingKiss = 85,
        [JsonProperty("DAZZLING_GLEAM")]
        DazzlingGleam = 86,
        [JsonProperty("MOONBLAST")]
        Moonblast = 87,
        [JsonProperty("PLAY_ROUGH")]
        PlayRough = 88,
        [JsonProperty("CROSS_POISON")]
        CrossPoison = 89,
        [JsonProperty("SLUDGE_BOMB")]
        SludgeBomb = 90,
        [JsonProperty("SLUDGE_WAVE")]
        SludgeWave = 91,
        [JsonProperty("GUNK_SHOT")]
        GunkShot = 92,
        [JsonProperty("MUD_SHOT")]
        MudShot = 93,
        [JsonProperty("BONE_CLUB")]
        BoneClub = 94,
        [JsonProperty("BULLDOZE")]
        Bulldoze = 95,
        [JsonProperty("MUD_BOMB")]
        MudBomb = 96,
        [JsonProperty("FURY_CUTTER")]
        FuryCutter = 97,
        [JsonProperty("BUG_BITE")]
        BugBite = 98,
        [JsonProperty("SIGNAL_BEAM")]
        SignalBeam = 99,
        [JsonProperty("X_SCISSOR")]
        XScissor = 100,
        [JsonProperty("FLAME_CHARGE")]
        FlameCharge = 101,
        [JsonProperty("FLAME_BURST")]
        FlameBurst = 102,
        [JsonProperty("FIRE_BLAST")]
        FireBlast = 103,
        [JsonProperty("BRINE")]
        Brine = 104,
        [JsonProperty("WATER_PULSE")]
        WaterPulse = 105,
        [JsonProperty("SCALD")]
        Scald = 106,
        [JsonProperty("HYDRO_PUMP")]
        HydroPump = 107,
        [JsonProperty("PSYCHIC")]
        Psychic = 108,
        [JsonProperty("PSYSTRIKE")]
        Psystrike = 109,
        [JsonProperty("ICE_SHARD")]
        IceShard = 110,
        [JsonProperty("ICY_WIND")]
        IcyWind = 111,
        [JsonProperty("FROST_BREATH")]
        FrostBreath = 112,
        [JsonProperty("ABSORB")]
        Absorb = 113,
        [JsonProperty("GIGA_DRAIN")]
        GigaDrain = 114,
        [JsonProperty("FIRE_PUNCH")]
        FirePunch = 115,
        [JsonProperty("SOLAR_BEAM")]
        SolarBeam = 116,
        [JsonProperty("LEAF_BLADE")]
        LeafBlade = 117,
        [JsonProperty("POWER_WHIP")]
        PowerWhip = 118,
        [JsonProperty("SPLASH")]
        Splash = 119,
        [JsonProperty("ACID")]
        Acid = 120,
        [JsonProperty("AIR_CUTTER")]
        AirCutter = 121,
        [JsonProperty("HURRICANE")]
        Hurricane = 122,
        [JsonProperty("BRICK_BREAK")]
        BrickBreak = 123,
        [JsonProperty("CUT")]
        Cut = 124,
        [JsonProperty("SWIFT")]
        Swift = 125,
        [JsonProperty("HORN_ATTACK")]
        HornAttack = 126,
        [JsonProperty("STOMP")]
        Stomp = 127,
        [JsonProperty("HEADBUTT")]
        Headbutt = 128,
        [JsonProperty("HYPER_FANG")]
        HyperFang = 129,
        [JsonProperty("SLAM")]
        Slam = 130,
        [JsonProperty("BODY_SLAM")]
        BodySlam = 131,
        [JsonProperty("REST")]
        Rest = 132,
        [JsonProperty("STRUGGLE")]
        Struggle = 133,
        [JsonProperty("SCALD_BLASTOISE")]
        ScaldBlastoise = 134,
        [JsonProperty("HYDRO_PUMP_BLASTOISE")]
        HydroPumpBlastoise = 135,
        [JsonProperty("WRAP_GREEN")]
        WrapGreen = 136,
        [JsonProperty("WRAP_PINK")]
        WrapPink = 137,
        [JsonProperty("FURY_CUTTER_FAST")]
        FuryCutterFast = 200,
        [JsonProperty("BUG_BITE_FAST")]
        BugBiteFast = 201,
        [JsonProperty("BITE_FAST")]
        BiteFast = 202,
        [JsonProperty("SUCKER_PUNCH_FAST")]
        SuckerPunchFast = 203,
        [JsonProperty("DRAGON_BREATH_FAST")]
        DragonBreathFast = 204,
        [JsonProperty("THUNDER_SHOCK_FAST")]
        ThunderShockFast = 205,
        [JsonProperty("SPARK_FAST")]
        SparkFast = 206,
        [JsonProperty("LOW_KICK_FAST")]
        LowKickFast = 207,
        [JsonProperty("KARATE_CHOP_FAST")]
        KarateChopFast = 208,
        [JsonProperty("EMBER_FAST")]
        EmberFast = 209,
        [JsonProperty("WING_ATTACK_FAST")]
        WingAttackFast = 210,
        [JsonProperty("PECK_FAST")]
        PeckFast = 211,
        [JsonProperty("LICK_FAST")]
        LickFast = 212,
        [JsonProperty("SHADOW_CLAW_FAST")]
        ShadowClawFast = 213,
        [JsonProperty("VINE_WHIP_FAST")]
        VineWhipFast = 214,
        [JsonProperty("RAZOR_LEAF_FAST")]
        RazorLeafFast = 215,
        [JsonProperty("MUD_SHOT_FAST")]
        MudShotFast = 216,
        [JsonProperty("ICE_SHARD_FAST")]
        IceShardFast = 217,
        [JsonProperty("FROST_BREATH_FAST")]
        FrostBreathFast = 218,
        [JsonProperty("QUICK_ATTACK_FAST")]
        QuickAttackFast = 219,
        [JsonProperty("SCRATCH_FAST")]
        ScratchFast = 220,
        [JsonProperty("TACKLE_FAST")]
        TackleFast = 221,
        [JsonProperty("POUND_FAST")]
        PoundFast = 222,
        [JsonProperty("CUT_FAST")]
        CutFast = 223,
        [JsonProperty("POISON_JAB_FAST")]
        PoisonJabFast = 224,
        [JsonProperty("ACID_FAST")]
        AcidFast = 225,
        [JsonProperty("PSYCHO_CUT_FAST")]
        PsychoCutFast = 226,
        [JsonProperty("ROCK_THROW_FAST")]
        RockThrowFast = 227,
        [JsonProperty("METAL_CLAW_FAST")]
        MetalClawFast = 228,
        [JsonProperty("BULLET_PUNCH_FAST")]
        BulletPunchFast = 229,
        [JsonProperty("WATER_GUN_FAST")]
        WaterGunFast = 230,
        [JsonProperty("SPLASH_FAST")]
        SplashFast = 231,
        [JsonProperty("WATER_GUN_FAST_BLASTOISE")]
        WaterGunFastBlastoise = 232,
        [JsonProperty("MUD_SLAP_FAST")]
        MudSlapFast = 233,
        [JsonProperty("ZEN_HEADBUTT_FAST")]
        ZenHeadbuttFast = 234,
        [JsonProperty("CONFUSION_FAST")]
        ConfusionFast = 235,
        [JsonProperty("POISON_STING_FAST")]
        PoisonStingFast = 236,
        [JsonProperty("BUBBLE_FAST")]
        BubbleFast = 237,
        [JsonProperty("FEINT_ATTACK_FAST")]
        FeintAttackFast = 238,
        [JsonProperty("STEEL_WING_FAST")]
        SteelWingFast = 239,
        [JsonProperty("FIRE_FANG_FAST")]
        FireFangFast = 240,
        [JsonProperty("ROCK_SMASH_FAST")]
        RockSmashFast = 241,
    }
}