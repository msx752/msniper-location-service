using Newtonsoft.Json;

namespace MSniperService.Models
{
    public enum PokemonId : short
    {
        [JsonProperty("BULBASAUR")]
        Bulbasaur = 1,

        [JsonProperty("IVYSAUR")]
        ivysaur = 2,

        [JsonProperty("VENUSAUR")]
        Venusaur = 3,

        [JsonProperty("CHARMANDER")]
        Charmander = 4,

        [JsonProperty("CHARMELEON")]
        Charmeleon = 5,

        [JsonProperty("CHARIZARD")]
        Charizard = 6,

        [JsonProperty("SQUIRTLE")]
        Squirtle = 7,

        [JsonProperty("WARTORTLE")]
        Wartortle = 8,

        [JsonProperty("BLASTOISE")]
        Blastoise = 9,

        [JsonProperty("CATERPIE")]
        Caterpie = 10,

        [JsonProperty("METAPOD")]
        Metapod = 11,

        [JsonProperty("BUTTERFREE")]
        Butterfree = 12,

        [JsonProperty("WEEDLE")]
        Weedle = 13,

        [JsonProperty("KAKUNA")]
        Kakuna = 14,

        [JsonProperty("BEEDRILL")]
        Beedrill = 15,

        [JsonProperty("PIDGEY")]
        Pidgey = 16,

        [JsonProperty("PIDGEOTTO")]
        Pidgeotto = 17,

        [JsonProperty("PIDGEOT")]
        Pidgeot = 18,

        [JsonProperty("RATTATA")]
        Rattata = 19,

        [JsonProperty("RATICATE")]
        Raticate = 20,

        [JsonProperty("SPEAROW")]
        Spearow = 21,

        [JsonProperty("FEAROW")]
        Fearow = 22,

        [JsonProperty("EKANS")]
        Ekans = 23,

        [JsonProperty("ARBOK")]
        Arbok = 24,

        [JsonProperty("PIKACHU")]
        Pikachu = 25,

        [JsonProperty("RAICHU")]
        Raichu = 26,

        [JsonProperty("SANDSHREW")]
        Sandshrew = 27,

        [JsonProperty("SANDSLASH")]
        Sandslash = 28,

        [JsonProperty("NIDORAN_FEMALE")]
        NidoranFemale = 29,

        [JsonProperty("NIDORINA")]
        Nidorina = 30,

        [JsonProperty("NIDOQUEEN")]
        Nidoqueen = 31,

        [JsonProperty("NIDORAN_MALE")]
        NidoranMale = 32,

        [JsonProperty("NIDORINO")]
        Nidorino = 33,

        [JsonProperty("NIDOKING")]
        Nidoking = 34,

        [JsonProperty("CLEFAIRY")]
        Clefairy = 35,

        [JsonProperty("CLEFABLE")]
        Clefable = 36,

        [JsonProperty("VULPIX")]
        Vulpix = 37,

        [JsonProperty("NINETALES")]
        Ninetales = 38,

        [JsonProperty("JIGGLYPUFF")]
        Jigglypuff = 39,

        [JsonProperty("WIGGLYTUFF")]
        Wigglytuff = 40,

        [JsonProperty("ZUBAT")]
        Zubat = 41,

        [JsonProperty("GOLBAT")]
        Golbat = 42,

        [JsonProperty("ODDISH")]
        Oddish = 43,

        [JsonProperty("GLOOM")]
        Gloom = 44,

        [JsonProperty("VILEPLUME")]
        Vileplume = 45,

        [JsonProperty("PARAS")]
        Paras = 46,

        [JsonProperty("PARASECT")]
        Parasect = 47,

        [JsonProperty("VENONAT")]
        Venonat = 48,

        [JsonProperty("VENOMOTH")]
        Venomoth = 49,

        [JsonProperty("DIGLETT")]
        Diglett = 50,

        [JsonProperty("DUGTRIO")]
        Dugtrio = 51,

        [JsonProperty("MEOWTH")]
        Meowth = 52,

        [JsonProperty("PERSIAN")]
        Persian = 53,

        [JsonProperty("PSYDUCK")]
        Psyduck = 54,

        [JsonProperty("GOLDUCK")]
        Golduck = 55,

        [JsonProperty("MANKEY")]
        Mankey = 56,

        [JsonProperty("PRIMEAPE")]
        Primeape = 57,

        [JsonProperty("GROWLITHE")]
        Growlithe = 58,

        [JsonProperty("ARCANINE")]
        Arcanine = 59,

        [JsonProperty("POLIWAG")]
        Poliwag = 60,

        [JsonProperty("POLIWHIRL")]
        Poliwhirl = 61,

        [JsonProperty("POLIWRATH")]
        Poliwrath = 62,

        [JsonProperty("ABRA")]
        Abra = 63,

        [JsonProperty("KADABRA")]
        Kadabra = 64,

        [JsonProperty("ALAKAZAM")]
        Alakazam = 65,

        [JsonProperty("MACHOP")]
        Machop = 66,

        [JsonProperty("MACHOKE")]
        Machoke = 67,

        [JsonProperty("MACHAMP")]
        Machamp = 68,

        [JsonProperty("BELLSPROUT")]
        Bellsprout = 69,

        [JsonProperty("WEEPINBELL")]
        Weepinbell = 70,

        [JsonProperty("VICTREEBEL")]
        Victreebel = 71,

        [JsonProperty("TENTACOOL")]
        Tentacool = 72,

        [JsonProperty("TENTACRUEL")]
        Tentacruel = 73,

        [JsonProperty("GEODUDE")]
        Geodude = 74,

        [JsonProperty("GRAVELER")]
        Graveler = 75,

        [JsonProperty("GOLEM")]
        Golem = 76,

        [JsonProperty("PONYTA")]
        Ponyta = 77,

        [JsonProperty("RAPIDASH")]
        Rapidash = 78,

        [JsonProperty("SLOWPOKE")]
        Slowpoke = 79,

        [JsonProperty("SLOWBRO")]
        Slowbro = 80,

        [JsonProperty("MAGNEMITE")]
        Magnemite = 81,

        [JsonProperty("MAGNETON")]
        Magneton = 82,

        [JsonProperty("FARFETCHD")]
        Farfetchd = 83,

        [JsonProperty("DODUO")]
        Doduo = 84,

        [JsonProperty("DODRIO")]
        Dodrio = 85,

        [JsonProperty("SEEL")]
        Seel = 86,

        [JsonProperty("DEWGONG")]
        Dewgong = 87,

        [JsonProperty("GRIMER")]
        Grimer = 88,

        [JsonProperty("MUK")]
        Muk = 89,

        [JsonProperty("SHELLDER")]
        Shellder = 90,

        [JsonProperty("CLOYSTER")]
        Cloyster = 91,

        [JsonProperty("GASTLY")]
        Gastly = 92,

        [JsonProperty("HAUNTER")]
        Haunter = 93,

        [JsonProperty("GENGAR")]
        Gengar = 94,

        [JsonProperty("ONIX")]
        Onix = 95,

        [JsonProperty("DROWZEE")]
        Drowzee = 96,

        [JsonProperty("HYPNO")]
        Hypno = 97,

        [JsonProperty("KRABBY")]
        Krabby = 98,

        [JsonProperty("KINGLER")]
        Kingler = 99,

        [JsonProperty("VOLTORB")]
        Voltorb = 100,

        [JsonProperty("ELECTRODE")]
        Electrode = 101,

        [JsonProperty("EXEGGCUTE")]
        Exeggcute = 102,

        [JsonProperty("EXEGGUTOR")]
        Exeggutor = 103,

        [JsonProperty("CUBONE")]
        Cubone = 104,

        [JsonProperty("MAROWAK")]
        Marowak = 105,

        [JsonProperty("HITMONLEE")]
        Hitmonlee = 106,

        [JsonProperty("HITMONCHAN")]
        Hitmonchan = 107,

        [JsonProperty("LICKITUNG")]
        Lickitung = 108,

        [JsonProperty("KOFFING")]
        Koffing = 109,

        [JsonProperty("WEEZING")]
        Weezing = 110,

        [JsonProperty("RHYHORN")]
        Rhyhorn = 111,

        [JsonProperty("RHYDON")]
        Rhydon = 112,

        [JsonProperty("CHANSEY")]
        Chansey = 113,

        [JsonProperty("TANGELA")]
        Tangela = 114,

        [JsonProperty("KANGASKHAN")]
        Kangaskhan = 115,

        [JsonProperty("HORSEA")]
        Horsea = 116,

        [JsonProperty("SEADRA")]
        Seadra = 117,

        [JsonProperty("GOLDEEN")]
        Goldeen = 118,

        [JsonProperty("SEAKING")]
        Seaking = 119,

        [JsonProperty("STARYU")]
        Staryu = 120,

        [JsonProperty("STARMIE")]
        Starmie = 121,

        [JsonProperty("MR_MIME")]
        MrMime = 122,

        [JsonProperty("SCYTHER")]
        Scyther = 123,

        [JsonProperty("JYNX")]
        Jynx = 124,

        [JsonProperty("ELECTABUZZ")]
        Electabuzz = 125,

        [JsonProperty("MAGMAR")]
        Magmar = 126,

        [JsonProperty("PINSIR")]
        Pinsir = 127,

        [JsonProperty("TAUROS")]
        Tauros = 128,

        [JsonProperty("MAGIKARP")]
        Magikarp = 129,

        [JsonProperty("GYARADOS")]
        Gyarados = 130,

        [JsonProperty("LAPRAS")]
        Lapras = 131,

        [JsonProperty("DITTO")]
        Ditto = 132,

        [JsonProperty("EEVEE")]
        Eevee = 133,

        [JsonProperty("VAPOREON")]
        Vaporeon = 134,

        [JsonProperty("JOLTEON")]
        Jolteon = 135,

        [JsonProperty("FLAREON")]
        Flareon = 136,

        [JsonProperty("PORYGON")]
        Porygon = 137,

        [JsonProperty("OMANYTE")]
        Omanyte = 138,

        [JsonProperty("OMASTAR")]
        Omastar = 139,

        [JsonProperty("KABUTO")]
        Kabuto = 140,

        [JsonProperty("KABUTOPS")]
        Kabutops = 141,

        [JsonProperty("AERODACTYL")]
        Aerodactyl = 142,

        [JsonProperty("SNORLAX")]
        Snorlax = 143,

        [JsonProperty("ARTICUNO")]
        Articuno = 144,

        [JsonProperty("ZAPDOS")]
        Zapdos = 145,

        [JsonProperty("MOLTRES")]
        Moltres = 146,

        [JsonProperty("DRATINI")]
        Dratini = 147,

        [JsonProperty("DRAGONAIR")]
        Dragonair = 148,

        [JsonProperty("DRAGONITE")]
        Dragonite = 149,

        [JsonProperty("MEWTWO")]
        Mewtwo = 150,

        [JsonProperty("MEW")]
        Mew = 151,
    }
}