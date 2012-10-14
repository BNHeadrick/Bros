using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Node for learning things
 */
namespace CS8803AGA.learning
{
    class ActionNode
    {
        private List<ActionNode> children; /**< the next nodes */
        private int data; /**< whatever the data of the node is */

        /**
         * Creates a new node
         *@param _data the data
         */
        public ActionNode(int _data)
        {
            data = _data;
            children = new List<ActionNode>();
        }

        /**
         * Adds a child node
         */
        public void addChild(ActionNode child) 
        {
            children.Add(child);
        }

        /**
         * Adds a leaf to the tree
         *@param node the node to add
         */
        public void addLeaf(ActionNode node)
        {
            ActionNode current = this;
            while (current.children.Count != 0)
            {
                current = current.children[0];
            }
            current.children.Add(node);
        }

        /**
         * Finda a node and returns it
         *@param node the node to find
         *@return null if node doesn't exist, or the node
         */
        public ActionNode findNode(ActionNode node)
        {
            List<ActionNode> next = new List<ActionNode>();
            next.Add(this);
            for (int i = 0; i < next.Count; i++)
            {
                if (node.equals(next[i]))
                {
                    return next[i];
                }
                for (int j = 0; j < next[i].children.Count; j++)
                {
                    next.Add(next[i].children[j]);
                }
            }
            return null;
        }

        /**
         * Checks if 2 nodes equal each other
         *@param node the node to compare with this
         *@return true if they are equal, otherwise false
         */
        public bool equals(ActionNode node)
        {
            return(node.data == data);
        }

        /**
         * Returns a copy (data) of this node
         *@return a new copy
         */
        public ActionNode copy()
        {
            return new ActionNode(data);
        }

        /**
         * Finds the longest common subsequence
         *@return the longest common subsequence
         */
        public ActionNode longestSubsequence(ActionNode other)
        {
            ActionNode root = this;
            int length = 1;

            ActionNode current = other;
            ActionNode curSubSeq = null;
            ActionNode curChildren = null;
            int curLength = 0;
            while (current != null)
            {
                //build the subseq
                if (curSubSeq == null)
                {
                    curChildren = findNode(current);
                    if (curChildren != null)
                    {
                        curSubSeq = curChildren.copy();
                        curLength = 1;
                    }
                }
                else
                {
                    bool childFound = false;
                    for (int i = 0; i < curChildren.children.Count; i++)
                    {
                        if (current.equals(curChildren.children[i]))
                        {
                            curChildren = curChildren.children[i];
                            curSubSeq.addLeaf(curChildren);
                            curLength++;
                            childFound = true;
                            break;
                        }
                    }
                    if (!childFound)
                    {
                        curSubSeq = null;
                        curLength = 0;
                    }
                    // this is the new longest subsequence
                    else if (curLength > length)
                    {
                        root = curSubSeq;
                        length = curLength;
                    }
                }

                // the other list should be a unary tree
                if (current.children.Count != 0)
                {
                    current = current.children[0];
                }
                else
                {
                    current = null;
                }
            }

            return root;
        }

        /**
         * Merge a training session with this
         *@param training the training to merge
         */
        public void merge(ActionNode training)
        {
            ActionNode lcs = longestSubsequence(training);
            // somehow... merge
        }
    }
}
