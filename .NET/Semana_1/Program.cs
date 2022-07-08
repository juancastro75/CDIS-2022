using System;

namespace Semana_1
{
    public class Program
    {
        static int op, nretiros, c;
        static int[] cantidades = new int[' '];
        public static void Main(string[] args)
        {
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("----------------------- Banco CDIS -----------------------\n");
                    Console.WriteLine("1.- Ingresar cuantos retiros y la cantidad retirada");
                    Console.WriteLine("2.- Revisar la cantidad entregada de billetes y monedas");
                    Console.WriteLine("3.- Salir");
                    Console.WriteLine("\nIngrese la opcion deseada:");
                    op = int.Parse(Console.ReadLine());
                } while (op < 1 || op > 3);
                switch (op)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("----------------------- Banco CDIS -----------------------\n");
                            Console.WriteLine("Cuantos retiros va a hacer (maximo 10): ");
                            nretiros = int.Parse(Console.ReadLine());
                        }while(nretiros < 1 || nretiros > 10);
                        cantidades = Retirar(nretiros, cantidades);
                        Console.WriteLine("Presione cualquier tecla para continuar....");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("----------------------- Banco CDIS -----------------------\n");
                        for(c = 1; c <= nretiros; c++)
                        {
                            Console.WriteLine("Retiro No." + c);
                            Revisar(cantidades[c]);
                        }
                        Console.WriteLine("Presione cualquier tecla para continuar....");
                        Console.ReadKey();
                        break;
                }
            } while (op != 3);
        }

        public static int[] Retirar(int n, int[] retiros)
        {
            int i;
            for (i = 1; i <= n; i++)
            {
                Console.WriteLine("Ingresa la cantidad (solo cantidades enteras) del retiro No." + i + ": ");
                retiros[i] = int.Parse(Console.ReadLine());
            }
            return retiros;
        }

        public static void Revisar(int cantidad)
        {
            int billetes, monedas;
            billetes = 0;
            monedas = 0; 
            do
            {
                if(cantidad >= 500)
                {
                    billetes += cantidad / 500;
                    cantidad = cantidad % 500;
                }
                else 
                {
                    if(cantidad >= 200)
                    {
                        billetes += cantidad / 200;
                        cantidad = cantidad % 200;
                    }
                    else
                    {
                        if(cantidad >= 100)
                        {
                            billetes += cantidad / 100;
                            cantidad = cantidad % 100;
                        }
                        else
                        {
                            if(cantidad >= 50)
                            {
                                billetes += cantidad / 50;
                                cantidad = cantidad % 50;
                            }
                            else
                            {
                                if(cantidad >= 20)
                                {
                                    billetes += cantidad / 20;
                                    cantidad = cantidad % 20;
                                }
                                else
                                {
                                    if(cantidad >= 10)
                                    {
                                        monedas += cantidad / 10;
                                        cantidad = cantidad % 10;
                                    }
                                    else
                                    {
                                        if(cantidad >= 5)
                                        {
                                            monedas += cantidad / 5;
                                            cantidad = cantidad % 5;
                                        }
                                        else 
                                        {
                                            if(cantidad >= 1)
                                            {
                                                monedas += cantidad / 1;
                                                cantidad = cantidad % 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }while(cantidad != 0);
            Console.WriteLine("Billetes entregados: " + billetes);
            Console.WriteLine("Monedas entregados: " + monedas);
        }
    }  
}