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
        }

        // HeapSort in-place al estilo clásico
        public void Ordenar()
        {
            int n = heap.Count;

            // FASE 1: Construir el heap (max heap)
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(n, i);
            }

            // FASE 2: Extraer elementos uno por uno del heap
            for (int i = n - 1; i > 0; i--)
            {
                // Mover la raíz actual al final
                (heap[0], heap[i]) = (heap[i], heap[0]);

                // Llamar a heapify en el heap reducido
                Heapify(i, 0);
            }
        }

        // Heapify recursivo que mantiene la propiedad de max heap
        private void Heapify(int tamaño, int index)
        {
            int mayor = index;                // Inicialmente, la raíz es el mayor
            int izq = 2 * index + 1;          // Índice del hijo izquierdo
            int der = 2 * index + 2;          // Índice del hijo derecho

            if (izq < tamaño && heap[izq].Gravedad > heap[mayor].Gravedad)
                mayor = izq;

            if (der < tamaño && heap[der].Gravedad > heap[mayor].Gravedad)
                mayor = der;

            if (mayor != index)
            {
                (heap[index], heap[mayor]) = (heap[mayor], heap[index]);

                // Llamada recursiva
                Heapify(tamaño, mayor);
            }
        }

        // Función para obtener las gravedades ordenadas (opcional)
        public List<int> GetGravedades()
        {
            List<int> lista = new List<int>();
            foreach (var p in heap)
                lista.Add(p.Gravedad);
            return lista;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            Random random = new Random();
            MaxHeap heap = new MaxHeap();

            Console.WriteLine($"\nGenerando {n} elementos...");

            for (int i = 0; i < n; i++)
            {
                int gravedad = random.Next(1, n);
                heap.Insert(new Paciente(gravedad));
            }

            Console.WriteLine("Datos generados. Iniciando HeapSort...");

            var inicio = DateTime.Now;
            heap.Ordenar();
            var fin = DateTime.Now;

            TimeSpan t = fin - inicio;

            Console.WriteLine($"\nHeapSort completado.");
            Console.WriteLine($"Cantidad total: {n}");
            Console.WriteLine($"Tiempo: {t.TotalMilliseconds} ms");
            Console.WriteLine($"Tiempo: {t.TotalSeconds} s");
            Console.WriteLine($"Consumo de memoria en bytes: {GC.GetTotalMemory(false)}");
            // Mostrar resultados
            Console.Write($"\nPrimeros {n} valores ordenados: ");
            var ordenados = heap.GetGravedades();
            for (int i = 0; i < Math.Min(20, ordenados.Count); i++)
                Console.Write(ordenados[i] + " ");

            Console.WriteLine("\n\n(Visualización limitada para evitar saturar la consola)");
        }
    }
}
