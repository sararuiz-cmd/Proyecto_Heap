using System;
using System.Collections.Generic;

namespace HeapSortPacientes
{
    // ============================================
    // Clase Paciente
    // ============================================
    public class Paciente
    {
        public string Nombre { get; set; }
        public int Gravedad { get; set; }  // 1 = leve, 5 = crítico

        public Paciente(string nombre, int gravedad)
        {
            Nombre = nombre;
            Gravedad = gravedad;
        }
    }

    // ============================================
    // Clase Heap (Max-Heap)
    // ============================================
    public class Heap
    {
        private List<Paciente> datos;

        public Heap()
        {
            datos = new List<Paciente>();
        }

        // Insertar un paciente en el heap
        public void Insert(Paciente p)
        {
            datos.Add(p);
            int index = datos.Count - 1;

            // Reacomodar hacia arriba
            while (index > 0)
            {
                int padre = (index - 1) / 2;

                if (datos[index].Gravedad > datos[padre].Gravedad)
                {
                    (datos[index], datos[padre]) = (datos[padre], datos[index]);
                    index = padre;
                }
                else
                    break;
            }
        }

        // Construir montículo inicial (para HeapSort)
        public void BuildHeap(List<Paciente> lista)
        {
            datos = lista;

            // Aplicar heapify desde la mitad hacia arriba
            for (int i = datos.Count / 2 - 1; i >= 0; i--)
                Heapify(i);
        }

        // Heapify (reacomoda hacia abajo)
        private void Heapify(int i)
        {
            int mayor = i;
            int izquierda = 2 * i + 1;
            int derecha = 2 * i + 2;

            if (izquierda < datos.Count && datos[izquierda].Gravedad > datos[mayor].Gravedad)
                mayor = izquierda;

            if (derecha < datos.Count && datos[derecha].Gravedad > datos[mayor].Gravedad)
                mayor = derecha;

            if (mayor != i)
            {
                (datos[mayor], datos[i]) = (datos[i], datos[mayor]);
                Heapify(mayor);
            }
        }

        // Extraer al paciente de mayor gravedad
        public Paciente ExtractMax()
        {
            if (datos.Count == 0)
                return null;

            Paciente maximo = datos[0];

            datos[0] = datos[datos.Count - 1];
            datos.RemoveAt(datos.Count - 1);

            Heapify(0);

            return maximo;
        }

        // HeapSort
        public List<Paciente> HeapSort(List<Paciente> lista)
        {
            BuildHeap(lista);
            List<Paciente> ordenados = new List<Paciente>();

            while (datos.Count > 0)
                ordenados.Add(ExtractMax());

            return ordenados;
        }

        // Mostrar lista (para consola)
        public static void MostrarLista(List<Paciente> lista)
        {
            foreach (var p in lista)
                Console.WriteLine($"Paciente: {p.Nombre} | Gravedad: {p.Gravedad}");
        }
    }

    // ============================================
    // PROGRAMA PRINCIPAL CON MENÚ
    // ============================================
    class Program
    {
        static List<Paciente> listaPacientes = new List<Paciente>();
        static Heap heap = new Heap();

        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=======================================");
                Console.WriteLine("   SISTEMA DE TRIAGE MÉDICO - HEAP SORT");
                Console.WriteLine("=======================================\n");

                Console.WriteLine("1. Agregar paciente");
                Console.WriteLine("2. Mostrar lista de pacientes");
                Console.WriteLine("3. Ordenar pacientes por gravedad (HeapSort)");
                Console.WriteLine("4. Atender paciente más urgente (ExtractMax)");
                Console.WriteLine("5. Salir\n");

                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        AgregarPaciente();
                        break;

                    case 2:
                        MostrarPacientes();
                        break;

                    case 3:
                        OrdenarPacientes();
                        break;

                    case 4:
                        AtenderPacienteMax();
                        break;

                    case 5:
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }

                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();

            } while (opcion != 5);
        }

        // ================= MÉTODOS DEL MENÚ =================

        static void AgregarPaciente()
        {
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine();

            int gravedad;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("====================================");
                Console.WriteLine("Gravedad 1: Muy leve ");
                Console.WriteLine("Gravedad 2: Leve ");
                Console.WriteLine("Gravedad 3: Moderado ");
                Console.WriteLine("Gravedad 4: Grave ");
                Console.WriteLine("Gravedad 5: Crítico ");
                Console.WriteLine("====================================");
                Console.WriteLine("");
                Console.Write("Gravedad (1-5): ");
                gravedad = int.Parse(Console.ReadLine());
            } while (gravedad < 1 || gravedad > 5);

            Paciente p = new Paciente(nombre, gravedad);
            listaPacientes.Add(p);
            heap.Insert(p);

            Console.WriteLine("Paciente agregado correctamente.");
        }

        static void MostrarPacientes()
        {
            if (listaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.WriteLine("LISTA DE PACIENTES:");
            Heap.MostrarLista(listaPacientes);
        }

        static void OrdenarPacientes()
        {
            if (listaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes para ordenar.");
                return;
            }

            var ordenados = heap.HeapSort(new List<Paciente>(listaPacientes));

            Console.WriteLine("PACIENTES ORDENADOS POR GRAVEDAD (HeapSort):");
            Heap.MostrarLista(ordenados);
        }

        static void AtenderPacienteMax()
        {
            var paciente = heap.ExtractMax();

            if (paciente == null)
            {
                Console.WriteLine("No hay pacientes por atender.");
                return;
            }

            Console.WriteLine($"Se está atendiendo a: {paciente.Nombre} (Gravedad {paciente.Gravedad})");
        }
    }
}
