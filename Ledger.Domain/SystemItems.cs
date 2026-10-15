namespace Ledger.Domain;

public static class SystemItems
{
    private static readonly Currency Dia = new("DIA");
    private static readonly Currency Gld = new("GLD");
    private static readonly Currency Coi = new("COI");

    private static ItemPrice Pay(Currency currency, decimal amount) => new(currency, amount);

    public static readonly IReadOnlyList<Item> All =
    [
        // --- DIA + GLD (редкие) ---
        new("golden_rifle", "Golden Rifle", SystemCategories.Weapon.Id, "/images/golden-rifle.svg",
            [Pay(Dia, 300m), Pay(Gld, 3000m)]),
        new("royal_helmet", "Royal Helmet", SystemCategories.Helmet.Id, "/images/royal-helmet.svg",
            [Pay(Dia, 150m), Pay(Gld, 1500m)]),
        new("elite_agent", "Elite Agent", SystemCategories.Character.Id, "/images/elite-agent.svg",
            [Pay(Dia, 400m), Pay(Gld, 4000m)]),

        // --- DIA + COI (средние) ---
        new("sniper_rifle", "Sniper Rifle", SystemCategories.Weapon.Id, "/images/sniper-rifle.svg",
            [Pay(Dia, 120m), Pay(Coi, 6000m)]),
        new("curved_knife", "Curved Knife", SystemCategories.Knife.Id, "/images/curved-knife.svg",
            [Pay(Dia, 100m), Pay(Coi, 5000m)]),
        new("agent", "Agent", SystemCategories.Character.Id, "/images/agent.svg",
            [Pay(Dia, 250m), Pay(Coi, 12000m)]),

        // --- только COI (обычные) ---
        new("assault_rifle", "Assault Rifle", SystemCategories.Weapon.Id, "/images/assault-rifle.svg",
            [Pay(Coi, 2500m)]),
        new("smg", "SMG", SystemCategories.Weapon.Id, "/images/smg.svg",
            [Pay(Coi, 1500m)]),
        new("classic_pistol", "Classic Pistol", SystemCategories.Pistol.Id, "/images/classic-pistol.svg",
            [Pay(Coi, 550m)]),
        new("heavy_pistol", "Heavy Pistol", SystemCategories.Pistol.Id, "/images/heavy-pistol.svg",
            [Pay(Coi, 2000m)]),
        new("revolver", "Revolver", SystemCategories.Pistol.Id, "/images/revolver.svg",
            [Pay(Coi, 1200m)]),
        new("combat_knife", "Combat Knife", SystemCategories.Knife.Id, "/images/combat-knife.svg",
            [Pay(Coi, 800m)]),
        new("steel_helmet", "Steel Helmet", SystemCategories.Helmet.Id, "/images/steel-helmet.svg",
            [Pay(Coi, 1000m)]),
        new("tactical_helmet", "Tactical Helmet", SystemCategories.Helmet.Id, "/images/tactical-helmet.svg",
            [Pay(Coi, 3000m)]),
        new("soldier", "Soldier", SystemCategories.Character.Id, "/images/soldier.svg",
            [Pay(Coi, 8000m)])
    ];
}