using UnityEngine;

namespace CommandStructure
{
    public class CommandStructure : MonoBehaviour
    {
        void Start()
        {
            Receiver receiver = new Receiver();               // 厨师
            Command command = new ConcreteCommand(receiver);  // 点一道菜
            Invoker invoker = new Invoker();                  // 客人

            // 客人提出对“菜”的要求
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }

    /// <summary>
    /// 命令封装的基础模板
    /// </summary>
    abstract class Command
    {
        // 谁接收，谁去做
        protected Receiver receiver;

        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }

        // 执行
        public abstract void Execute();
    }

    class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver) : base(receiver) { }

        public override void Execute()
        {
            receiver.Action1();
        }
    }

    /// <summary>
    /// 功能的提供者,相当于“厨师”
    /// </summary>
    class Receiver
    {
        //功能1
        public void Action1()
        {
            Debug.Log("Called Receiver.Action()");
        }
    }

    /// <summary>
    /// 下命令的“客人”，命令管理者
    /// </summary>
    class Invoker
    {
        private Command _command;

        public void SetCommand(Command command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }

}