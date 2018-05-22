using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // while(true)
            // {
            //     Console.WriteLine("Echo");
            // }

            // for(int i = 0, j = 0; i < 10; i++, j++)
            // {
            // }

            var list = new int[] { 1, 2, 3, 4, 5, 6, 7};
            foreach (var item in list)
            {
                if(item % 2 == 0)
                    continue; // like skipping to next iteration

                if(item == 4)
                    goto label; // You never see this, goto disrupts execution path
                                // goto not a good idea

                if(item == 5)
                    break;                
                
                if(item == 6)
                    throw new Exception();  // throw is another jump syntax
            }
            label:
                Console.WriteLine("Whatwhat");

            
            var sb = new StringBuilder();

            sb.Append("This is on one line");
            sb.AppendLine("This includes a new line");

            Console.WriteLine(sb);

            var testString = "The quick brown fox jumped over the lazy dog";
            Console.WriteLine(testString.Substring(4,5));
            Console.WriteLine(String.Concat("hay ", "monet"));
            Console.WriteLine(testString.Replace("the", "bee"));
            Console.WriteLine(testString.ToCharArray());
            Console.WriteLine(testString.ToUpper());
            Console.WriteLine(testString.ToLower());


            var dog = new Dog { Name = "Graustark" };

            //Reflection: inspect metadata at runtime
            Type t1 = typeof(Dog);
            Console.WriteLine(t1);

            Type t2 = dog.GetType();

            Console.WriteLine(t2.Name);
            Console.WriteLine(t2.Assembly);

            //dynamically reflect and create an instance
            //just knowing its type

            //Reflection

            var newDog = (Dog)Activator.CreateInstance(typeof(Dog));
            var genericDog = Activator.CreateInstance<Dog>();
            

            var dogConstuctor = typeof (Dog).GetConstructors()[0];


            Property();

            var ldog = Activator.CreateInstance(typeof (Dog)) as Dog;
            var properties = ldog.GetType().GetProperties();

            PropertyInfo woofProperty = null;

            foreach(var prop in properties)
            {
                if(prop.Name.Equals("Woof",StringComparison.CurrentCulture))
                {
                    woofProperty = prop;
                }
                Console.WriteLine(prop);
            }

            woofProperty.SetValue(ldog, "BRROCK", null);
            Console.WriteLine(woofProperty.GetValue(ldog, null));

            var defaultConstructor = typeof (Dog).GetConstructor(new Type[0]);

            var wDog = (Dog) defaultConstructor.Invoke(null);

            Console.WriteLine(wDog.Woof);

            

        }


        static void Property()
        {
            var horse = new Animal() {  Temp = 100 };
            var type = horse.GetType();
            var property = type.GetProperty("Temp");
            var value = property.GetValue(horse, null);
            Console.WriteLine(value);
        }

        static void Method()
        {
            var horse  = new Horse();
            var type = horse.GetType();
            var method = type.GetMethod("Speak");
            var value = (string)method.Invoke(horse, null);
            Console.WriteLine(value);
        }

    }

    public class Horse
    {
        public string Speak() {return "Hello";}
    }



    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }


    public class Demo : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                // release managed resources
            }

            //release unmanaged resources
        }

        //This is called a Finalizer
        ~Demo() 
        {
            Dispose(false);
        }
    }


        //one class can be spread across 2 files

    public partial class Dog
    {
        //properties hold values
        public string Name { get; set; }

        //full property
        //backing store
        private string _name2;
        public string Name2
        {
            get { return _name2;}
            private set { 
                //look at value
                _name2 = value;
            }
        }
        

        //only by this class
        private void Foo() {}

        //only this and derived classes
        protected void FBB() {}

        //only in this assembly
        internal void Bar() {}

        //void means method returns nothing
        public void Speak(string what = "bark") {
            //TODO
            Console.WriteLine(what);
        }

        // what is method's parameter, "bark" is the argument
        public void Speak(int times, string what = "bark", bool sit = true) {
            //TODO
            for(int i = 0; i <= times;  i++)
            {
                Console.WriteLine(what);
            }

            //someone must be subscribed to the event
            if (HasSpoken != null)
                this.HasSpoken(this, EventArgs.Empty);
        }

        //EventHandler is a delegate, has object that raises event, and EventArgs
        //EventHandler marks the type of delegate
        public event EventHandler HasSpoken;
    }

    class Poodle: Dog
    {
        void X() { 
            this.Speak(what: "BARKJKJ", sit: true, times: 3);

        }
    }


    public class Trainer
    {
        void Operate()
        {
            var dog = new Poodle();
            dog.HasSpoken += dog_HasSpoken;
        }

        //This is because HasSpoken requires EventHandler signature
        void dog_HasSpoken(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }


    public abstract class Offspring {}
    public class Animal<T> where T : Offspring
    {
        public T Offspring { get; set; }
    }

    public class Egg : Offspring {}
    public class Piglet : Offspring {}

    public class Breakfast
    {

        public void GetBreakfast() {
            var bird = new Animal<Egg>();
            var pig = new Animal<Piglet>();
        }
    }


    public class Animal {

        public int Temp { get; set; }   
    }
    public class Camel : Animal {
        public int Lumps { get; set; }
    }
    public class Ernesty : Camel {
        public string Groomer { get; set; }
    }

    public class Z
    {
        public void x()
        {
            var animal = new Animal();
            var cammy = new Camel();
            var ernest = new Ernesty();

            TakeAnimal(ernest);
        }
        public void TakeAnimal(Animal a)
        {
            a.Temp = 10;

            if (a is Camel)
            {
                var dog = a as Camel;
                dog.Lumps = 4;
            }
            
            // var cam = (Camel) a;
            //var dog = a as Camel; // this will be null for non-camel arguments
            //if(dog != null)
        }

    }

    public class yy
    {
        public void xx()
        {
            var path = "c:\test.txt";
            // var file = File.Open(path, FileMode.Open);
            // //var file2 = File.Open(path, FileMode.Open); //fails
            // //TODO
            // file.Close();
            
            //using calls dispose not file.Close()
            using(var file = File.Open(path, FileMode.Open))
            {
                // TODO
            }


            
        }
    }

}
