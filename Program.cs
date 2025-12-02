using System;
using System.Collections.Generic;

namespace HeapGravedad
{
    public class Paciente
    {
        public int Gravedad { get; set; }

        public Paciente(int gravedad)
        {
            Gravedad = gravedad;
        }
    }

    public class MaxHeap
    {
        private List<Paciente> heap = new List<Paciente>();

        // Insertar paciente
        public void Insert(Paciente p)
        {
            heap.Add(p);
            Subir(heap.Count - 1);
        }

        private void Subir(int index)
        {
            while (index > 0)
            {
                int padre = (index - 1) / 2;

                if (heap[index].Gravedad > heap[padre].Gravedad)
                {
                    (heap[index], heap[padre]) = (heap[padre], heap[index]);
                    index = padre;
                }
                else break;
            }
        }

        // Extraer el paciente con mayor gravedad
        public Paciente ExtraerMax()
        {
            if (heap.Count == 0)
                return null;

            Paciente max = heap[0];
            heap[0] = heap[^1];
            heap.RemoveAt(heap.Count - 1);

            Bajar(0);
            return max;
        }

        private void Bajar(int index)
        {
            int tamaño = heap.Count;

            while (true)
            {
                int izq = 2 * index + 1;
                int der = 2 * index + 2;
                int mayor = index;

                if (izq < tamaño && heap[izq].Gravedad > heap[mayor].Gravedad)
                    mayor = izq;

                if (der < tamaño && heap[der].Gravedad > heap[mayor].Gravedad)
                    mayor = der;

                if (mayor != index)
                {
                    (heap[index], heap[mayor]) = (heap[mayor], heap[index]);
                    index = mayor;
                }
                else break;
            }
        }

        // HeapSort
        public List<Paciente> Ordenar()
        {
            List<Paciente> orden = new List<Paciente>();

            while (heap.Count > 0)
                orden.Add(ExtraerMax());

            return orden;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // tamaño de la talla
            int n = 10;

            Random random = new Random();
            MaxHeap heap = new MaxHeap();

            Console.WriteLine($"\nGenerando {n} elementos...");

            // Insertar datos grandes para análisis
            for (int i = 0; i < n; i++)
            {
                int gravedad = random.Next(1, n); // Valor entre 1–n elementos
                heap.Insert(new Paciente(gravedad));
            }

            Console.WriteLine("Datos generados. Iniciando HeapSort...");

            var inicio = DateTime.Now;
            List<Paciente> ordenados = heap.Ordenar();
            var fin = DateTime.Now;

            TimeSpan t = fin - inicio;

            Console.WriteLine($"\nHeapSort completado.");
            Console.WriteLine($"Cantidad total: {n}");
            Console.WriteLine($"Tiempo: {t.TotalMilliseconds} ms");
            Console.WriteLine($"Tiempo: {t.TotalSeconds} s");

            // Mostrar solo primeros valores para no saturar la consola
            Console.Write("\nPrimeros 20 valores ordenados: ");
            for (int i = 0; i < Math.Min(20, ordenados.Count); i++)
                Console.Write(ordenados[i].Gravedad + " ");

            Console.WriteLine("\n\n(Visualización limitada para evitar saturar la consola)");
        }
    }
}
