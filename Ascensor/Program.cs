using Ascensor.WebAPI.DTO.Entities;
using Ascensor.WebAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ascensor
{
    class Program
    {
        private static List<AscensorEntity> ascensors = new List<AscensorEntity>();
        private static List<AscensorEntity> totalPisos = new List<AscensorEntity>();
        internal static int posicionActual = 1;
        internal static int posicionfuturo = 0;
        internal static bool Asignado = false;
        internal static bool primeravez = true;

        static void Main(string[] args)
        {
            buildAscensor();
        }

        public static void buildAscensor()
        {
            try
            {
                dataEn();
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void dataEn()
        {
            try
            {
                totalPisos = new CallRestAPI().GetAll();
                Console.Write(Environment.NewLine);
                if (primeravez)
                {
                    Console.WriteLine("Total Pisos: " + totalPisos.Count);
                    primeravez = false;
                }
                Console.Write("Ingrese el numero Piso:");
                var pisoA = Convert.ToInt32(Console.ReadLine());
                posicionActual = pisoA;
                Console.Write("Ingrese el numero del Piso que deseas ir:");

                var pisoB = Convert.ToInt32(Console.ReadLine());
                posicionfuturo = pisoB;
                

                ascensors = new CallRestAPI().MoveFromInside(pisoA, pisoB);

                if (pisoA > totalPisos.Count || pisoA < 0 || pisoB > totalPisos.Count || pisoB <= 0 || pisoA==pisoB )
                {
                    Console.WriteLine("El piso ingresado no existe o estas en el mismo piso!!!");
                    Console.Write(Environment.NewLine);

                    Console.Write("¿Desea Continuar?");
                    Console.Write("Ingrese SI o NO:");

                    var confirm = Convert.ToString(Console.ReadLine());
                    if (confirm.ToUpper() == "SI")
                    {
                        dataEn();
                        primeravez = true;
                    }
                    else
                    {
                        Environment.Exit(0);// exit
                    }
                }
                else
                {
                    print(pisoB);
                    dataEn();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void print(int num)
        {
            try
            {
                int n = 1;
                int y = 1;
                //var ascensors2 = new CallRestAPI().ListOfPendingFloors(posicionActual, posicionfuturo);
                var currentFloor = new CallRestAPI().GetCurrentFloor();

                posicionfuturo = num;
                if (posicionActual <= posicionfuturo)
                {
                    y = y + posicionfuturo - posicionActual;
                }
                else
                {
                    y = y + posicionActual - posicionfuturo;
                    n = y;
                }

                for (int i = 1; i <= y; i++)
                {
                    Asignado = false;
                    int contA = 1;// ascensors.Count;
                    Thread.Sleep(1000); //Pisos por Segundo
                    Console.Clear();
                    Console.WriteLine("*********Total Pisos: " + totalPisos.Count+"******");
                    Console.WriteLine("===============================");
                    Console.WriteLine("--------- Mi Ascensor ---------");
                    Console.WriteLine("===============================");
                    foreach (var item in ascensors)
                    {
                        if (!Asignado)
                        {
                            if (item.Asce_OrdenR == n)
                            {
                                if (posicionActual < posicionfuturo)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    n++;
                                    Asignado = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    n--;
                                    Asignado = true;

                                }

                            }
                        }

                        Console.Write(Environment.NewLine);
                        Console.WriteLine(" -------");
                        Console.WriteLine("|       |");
                        Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                        if (item.Asce_Estado && item.Asce_MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  <==");
                            Console.WriteLine(" __| |__");
                        }

                        else
                        {
                            Console.WriteLine("|       |");
                            Console.WriteLine(" -------");
                        }

                        Console.ResetColor();

                        contA = contA + 1;

                    }
                }
                Console.WriteLine("El ascensor se encuentra en piso: " + currentFloor.FirstOrDefault().Asce_Piso);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}