using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ascensor
{
    class Program
    {
        private static List<Ascensor> ascensors = new List<Ascensor>();
        internal static int posicionActual = 1;
        internal static int posicionfuturo = 0;
        internal static int nummiubicacion = 0;
        internal static bool primeravez = true;

        static void Main(string[] args)
        {
            ascensors.Add(new Ascensor { Piso = 1, MiUbicacion = false, Tiempo = 1, Estado = true });
            ascensors.Add(new Ascensor { Piso = 2, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 3, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 4, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 5, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 6, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 7, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 8, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 9, MiUbicacion = false, Tiempo = 1, Estado = false });
            ascensors.Add(new Ascensor { Piso = 10, MiUbicacion = false, Tiempo = 1, Estado = false });

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
                Random random = new Random();
                int num = random.Next(1, ascensors.Count);
                nummiubicacion = random.Next(1, ascensors.Count);

                ascensors.Where(x => x.Piso == nummiubicacion).ToList().ForEach(s => s.MiUbicacion = true);

                Imprimir(num);
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
                    posicionfuturo = num;
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
                    int contA = ascensors.Count;
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("--------- Mi Ascensor ---------");
                    Console.WriteLine("===============================");
                    foreach (var item in ascensors.OrderByDescending(x => x.Piso))
                    {
                        if (item.Estado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(Environment.NewLine);
                        Console.WriteLine(" -------");
                        Console.WriteLine("|       |");
                        Console.WriteLine($"|  P{Convert.ToString(contA).PadLeft(2, '0')}  |");
                        if (item.Estado && item.MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  <== [°]");
                            Console.WriteLine("|  | |  |");
                            Console.WriteLine(" __| |__");
                        }
                        else if (item.Estado && !item.MiUbicacion)
                        {
                            Console.WriteLine("|  ---  |  <==");
                            Console.WriteLine("|  | |  |");
                            Console.WriteLine(" __| |__");
                        }
                        else if (!item.Estado && item.MiUbicacion)
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

                        if (item.Estado)
                        {
                            Console.ResetColor();
                        }

                        contA = contA - 1;
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
                primeravez = false;
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
            //ascensors.Where(x => x.Piso == nummiubicacion).ToList().ForEach(s => s.MiUbicacion = true);

            foreach (var item in ascensors)
            {
                if (primeravez == false)
                {
                    if (posicionfuturo == nummiubicacion)
                    {
                        nummiubicacion = posicionfuturo;
                        item.MiUbicacion = true;
                    }

                }

                if (asignado == false)
                {
                    if (posicionActual < posicionfuturo)
                    {
                        if (item.Estado)
                        {
                            item.Estado = false;
                            if (primeravez == false)
                            {
                                item.MiUbicacion = false;
                            }

                            z++;
                        }

                        if (y == 2)
                        {
                            item.Estado = true;
                            if (primeravez == false)
                            {
                                item.MiUbicacion = true;
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

                        if (item.Piso == posicionActual - 1)
                        {
                            item.Estado = true;
                            if (primeravez == false)
                            {
                                item.MiUbicacion = true;
                            }
                            posicionActual = posicionActual - 1;
                            w++;
                        }

                        if (y == 2)
                        {
                            item.Estado = false;
                            if (primeravez == true)
                            {
                                item.MiUbicacion = false;
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