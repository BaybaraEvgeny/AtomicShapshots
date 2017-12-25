namespace AtomicShapshots
{
    
    public class Register
    {
        public int value;
        public bool[] p;
        public bool toogle;
        public int[] snapshot;

        public Register(int n)
        {
            p = new bool[n];
            snapshot = new int[n];
        }
    }
    
}