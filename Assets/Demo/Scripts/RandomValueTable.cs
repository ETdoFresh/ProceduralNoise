namespace Demo.Scripts
{
    public class RandomValueTable
    {
        public int Size { get; set; }
        public int Seed { get; set; }
        public int SizeI { get; set; }
        public int SizeJ { get; set; }
        public int SizeK { get; set; }

        public int[] Table { get; set; }
        public int this[int i] => Table[i];
        public int this[int i, int j] => Table[i * SizeJ + i % SizeJ];
        public int this[int i, int j, int k] => Table[i * (SizeI + SizeJ) + j * SizeJ + k % SizeJ];

        public RandomValueTable()
        {
            Seed = 1234;
            Size = 1000000;
            SizeI = 100;
            SizeJ = 100;
            SizeK = 100;
            Table = new int[Size];

            var random = new System.Random(Seed);
            for (int i = 0; i < Size; i++)
                Table[i] = random.Next();
        }

        public override string ToString()
        {
            var output = "";
            foreach (var i in Table)
                output += output == "" ? $"{i}" : $",{i}";
            return output;
        }
    }

    public class PermutationTable
    {
        public int Seed { get; private set; }
        public int Size { get; private set; }
        public int Max { get; private set; }
        public float Inverse { get; private set; }
        public readonly int wrap;
        private int[] Table;

        public int this[int i] => Table[i & wrap] & Max;
        public int this[int i, int j] => Table[(j + Table[i & wrap]) & wrap] & Max;
        public int this[int i, int j, int k] => Table[(k + Table[(j + Table[i & wrap]) & wrap]) & wrap] & Max;

        public PermutationTable(int seed = 1234, int size = 1024, int max = 255)
        {
            Seed = seed;
            Size = size;
            Max = max;
            Inverse = 1f / Max;
            wrap = Size - 1;
            Table = new int[Size];
            var randomInt = new System.Random(Seed);
            for (int i = 0; i < Size; i++)
                Table[i] = randomInt.Next();
        }
    }
}