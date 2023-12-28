using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ICS_Lab_4_dll
{
    public static class MyRSA
    {
        //Checking the number for simplicity
        public static bool Miller_Rabin(BigInteger n, int k)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;
            BigInteger t = n - 1;
            int s = 0;
            while (t % 2 == 0)
            {
                t /= 2;
                s++;
            }
            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] _a = new byte[n.ToByteArray().LongLength];
                BigInteger a;
                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                } while (a < 2 || a >= n - 2);
                BigInteger x = BigInteger.ModPow(a, t, n);
                rng.Dispose();
                _a = null;
                a = BigInteger.Zero;
                GC.Collect();
                if (x == 1 || x == n - 1)
                    continue;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);

                    if (x == 1)
                        return false;

                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }
            return true;
        }

        public static int n_module(int num_p, int num_q)
        {
            //Additional function for module number
            return num_p * num_q;
        }

        public static int euler_f(int num_p, int num_q)
        {
            //Function for Euler function number
            return (num_p - 1) * (num_q - 1);
        }

        public static bool e_check(int num_e, int euler_num, int pn, int pq)
        {
            //Checking "e" number 
            if ((num_e >= n_module(pn, pq)) || (num_e >= euler_num) || (num_e <= 1) || (num_e % euler_num == 0) || (euler_num % num_e == 0))
                return false;
            else
                return true;
        }

        public static int d_find(int num_e, int num_euler)
        {
            //Similarity of extended Euclid algorithm for findin
            int d = 10;

            while (true)
            {
                if ((d * num_e) % num_euler == 1)
                    break;
                else
                    d++;
            }

            return d;
        }

        public static int RSA_encrypt(int pack, int e, int n)
        {
            //Encryption method
            BigInteger c = pack;
            BigInteger k = 1;
            for (int i = 0; i < e; i++)
            {
                k = c * k;
                k = k % n;
            }
            c = k;
            return (int)c;
        }

        public static char RSA_decrypt(int encrypted, int d, int n)
        {
            //Decryption method
            BigInteger m = encrypted;
            BigInteger k = 1;
            for (int i = 0; i < d; i++)
            {
                k = m * k;
                k = k % n;
            }
            m = k;
            return (char)(int)m;
        }
    }
}
