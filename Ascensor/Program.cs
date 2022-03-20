using Ascensor.WebAPI.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ascensor
{
    class Program
    {
        private static List<AscensorEntity> ascensors = new List<AscensorEntity>();
        internal static int posicionActual = 1;
        internal static int posicionSubjetivp = 1;
        internal static int posicionfuturo = 0;
        internal static int nummiubicacion = 0;
        internal static bool primeravez = true;

        static void Main(string[] args)
        {
            ascensors = new CallRestAPI().GetAll();
            buildAscensor2();
        }

        public static void buildAscensor(bool showTime, decimal time)
        {
            int contA = 1;
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("--------- Mi Ascensor ---------");
            Console.WriteLine("===============================");

            foreach (var item in ascensors)
            {
                if (item.Asce_Estado)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    time = contA - 1;
                }

                Console.Write(Environment.NewLine);
                Console.WriteLine(" -------");
                Console.WriteLine("|       |");
                Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                if (item.Asce_Estado)
                {
                    Console.WriteLine("|  ---  |  <==");
                    Console.WriteLine("|  | |  |");
                    Console.WriteLine(" __| |__");
                }
                else
                {
                    Console.WriteLine("|       |");
                    Console.WriteLine("|       |");
                    Console.WriteLine(" -------");
                }

                if (item.Asce_Estado)
                {
                    Console.ResetColor();
                }

                contA = contA + 1;
            }

            Console.Write(Environment.NewLine);
            if (showTime)
            {
                var seconds = (time == 1) ? "un segundo" : "segundos";
                Console.WriteLine($"El tiempo recorrido entre los pisos es de: {time} {seconds}!!!");
            }

            Console.Write("Ingrese el número del Piso :");

            var input = Convert.ToInt32(Console.ReadLine());
            if (input > ascensors.Count || input <= 0 || ascensors.Where(x => x.Asce_Piso == input && x.Asce_Estado == true).Any())
            {
                Console.WriteLine("El piso ingresado no es válido!!!");
                Console.Write(Environment.NewLine);

                Console.Write("¿Desea Continuar?");
                Console.Write("Ingrese SI o NO:");

                var confirm = Convert.ToString(Console.ReadLine());
                if (confirm.ToUpper() == "SI")
                {
                    buildAscensor(false, 0);
                }
                else
                {
                    Environment.Exit(0);// exit
                }
            }
            else
            {
                int cont = 1;
                decimal seconds = 0;
                foreach (var item in ascensors)
                {
                    item.Asce_Estado = false;
                    if (item.Asce_Piso == input)
                    {
                        item.Asce_Estado = true;
                    }

                    if (cont == item.Asce_Piso)
                    {
                        seconds = seconds + item.Asce_Tiempo;
                    }

                    cont = cont + 1;
                }

                buildAscensor(true, seconds);
            }
        }

        public static void buildAscensor2()
        {
            try
            {
                Random random = new Random();
                int num = 1;//random.Next(1, ascensors.Count);
                nummiubicacion = 2;// random.Next(1, ascensors.Count);
               // posicionfuturo = num;
                ascensors.Where(x => x.Asce_Piso == num).ToList().ForEach(s => s.Asce_Estado = true);
                ascensors.Where(x => x.Asce_Piso == nummiubicacion).ToList().ForEach(s => s.Asce_MiUbicacion = true);

                Imprimir(num);
                InData();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Imprimir(int num)
        {
            try
            {
                int n = 1;
                int y = 1;
                
                
                posicionfuturo = num;
                if (posicionActual <= posicionfuturo)
                {
                  y = posicionfuturo - posicionActual;
                }
                else
                {
                  y = posicionActual - posicionfuturo;
                }
                
                for (int i = 0; i < y; i++)
                {
                    if (posicionfuturo > 0)
                    {
                        cambioEstado(0);
                    }
                    int contA = ascensors.Count;
                    //Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("--------- Mi Ascensor ---------");
                    Console.WriteLine("===============================");
                    foreach (var item in ascensors.OrderByDescending(x => x.Asce_Piso))
                    {
                        if (item.Asce_Estado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(Environment.NewLine);
                        Console.WriteLine(" -------");
                        Console.WriteLine("|       |");
                        Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                        if (item.Asce_Estado && item.Asce_MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  <== [°]");
                            Console.WriteLine("|  | |  |");
                            Console.WriteLine(" __| |__");
                        }
                        else if (item.Asce_Estado && !item.Asce_MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  <==");
                            Console.WriteLine("|  | |  |");
                            Console.WriteLine(" __| |__");
                        }
                        else if (!item.Asce_Estado && item.Asce_MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  [°]");
                            Console.WriteLine("|  | |  |");
                            Console.WriteLine(" __| |__");
                        }
                        else
                        {
                            Console.WriteLine("|       |");
                            Console.WriteLine("|       |");
                            Console.WriteLine(" -------");
                        }

                        if (item.Asce_Estado)
                        {
                            Console.ResetColor();
                        }

                        contA = contA - 1;
                    }
                    n++;


                }
                
                if (num == 0)
                {
                    posicionActual = 1;
                    posicionSubjetivp = posicionActual;
                }
                else
                {
                    if (posicionActual == nummiubicacion)
                    {
                        nummiubicacion = posicionfuturo;
                    }
                    posicionActual = posicionfuturo;
                    posicionSubjetivp = posicionActual;
                }
                Console.WriteLine("El ascensor esta en piso: " + posicionActual);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InData()
        {
            try
            {
                Console.Write(Environment.NewLine);

                Console.Write("Ingrese el número del Piso :");

                var input = Convert.ToInt32(Console.ReadLine());
                posicionfuturo = input;
                primeravez = false;
                if (input > ascensors.Count || input <= 0 || ascensors.Where(x => x.Asce_Piso == input && x.Asce_Estado == true).Any())
                {
                    Console.WriteLine("El piso ingresado no es válido!!!");
                    Console.Write(Environment.NewLine);

                    Console.Write("¿Desea Continuar?");
                    Console.Write("Ingrese SI o NO:");

                    var confirm = Convert.ToString(Console.ReadLine());
                    if (confirm.ToUpper() == "SI")
                    {
                        Imprimir(input);
                        InData();
                    }
                    else
                    {
                        Environment.Exit(0);// exit
                    }
                }
                else
                {
                    Imprimir(input);
                    InData();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void cambioEstado(int cn)
        {
            int y = 1;
            int z = 1;
            int w = 1;
            int cont = 1;
            bool asignado = false;
           
            foreach (var item in ascensors)
            {

                if (asignado == false)
                {
                    if (posicionActual < posicionfuturo)
                    {
                        if (item.Asce_Estado)
                        {
                            item.Asce_Estado = false;
                            if (primeravez == false)
                            {
                                if (posicionActual == nummiubicacion)
                                {
                                    item.Asce_MiUbicacion = false;
                                }                                   
                            }
                            z++;
                        }
                        if (y == 2)
                        {
                            item.Asce_Estado = true;
                            if (primeravez == false)
                            {
                                if (posicionActual == nummiubicacion)
                                {
                                    item.Asce_MiUbicacion = true;
                                }

                            }
                            asignado = true;
                        }
                        if (z == 2)
                        {
                            y = 2;
                            z = 5;
                        }
                        else
                        {
                            y = 1;
                        }
                    }
                    else
                    {
                        if (item.Asce_Piso == posicionSubjetivp - 1)
                        {
                            item.Asce_Estado = true;
                            if (primeravez == false)
                            {
                                if (posicionActual - 1 == nummiubicacion - 1)
                                {
                                    item.Asce_MiUbicacion = true;
                                }

                            }
                            posicionSubjetivp = posicionSubjetivp - 1;
                            //nummiubicacion = nummiubicacion - 1;
                            w++;
                        }

                        if (y == 2)
                        {
                            item.Asce_Estado = false;
                            if (primeravez == false)
                            {

                                if (posicionActual - 1 == nummiubicacion - 1)
                                {
                                    item.Asce_MiUbicacion = false;
                                }
                            }
                            asignado = true;
                        }
                        if (w == 2)
                        {
                            y = 2;
                            w = 5;
                        }
                        else
                        {
                            y = 1;
                        }
                    }
                }
                cont++;
            }

        }
    }
}