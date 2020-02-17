namespace ETdoFresh.PerlinNoise
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
}