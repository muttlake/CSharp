using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Session5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            const string dataToProtect = "This is a bunch of super secret content!";
            var dataToProtectAsArray = Encoding.Unicode.GetBytes(dataToProtect);
            //use byte array so you can encrypt numbers

            //below encrypts file
            #region File.Encrypt
            
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyDataFile.txt");
            File.WriteAllText(fileName, dataToProtect);
            // File.Encrypt(fileName);

            #endregion
        
            //below encrypts something in memory
            #region Windows Data Protection

            //     var wdpEncryptedData = ProtectedData.Protect(
            //         dataToProtectAsArray, null, DataProtectionScope.CurrentUser
            //     );


            #endregion

            #region Hashing

            var storedPasswordHash = new byte []
            {
                148, 152, 235, 251, 242, 51, 18, 100, 176, 51, 147,
                249, 128, 175, 164, 106, 204, 48, 47, 154, 75,
                82, 83, 170, 111, 8, 107, 51, 13, 83, 2, 252
            };

            var password = Encoding.Unicode.GetBytes("P4ssw0rd!");
            var passwordHash = SHA256.Create().ComputeHash(password);

            if (passwordHash.SequenceEqual(storedPasswordHash))
            {
                Console.WriteLine("Passwords match!");
            }

            #endregion

            #region Symmetric
            //symmetric encryption

            //Uses Rijndael as an algorithm
            //Two classes Rijndael and Aes - use Aes (more secure)

            //array of 16 random bytes - should be secret, must be used for decryption
            var key = new byte[] { 12, 2, 56, 117, 12, 67, 33, 23, 12,
            2, 56, 117, 12, 67, 33, 23};

            //another list of 16 bytes can be shared publicly, 
            //should be changed for each message exchange
            //like a seed
            var initializationVector = new byte[] {
                37, 99, 102, 23, 12, 22, 156, 204,
                11, 12, 23, 44, 55, 1, 157, 233
            };

            byte[] symEncryptedData;

            var algorithm = Aes.Create();

            // encrypt, can use multiple usings back to back
            using(var encryptor = algorithm.CreateEncryptor(key, initializationVector))
            using(var memoryStream = new MemoryStream())
            using(var cryptoStream = new CryptoStream(memoryStream, encryptor, 
                              CryptoStreamMode.Write))
            {
                 cryptoStream.Write(dataToProtectAsArray, 0, dataToProtectAsArray.Length);
                 cryptoStream.FlushFinalBlock();
                 symEncryptedData = memoryStream.ToArray();   
            }


            //decrypt
            byte[] symUnencryptedData;
            using (var decryptor = algorithm.CreateDecryptor(key, initializationVector))
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(symEncryptedData, 0, symEncryptedData.Length);
                cryptoStream.FlushFinalBlock();
                symUnencryptedData = memoryStream.ToArray();
            }

            algorithm.Dispose();

            if(dataToProtectAsArray.SequenceEqual(symUnencryptedData))
            {
                Console.WriteLine("Symmetric encrypted values match!");
            }



            #endregion
        }
    }

    public class Person
    {
        public string Name { get; set; }

        //Defensive coding
        public void SetName(string value)
        {
            // validate empty
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");
            
            // validate conflict
            if (value == this.Name)
                throw new ArgumentException("value is duplicate");
            
            // validate size
            if (value.Length > 10)
                throw new ArgumentException("value is too long");

            this.Name = value;
        }


        public abstract class Animal {

            public string Name { get; protected set; }
            public abstract void SetName(string value);

        }

        public class Cat: Animal {

            //non-static validations
            public override void SetName(string value)
            {
                // validate empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                
                // validate conflict
                if (value == this.Name)
                    throw new ArgumentException("value is duplicate");
                
                // validate size
                if (value.Length > 10)
                    throw new ArgumentException("value is too long");

                this.Name = value;
            }



        }

        public class Dog: Animal {

            public override void SetName(string value)
            {
                Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty");
                Contract.Requires((value.Length > 10), "value is too long");
                Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is empty");
                this.Name = value;
            }

            public string GetName()
            {
                Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
                return this.Name;
            }
            
        }

        public class PlayWithCatsAndDogs
        {
            public void Play()
            {
                Animal cat = new Cat();
                Animal dog = new Dog();

                if(cat is Dog)
                    throw new NotSupportedException("Dogs Only");
                
                if(cat == dog)
                    throw new Exception("Not the same");

                if (cat.Equals(dog))
                    throw new Exception("Not equal");
            }

            public void PlayExcept()
            {
                try
                {
                    Play();
                }
                catch (DivideByZeroException ex)
                {
                    //specific exception
                    Console.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    //generic exception
                    Console.WriteLine(ex);
                }
                finally
                {

                }
            }
        }




    }
}
