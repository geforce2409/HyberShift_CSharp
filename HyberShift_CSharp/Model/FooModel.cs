using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model
{
    public class FooModel
    {
        // attribute of model
        private string attribute1;
        private double attribute2;
        private bool isFoo;

        // getter and setter
        public string Attribute1
        {
            get { return attribute1; }
            set { attribute1 = value; }
        }

        public double Attribute2
        {
            get { return attribute2; }
            set { attribute2 = value; }
        }

        public bool IsFoo
        {
            get { return isFoo; }
            set { isFoo = value; }
        }

        //methods of model
        public void Method1()
        {

        }

        public double Method2()
        {
            return 0f;
        }

        public bool IsValidAttribute1() //example about data validation
        {
            return true;
        }

        // others method ...
    }
}
