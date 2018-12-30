using UnityEngine;

/*
 * Convert the interface of a class into another interface clients expect.
 * Adapter lets classes work together that couldn't otherwise because of incompatible interfaces.
 * This real-world code demonstrates the use of a legacy chemical databank. 
 * Chemical compound objects access the databank through an Adapter interface.
 */

namespace AdapterPatternExample1
{
    public class AdapterPatternExample1 : MonoBehaviour
    {
        void Start()
        {
            // Non-adapted chemical compound
            Compound unknown = new Compound(CompoundName.Unknown.ToString());
            unknown.Display();

            // Adapted chemical compounds
            Compound water = new RichCompound(CompoundName.Water.ToString());
            water.Display();

            Compound benzene = new RichCompound(CompoundName.Benzene.ToString());
            benzene.Display();

            Compound ethanol = new RichCompound(CompoundName.Ethanol.ToString());
            ethanol.Display();
        }
    }


    enum CompoundName
    {
        Unknown,
        Water,
        Benzene,
        Ethanol
    }

    /// <summary>
    /// The "Target" class
    /// </summary>
    class Compound  /*混合物,复合词*/
    {
        //HACK :【04】父类就是标准，子类必须严格遵守这个标准.
        //但可以有自己的特质,即自己的构造器，自己的Display()
        protected string _chemical;          //化学制品，化学药品
        protected float _boilingPoint;       //沸点
        protected float _meltingPoint;       //熔点
        protected double _molecularWeight;   //分子重量
        protected string _molecularFormula;  //分子式

        public Compound(string chemical)
        {
            Debug.Log("基类 : Compound " + chemical);
            _chemical = chemical;
        }

        public virtual void Display()
        {
            Debug.Log("基类 :Compound :  " + _chemical + "------");
        }
    }

    /// <summary>
    /// The "Adapter" class
    /// </summary>
    class RichCompound : Compound
    {
        //只是一个针对这个例子抽离出来的一个工具类
        private ChemicalDatabank _bank;

        public RichCompound(string name) : base(name)
        {
            Debug.Log("子类 : RichCompound " + name);
        }

        public override void Display()
        {
            //HACK :【04】适应者
            // The Adaptee : 适应者
            _bank = new ChemicalDatabank();

            _boilingPoint = _bank.GetCriticalPoint(_chemical, "B");
            _meltingPoint = _bank.GetCriticalPoint(_chemical, "M");
            _molecularWeight = _bank.GetMolecularWeight(_chemical);
            _molecularFormula = _bank.GetMolecularStructure(_chemical);

            base.Display();
            Debug.Log(" Formula    :  " + _molecularFormula);
            Debug.Log(" Weight     :  " + _molecularWeight);
            Debug.Log(" Melting Pt :  " + _meltingPoint);
            Debug.Log(" Boiling Pt :  " + _boilingPoint);
        }
    }

    /// <summary>
    /// The "Adaptee" class : 适应者
    /// 化学品数据银行
    /// </summary>
    class ChemicalDatabank
    {
        // The databank 'legacy API'
        /// <summary>
        /// Critical : 关键点
        /// </summary>
        /// <param name="compound">化学品名字</param>
        /// <param name="point"></param>
        /// <returns></returns>
        public float GetCriticalPoint(string compound, string point)
        {
            //熔点
            if (point == "M")
            {
                switch (compound.ToLower())
                {
                    case "water": return 0.0f;
                    case "benzene": return 5.5f;
                    case "ethanol": return -114.1f;
                    default: return 0f;
                }
            }
            //沸点
            else
            {
                switch (compound.ToLower())
                {
                    case "water": return 100.0f;
                    case "benzene": return 80.1f;
                    case "ethanol": return 78.3f;
                    default: return 0f;
                }
            }
        }
        /// <summary>
        /// 得到分子的结构/构造
        /// </summary>
        /// <param name="compound">化学品名字</param>
        /// <returns></returns>
        public string GetMolecularStructure(string compound)
        {
            switch (compound.ToLower())
            {
                case "water": return "H20";
                case "benzene": return "C6H6";
                case "ethanol": return "C2H5OH";
                default: return "";
            }
        }

        public double GetMolecularWeight(string compound)
        {
            switch (compound.ToLower())
            {
                case "water": return 18.015;
                case "benzene": return 78.1134;
                case "ethanol": return 46.0688;
                default: return 0d;
            }
        }
    }
}