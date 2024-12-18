namespace WineSopTests;

using WineShop;

public class Tests
{
    private Address address = new Address("Short", 12,1,"12-232");
    [Test]
    public void CheckAddressStreetCorrectInfoBack()
    {
        Assert.That(address.Street, Is.EqualTo("Short"));
    }
    
    [Test]
    public void CheckAddressStreetNumberCorrectInfoBack()
    {
        Assert.That(address.StreetNumber, Is.EqualTo(12));
    }
    
    [Test]
    public void CheckAddressApartmentNumberCorrectInfoBack()
    {
        Assert.That(address.ApartmentNumber, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckAddressZipCodeCorrectInfoBack()
    {
        Assert.That(address.ZipCode, Is.EqualTo("12-232"));
    }
    
    [Test]
    public void CheckAddressStreetIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => address.Street = "");
    }
    
    [Test]
    public void CheckAddressStreetIsNullException()
    {
        Assert.Throws<ArgumentException>(() => address.Street = null);
    }
    
    [Test]
    public void CheckAddressStreetNumberIsLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => address.StreetNumber = -3);
    }
    
    [Test]
    public void CheckAddressApartmentNumberIsLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => address.ApartmentNumber = -3);
    }
    
    [Test]
    public void CheckAddressZipCodeIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => address.ZipCode = "");
    }
    
    [Test]
    public void CheckAddressZipCodeIsNullException()
    {
        Assert.Throws<ArgumentException>(() => address.ZipCode = null);
    }
    
    private Alcohol alcohol = new Alcohol("Whiskey", "Jack Daniels", 99.99, Type.Spirit, 2018);
    
    [Test]
    public void CheckAlcoholNameCorrectInfoBack()
    {
        Assert.That(alcohol.Name, Is.EqualTo("Whiskey"));
    }
    
    [Test]
    public void CheckAlcoholBrandCorrectInfoBack()
    {
        Assert.That(alcohol.Brand, Is.EqualTo("Jack Daniels"));
    }
    
    [Test]
    public void CheckAlcoholPriceCorrectInfoBack()
    {
        Assert.That(alcohol.Price, Is.EqualTo(99.99));
    }
    
    [Test]
    public void CheckAlcoholTypeCorrectInfoBack()
    {
        Assert.That(alcohol.Type, Is.EqualTo(Type.Spirit));
    }
    
    [Test]
    public void CheckAlcoholYearCorrectInfoBack()
    {
        Assert.That(alcohol.YearOfManufacture, Is.EqualTo(2018));
    }
    
    [Test]
    public void CheckAlcoholAgeCorrectInfoBack()
    {
        Assert.That(alcohol.Age, Is.EqualTo(6));
    }

    [Test]
    public void CheckAlcoholNameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.Name = "");
    }
    
    [Test]
    public void CheckAlcoholNameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.Name = null);
    }
    
    [Test]
    public void CheckAlcoholBrandIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.Brand = "");
    }
    
    [Test]
    public void CheckAlcoholBrandIsNullException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.Brand = null);
    }
    
    [Test]
    public void CheckAlcoholPriceIsLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.Price = -1);
    }
    
    [Test]
    public void CheckAlcoholYearOfManufactureIsLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.YearOfManufacture = -1);
    }
    
    [Test]
    public void CheckAlcoholYearOfManufactureIsGraterThanTodaysYearException()
    {
        Assert.Throws<ArgumentException>(() => alcohol.YearOfManufacture = DateTime.Now.Year+3);
    }

    private Alcohol alcohol2 = new Alcohol("Red Wine", "NewWood", 300.50, Type.Wine, 2010);
    
    [Test]
    public void CheckAlcoholExtentStoresCorrectClasses()
    { 
        Alcohol alcohol0 = new Alcohol("White Wine", "Krym", 150.50, Type.Wine, 2016);
        Assert.That(alcohol0, Is.EqualTo(Alcohol.AlcoholExtent[2]));
    }
    
    [Test]
    public void CheckAlcoholExtentStoresCorrectAmount()
    {
        Assert.That(Alcohol.AlcoholExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckAlcoholExtentPersistence()
    {
        List<Alcohol> alcohols = Alcohol.AlcoholExtent;
        Alcohol.save();
        bool isLoaded = Alcohol.load();
        bool isEqual = true;
        
        if (alcohols.Count != Alcohol.AlcoholExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < alcohols.Count; i++)
            {
                if (alcohols[i].Name != Alcohol.AlcoholExtent[i].Name ||
                    alcohols[i].Brand != Alcohol.AlcoholExtent[i].Brand ||
                    alcohols[i].Price != Alcohol.AlcoholExtent[i].Price || 
                    alcohols[i].Type != Alcohol.AlcoholExtent[i].Type ||
                    alcohols[i].YearOfManufacture != Alcohol.AlcoholExtent[i].YearOfManufacture)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckAlcoholExtentModifyOutsideAClass()
    {
        Alcohol.AlcoholExtent.Clear();
        
        Assert.That(Alcohol.AlcoholExtent.Count, Is.GreaterThan(0));
    }
    
    private Cocktail cocktail = new Cocktail(1, "Pornstar Martini", ["ice", "passion fruit"]);
    
    [Test]
    public void CheckCocktailIdCorrectInfoBack()
    {
        Assert.That(cocktail.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckCocktailNameCorrectInfoBack()
    {
        Assert.That(cocktail.Name, Is.EqualTo("Pornstar Martini"));
    }
    [Test]
    public void CheckCocktailIngredientsCorrectInfoBack()
    {
        List<string> ingredients = ["ice", "passion fruit"];
        bool flag = true;
        if (ingredients.Count == cocktail.ListOfIngredients.Count)
        {
            for (int i = 0; i < cocktail.ListOfIngredients.Count; i++)
            {
                if (cocktail.ListOfIngredients[i] != ingredients[i])
                {
                    flag = false;
                }
            }
        }
        else
        {
            flag = false;
        }
        
        Assert.That(flag);
    }

    [Test]
    public void CheckCocktailIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => cocktail.Id = -2);
    }
    
    [Test]
    public void CheckCocktailNameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => cocktail.Name = "");
    }
    
    [Test]
    public void CheckCocktailNameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => cocktail.Name = null);
    }
    
    [Test]
    public void CheckCocktailIngredientsContainEmptyStringException()
    {
        List<string> ingredients = ["ice", ""];
        Assert.Throws<ArgumentException>(() => cocktail.ListOfIngredients = ingredients);
    }
    Cocktail cocktail0 = new Cocktail(2, "Cuba Libre", ["ice", "cola"]);

    [Test]
    public void CheckCocktailAddIngredient()
    {
        cocktail0.AddIngredient("orange juice");
        Assert.That(cocktail0.ListOfIngredients.Last() == "orange juice");
    }
    
    [Test]
    public void CheckCocktailRemoveIngredient()
    {
        cocktail0.RemoveIngredient(0);
        Assert.That(cocktail0.ListOfIngredients.First(), Is.EqualTo("cola"));
    }
    
    [Test]
    public void CheckCocktailExtentStoresCorrectClasses()
    { 
        Assert.That(cocktail0.Id, Is.EqualTo(Cocktail.CocktailExtent[1].Id));
    }
    
    [Test]
    public void CheckCocktailExtentStoresCorrectAmount()
    {
        Assert.That(Cocktail.CocktailExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckCocktailExtentPersistence()
    {
        List<Cocktail> cocktails = Cocktail.CocktailExtent;
        Cocktail.save();
        bool isLoaded = Cocktail.load();
        bool isEqual = true;
        
        if (cocktails.Count != Cocktail.CocktailExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < cocktails.Count; i++)
            {
                if (cocktails[i].Id != Cocktail.CocktailExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckCocktailExtentModifyOutsideAClass()
    {
        Cocktail.CocktailExtent.Clear();
        
        Assert.That(Cocktail.CocktailExtent.Count, Is.GreaterThan(0));
    }
    private static Facility facility1 = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
    private Employee employee = new Employee(123456789, "John", "Doe",new Address("Zlota", 10, 3, "22-033"), facility1);
    
    [Test]
    public void CheckEmployeePeselCorrectInfoBack()
    {
        Assert.That(employee.Pesel, Is.EqualTo(123456789));
    }
    
    [Test]
    public void CheckEmployeeNameCorrectInfoBack()
    {
        Assert.That(employee.Name, Is.EqualTo("John"));
    }
    
    [Test]
    public void CheckEmployeeSurnameCorrectInfoBack()
    {
        Assert.That(employee.Surname, Is.EqualTo("Doe"));
    }
    
    [Test]
    public void CheckEmployeeAddressCorrectInfoBack()
    {
        bool isCorrect = true;
        if (employee.Address.ApartmentNumber != 3 ||
            employee.Address.StreetNumber != 10 ||
            employee.Address.Street != "Zlota" ||
            employee.Address.ZipCode != "22-033")
        {
            isCorrect = false;
        }
        
        Assert.That(true);
    }
    
    [Test]
    public void CheckEmployeeWorkingHoursCorrectInfoBack()
    {
        Assert.That(Employee.WorkingHours, Is.EqualTo(8));
    }
    
    [Test]
    public void CheckEmployeePeselLessThanZeroException()
    {
        Assert.Throws<ArgumentException>(() => employee.Pesel = -1);
    }
    
    [Test]
    public void CheckEmployeePeselLessThan9DigitsException()
    {
        Assert.Throws<ArgumentException>(() => employee.Pesel = 123);
    }
    
    [Test]
    public void CheckEmployeeNameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => employee.Name = "");
    }
    
    [Test]
    public void CheckEmployeeNameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => employee.Name = null);
    }
    
    [Test]
    public void CheckEmployeeSurnameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => employee.Surname = "");
    }
    
    [Test]
    public void CheckEmployeeSurnameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => employee.Surname = null);
    }

    private Employee employee0 = new Employee(987654321, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility1);
    
    [Test]
    public void CheckEmployeeExtentStoresCorrectClasses()
    { 
        Assert.That(employee0.Pesel, Is.EqualTo(Employee.EmployeeExtent[1].Pesel));
    }
    
    [Test, Order(1)]
    public void CheckEmployeeExtentStoresCorrectAmount()
    {
        Assert.That(Employee.EmployeeExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckEmployeeExtentPersistence()
    {
        List<Employee> employees = Employee.EmployeeExtent;
        Employee.save();
        bool isLoaded = Employee.load();
        bool isEqual = true;
        
        if (employees.Count != Employee.EmployeeExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Pesel != Employee.EmployeeExtent[i].Pesel)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckEmployeeExtentModifyOutsideAClass()
    {
        Employee.EmployeeExtent.Clear();
        Assert.That(Employee.EmployeeExtent.Count, Is.GreaterThan(0));
    }

    private IndividualAccount individualAccount = new IndividualAccount(1,"first@mail.com", "4568909797","Steve", "Black","blacksteve",44);
    
    [Test]
    public void CheckIndividualAccountIdCorrectInfoBack()
    {
        Assert.That(individualAccount.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckIndividualAccountEmailCorrectInfoBack()
    {
        Assert.That(individualAccount.Email, Is.EqualTo("first@mail.com"));
    }
    
    [Test]
    public void CheckIndividualAccountPhoneCorrectInfoBack()
    {
        Assert.That(individualAccount.Phone, Is.EqualTo("4568909797"));
    }
    
    [Test]
    public void CheckIndividualAccountNameCorrectInfoBack()
    {
        Assert.That(individualAccount.Name, Is.EqualTo("Steve"));
    }
    
    [Test]
    public void CheckIndividualAccountSurnameCorrectInfoBack()
    {
        Assert.That(individualAccount.Surname, Is.EqualTo("Black"));
    }
    
    [Test]
    public void CheckIndividualAccountUsernameCorrectInfoBack()
    {
        Assert.That(individualAccount.Username, Is.EqualTo("blacksteve"));
    }
    
    [Test]
    public void CheckIndividualAccountAgeCorrectInfoBack()
    {
        Assert.That(individualAccount.Age, Is.EqualTo(44));
    }
    
    [Test]
    public void CheckIndividualAccountIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Id = -1);
    }
    
    [Test]
    public void CheckIndividualAccountEmailIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Email = "");
    }
    
    [Test]
    public void CheckIndividualAccountEmailIsNullException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Email = null);
    }
    
    [Test]
    public void CheckIndividualAccountPhoneIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Phone = "");
    }
    
    [Test]
    public void CheckIndividualAccountPhoneIsNullException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Phone = null);
    }
    
    [Test]
    public void CheckIndividualAccountNameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Name = "");
    }
    
    [Test]
    public void CheckIndividualAccountNameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Name = null);
    }
    
    [Test]
    public void CheckIndividualAccountSurnameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Surname = "");
    }
    
    [Test]
    public void CheckIndividualAccountSurnameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Surname = null);
    }
    
    [Test]
    public void CheckIndividualAccountUsernameIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Username = "");
    }
    
    [Test]
    public void CheckIndividualAccounUsernameIsNullException()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Username = null);
    }
    
    [Test]
    public void CheckIndividualAccountAgeLessEqual18Exception()
    {
        Assert.Throws<ArgumentException>(() => individualAccount.Age = 10);
    }

    private IndividualAccount individualAccount0 = new IndividualAccount(2, "jane@mail.com", "456234987", "Jane", "Doe", "janedoe", 25);
    [Test]
    public void CheckIndividualAccountExtentStoresCorrectClasses()
    { 
        Assert.That(individualAccount0.Id, Is.EqualTo(IndividualAccount.IndividualAccountExtent[1].Id));
    }
    
    [Test]
    public void CheckIndividualAccountExtentStoresCorrectAmount()
    {
        Assert.That(IndividualAccount.IndividualAccountExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckIndividualAccountExtentPersistence()
    {
        List<IndividualAccount> individualAccounts = IndividualAccount.IndividualAccountExtent;
        IndividualAccount.save();
        bool isLoaded = IndividualAccount.load();
        bool isEqual = true;
        
        if (individualAccounts.Count != IndividualAccount.IndividualAccountExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < individualAccounts.Count; i++)
            {
                if (individualAccounts[i].Id != IndividualAccount.IndividualAccountExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckIndividualAccountExtentModifyOutsideAClass()
    {
        IndividualAccount.IndividualAccountExtent.Clear();
        Assert.That(IndividualAccount.IndividualAccountExtent.Count, Is.GreaterThan(0));
    }

    private NonPremium nonPremium = new NonPremium(1, "newuser@mail.com", "234654789");

    [Test]
    public void CheckNonPremiumIdCorrectInfoBack()
    {
        Assert.That(nonPremium.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckNonPremiumEmailCorrectInfoBack()
    {
        Assert.That(nonPremium.Email, Is.EqualTo("newuser@mail.com"));
    }
    
    [Test]
    public void CheckNonPremiumPhoneCorrectInfoBack()
    {
        Assert.That(nonPremium.Phone, Is.EqualTo("234654789"));
    }
    
    [Test]
    public void CheckNonPremiumIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => nonPremium.Id = -1);
    }
    
    [Test]
    public void CheckNonPremiumEmailIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => nonPremium.Email = "");
    }
    
    [Test]
    public void CheckNonPremiumEmailIsNullException()
    {
        Assert.Throws<ArgumentException>(() => nonPremium.Email = null);
    }
    
    [Test]
    public void CheckNonPremiumPhoneIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => nonPremium.Phone = "");
    }
    
    [Test]
    public void CheckNonPremiumPhoneIsNullException()
    {
        Assert.Throws<ArgumentException>(() => nonPremium.Phone = null);
    }
    
    private NonPremium nonPremium0 = new NonPremium(2, "jane@mail.com", "456234987");
    [Test]
    public void CheckNonPremiumExtentStoresCorrectClasses()
    { 
        Assert.That(nonPremium0.Id, Is.EqualTo(NonPremium.NonPremiumExtent[1].Id));
    }
    
    [Test]
    public void CheckNonPremiumExtentStoresCorrectAmount()
    {
        Assert.That(NonPremium.NonPremiumExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckNonPremiumExtentPersistence()
    {
        List<NonPremium> nonPremiums = NonPremium.NonPremiumExtent;
        NonPremium.save();
        bool isLoaded = NonPremium.load();
        bool isEqual = true;
        
        if (nonPremiums.Count != NonPremium.NonPremiumExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < nonPremiums.Count; i++)
            {
                if (nonPremiums[i].Id != NonPremium.NonPremiumExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckNonPremiumExtentModifyOutsideAClass()
    {
        NonPremium.NonPremiumExtent.Clear();
        Assert.That(NonPremium.NonPremiumExtent.Count, Is.GreaterThan(0));
    }
    
    private Premium premium = new Premium(1, "newuser@mail.com", "234654789", new MyDate(12, 7, 2024), new MyDate(12, 1, 2025), ["free delivery"]);

    [Test]
    public void CheckPremiumIdCorrectInfoBack()
    {
        Assert.That(premium.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckPremiumEmailCorrectInfoBack()
    {
        Assert.That(premium.Email, Is.EqualTo("newuser@mail.com"));
    }
    
    [Test]
    public void CheckPremiumPhoneCorrectInfoBack()
    {
        Assert.That(premium.Phone, Is.EqualTo("234654789"));
    }
    
    [Test]
    public void CheckPremiumIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => premium.Id = -1);
    }
    
    [Test]
    public void CheckPremiumEmailIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => premium.Email = "");
    }
    
    [Test]
    public void CheckPremiumEmailIsNullException()
    {
        Assert.Throws<ArgumentException>(() => premium.Email = null);
    }
    
    [Test]
    public void CheckPremiumPhoneIsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => premium.Phone = "");
    }
    
    [Test]
    public void CheckPremiumPhoneIsNullException()
    {
        Assert.Throws<ArgumentException>(() => premium.Phone = null);
    }
    
    [Test]
    public void CheckPremiumEndDateEarlierThanStartDateException()
    {
        Assert.Throws<ArgumentException>(() => premium.EndDate = new MyDate(1, 1, 2022));
    }
    
    [Test]
    public void CheckPremiumBenefitsEmptyException()
    {
        Assert.Throws<ArgumentException>(() => premium.Benefits = []);
    }
    
    [Test]
    public void CheckPremiumBenefitsHasEmptyStringException()
    {
        Assert.Throws<ArgumentException>(() => premium.Benefits = ["", "free delivery"]);
    }
    
    private Premium premium0 = new Premium(2, "jane@mail.com", "456234987", new MyDate(1,5,2024), new MyDate(30, 11,2024),["free delivery", "priority delivery"]);
    [Test]
    public void CheckPremiumExtentStoresCorrectClasses()
    { 
        Assert.That(premium0.Id, Is.EqualTo(Premium.PremiumExtent[1].Id));
    }
    
    [Test]
    public void CheckPremiumExtentStoresCorrectAmount()
    {
        Assert.That(Premium.PremiumExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckPremiumExtentPersistence()
    {
        List<Premium> premiums = Premium.PremiumExtent;
        Premium.save();
        bool isLoaded = Premium.load();
        bool isEqual = true;
        
        if (premiums.Count != Premium.PremiumExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < premiums.Count; i++)
            {
                if (premiums[i].Id != Premium.PremiumExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckPremiumExtentModifyOutsideAClass()
    {
        Premium.PremiumExtent.Clear();
        Assert.That(Premium.PremiumExtent.Count, Is.GreaterThan(0));
    }
    
    [Test]
    public void CheckPremiumAddBenefit()
    {
        premium0.AddBenefit("coupons");
        Assert.That(premium0.Benefits.Last() == "coupons");
    }
    
    [Test]
    public void CheckPremiumRemoveBenefit()
    {
        premium0.RemoveBenefit(0);
        Assert.That(premium0.Benefits.First(), Is.EqualTo("priority delivery"));
    }
    
    private Warehouse warehouse = new Warehouse(10,1, new Address("Pine", 1, 1,"11-110"));
    
    [Test]
    public void CheckWarehouseIdCorrectInfoBack()
    {
        Assert.That(warehouse.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckWarehouseStorageCorrectInfoBack()
    {
        Assert.That(warehouse.StorageLeft, Is.EqualTo(10));
    }
    
    [Test]
    public void CheckWarehouseAddressCorrectInfoBack()
    {
        bool isCorrect = true;
        if (warehouse.Address.ApartmentNumber != 1 ||
            warehouse.Address.StreetNumber != 1 ||
            warehouse.Address.Street != "Pine" ||
            warehouse.Address.ZipCode != "11-110")
        {
            isCorrect = false;
        }
        
        Assert.That(true);
    }
    
    [Test]
    public void CheckWarehouseIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => warehouse.Id = -1);
    }
    
    [Test]
    public void CheckWarehouseStorageLessThanZeroException()
    {
        Assert.Throws<ArgumentException>(() => warehouse.StorageLeft = -1);
    }

    private Warehouse warehouse0 = new Warehouse(2, 2,new Address("Pine", 1, 10,"11-110"));
    [Test]
    public void CheckWarehouseExtentStoresCorrectClasses()
    { 
        Assert.That(warehouse0.Id, Is.EqualTo(Warehouse.WarehouseExtent[2].Id));
    }
    
    [Test]
    public void CheckWarehouseExtentStoresCorrectAmount()
    {
        Assert.That(Warehouse.WarehouseExtent.Count, Is.EqualTo(3));
    }
    
    [Test]
    public void CheckWarehouseExtentPersistence()
    {
        List<Warehouse> warehouses = Warehouse.WarehouseExtent;
        Warehouse.save();
        bool isLoaded = Warehouse.load();
        bool isEqual = true;
        
        if (warehouses.Count != Warehouse.WarehouseExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < warehouses.Count; i++)
            {
                if (warehouses[i].Id != Warehouse.WarehouseExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckWarehouseExtentModifyOutsideAClass()
    {
        Warehouse.WarehouseExtent.Clear();
        Assert.That(Warehouse.WarehouseExtent.Count, Is.GreaterThan(0));
    }
    
    private Store store = new Store(1, new Address("Pine", 1, 1,"11-110"), new Time(8,0,0), new Time(20,0,0));
    
    [Test]
    public void CheckStoreIdCorrectInfoBack()
    {
        Assert.That(store.Id, Is.EqualTo(1));
    }
    
    [Test]
    public void CheckStoreOpeningTimeCorrectInfoBack()
    {
        Assert.That(store.OpeningTime.hours, Is.EqualTo(8));
    }
    
    [Test]
    public void CheckStoreClosingTimeCorrectInfoBack()
    {
        Assert.That(store.ClosingTime.hours, Is.EqualTo(20));
    }
    
    [Test]
    public void CheckStoreAddressCorrectInfoBack()
    {
        bool isCorrect = true;
        if (store.Address.ApartmentNumber != 1 ||
            store.Address.StreetNumber != 1 ||
            store.Address.Street != "Pine" ||
            store.Address.ZipCode != "11-110")
        {
            isCorrect = false;
        }
        
        Assert.That(true);
    }
    
    [Test]
    public void CheckStoreIdLessEqualZeroException()
    {
        Assert.Throws<ArgumentException>(() => store.Id = -1);
    }

    private Store store0 = new Store(2, new Address("Pine", 1, 10,"11-110"), new Time(9,0,0), new Time(21,0,0));
    [Test]
    public void CheckStoreExtentStoresCorrectClasses()
    { 
        Assert.That(store0.Id, Is.EqualTo(Store.StoreExtent[1].Id));
    }
    
    [Test]
    public void CheckStoreExtentStoresCorrectAmount()
    {
        Assert.That(Store.StoreExtent.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void CheckStoreExtentPersistence()
    {
        List<Store> stores = Store.StoreExtent;
        Store.save();
        bool isLoaded = Store.load();
        bool isEqual = true;
        
        if (stores.Count != Store.StoreExtent.Count)
        {
            isEqual = false;
        }
        else
        {
            for (int i = 0; i < stores.Count; i++)
            {
                if (stores[i].Id != Store.StoreExtent[i].Id)
                {
                    isEqual = false;
                    break;
                }
            }
        }
        
        Assert.IsTrue(isEqual && isLoaded);
    }
    
    [Test]
    public void CheckStoreExtentModifyOutsideAClass()
    {
        Store.StoreExtent.Clear();
        Assert.That(Store.StoreExtent.Count, Is.GreaterThan(0));
    }

//-------------------------------NEW TESTS FOR ASSOCIATIONS---------------------------------------------
    [Test]
    public void CheckEmployeeAddManager()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility);
        emp.AddManager(manager);
        Assert.True(emp.Pesel == manager.EmployeesUnderThisManager[0].Pesel && manager.Pesel == emp.Manager.Pesel);
    }
    
    [Test]
    public void CheckEmployeeRemoveManager()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility);
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility, manager);
        emp.RemoveManager();
        Assert.True(emp.Manager == null && manager.EmployeesUnderThisManager.Count == 0);
    }
    
    [Test]
    public void CheckEmployeeEditManager()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility, [emp]);
        Employee newManager = new Employee(123132323, "Jenifer", "Watson", new Address("Short", 21, 7, "12-316"), facility);
        emp.EditManager(newManager);
        Assert.True(emp.Pesel == newManager.EmployeesUnderThisManager[0].Pesel && newManager.Pesel == emp.Manager.Pesel && manager.EmployeesUnderThisManager.Count == 0);
    }
    
    [Test]
    public void CheckEmployeeAddEmployeeToManager()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility);
        manager.AddEmployeeToManager(emp);
        Assert.True(emp.Pesel == manager.EmployeesUnderThisManager[0].Pesel && manager.Pesel == emp.Manager.Pesel);
    }
    
    [Test]
    public void CheckEmployeeRemoveEmployee()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility, [emp]);
        manager.RemoveEmployeeFromManager(emp);
        Assert.True(emp.Manager == null && manager.EmployeesUnderThisManager.Count == 0);
    }
    
    [Test]
    public void CheckEmployeeEditEmployees()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility, [emp]);
        Employee newEmployee = new Employee(123132323, "Jenifer", "Watson", new Address("Short", 21, 7, "12-316"), facility);
        manager.EditEmployees([newEmployee]);
        Assert.True(newEmployee.Pesel == manager.EmployeesUnderThisManager[0].Pesel && manager.Pesel == newEmployee.Manager.Pesel && emp.Manager == null);
    }
    
    [Test]
    public void CheckEmployeeAddManagerNullException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => emp.AddManager(null));
    }
    
    [Test]
    public void CheckEmployeeAddManagerEmployeeAlreadyHasAManagerException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility, [emp]);
        Employee anotherManager = new Employee(123132323, "Jenifer", "Watson", new Address("Short", 21, 7, "12-316"), facility);
        Assert.Throws<ArgumentException>(() => emp.AddManager(anotherManager));
    }

    [Test]
    public void CheckEmployeeAddEmployeeEmployeeAlreadyHasAManagerException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility, [emp]);
        Employee anotherManager = new Employee(123132323, "Jenifer", "Watson", new Address("Short", 21, 7, "12-316"), facility);
        Assert.Throws<ArgumentException>(() => anotherManager.AddEmployeeToManager(emp));
    }

    [Test]
    public void CheckEmployeeAddEmployeeNullException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee manager = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => manager.AddEmployeeToManager(null));
    }
    
    [Test]
    public void CheckEmployeeRemoveManagerEmployeeDoesNotHaveAManagerException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentException>(() => emp.RemoveManager());
    }
    
    [Test]
    public void CheckEmployeeRemoveEmployeeNullException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee manager = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => manager.RemoveEmployeeFromManager(null));
    }
    
    [Test]
    public void CheckEmployeeRemoveEmployeeManagerDoesNotHaveThisEmployeeException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), facility);
        Assert.Throws<ArgumentException>(() => manager.RemoveEmployeeFromManager(emp));
    }
    
    [Test]
    public void CheckEmployeeEditManagerNullException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => emp.EditManager(null));
    }
    
    [Test]
    public void CheckEmployeeEditEmployeesNullException()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee manager = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => manager.EditEmployees(null));
    }

    [Test]
    public void CheckAddFacilityForEmployee()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        
        Assert.True(facility.EmployeesWorkInThisFacility[0] == emp && emp.FacilityWhereEmployeeWorks == facility);
    }

    [Test]
    public void CheckEditFacilityForEmployee()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Facility facility2 = new Warehouse(35, 2, new Address("Short", 2, 60, "12-445"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        emp.EditFacilityForEmployee(facility2);
        Assert.True(facility2.EmployeesWorkInThisFacility[0] == emp && emp.FacilityWhereEmployeeWorks == facility2 && facility.EmployeesWorkInThisFacility.Count == 0);
    }
    
    [Test]
    public void CheckAddFacilityForEmployeeFacilityEqualsNull()
    {
        Employee emp = new Employee();
        Assert.Throws<ArgumentNullException>(() => emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), null));
    }
    
    [Test]
    public void CheckEditFacilityForEmployeeFacilityEqualsNull()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentNullException>(() => emp.EditFacilityForEmployee(null));
    }
    
    [Test]
    public void CheckEditFacilityForEmployeeAlreadyWorksAtThisFacility()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        Assert.Throws<ArgumentException>(() => emp.EditFacilityForEmployee(facility));
    }
    
    [Test]
    public void CheckAddEmployeeToFacility()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), facility);
        
        Assert.True(facility.EmployeesWorkInThisFacility[0] == emp && emp.FacilityWhereEmployeeWorks == facility);
    }
    
    
    [Test]
    public void CheckDeleteFacilityInWarehouse()
    {
        Facility warehouse = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Facility store = new Store(1, new Address("Long", 10, 8, "12-446"), new Time(8,0,0), new Time(23,0,0));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), warehouse);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), store, [emp]);
        
        warehouse.DeleteFacility();
        
        Assert.True(!Warehouse.WarehouseExtent.Contains(warehouse) && !Employee.EmployeeExtent.Contains(emp) && manager.EmployeesUnderThisManager.Count == 0);
    }
    
    [Test]
    public void CheckDeleteFacilityInStore()
    {
        Facility warehouse = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Facility store = new Store(1, new Address("Long", 10, 8, "12-446"), new Time(8,0,0), new Time(23,0,0));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), warehouse);
        Employee manager = new Employee(227654333, "Joe", "Stevenson", new Address("Long", 12, 7, "12-326"), store, [emp]);
        
        store.DeleteFacility();
        
        Assert.True(!Store.StoreExtent.Contains(store) && !Employee.EmployeeExtent.Contains(manager) && emp.Manager == null);
    }
        
    [Test]
    public void CheckAddEmployeeToFacilityEmployeeEqualsNull()
    {
        Facility facility = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Assert.Throws<ArgumentNullException>(() => facility.AddEmployeeToFacility(null));
    }
    
    [Test]
    public void CheckAddEmployeeToFacilityEmployeeHasAnotherFacility()
    {
        Facility warehouse = new Warehouse(20, 1, new Address("Long", 10, 6, "12-446"));
        Facility store = new Store(1, new Address("Long", 10, 8, "12-446"), new Time(8,0,0), new Time(23,0,0));
        Employee emp = new Employee(987654333, "Jane", "Smith", new Address("Long", 13, 7, "12-456"), warehouse);

        Assert.Throws<ArgumentException>(() => store.AddEmployeeToFacility(emp));
    }
}