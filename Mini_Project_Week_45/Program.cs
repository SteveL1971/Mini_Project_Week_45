
//List that adds and cotains 9 Items
List<Item> itemList = new List<Item>()
{
    new Mobile("iPhone", "8", 970),
    new Laptop("HP", "Elitebook", 1423),
    new Mobile("iPhone", "11", 990),
    new Mobile("iPhone", "X", 1245),
    new Mobile("Motorola", "Razr", 970),
    new Laptop("HP", "Elitebook", 588),
    new Laptop("Asus", "W234", 1200),
    new Laptop("Lenova", "Yoga 730", 835),
    new Laptop("Lenova", "Yoga 530", 1030)
};

// List that adds and contains 3 Offices
List<Office> officeList = new List<Office>()
{
    new Office("Spain", "EUR", 0.82645), 
    new Office("USA", "USD", 1.0), 
    new Office("Sweden", "SEK", 8.334)
};

// List which adds and initially contains 10 Assets
List<Asset> assetList = new List<Asset>()
{
    new Asset(itemList[0], officeList[0], Convert.ToDateTime("12-29-2018")),
    new Asset(itemList[1], officeList[0], Convert.ToDateTime("6-1-2019")),
    new Asset(itemList[2], officeList[0], Convert.ToDateTime("9-25-2020")),
    new Asset(itemList[3], officeList[2], Convert.ToDateTime("7-15-2018")),
    new Asset(itemList[4], officeList[2], Convert.ToDateTime("10-2-2020")),
    new Asset(itemList[5], officeList[2], Convert.ToDateTime("10-2-2020")),
    new Asset(itemList[6], officeList[1], Convert.ToDateTime("4-21-2017")),
    new Asset(itemList[7], officeList[1], Convert.ToDateTime("5-28-2018")),
    new Asset(itemList[8], officeList[1], Convert.ToDateTime("5-21-2019")),
    new Asset(itemList[8], officeList[1], Convert.ToDateTime("2-21-2020"))
};

//Start of start code block. Creates a main menu which loops
bool menuLoop = true; // Boolean which monitors looping of main menu.
while(menuLoop)
{
    titleText();
    mainMenuText(); // calls a funstion which displays the main menu text
    
    // switch case that handles main menu choices
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            Console.WriteLine(" - Add an item\n");
            Thread.Sleep(200); 
            addItem(); 
            break;
        case ConsoleKey.D2:
            Console.WriteLine(" - List items\n");
            Thread.Sleep(200);
            displayItems(); 
            break;
        case ConsoleKey.D3:
            Console.WriteLine(" - Add an asset\n");
            Thread.Sleep(200);
            addAsset(); 
            break;
        case ConsoleKey.D4:
            Console.WriteLine(" - List assets\n");
            Thread.Sleep(200);
            displayAssets();
            break;
        case ConsoleKey.D5:
            Console.WriteLine(" - Quit application");
            menuLoop = false;
            Thread.Sleep(200);
            break;
        default:
            break;
    }
}

// Adds an asset to assetList. Requires an Item, Office and a valid dateTime 
void addAsset()
{
    // creates some variables to control looping because text gets updated once user has chosen an item and an office
    bool firstLoop = true;
    int selectedItem = -1;
    int selectedOffice = -1;
    bool allSelected = false;
    while (true)
    {
        if (selectedItem > -1 && selectedOffice > -1) allSelected = true;  // If true moves on to date selection at end of function 
        
        loopStart:

        titleText();
        Console.WriteLine("Add an asset\n");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("     " + "Type".PadRight(14) + " " + "Brand".PadRight(14) + " " + "Model".PadRight(14) + " " + "Price".ToString().PadRight(14));
        Console.ResetColor();

        Console.WriteLine("     " + "----".PadRight(14) + " " + "-----".PadRight(14) + " " + "-----".PadRight(14) + " " + "-----".PadRight(14));

        List<Item> sortedList = itemList.OrderBy(o => o.GetType().ToString()).ThenBy(o => o.Brand).ToList(); // sorts items by Type and then by Brand

        //for loop to list all Items so user can choose
        for (var i = 0;  i < sortedList.Count; i++)
        {
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(i);
            Console.ResetColor();
            if (selectedItem == i)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine(") ".PadRight(3) + sortedList[i].GetType().ToString().PadRight(14) + " " + sortedList[i].Brand.PadRight(14) + " " + sortedList[i].Model.PadRight(14) + " $" + sortedList[i].Price.ToString().PadRight(14));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(") ".PadRight(3) + sortedList[i].GetType().ToString().PadRight(14) + " " + sortedList[i].Brand.PadRight(14) + " " + sortedList[i].Model.PadRight(14) + " $" + sortedList[i].Price.ToString().PadRight(14));
            }
        }

        var value = -1; // intended to validate int input, but is redundant because int check failure changes this value to 0 regardless
        bool isInt = false; 
        if (selectedItem == -1)
        {
            Console.WriteLine("\nSelect an item: ");
            string data = Console.ReadLine();
            Console.WriteLine("--------------------\n");
            isInt = int.TryParse(data, out value); // reflection: strange that a non-integer entry returns a 0
        }
        else
        {
            Console.WriteLine("\n--------------------\n");
            value = selectedItem;
            isInt = true;
        }

        // if item choice is successful move on to office choice

        if (isInt && value >= 0 && value < sortedList.Count) // needs to check value and isInt because Parse test returns 0 even if it is not a number
        {
            //display all Offices
            
            selectedItem = value;
            for (var i = 0; i < officeList.Count; i++)
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i);
                Console.ResetColor();
                if (selectedOffice == i)
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine(") " + officeList[i].Name.PadRight(14));
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(") " + officeList[i].Name.PadRight(14));
                }
            }

            if (firstLoop == true)
            {
                firstLoop = false;
                goto loopStart;
            }
            

            if (!allSelected) // run if no office is chosen
            {
                Console.WriteLine("\nChoose an office");
                var data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 0 && value < officeList.Count)
                {
                    selectedOffice = value;
                }
            }
            else // otherwise move forward to date selection
            {
                Console.WriteLine("\nPurchase date:\n");
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1");
                Console.ResetColor();
                Console.WriteLine(") Today's date");
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2");
                Console.ResetColor();
                Console.WriteLine(") Enter a different date");
                var data = Console.ReadLine();
                isInt = int.TryParse(data, out value);
                if (isInt && value >= 1 && value <= 2)
                {
                    switch(value) // handles menu choices. 
                    {
                        case 1: // make a date from today's date
                            assetList.Add(new Asset(sortedList[selectedItem], officeList[selectedOffice], DateTime.Now));
                            Console.WriteLine("\nAsset added!");
                            Thread.Sleep(1000);
                            break;
                        case 2: // make a date from user input. 
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            Console.Write("Enter day dd: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if(isInt && value >= 1 && value <= 31) day=value;
                            Console.Write("Enter month MM: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if (isInt && value >= 1 && value <= 12) month = value;
                            Console.Write("Enter year YYYY: ".PadRight(14));
                            data = Console.ReadLine();
                            isInt = int.TryParse(data, out value);
                            if (value.ToString().Length==4 && isInt && value >= 1970 && value <= 2099) year = value; // validates date and year is within an interval

                            try // tries to create a date 
                            {
                                DateTime dt = new DateTime(year, month, day);
                                assetList.Add(new Asset(sortedList[selectedItem], officeList[selectedOffice], dt));
                                Console.WriteLine("\nAsset added!");
                                Thread.Sleep(1000);
                                break;
                            }
                            catch(Exception e)  // handles an invalid date by informing user and moving back to loopStart; 
                            {
                                Console.ForegroundColor= ConsoleColor.Red;
                                Console.WriteLine("\nInvalid date!");
                                Console.ResetColor();
                                Thread.Sleep(1000);
                                goto loopStart;
                            }
                    }
                    break;
                }
            }
        }
    }
}


// function which allows a user to add an Item to itemList 
void addItem()   
{    
    loopStart:
    titleText();
    Console.WriteLine("Select a Type:\n");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("1");
    Console.ResetColor();
    Console.WriteLine(") Mobile");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("2");
    Console.ResetColor();
    Console.WriteLine(") Laptop\n");
    var type = "";
    bool menuLoop = true;
    while (menuLoop)
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.D1:
                Console.WriteLine(" - Mobile\n");
                type = "Mobile";
                Thread.Sleep(200);
                menuLoop = false;
                break;
            case ConsoleKey.D2:
                Console.WriteLine(" - Laptop\n");
                type = "Laptop";
                Thread.Sleep(200);
                menuLoop = false;
                break;
            default:
                break;
        }
    }
    Console.Write("Brand: ");
    string brand = Console.ReadLine();
    if(brand=="" || brand==null)
    {
        Console.WriteLine("\nInvalid entry - Brand can't be empty!\n");
        Thread.Sleep(1000);
        goto loopStart;
    }
    Console.Write("Model: ");
    string model = Console.ReadLine();
    if (model == "" || model == null)
    {
        Console.WriteLine("\nInvalid entry - Model can't be empty!\n");
        Thread.Sleep(1000);
        goto loopStart;
    }
    Console.Write("Price in USD: ");
    int price;
    string srtPrice = Console.ReadLine();
    bool isInt = int.TryParse(srtPrice, out price);
    if (!isInt)
    {
        Console.WriteLine("\nInvalid entry - Price must be an integer!\n");
        Thread.Sleep(1000);
        goto loopStart;
    }

    createItem(type, brand, model, price);
    Console.WriteLine("\nItem added!");
    Thread.Sleep(1000);
}

// Function that allows a user to create an Item and adds it to itemList as either a Mobile or a Laptop
void createItem(string type, string brand, string model, int price)
{
    if (type == "Mobile")
    {
        itemList.Add(new Mobile(brand, model, price));
    }
    else if (type == "Laptop")
    {
        itemList.Add(new Laptop(brand, model, price));
    }
}

// Fuction that loops through a sorted itemList to display all Items
void displayItems()
{
    titleText();
    Console.WriteLine("\nAll items listed:\n");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("    " + "Type".PadRight(14) + " " + "Brand".PadRight(14) + " " + "Model".PadRight(14) + " " + "Price".ToString().PadRight(14));
    Console.ResetColor();

    Console.WriteLine("    " + "----".PadRight(14) + " " + "-----".PadRight(14) + " " + "-----".PadRight(14) + " " + "-----".PadRight(14));
    
    List<Item> sortedList = itemList.OrderBy(o => o.GetType().ToString()).ThenBy(o => o.Brand).ToList();

    for (int i = 0; i<sortedList.Count; i++)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write((i+1).ToString().PadRight(4));
        Console.ResetColor();
        Console.WriteLine(sortedList[i].GetType().ToString().PadRight(14) + " " + sortedList[i].Brand.PadRight(14) + " " + sortedList[i].Model.PadRight(14) + " $" + sortedList[i].Price.ToString().PadRight(14));
    }
    Console.WriteLine("\nPress any key to return to main menu");
    Console.ReadLine();
    Thread.Sleep(200);
}

// Function that loops through a sorted assetList to display all Assets
void displayAssets()
{
    titleText();
    Console.WriteLine("\nAll assets listed:\n");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Type".PadRight(14) + " " + "Brand".PadRight(14) + " " + "Model".PadRight(14) + " " + "Office".PadRight(14) + " " + "Purchase Date".PadRight(14) + " " + "Price in USD".PadRight(14) + " " + "Currency".PadRight(10) + " " + "Local Price today".PadRight(17));
    Console.ResetColor();
    Console.WriteLine("----".PadRight(14) + " " + "-----".PadRight(14) + " " + "-----".PadRight(14) + " " + "------".PadRight(14) + " " + "-------------".PadRight(14) + " " + "------------".PadRight(14) + " " + "--------".PadRight(10) + " " + "-----------------".PadRight(17));

    List<Asset> SortedList = assetList.OrderBy(o => o.Office.Name).ThenBy(o => o.PurchaseDate).ToList();

    foreach (Asset asset in SortedList)
        asset.printDetails();
    Console.WriteLine("\nPress any key to return to main menu");
    Console.ReadLine();
    Thread.Sleep(200);
}

// Function used to continually show the same title text regardless of which page user is on
void titleText()
{
    Console.Clear();
    Console.WriteLine("~ Asset tracking ~");
    Console.WriteLine("------------------");
}

// Funktion which creates and displays the main menu in the console
void mainMenuText() {
    Console.WriteLine("\nChoose from the following options:\n");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("1");
    Console.ResetColor();
    Console.WriteLine(") Add an item");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("2");
    Console.ResetColor();
    Console.WriteLine(") List all items");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("3");
    Console.ResetColor();
    Console.WriteLine(") Add an asset");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("4");
    Console.ResetColor();
    Console.WriteLine(") List all assets");
    Console.Write("(");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("5");
    Console.ResetColor();
    Console.WriteLine(") Quit Application\n");
}

// Class for an Item containing Brand, Model and Price
class Item
{
    public Item(string brand, string model, int price)
    {
        Brand = brand;
        Model = model;
        Price = price;
    }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Price { get; set; }
}

// Class for a Laptop which derives from base class Item
class Laptop : Item
{
    public Laptop(string brand, string model, int price) : base(brand, model, price)
    {
        Brand = brand;
        Model = model;
        Price = price;
    }
    public new string Brand { get; set; }
    public new string Model { get; set; }
    public new int Price { get; set; }
}

// Class for a Mobile which derives from base class Item
class Mobile : Item 
{
    public Mobile(string brand, string model, int price) : base(brand, model, price)
    {
        Brand = brand;
        Model = model;
        Price = price;
    }
    public new string Brand { get; set; }
    public new string Model { get; set; }
    public new int Price { get; set; }
}

// Class for an Office which includes Name, Currency and Rate
class Office
{
    public Office(string name, string currency, double rate)
    {
        Name = name;
        Currency = currency;
        Rate = rate;
    }

    public string Name { get; set; }
    public string Currency { get; set; }
    public double Rate { get; set; }
}

// Class for an Asset which includes an Item, Office and DateTime
class Asset 
{
    public Asset(Item item, Office office, DateTime purchaseDate)
    {
        Item = item;
        Office = office;
        PurchaseDate = purchaseDate;
    }

    public Item Item { get; }
    public Office Office { get; }
    public DateTime PurchaseDate { get; set; }

    public void printDetails()
    {
        DateTime dateNow = DateTime.Now;
        DateTime dateLife = this.PurchaseDate.AddYears(2).AddMonths(6);
        TimeSpan diff = dateNow - dateLife;
        int res = DateTime.Compare(dateLife, dateNow);
        if (res < 0)
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
        }
        dateLife = this.PurchaseDate.AddYears(2).AddMonths(9);
        TimeSpan diff2 = dateNow - dateLife;
        res = DateTime.Compare(dateLife, dateNow);
        if (res < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine(this.Item.GetType().ToString().PadRight(14) + " " + this.Item.Brand.PadRight(14) + " " + this.Item.Model.PadRight(14) + " " + this.Office.Name.PadRight(14) + " " + this.PurchaseDate.ToString("dd/MM/yyyy").PadRight(14) + " " + this.Item.Price.ToString().PadRight(14) + " " + this.Office.Currency.PadRight(10) + " " + (this.Item.Price * this.Office.Rate).ToString("#.##").PadRight(17));
        Console.ResetColor();
    }
}




