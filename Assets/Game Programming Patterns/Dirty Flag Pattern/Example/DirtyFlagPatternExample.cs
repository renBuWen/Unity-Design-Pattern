using UnityEngine;

namespace DirtyFlagPatternExample
{
    public class DirtyFlagPatternExample : MonoBehaviour
    {
        GraphNode graphNode = new GraphNode(new MeshEX());
        TransformEX parentWorldTransform = new TransformEX();
        void Start()
        {
            //初始化子节点
            for (int i = 0; i < graphNode.NumChildren; i++)
            {
                graphNode.Children[i] = new GraphNode(new MeshEX());
            }
            //进行渲染
            graphNode.render(TransformEX.origin, true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                //修改位置,触发脏标记
                TransformEX newLocalTransform = new TransformEX();
                newLocalTransform.Position = new Vector3(2, 2, 2);
                graphNode.setTransform(newLocalTransform);

                //再次进行渲染
                graphNode.render(parentWorldTransform, true);
            }
        }
    }

    /// <summary>
    /// 网格类
    /// </summary>
    class MeshEX
    {

    }

    /// <summary>
    /// 位置类
    /// </summary>
    class TransformEX
    {
        private Vector3 position = new Vector3(1, 1, 1);
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public static TransformEX origin = new TransformEX();

        public TransformEX combine(TransformEX other)
        {

            TransformEX trans = new TransformEX();
            if (other != null)
            {
                trans.Position = Position + other.Position;
            }
            return trans;
        }

    };

    /// <summary>
    /// 场景图节点
    /// </summary>
    class GraphNode
    {
        //脏标记 dirty flag
        private bool dirty_;

        private MeshEX mesh_;
        private TransformEX local_;
        private TransformEX world_ = new TransformEX();
        const int MAX_CHILDREN = 100;

        /// <summary>
        /// 子节点
        /// </summary>
        private GraphNode[] children_ = new GraphNode[MAX_CHILDREN];
        public GraphNode[] Children
        {
            get { return children_; }
            set { children_ = value; }
        }

        /// <summary>
        /// 子节点
        /// </summary>
        private int numChildren_ = 88;
        public int NumChildren
        {
            get { return numChildren_; }
            set { numChildren_ = value; }
        }

        public GraphNode(MeshEX mesh)
        {
            mesh_ = mesh;
            local_ = TransformEX.origin;
            dirty_ = true;

        }

        /// <summary>
        /// 设置局部坐标位置
        /// </summary>
        public void setTransform(TransformEX local)
        {
            local_ = local;
            dirty_ = true;
        }


        /// <summary>
        /// 渲染
        /// <para>参数1 : 你传入的目标数据</para>
        /// <para>参数2 : 将你传入的标记为“脏”</para>
        /// </summary>
        /// <param name="parentWorld"></param>
        /// <param name="dirty"></param>
        public void render(TransformEX parentWorld, bool dirty)
        {
            //HACK :【03】关键，bool值还能这样运算
            //如果父链中它之上的任何物体标记为脏，则它将被置为true
            //当且仅当两个操作数均为 false 时，结果才为 false
            dirty |= dirty_;

            //而当节点没有改动时（dirty=false），跳过combine的过程，
            //否则，表示此链为脏，进行combine
            if (dirty)
            {
                Debug.Log("this node is dirty,combine it!");
                world_ = local_.combine(parentWorld);
                dirty_ = false;
            }

            //渲染mesh
            if (mesh_ != null)
            {
                renderMesh(mesh_, world_);
            }

            for (int i = 0; i < numChildren_; i++)
            {
                if (children_[i] != null)
                {
                    children_[i].render(world_, dirty);
                }

            }
        }

        /// <summary>
        /// 网格的渲染
        /// </summary>
        private void renderMesh(MeshEX mesh_, TransformEX world_)
        {
            Debug.Log("renderMesh!");
        }
    }

}

