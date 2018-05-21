using System;

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

                if(item == 5)
                    goto label; // You never see this, goto disrupts execution path
                                // goto not a good idea

                if(item == 5)
                    break;
            }
            label:
                Console.WriteLine("Whatwhat");
        }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
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


}
