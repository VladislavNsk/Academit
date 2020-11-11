namespace TreeMain

{
    class TreeNode<T>
    {
        public TreeNode<T> LeftChild { get; set; }

        public TreeNode<T> RightChild { get; set; }

        private T data;

        public T Data
        {
            get
            {
                return data;
            }
        }

        public TreeNode(T data)
        {
            this.data = data;
        }

        public TreeNode(TreeNode<T> treeNode)
        {
            data = treeNode.data;
            LeftChild = treeNode.LeftChild;
            RightChild = treeNode.RightChild;
        }

        public TreeNode<T>[] GetChildren()
        {
            return new TreeNode<T>[] { LeftChild, RightChild };
        }

        public int GetChildrenCount()
        {
            int childrenCount = 0;

            if (LeftChild != null)
            {
                childrenCount++;
            }

            if (RightChild != null)
            {
                childrenCount++;
            }

            return childrenCount;
        }
    }
}
