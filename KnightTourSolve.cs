using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TempProject.One
{
    class Generator
    {
        int X;
        int Y;
        int[,] table;
        int timer;
        List<Option> Options;
        public void Initialize(int x, int y)
        {
            X = x;
            Y = y;
            table = new int[x, y];
            timer = 0;
            // Setting Possible Way Option
            Options = new List<Option>();
            Options.Add(new Option()
            {
                X = 2,
                Y = 1
            });
            Options.Add(new Option()
            {
                X = 1,
                Y = 2
            });
            //2
            Options.Add(new Option()
            {
                X = 2,
                Y = -1
            });
            Options.Add(new Option()
            {
                X = -1,
                Y = 2
            });
            //4
            Options.Add(new Option()
            {
                X = -2,
                Y = 1
            });
            Options.Add(new Option()
            {
                X = 1,
                Y = -2
            });
            //6
            Options.Add(new Option()
            {
                X = -2,
                Y = -1
            });
            Options.Add(new Option()
            {
                X = -1,
                Y = -2
            });
            //8
            // declare to starting point
            Option firstLocation = new Option() { X = 0, Y = 0 };
            bool returnValue = false;
            
            while(Progress(firstLocation, 0, fillTable(firstLocation, new int[8, 8])))// Loop while find best path 
            {

            }
            
        }
        int[,] fillTable(Option myFirstLocation, int[,] myTable)
        {
            //For create scalable chess table
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    myTable[i, j] = -1;
                }
            }
            return myTable;
        }
        bool allComplate = false;
        bool Progress(Option myLocation, int Counter, int[,] myTable)
        {
            int newCounter = Counter + 1;
            myTable[myLocation.X, myLocation.Y] = Counter;//set previous location value 
            bool myValue = false;
            timer++;
            

            if (!allComplate && timer<10000) //Check Complate and set max-range for loop
            {
                if (isFinish(myTable))
                {

                    printTable(myTable);
                    allComplate = true;
                }
                else
                {
                    Option newLocation = changeLocation(myLocation, findWay(myLocation, myTable.DeepClone(), newCounter));
                    myValue = Progress(newLocation, newCounter, myTable);//recursive func 
                }
            }
            else { return true; }
            if (!myValue)
            {
                myTable[myLocation.X, myLocation.Y] = -1; // turn in back
            }
            return myValue;
        }
        void printProgress(int value) // for primitive statistics 
        {
            string wrtstr = "";

            for (int i = 0; i < value; i++)
            {

                wrtstr+="+";
            }
            for (int i = 0; i < (X * Y)-value; i++)
            {
                wrtstr += "-";
            }
           
            Console.WriteLine(wrtstr);
        }
        void printTable(int[,] myTable) // for look on the course
        {
            Console.WriteLine();

            for (int i = 0; i < X; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < Y; j++)
                {
                    Console.Write("|" + myTable[j, i] + "|");
                }
            }
        }
        Option changeLocation(Option location, Option move)
        {
            Option tmpLocation = new Option();
            tmpLocation.X = location.X + move.X;
            tmpLocation.Y = location.Y + move.Y;
            return tmpLocation;
        }
        int CalculateTick()
        {


            return 0;
        }
        bool isFinish(int[,] myTable) //check for all option isValid
        {
            bool myValue = true;
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (myTable[i, j] == -1)
                    {
                        return false;
                    }
                }
            }
            return myValue;
        }
        bool isEmpty(Option feature, int[,] myTable) // check for way is empty
        {
            if (myTable[feature.X, feature.Y] == -1)
            {
                return true;
            }
            return false;
        }
        bool isFirst(Option feature) //draw 
        {
            if (table[feature.X, feature.Y] == -1)
            {
                return false;
            }
            return true;
        }
        bool isRule(Option location, Option move, int[,] myTable) // check option logical way
        {
            bool myValue = false;
            //Console.WriteLine("Location X:" + location.X + "Location Y:" + location.Y);
            //Console.WriteLine("Move X:" + move.X + " Move Y:" + move.Y);

            int moveToX = location.X + move.X;
            int moveToY = location.Y + move.Y;
            if (moveToX >= 0 & moveToY >= 0 &
                moveToX < X & moveToY < Y)
            {
                Option tmpMove = new Option();

                tmpMove.X = moveToX;
                tmpMove.Y = moveToY;
                if (isEmpty(tmpMove, myTable))
                {
                    myValue = true;
                }
            }
            
            return myValue;
        }
        Option findWay(Option location,int[,] myTable,int count) // find way have most option
        {
            Option currentOption = new Option();
            
            List<(int, Option)> match = new List<(int, Option)>();//declare rate and option matching list 
            foreach (var item in Options)
            {
                if (isRule(location, item, myTable))
                {
                    Console.WriteLine(count);
                    Option myNewLocation = changeLocation(location, item);
                    myTable[myNewLocation.X, myNewLocation.Y] = count;
                    match.Add((countWay(location, item, myTable), new Option() { X = item.X, Y = item.Y }));// add possible way with counted possible way 
                }
            }
            
          
               
            int lastCount = 0;
            foreach (var item in match)// choose option for have most possible way, I'm starving :P 
            {
                if (lastCount == 0)
                {
                    lastCount = item.Item1;
                    currentOption = item.Item2;
                }
                else if (item.Item1 > lastCount)
                {
                    lastCount = item.Item1;
                    currentOption = item.Item2;
                }
            }
          
            //foreach (var item in match)
            //{
               
            //}
            return currentOption;
            
        }
        int countWay(Option location, Option move, int[,] myTable)
        {
            int count = 0;
            foreach (var item in Options)
            {
                if (isRule(move, item,myTable))
                {
                    count++;
                }
            }
            
            return count;
        }
    }
    class Option
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    [Serializable]
    public class X
    {
        public string str;
    }

    public static class Extensions
    {
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
