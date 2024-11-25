using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAUT
{
    public class RSAUtility
    {
        private HashSet<int> primes = new HashSet<int>();
        private Random random = new Random();
        public int PublicKey { get; private set; }
        public int PrivateKey { get; private set; }
        public int N { get; private set; }

        public RSAUtility()
        {
            GeneratePrimes();
        }

        private void GeneratePrimes()
        {
            bool[] sieve = new bool[250];
            for (int i = 2; i < 250; i++) sieve[i] = true;

            for (int i = 2; i < 250; i++)
            {
                if (sieve[i])
                {
                    for (int j = i * 2; j < 250; j += i)
                    {
                        sieve[j] = false;
                    }
                }
            }

            for (int i = 2; i < sieve.Length; i++)
            {
                if (sieve[i]) primes.Add(i);
            }
        }

        private int PickRandomPrime()
        {
            int k = random.Next(0, primes.Count - 1);
            var enumerator = primes.GetEnumerator();
            for (int i = 0; i <= k; i++) enumerator.MoveNext();

            int ret = enumerator.Current;
            primes.Remove(ret);
            return ret;
        }

        public void GenerateKeys()
        {
            int prime1 = PickRandomPrime();
            int prime2 = PickRandomPrime();

            N = prime1 * prime2;
            int phi = (prime1 - 1) * (prime2 - 1);

            int e = 2;
            while (GCD(e, phi) != 1) e++;

            PublicKey = e;

            int d = 2;
            while ((d * e) % phi != 1) d++;

            PrivateKey = d;
        }

        public int Encrypt(int message, int publicKey, int n)
        {
            int encrypted = 1;
            for (int i = 0; i < publicKey; i++) encrypted = (encrypted * message) % n;
            return encrypted;
        }

        public int Decrypt(int cipher, int privateKey, int n)
        {
            int decrypted = 1;
            for (int i = 0; i < privateKey; i++) decrypted = (decrypted * cipher) % n;
            return decrypted;
        }

        private int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
