using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.puzzle;

/**
 * Node for learning things
 */
namespace CS8803AGA.learning
{
    /// <summary>
    /// NOTE: first node in KNOWLEDGE SET should be an EMPTY node.
    /// (this is so merge will work properly)
    /// </summary>
    class ActionNode
    {
        private List<ActionNode> children; /**< the next nodes */
        private List<ActionNode> parents; /**< the previous nodes */
        private PuzzleObject data; /**< whatever the data of the node is */
        public static PuzzleObject EMPTY = new EmptyPuzzleObject(); /**< empty data node */

        private int id; /**< unique id */
        private static int next_id = 0; /**< next id */

        /**
         * Creates a new node
         *@param _data the data
         */
        public ActionNode(PuzzleObject _data)
        {
            data = _data.copy();
            id = next_id;
            next_id++;
            children = new List<ActionNode>();
            parents = new List<ActionNode>();
        }

        /**
         * Adds a child node
         */
        public void addChild(ActionNode child) 
        {
            children.Add(child);
            child.parents.Add(this);
        }

        /**
         * Adds a leaf node (training constsruction)
         */
        public void addLeaf(ActionNode child)
        {
            ActionNode current = this;
            while (current.children.Count != 0)
            {
                current = current.children[0];
            }
            current.addChild(child);
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
         * Finds a node that is yet to be visited and returns it
         *@param node the node to find
         *@param visited nodes that have already been visited
         *@return null if the node doesn't exist, or the node
         */
        public ActionNode findNode(ActionNode node, List<int> visited)
        {
            List<ActionNode> next = new List<ActionNode>();
            next.Add(this);
            for (int i = 0; i < next.Count; i++)
            {
                if (!visited.Contains(next[i].id) && node.equals(next[i]))
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
            return(node.data.equals(data));
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
         * Merge a training session with this
         *@param training the training to merge
         */
        public void merge(ActionNode training)
        {
            ActionNode current = this;
            List<int> visited = new List<int>();
            // somehow... merge
            for (ActionNode i = training; i != null;)
            {
                ActionNode next = findNode(i, visited);
                if (next == null) // add i as child of current, update current to..???
                {
                    next = i.copy();
                }
                bool hasChild = false;
                for (int j = 0; j < current.children.Count; j++)
                {
                    if (next.equals(current.children[j]))
                    {
                        hasChild = true;
                        break;
                    }
                }
                if (!hasChild)
                {
                    current.addChild(next);
                }
                current = next;

                // populate visited list (magically)
                List<ActionNode> ancestors = new List<ActionNode>();
                for (int j = 0; j < current.parents.Count; j++)
                {
                    ancestors.Add(current.parents[j]);
                }
                for (int j = 0; j < ancestors.Count; j++)
                {
                    if (!visited.Contains(ancestors[j].id))
                    {
                        visited.Add(ancestors[j].id);
                    }
                    for (int k = 0; k < ancestors[j].parents.Count; k++)
                    {
                        ancestors.Add(ancestors[j].parents[k]);
                    }
                }

                if (i.children.Count != 0)
                {
                    i = i.children[0];
                }
                else
                {
                    i = null;
                }
            }
        }

        /**
         * Somehow tries to print the thingy
         */
        public void debugPrint()
        {
            List<ActionNode> next = new List<ActionNode>();
            next.Add(this);
            for (int i = 0; i < next.Count; i++)
            {
                if (next[i].data.equals(EMPTY))
                {
                    Console.Write("{}");
                }
                else
                {
                    Console.Write(next[i].data);
                }
                Console.Write("(" + next[i].id + ") children: [");

                for (int j = 0; j < next[i].children.Count; j++)
                {
                    if (next[i].children[j].data.equals(EMPTY))
                    {
                        Console.Write("{}, ");
                    }
                    else
                    {
                        Console.Write(next[i].children[j].data+"("+next[i].children[j].id+"), ");
                    }
                    next.Add(next[i].children[j]);
                }
                Console.WriteLine("]");
            }
        }
    }
}
