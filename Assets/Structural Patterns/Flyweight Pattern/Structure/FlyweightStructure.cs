//-------------------------------------------------------------------------------------
//	FlyweightStructure.cs
//-------------------------------------------------------------------------------------

// [Definition]
//----------------------------------------------------------------------
// Use sharing to support large numbers of fine-grained objects efficiently.

// [Participants]
//----------------------------------------------------------------------
//     The classes and objects participating in this pattern are:
// 
// Flyweight
//      declares an interface through which flyweights can receive and act on extrinsic state.
// ConcreteFlyweight
//      implements the Flyweight interface and adds storage for intrinsic state, if any.A ConcreteFlyweight object must be sharable.Any state it stores must be intrinsic, that is, it must be independent of the ConcreteFlyweight object's context.
// UnsharedConcreteFlyweight
//      not all Flyweight subclasses need to be shared.The Flyweight interface enables sharing, but it doesn't enforce it. It is common for UnsharedConcreteFlyweight objects to have ConcreteFlyweight objects as children at some level in the flyweight object structure (as the Row and Column classes have).
// FlyweightFactory
//      creates and manages flyweight objects
//      ensures that flyweight are shared properly.When a client requests a flyweight, the FlyweightFactory objects assets an existing instance or creates one, if none exists.
// Client
//      maintains a reference to flyweight(s).
//      computes or stores the extrinsic state of flyweight(s).



using UnityEngine;
using System.Collections;

public class FlyweightStructure : MonoBehaviour
{
    // Step :【05】调用
    void Start()
    {
        // Arbitrary extrinsic state(外部状态)
        int externalState = 22;

        FlyweightFactory factory = new FlyweightFactory();

        // Work with different flyweight instances
        Flyweight fx = factory.GetFlyweight("X");
        fx.Operation(--externalState);

        Flyweight fy = factory.GetFlyweight("Y");
        fy.Operation(--externalState);

        Flyweight fz = factory.GetFlyweight("Z");
        fz.Operation(--externalState);

        UnsharedConcreteFlyweight fu = new
        UnsharedConcreteFlyweight();

        fu.Operation(--externalState);

    }
}


// Step :【05】享元工厂
class FlyweightFactory
{
    // Step :【05】将所有的“内在状态”对象都放进一个List中

    // Note :【01】哈希表的使用
    private Hashtable ht_flyweights = new Hashtable();

    public FlyweightFactory()
    {
        ht_flyweights.Add("X", new ConcreteFlyweight());
        ht_flyweights.Add("Y", new ConcreteFlyweight());
        ht_flyweights.Add("Z", new ConcreteFlyweight());
    }

    // Step :【05】从List中取出“小对象”
    public Flyweight GetFlyweight(string key)
    {
        return ((Flyweight)ht_flyweights[key]);
    }
}


#region 细胞粒度的简单对象
abstract class Flyweight
{
    public abstract void Operation(int externalState);
}

class ConcreteFlyweight : Flyweight
{
    public override void Operation(int externalState)
    {
        Debug.Log("ConcreteFlyweight: " + externalState);
    }
}
#endregion


class UnsharedConcreteFlyweight : Flyweight
{
    public override void Operation(int externalState)
    {
        Debug.Log("UnsharedConcreteFlyweight: " + externalState);
    }
}
