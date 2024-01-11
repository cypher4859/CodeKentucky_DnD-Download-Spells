using System.Xml.Linq;
// This app gets all the spells from the D&D 5e Player's Handbook from FightClub5EXML 
// Feel free to try it with a different source in that github's listing,
// such as Xanathar's Guide To Everything!

// NOTE: You'll need to go to the MagicSources:GetDataSource() function and swap out the path
// to your locally downloaded `spells-phb.xml` that you downloaded from
// https://github.com/kinkofer/FightClub5eXML/blob/main/Sources/PlayersHandbook/spells-phb.xml
// - For Mac: /Users/<your username>/Downloads/spells-phb.xml
// - For Windows (roughly): C:/Users/<your username>/Downloads/spells-phb.xml

// TODO: Modify MagicSources:GetDataSource() to grab data from an API instead of parsing a 
// downloaded file.

Console.WriteLine("Hello, World!");
EntryPoint.Main();

class EntryPoint {
    public static void Main() {
        PrimaryController controller = new PrimaryController();
        controller.DoThing();
    }
}

class SpellLevelsConstants {
    public static int CANTRIP = 0;
    public static int FIRST_LEVEL = 1;
    public static int SECOND_LEVEL = 2;
}

class PrimaryController {
    public PrimaryController() {

    }

    public void DoThing() {
        Wizard harry = new Wizard(); // Create a Wizard.
        harry.ListSpells(); // Find out what spells they have
    }
}

class Player {

}

class Wizard : Player {
    public string playerClass = "Wizard";
    public Wizard() {

    }

    public void ListSpells() {
        List<Spell> spells = GetAllPlayerSpells(); // get the spells
        List<Spell> filteredSpells = spells.Where(sp => sp.SpellLevel == SpellLevelsConstants.CANTRIP && sp.Classes.Contains(playerClass)).ToList(); // filter by spell level
        DisplayOurSpells(filteredSpells);// Display the list of spells
    }

    public List<Spell> GetAllPlayerSpells() {
        MagicSource magicSource = new MagicSource();
        return magicSource.GetAllSpells();
    }

    public void DisplayOurSpells(List<Spell> spells) {
        foreach(Spell sp in spells) {
            Console.WriteLine(sp.Name);
        }
    }
}

class Spell {
    public Spell () {

    }

    public string Name {get;set;}
    public string Classes {get;set;}
    public int SpellLevel {get;set;}
}

class MagicSource {
    public XElement dataSource;
    public MagicSource() {
        this.dataSource = GetDataSource();
    }

    public XElement GetDataSource() {
        // TODO: Instead of loading this from local machine
        // Pull it from github itself
        return XElement.Load(@"/Users/blackcypher/Downloads/spells-phb.xml");
    }

    public List<Spell> GetAllSpells() {
        IEnumerable<Spell> spells = this.dataSource
            .Descendants("spell")
            .Select(theSpell => new Spell
                {
                    Name = (string) theSpell.Element("name"),
                    Classes = (string) theSpell.Element("classes"),
                    SpellLevel = (int) theSpell.Element("level")
                }
            );

        return spells.ToList();
    }
}