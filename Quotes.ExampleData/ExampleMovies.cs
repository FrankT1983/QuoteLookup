namespace Quotes.ExampleData;

public static class ExampleActors
{
    public static Actor SW = new Actor("sweaver", new Name("Sigourney", "Weaver"));
}

public static class ExampleMovies
{
    public static Movie Aliens = new Movie(
        "fn_al_tt_1", "Aliens", new Character[]
        {
            ExampleCharacters.AliensEllenRippley
        },
        new[]
        {
            new Quote("1",
            new Dialog(new[]
            {
                // 01:21:43 : It's the only way to be sure.
                // 01:21:45 : Fuckin' "A"! Hold on. Hold on one second.
                // 01:21:48 : This installation has a substantial dollar value attached to it.
                // 01:21:52 : They can bill me.
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(22)),
                    Character.Unknown,
                    "All right. We got seven canisters of CN-20."
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(25)),
                    Character.Unknown,
                    "I say we roll them in there and nerve gas the whole fucking nest."
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(29)),
                    Character.Unknown,
                    "That's worth a try, but we don't even know if it's gonna affect them."
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(32)),
                    Character.Unknown,
                    "Let's just bug out and call it even. Okay? What are we talking about this for?"
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(36)),
                    ExampleCharacters.AliensEllenRippley,
                    "I say we take off and nuke the entire site from orbit."
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(43)),
                    ExampleCharacters.AliensEllenRippley,
                    "It's the only way to be sure"
                ),
                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(45)),
                    Character.Unknown,
                    "Fuckin' A!"
                )
            }))
        }
    );

    public static Movie BladeRunner = new Movie(
       "tt0083658", "Blade Runner", new Character[]
       {
            ExampleCharacters.RoyBatty,
             ExampleCharacters.RickDeckard
       },
       new[]
       {
            new Quote("1",
            new Dialog(new[]
            {

                new DialogLine(
                    TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(21)).Add(TimeSpan.FromSeconds(45)),
                    ExampleCharacters.RoyBatty,
                    "I've seen things you people wouldn't believe. Attack ships on fire off the shoulder of Orion. I watched C-beams glitter in the dark near the Tannhauser gate. All those moments will be lost in time... like tears in rain... Time to die."
                )
            })),
            new Quote("2",
            new Dialog(new[]
            {

                new DialogLine(
                    TimeSpan.FromHours(0).Add(TimeSpan.FromMinutes(20)).Add(TimeSpan.FromSeconds(45)),
                    ExampleCharacters.RickDeckard,
                    "I have had people walk out on me before, but not... when I was being so charming."
                )
            }))
       }
   );


    public static Movie WithoutQuotes(this Movie m)
    {
        return new Movie(m.Name, m.Name, m.Characters, Array.Empty<Quote>());
    }
}

public static class ExampleCharacters
{
    public static Character AliensEllenRippley = new Character("1", new Name("Ellen", "Rippley"), ExampleActors.SW.Id);

    public static Character RoyBatty = new Character("Batty", new Name("Roy", "Rippley"));

    public static Character RickDeckard = new Character("Deckard", new Name("Rick", "Deckard"));
}