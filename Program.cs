using System;

class HeapSortExample
{
    public static void HeapSort(int[] A)
    {
        int n = A.Length;
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(A, n, i);

        for (int i = n - 1; i > 0; i--)
        {
            (A[0], A[i]) = (A[i], A[0]);
            Heapify(A, i, 0);
        }
    }

    public static void Heapify(int[] A, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && A[left] > A[largest])
            largest = left;
        if (right < n && A[right] > A[largest])
            largest = right;

        if (largest != i)
        {
            (A[i], A[largest]) = (A[largest], A[i]);
            Heapify(A, n, largest);
        }
    }

    public static void PrintArrayParcial(int[] A, int max = 10)
    {
        for (int i = 0; i < Math.Min(max, A.Length); i++)
            Console.Write(A[i] + " ");
        if (A.Length > max)
            Console.Write("...");
        Console.WriteLine();
    }

    static long MemoriaArreglo(int[] arr)
    {
        return 24 + (arr.Length * 4);
    }

    static void Main()
    {
        int n = 1_000_000;
        Random random = new Random();

        void MedirCaso(string nombre, Func<int[]> generar)
        {
            int[] arreglo = generar();
            Console.WriteLine($"=== {nombre.ToUpper()} ===");

            Console.Write("Original: ");
            PrintArrayParcial(arreglo);

            long memoria = MemoriaArreglo(arreglo);

            var inicio = DateTime.Now;
            HeapSort(arreglo);
            var fin = DateTime.Now;

            Console.Write("Ordenado: ");
            PrintArrayParcial(arreglo);

            Console.WriteLine($"Tiempo: {(fin - inicio).TotalMilliseconds} ms");
            Console.WriteLine($"Memoria del arreglo: {memoria} bytes ({memoria / 1024.0 / 1024.0:F2} MB)");
            Console.WriteLine();
        }

        MedirCaso("Caso promedio", () =>
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = random.Next(1, 1_000_000);
            return arr;
        });

        MedirCaso("Mejor caso", () =>
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = n - i;
            return arr;
        });

        MedirCaso("Peor caso", () =>
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
                arr[i] = i + 1;
            return arr;
        });
    }
}
