using System;
using System.Xml.Schema;

namespace AtomicShapshots
{
    public class RegisterManager
    {

        private readonly int n;

        private bool[,] q;
        private Register[] r;

        public RegisterManager(int n)
        {
            this.n = n;
            r = new Register[n];
            for (int i = 0; i < n; i++)
            {
                r[i] = new Register(n);
            }
            q = new bool[n, n];
        }

        public int[] Scan(int id)
        {
            
            int[] marked = new int[n];

            while (true)
            {
                for (int j = 0; j < n; j++)
                {
                    q[id, j] = r[j].p[id];
                }

                var a = r;
                var b = r;

                var notInterrupted = true;

                for (int j = 0; j < n; j++)
                {
                    if (    a[j].toogle != b[j].toogle 
                         ||    q[id, j] != a[j].p[id]     
                         ||    q[id, j] != b[j].p[id]     )
                    {
                        if (marked[j] == 1) 
                            return b[j].snapshot;
                        
                        notInterrupted = false;
                        marked[j]++;
                    }
                }

                if (notInterrupted)
                {
                    var snapshot = new int[n];
                    for (int j = 0; j < n; j++)
                    {
                        snapshot[j] = b[j].value;
                    }
                    return snapshot;
                }
            }
            
        }

        public void Update(int id, int value)
        {

            var pBits = new bool[n];
            for (int j = 0; j < n; j++)
            {
                pBits[j] = !q[j, id];
            }

            var snapshot = Scan(id);

            r[id].value = value;
            r[id].p = pBits;
            r[id].toogle = !r[id].toogle;
            r[id].snapshot = snapshot;

        }

    }
}