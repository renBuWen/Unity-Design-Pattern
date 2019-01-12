using UnityEngine;
using System.Collections.Generic;

namespace ComponentPatternExample
{

    public class ComponentPatternExample : MonoBehaviour
    {
        RPGGame rpgGame = new RPGGame();

        void Start()
        {
            rpgGame.Start();
        }

        void Update()
        {
            rpgGame.Update();
        }
    }

    /// <summary>
    /// 游戏类
    /// </summary>
    class RPGGame
    {
        public int velocity;
        public int x = 0, y = 0;

        //辅助类对象
        public WorldX worldX = new WorldX();
        public GraphicsX graphicsX = new GraphicsX();

        //组件
        private InputComponent inputComponent;
        private PhysicsComponent physicsComponent;
        private GraphicsComponent graphicsComponent;

        //组件List
        public List<BaseComponent> ComponentList = new List<BaseComponent>();
        //组件List容量
        int componentAmount = -1;

        public void Start()
        {

            // Step :【01】初始化并添加到List中
            inputComponent = new PlayerInputComponent();
            physicsComponent = new PlayerPhysicsComponent();
            graphicsComponent = new PlayerGraphicsComponent();

            ComponentList.Add(inputComponent);
            ComponentList.Add(physicsComponent);
            ComponentList.Add(graphicsComponent);

            Debug.Log("Game Components Initialization Finish...");
            Debug.Log("Please enter LeftArrow or RightArrow button to play...");
        }

        public void Update()
        {
            if (ComponentList == null)
            {
                return;
            }

            // Step :【01】关键位置
            // Step :【01】我的思考
            /*
             * 01-将所有的脚本放到一个List中，然后for循环遍历这个List
             *     01 :可以控制哪个组件优先执行，使用键排序的List
             *     02 :可以让List中的每个元素都去维护一个东西
             *     03 :有两层，List中的东西是一层，调用List的那个地方是一层（全局掌控层）
             */
            componentAmount = ComponentList.Count;
            for (int i = 0; i < componentAmount; i++)
            {
                ComponentList[i].Update(this);
            }
        }
    };

    /// <summary>
    /// 输入组件
    /// </summary>
    class PlayerInputComponent : InputComponent
    {
        public void Update(RPGGame game)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                game.velocity -= WALK_ACCELERATION;
                Debug.Log(" game velocity= " + game.velocity.ToString());
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                game.velocity += WALK_ACCELERATION;
                Debug.Log(" game velocity= " + game.velocity.ToString());
            }
        }

        private int WALK_ACCELERATION = 1;
    }


    /// <summary>
    /// 物理组件
    /// </summary>
    class PlayerPhysicsComponent : PhysicsComponent
    {
        public void Update(RPGGame game)
        {
            game.x += game.velocity;

            //Handle Physics...

        }
    }


    /// <summary>
    /// 图形组件
    /// </summary>
    class PlayerGraphicsComponent : GraphicsComponent
    {
        public void Update(RPGGame game)
        {
            //Handle Graphics...

            if (game == null || game.graphicsX == null)
            {
                return;
            }

            Sprite sprite = spriteStand;
            if (game.velocity < 0)
            {
                sprite = spriteWalkLeft;
            }
            else if (game.velocity > 0)
            {
                sprite = spriteWalkRight;
            }
            game.graphicsX.Draw(sprite, game.x, game.y);


        }

        private Sprite spriteStand;
        private Sprite spriteWalkLeft;
        private Sprite spriteWalkRight;
    }

    #region interfaces

    interface BaseComponent
    {
        void Update(RPGGame game);
    }

    interface GraphicsComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    interface PhysicsComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    interface InputComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    #endregion

    #region 辅助类

    class WorldX
    {

    }

    class GraphicsX
    {
        public void Draw(Sprite sprite, float x, float y) { }
    }
}

# endregion

