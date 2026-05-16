using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                int opciones = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("QUE PROGRAMA DESEA INICIAR?\n" +
                    "1--MENU DE CONTROL DE TURNOS DE IPS\n" +
                    "2--MENU ACADÉMICO\n" +
                    "3--MENU DE OPERARIOS DE FABRICA");
                    Console.WriteLine("0--SALIR\n");
                    opciones = int.Parse(Console.ReadLine());
                    switch (opciones)
                    {
                        case 1:
                            Console.Clear();
                            MenuIPS();
                            break;
                        case 2:
                            Console.Clear();
                            MenuAcademico();
                            break;
                        case 3:
                            Console.Clear();
                            MenuFabrica();
                            break;
                    }
                    Console.ReadKey();
                } while (opciones != 0);
            }
        }

        // FUNCION PRINCIPAL DEL SISTEMA DE CONTROL DE TURNOS PARA UNA IPS
        static void MenuIPS()
        {

            int[] ndocumento = new int[30];
            string[] nombre = new string[30];
            int[] edad = new int[30];
            string[] tipoConsulta = new string[30];
            int[] nivelPrioridad = new int[30];
            int[] turno = new int[30];
            int c = 0;
            int urgencia = 0;
            int cGeneral = 0;
            int prioritaria = 0;

            IngresarPacientes(
                ndocumento,
                nombre,
                edad,
                tipoConsulta,
                nivelPrioridad,
                ref c,
                ref urgencia,
                ref cGeneral,
                ref prioritaria);
            int opcion = 0;

            do
            {
                Console.WriteLine("\nQUE DESEA HACER?");
                Console.WriteLine("1--LISTA GENERAL");
                Console.WriteLine("2--BUSCAR PACIENTE");
                Console.WriteLine("3--PACIENTES POR TIPO");
                Console.WriteLine("4--PACIENTE PRIORITARIO\n");
                Console.WriteLine("0--SALIR AL MENU PRINCIPAL\n");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        MostrarLista(
                            ndocumento,
                            nombre,
                            edad,
                            turno,
                            tipoConsulta,
                            nivelPrioridad,
                            c);
                        break;
                    case 2:
                        BuscarPaciente(
                            ndocumento,
                            nombre,
                            edad,
                            tipoConsulta,
                            nivelPrioridad,
                            c);
                        break;
                    case 3:
                        MostrarCantidadTipos(
                            urgencia,
                            cGeneral,
                            prioritaria);
                        break;
                    case 4:
                        MostrarPacientePrioritario(
                            ndocumento,
                            nombre,
                            edad,
                            tipoConsulta,
                            nivelPrioridad,
                            c);
                        break;
                }
            } while (opcion != 0);
        }
        static void IngresarPacientes(
            int[] ndocumento,
            string[] nombre,
            int[] edad,
            string[] tipoConsulta,
            int[] nivelPrioridad,
            ref int c,
            ref int urgencia,
            ref int cGeneral,
            ref int prioritaria)
        {
            int continuar = 1;

            Console.WriteLine("-----------------------------------------\n"
                    + "SISTEMA DE CONTROL DE TURNOS PARA UNA IPS\n-----------------------------------------");
            Console.WriteLine("\nIngrese los datos de los pacientes");
            while (continuar == 1)
            {
                // VALIDACION DE CUPOS
                if (c >= 30)
                {
                    Console.WriteLine("\nNO HAY CUPOS DISPONIBLES.");
                    Console.WriteLine("La IPS alcanzó el limite de 30 pacientes.\n");
                    break;
                }
                Console.WriteLine($"\nPaciente #{c + 1}");

                Console.Write("Documento: ");
                ndocumento[c] = int.Parse(Console.ReadLine());

                Console.Write("Nombre: ");
                nombre[c] = Console.ReadLine().ToUpper();

                Console.Write("Edad: ");
                edad[c] = int.Parse(Console.ReadLine());

                Console.Write("Tipo de atencion (urgencias, consulta general, prioritaria): ");
                tipoConsulta[c] = Console.ReadLine().ToUpper();

                if (tipoConsulta[c] == "URGENCIAS")
                    urgencia++;

                else if (tipoConsulta[c] == "CONSULTA GENERAL")
                    cGeneral++;

                else if (tipoConsulta[c] == "PRIORITARIA")
                    prioritaria++;

                do
                {
                    Console.Write("Nivel prioridad (1-5): ");
                    nivelPrioridad[c] = int.Parse(Console.ReadLine());

                } while (nivelPrioridad[c] < 1 || nivelPrioridad[c] > 5);

                c++;

                // VALIDAR SI TODAVIA HAY ESPACIO
                if (c < 30)
                {
                    Console.Write("Desea continuar? (1=SI, 0=NO): ");
                    continuar = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("\nCUPOS LLENOS.");
                }
            }
        }
        static void MostrarLista(
            int[] ndocumento,
            string[] nombre,
            int[] edad,
            int[] turno,
            string[] tipoConsulta,
            int[] nivelPrioridad,
            int c)
        {
            Console.WriteLine("\nLISTA GENERAL DE PACIENTES\n");

            Console.WriteLine(
                "TURNO".PadRight(10) +
                "DOCUMENTO".PadRight(20) +
                "NOMBRE".PadRight(35) +
                "EDAD".PadRight(10) +
                "TIPO DE ATENCION".PadRight(25) +
                "NIVEL PRIORIDAD");

            Console.WriteLine(new string('-', 100));

            for (int i = 0; i < c; i++)
            {
                turno[i] = i + 1;
                Console.WriteLine(
                    turno[i].ToString().PadRight(10) +
                    ndocumento[i].ToString().PadRight(20) +
                    nombre[i].PadRight(35) +
                    edad[i].ToString().PadRight(10) +
                    tipoConsulta[i].PadRight(25) +
                    nivelPrioridad[i]);
            }
        }
        static void BuscarPaciente(
            int[] ndocumento,
            string[] nombre,
            int[] edad,
            string[] tipoConsulta,
            int[] nivelPrioridad,
            int c)
        {
            Console.Write("\nDocumento a buscar: ");
            int id = int.Parse(Console.ReadLine());

            bool encontrado = false;
            int posicion = 0;

            for (int i = 0; i < c; i++)
            {
                if (ndocumento[i] == id)
                {
                    encontrado = true;
                    posicion = i;
                    break;
                }
            }

            if (encontrado)
            {
                Console.WriteLine("\nPACIENTE ENCONTRADO\n");

                Console.WriteLine("Documento: " + ndocumento[posicion]);
                Console.WriteLine("Nombre: " + nombre[posicion]);
                Console.WriteLine("Edad: " + edad[posicion]);
                Console.WriteLine("Tipo: " + tipoConsulta[posicion]);
                Console.WriteLine("Prioridad: " + nivelPrioridad[posicion]);
            }
            else
            {
                Console.WriteLine("Paciente no encontrado");
            }
        }
        static void MostrarCantidadTipos(
            int urgencia,
            int cGeneral,
            int prioritaria)
        {
            Console.WriteLine("\nPACIENTES POR TIPO\n");

            Console.WriteLine("Urgencias: " + urgencia);
            Console.WriteLine("Consulta General: " + cGeneral);
            Console.WriteLine("Prioritaria: " + prioritaria);
        }
        static void MostrarPacientePrioritario(
            int[] ndocumento,
            string[] nombre,
            int[] edad,
            string[] tipoConsulta,
            int[] nivelPrioridad,
            int c)
        {
            int mayor = nivelPrioridad[0];
            int p = 0;

            for (int i = 1; i < c; i++)
            {
                if (nivelPrioridad[i] > mayor)
                {
                    mayor = nivelPrioridad[i];
                    p = i;
                }
            }

            Console.WriteLine("\nPACIENTE PRIORITARIO\n");

            Console.WriteLine("Documento: " + ndocumento[p]);
            Console.WriteLine("Nombre: " + nombre[p]);
            Console.WriteLine("Edad: " + edad[p]);
            Console.WriteLine("Tipo: " + tipoConsulta[p]);
            Console.WriteLine("Nivel prioridad: " + nivelPrioridad[p]);
        }

        //FUNCION PRINCIPAL DEL SISTEMA DE CONTROL ACADEMICO DE SEGUIMIENTO DE NOTAS
        static void MenuAcademico()
        {
            string[] codigo = new string[25];
            string[] nombre = new string[25];
            double[,] notas = new double[25, 3];
            double[] definitiva = new double[25];
            string[] estado = new string[25];
            double promGrupo = 0;
            string[] asistencia = new string[25];

            int continuar = 1;
            int i = 0;
            double suma = 0;
            double sumDef = 0;
            int opciones = 0;

            IngresarEstudiantes(
                codigo,
                nombre,
                notas,
                asistencia,
                ref continuar,
                ref i);

            CalcularDefinitiva(
                notas,
                definitiva,
                ref i,
                ref suma,
                ref sumDef,
                ref promGrupo);

            do
            {
                Console.WriteLine("\nQUE DESEA HACER?");
                Console.WriteLine("1--PLANILLA ACADEMICA");
                Console.WriteLine("2--ESTADO DE ESTUDIANTES");
                Console.WriteLine("3--PROMEDIO GENERAL DEL GRUPO");
                Console.WriteLine("4--PROMEDIO SOBRESALIENTE/BAJO\n");
                Console.WriteLine("0--SALIR AL MENU PRINCIPAL\n");
                opciones = int.Parse(Console.ReadLine());

                switch (opciones)
                {
                    case 1:
                        MostrarDatos(
                            notas,
                            definitiva,
                            codigo,
                            nombre,
                            asistencia,
                            promGrupo,
                            ref i);
                        break;
                    case 2:
                        MostrarEstado(
                            codigo,
                            nombre,
                            definitiva,
                            estado,
                            ref i);
                        break;
                    case 3:
                        MostrarPromedioGrupo(promGrupo);
                        break;
                    case 4:
                        MostrarMejorYPeorPromedio(
                            codigo,
                            nombre,
                            definitiva,
                            ref i);
                        break;
                }
            } while (opciones != 0);
        }
        static void IngresarEstudiantes(
            string[] codigo,
            string[] nombre,
            double[,] notas,
            string[] asistencia,
            ref int continuar,
            ref int i)
        {
            Console.WriteLine(
                new string('-', 55) +
                "\nSISTEMA DE CONTROL ACADEMICO DE SEGUIMIENTO DE NOTAS\n" +
                new string('-', 55));

            Console.WriteLine("\nIngrese los datos de los estudiantes: ");

            while (continuar == 1 && i < 25)
            {
                Console.WriteLine($"\nEstudiante #{i + 1}");

                codigo[i] = (i + 1).ToString("D3");

                Console.WriteLine($"Codigo de estudiante: {codigo[i]}");

                Console.Write("Ingrese el nombre del estudiante: ");
                nombre[i] = Console.ReadLine().ToUpper();

                Console.WriteLine("Ingrese las 3 notas parciales del estudiante (0.0 - 5.0): ");

                for (int c = 0; c < 3; c++)
                {
                    Console.Write($"Nota #{c + 1}: ");

                    notas[i, c] = Convert.ToDouble(Console.ReadLine());

                    while (notas[i, c] < 0 || notas[i, c] > 5)
                    {
                        Console.WriteLine("Nota inválida, ingrese una nota válida:");
                        notas[i, c] = Convert.ToDouble(Console.ReadLine());
                    }
                }

                Console.Write("Ingrese el porcentaje de asistencia del estudiante: ");
                asistencia[i] = Console.ReadLine();

                i++;

                Console.Write("Desea ingresar más estudiantes? (1--SI, 0--NO): ");
                continuar = int.Parse(Console.ReadLine());
            }
        }
        static void CalcularDefinitiva(
            double[,] notas,
            double[] definitiva,
            ref int i,
            ref double suma,
            ref double sumDef,
            ref double promGrupo)
        {
            sumDef = 0;

            for (int f = 0; f < i; f++)
            {
                suma = 0;

                for (int c = 0; c < 3; c++)
                {
                    suma += notas[f, c];
                }

                definitiva[f] = suma / 3;

                sumDef += definitiva[f];
            }

            promGrupo = sumDef / i;
        }
        static void MostrarDatos(
            double[,] notas,
            double[] definitiva,
            string[] codigo,
            string[] nombre,
            string[] asistencia,
            double promGrupo,
            ref int i)
        {
            Console.WriteLine("\nLISTA DE ESTUDIANTES\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "ESTUDIANTE".PadRight(25) +
                "NOTA #1".PadRight(10) +
                "NOTA #2".PadRight(10) +
                "NOTA #3".PadRight(10) +
                "DEFINITIVA".PadRight(15) +
                "ASISTENCIA");

            Console.WriteLine(new string('-', 100));

            for (int n = 0; n < i; n++)
            {
                Console.Write(
                    codigo[n].PadRight(15) +
                    nombre[n].PadRight(25));

                for (int c = 0; c < 3; c++)
                {
                    Console.Write(notas[n, c].ToString("F1").PadRight(10));
                }

                Console.WriteLine(
                    definitiva[n].ToString("F2").PadRight(15) +
                    asistencia[n]);
            }
        }
        static void MostrarEstado(
            string[] codigo,
            string[] nombre,
            double[] definitiva,
            string[] estado,
            ref int i)
        {
            Console.WriteLine("\nESTADO DEL ESTUDIANTE\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "ESTUDIANTE".PadRight(25) +
                "DEFINITIVA".PadRight(15) +
                "ESTADO");

            Console.WriteLine(new string('-', 100));

            for (int n = 0; n < i; n++)
            {
                if (definitiva[n] < 2.5)
                {
                    estado[n] = "REPRUEBA";
                }
                else if (definitiva[n] >= 2.5 && definitiva[n] < 3)
                {
                    estado[n] = "HABILITA";
                }
                else
                {
                    estado[n] = "APROBADO";
                }

                Console.WriteLine(
                    codigo[n].PadRight(15) +
                    nombre[n].PadRight(25) +
                    definitiva[n].ToString("F2").PadRight(15) +
                    estado[n]);
            }
        }
        static void MostrarPromedioGrupo(double promGrupo)
        {
            Console.WriteLine($"\nEl promedio general del grupo es: {promGrupo:F2}");
        }
        static void MostrarMejorYPeorPromedio(
            string[] codigo,
            string[] nombre,
            double[] definitiva,
            ref int i)
        {
            double mayor = 0;
            double menor = 5;

            for (int n = 0; n < i; n++)
            {
                if (definitiva[n] > mayor)
                {
                    mayor = definitiva[n];
                }

                if (definitiva[n] < menor)
                {
                    menor = definitiva[n];
                }
            }

            Console.WriteLine("\nESTUDIANTE(S) CON EL MAYOR PROMEDIO\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "ESTUDIANTE".PadRight(25) +
                "DEFINITIVA");

            Console.WriteLine(new string('-', 60));

            for (int p = 0; p < i; p++)
            {
                if (definitiva[p] == mayor)
                {
                    Console.WriteLine(
                        codigo[p].PadRight(15) +
                        nombre[p].PadRight(25) +
                        definitiva[p].ToString("F2"));
                }
            }

            Console.WriteLine("\nESTUDIANTE(S) CON EL MENOR PROMEDIO\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "ESTUDIANTE".PadRight(25) +
                "DEFINITIVA");

            Console.WriteLine(new string('-', 60));

            for (int p = 0; p < i; p++)
            {
                if (definitiva[p] == menor)
                {
                    Console.WriteLine(
                        codigo[p].PadRight(15) +
                        nombre[p].PadRight(25) +
                        definitiva[p].ToString("F2"));
                }
            }
        }
        //SISTEMA DE CONTROL DE PRODUCCION PARA UNA FÁBRICA DE EMPAQUES
        static void MenuFabrica()
        {
            string[] codigo = new string[10];
            string[] nombre = new string[10];

            double[,] producDiaria = new double[10, 5];
            double[] producSemanal = new double[10];
            double[] promSemanal = new double[10];

            int opciones = 0;
            int i = 0;
            int continuar = 1;

            double mejorOp = 0;
            double promGeneral = 0;
            double menorProd = 0;
            double sumDiario = 0;

            IngresarOP(
                ref i,
                ref continuar,
                codigo,
                nombre,
                producDiaria);

            ProduccionSemanal(
                producSemanal,
                producDiaria,
                promSemanal,
                ref sumDiario,
                ref i);

            PromedioGeneral(
                producSemanal,
                ref promGeneral,
                ref i);

            do
            {
                Console.WriteLine("\nQUE DESEA HACER?");
                Console.WriteLine("1--PLANILLA DE OPERARIOS");
                Console.WriteLine("2--PRODUCCION SEMANAL DE CADA OP");
                Console.WriteLine("3--MEJOR OP DE LA SEMANA");
                Console.WriteLine("4--PROMEDIO DE PRODUCCION");
                Console.WriteLine("5--DIAS DE BAJA PRODUCCION");
                Console.WriteLine("0--SALIR AL MENU PRINCIPAL\n");

                opciones = int.Parse(Console.ReadLine());

                switch (opciones)
                {
                    case 1:
                        MostrarOperarios(
                            ref i,
                            codigo,
                            nombre,
                            producDiaria);
                        break;
                    case 2:
                        MostrarProduccionSemanal(
                            nombre,
                            codigo,
                            producSemanal,
                            ref i);
                        break;
                    case 3:
                        MejorOP(
                            codigo,
                            nombre,
                            producSemanal,
                            producDiaria,
                            ref mejorOp,
                            ref i);
                        break;
                    case 4:
                        MostrarPromedioGeneral(ref promGeneral);
                        break;
                    case 5:
                        MediaDeProduccion(
                            producDiaria,
                            ref menorProd,
                            ref i);
                        break;
                }
            } while (opciones != 0);
        }
        static void IngresarOP(
            ref int i,
            ref int continuar,
            string[] codigo,
            string[] nombre,
            double[,] producDiaria)
        {
            Console.WriteLine(
                new string('-', 55) +
                "\nSISTEMA DE CONTROL DE PRODUCCION PARA UNA FÁBRICA DE EMPAQUES\n" +
                new string('-', 55));

            Console.WriteLine("//La produccion promedio diaria de un operario es de 240");

            while (continuar == 1 && i < 10)
            {
                Console.WriteLine($"\nIngrese los datos del operario #{i + 1}");

                codigo[i] = (i + 1).ToString("D3");

                Console.WriteLine($"Codigo: {codigo[i]}");

                Console.Write("Nombre del operario: ");
                nombre[i] = Console.ReadLine();

                Console.WriteLine("Ingrese los datos de producción");

                for (int b = 0; b < 5; b++)
                {
                    Console.Write($"Dia #{b + 1}: ");

                    producDiaria[i, b] = Convert.ToDouble(Console.ReadLine());

                    while (producDiaria[i, b] < 0)
                    {
                        Console.Write("Valor inválido, ingrese nuevamente: ");
                        producDiaria[i, b] = Convert.ToDouble(Console.ReadLine());
                    }
                }

                i++;

                Console.Write("\nDesea ingresar más operarios? (1--SI, 0--NO): ");
                continuar = int.Parse(Console.ReadLine());
            }
        }
        static void ProduccionSemanal(
            double[] producSemanal,
            double[,] producDiaria,
            double[] promSemanal,
            ref double sumDiario,
            ref int i)
        {
            for (int a = 0; a < i; a++)
            {
                sumDiario = 0;

                for (int b = 0; b < 5; b++)
                {
                    sumDiario += producDiaria[a, b];
                }

                producSemanal[a] = sumDiario;
                promSemanal[a] = sumDiario / 5;
            }
        }
        static void PromedioGeneral(
            double[] producSemanal,
            ref double promGeneral,
            ref int i)
        {
            double sumGeneral = 0;

            for (int a = 0; a < i; a++)
            {
                sumGeneral += producSemanal[a];
            }

            promGeneral = sumGeneral / i;
        }
        static void MejorOP(
            string[] codigo,
            string[] nombre,
            double[] producSemanal,
            double[,] producDiaria,
            ref double mejorOp,
            ref int i)
        {
            mejorOp = producSemanal[0];

            for (int a = 1; a < i; a++)
            {
                if (producSemanal[a] > mejorOp)
                {
                    mejorOp = producSemanal[a];
                }
            }

            Console.WriteLine("\nMEJOR OPERARIO SEMANAL\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "NOMBRE".PadRight(25) +
                "DIA #1".PadRight(10) +
                "DIA #2".PadRight(10) +
                "DIA #3".PadRight(10) +
                "DIA #4".PadRight(10) +
                "DIA #5".PadRight(10) +
                "SEMANAL");

            Console.WriteLine(new string('-', 100));

            for (int p = 0; p < i; p++)
            {
                if (producSemanal[p] == mejorOp)
                {
                    Console.Write(
                        codigo[p].PadRight(15) +
                        nombre[p].PadRight(25));
                    for (int c = 0; c < 5; c++)
                    {
                        Console.Write(
                            producDiaria[p, c].ToString("F0").PadRight(10));
                    }
                    Console.WriteLine(producSemanal[p].ToString("F2"));
                }
            }
        }
        static void MostrarOperarios(
            ref int i,
            string[] codigo,
            string[] nombre,
            double[,] producDiaria)
        {
            Console.WriteLine("\nLISTA DE OPERARIOS\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "NOMBRE".PadRight(25) +
                "DIA #1".PadRight(10) +
                "DIA #2".PadRight(10) +
                "DIA #3".PadRight(10) +
                "DIA #4".PadRight(10) +
                "DIA #5".PadRight(10));

            Console.WriteLine(new string('-', 100));

            for (int n = 0; n < i; n++)
            {
                Console.Write(
                    codigo[n].PadRight(15) +
                    nombre[n].PadRight(25));

                for (int p = 0; p < 5; p++)
                {
                    Console.Write(
                        producDiaria[n, p].ToString("F0").PadRight(10));
                }

                Console.WriteLine();
            }
        }
        static void MediaDeProduccion(
            double[,] producDiaria,
            ref double menorProd,
            ref int i)
        {
            bool bajaProduccion = false;

            for (int a = 0; a < 5; a++)
            {
                double sumDiario = 0;

                for (int b = 0; b < i; b++)
                {
                    sumDiario += producDiaria[b, a];
                }

                if (sumDiario < 240 * i)
                {
                    bajaProduccion = true;

                    Console.WriteLine(
                        $"El dia #{a + 1} tuvo baja producción: {sumDiario:F0} unidades");
                }
            }

            if (!bajaProduccion)
            {
                Console.WriteLine("\nProducción estable durante toda la semana");
            }

            Console.WriteLine(
                "\nNota: El promedio esperado se calcula como:");
            Console.WriteLine("240 x número de operarios");
        }
        static void MostrarProduccionSemanal(
            string[] nombre,
            string[] codigo,
            double[] producSemanal,
            ref int i)
        {
            Console.WriteLine("\nPRODUCCION SEMANAL\n");

            Console.WriteLine(
                "CODIGO".PadRight(15) +
                "NOMBRE".PadRight(25) +
                "PROD.SEMANAL");

            Console.WriteLine(new string('-', 60));

            for (int n = 0; n < i; n++)
            {
                Console.WriteLine(
                    codigo[n].PadRight(15) +
                    nombre[n].PadRight(25) +
                    producSemanal[n].ToString("F2"));
            }
        }
        static void MostrarPromedioGeneral(ref double promGeneral)
        {
            Console.WriteLine();

            Console.WriteLine(new string('-', 60));

            Console.WriteLine(
                $"El promedio general de producción fue de {promGeneral:F2} unidades");
        }
    }
}
