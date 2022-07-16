using BankConsole;
using System.Text.RegularExpressions;

if(args.Length == 0)
{
    EmailService.SendMail();
}
else
{
    ShowMenu();
}

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("---------------------MENU---------------------");
    Console.WriteLine("1.- Crear un Usuario nuevo.");
    Console.WriteLine("2.- Eliminar un Usuario existente.");
    Console.WriteLine("3.- Salir");
    Console.WriteLine("Selecciona la Opcion:");

    int option = 0;
    do
    {
        string input = Console.ReadLine();

        if(!int.TryParse(input, out option))
            Console.WriteLine("Debes de ingresar un numero (1,2,3).");
        else if(option > 3)
            Console.WriteLine("Debes ingresar un número valido (1,2,3).");
    } while(option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}

void CreateUser()
{
    int ID;
    string name, email;
    decimal balance;
    char userType;
    User newUser;

    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario:");
    Console.Write("ID: ");
    ID = int.Parse(Console.ReadLine());
    if(!ValidarID(ID))
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Ingresa la información del usuario:");
            Console.WriteLine("El ID que ingreso ya le pertenece a un usuario existente, por favor ingrese otro ID.");
            Console.Write("ID: ");
            ID = int.Parse(Console.ReadLine());
        } while(!ValidarID(ID));
    }
    Console.Write("Nombre: ");
    name = Console.ReadLine();
    Console.Write("Email: ");
    email = Console.ReadLine();
    if(!ValidarEmail(email))
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Ingresa la información del usuario:");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Nombre: " + name);
            Console.WriteLine("El correo que ingreso no tiene el formato valido, por favor intentalo de nuevo.");
            Console.Write("Email: ");
            email = Console.ReadLine();
        } while(!ValidarEmail(email));
    }
    Console.Write("Saldo: ");
    balance = decimal.Parse(Console.ReadLine());
    if(balance <= 0)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Ingresa la información del usuario:");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Nombre: " + name);
            Console.WriteLine("Email: " + email);
            Console.WriteLine("El saldo que ingreso no es valido, por favor ingrese un nuevo saldo.");
            Console.Write("Saldo: ");
            balance = decimal.Parse(Console.ReadLine());
        } while(balance <= 0);
    }
    Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
    userType = char.Parse(Console.ReadLine());
    if(userType != 'c' && userType != 'e')
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Ingresa la información del usuario:");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Nombre: " + name);
            Console.WriteLine("Email: " + email);
            Console.WriteLine("Saldo: " + balance);
            Console.WriteLine("Por ingrese un caracter valido.");
            Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
            userType = char.Parse(Console.ReadLine());
        } while(userType != 'c' && userType != 'e');
    }
    if(userType.Equals('c'))
    {
        Console.Write("Regimen Fiscal: ");
        char taxRegime = char.Parse(Console.ReadLine());
        newUser = new Client(ID, name, email, balance, taxRegime);
    }
    else
    {
        Console.Write("Departamento: ");
        string department = Console.ReadLine();
        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser()
{
    Console.Clear();
    Console.Write("Ingresa el ID del usuario a eliminar: ");
    int ID = int.Parse(Console.ReadLine());
    if(ValidarID(ID))
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Ingresa la información del usuario:");
            Console.WriteLine("El ID que ingreso no existe, por favor ingrese otro ID.");
            Console.Write("ID: ");
            ID = int.Parse(Console.ReadLine());
        } while(ValidarID(ID));
    }
    string result = Storage.DeleteUser(ID);

    if(result.Equals("Success"))
    {
        Console.WriteLine("Usuario Eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }
}

bool ValidarID(int ID)
{
    bool IDExists = false, valID = false;
    List<User> Users = Storage.GetUsers();
    
    if(ID > 0)
    {
        foreach (User user in Users)
            if(ID.Equals(user.GetID()))
            {
                IDExists = true;
            }
        if(IDExists)
            valID = false;
        else
            valID = true;
    }
    else
        valID = false;
    return valID;
}

bool ValidarEmail(string email)
{
    bool valEmail = false;
    string expresion;
    expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    if(Regex.IsMatch(email, expresion))
    {
        if(Regex.Replace(email, expresion, String.Empty).Length == 0)
            valEmail = true;
        else
            valEmail = false; 
    }
    else
        valEmail = false;
    return valEmail;
}