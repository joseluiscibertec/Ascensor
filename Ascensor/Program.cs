using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ascensor
{
    class Program
    {
        private static List<Ascensor> ascensors = new List<Ascensor>();
        internal static int posicionActual;
        internal static int posicionfuturo;
        static void Main(string[] args)
        {
            ascensors.Add(new Ascensor { Piso = 1, Nombre = null, Tiempo = 1, Estado = true });
            ascensors.Add(new Ascensor { Piso = 2, Nombre = null, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 3, Nombre = null, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 4, Nombre = null, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 5, Nombre = null, Tiempo = 1, Estado = false });
            //ascensors.Add(new Ascensor { Piso = 6, Nombre = null, Tiempo = 1, Estado = false });
            //ascensors.Add(new Ascensor { Piso = 7, Nombre = null, Tiempo = 1, Estado = false });
            //ascensors.Add(new Ascensor { Piso = 8, Nombre = null, Tiempo = 1, Estado = false });
            //ascensors.Add(new Ascensor { Piso = 9, Nombre = null, Tiempo = 1, Estado = false });
            //ascensors.Add(new Ascensor { Piso = 10, Nombre = null, Tiempo = 1, Estado = false });

            buildAscensor2();
        }

        public static void buildAscensor(bool showTime, float time)
        {
            int contA = 1;
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("--------- Mi Ascensor ---------");
            Console.WriteLine("===============================");

            foreach (var item in ascensors)
            {
                if (item.Estado)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    time = contA - 1;
                }

                Console.Write(Environment.NewLine);
                Console.WriteLine(" -------");
                Console.WriteLine("|       |");
                Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                if (item.Estado)
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

                if (item.Estado)
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
            if (input > ascensors.Count || input <= 0 || ascensors.Where(x => x.Piso == input && x.Estado == true).Any())
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
                float seconds = 0;
                foreach (var item in ascensors)
                {
                    item.Estado = false;
                    if (item.Piso == input)
                    {
                        item.Estado = true;
                    }

                    if (cont == item.Piso)
                    {
                        seconds = seconds + item.Tiempo;
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
                Imprimir(0);
                InData();
            }
            catch (Exception ex)
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
                if (num != 0)
                {
                    if (posicionActual <= posicionfuturo)
                    {
                        y = posicionfuturo - posicionActual;
                    }
                    else
                    {
                        y = posicionActual - posicionfuturo;
                    }
                }
                for (int i = 0; i < y; i++)
                {
                    if (posicionfuturo > 0)
                    {
                        cambioEstado(0);
                    }
                    int contA = 1;
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("--------- Mi Ascensor ---------");
                    Console.WriteLine("===============================");
                    foreach (var item in ascensors)
                    {
                        if (item.Estado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(Environment.NewLine);
                        Console.WriteLine(" -------");
                        Console.WriteLine("|       |");
                        Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                        if (item.Estado)
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

                        if (item.Estado)
                        {
                            Console.ResetColor();
                        }

                        contA = contA + 1;
                    }
                    n++;


                }
                //Obtener posicion actual de ascensor
                if (num == 0)
                {
                    posicionActual = 1;
                }
                else
                {
                    posicionActual = posicionfuturo;
                }
                Console.WriteLine("El ascensor esta en piso: " + posicionActual);
            }
            catch (Exception ex)
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
                if (input > ascensors.Count || input <= 0 || ascensors.Where(x => x.Piso == input && x.Estado == true).Any())
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
            catch (Exception ex)
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
                        if (item.Estado)
                        {
                            item.Estado = false;
                            z++;
                        }

                        if (y == 2)
                        {
                            item.Estado = true;
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

                        if (item.Piso == posicionActual - 1)
                        {
                            item.Estado = true;
                            posicionActual = posicionActual - 1;
                            w++;
                        }

                        if (y == 2)
                        {
                            item.Estado = false;
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