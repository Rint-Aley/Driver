using System;
using System.Collections.Generic;

namespace KeypadDriver.DataTypes
{
    class NodeOfCombination
    {
        public string keys;
        public int delay;
        public NodeOfCombination nextNode = null;

        public NodeOfCombination(string keys, int delay)
        {
            this.keys = keys;
            this.delay = delay;
        }
    }
    internal class Combination
    {
        private NodeOfCombination headNode;
        private NodeOfCombination tailNode;
        private int lenght;
        public Combination(string sCombination) 
        {
            string[] aElementsOfCombination = sCombination.Split(' ');
            
            //This for loop was made for avoiding if construction:)
            for (int i = 0; i < aElementsOfCombination.Length / 2; i += 2)
            {
                add(aElementsOfCombination[i], aElementsOfCombination[i += 1]); //Add all elements, except the last
            }
            add(aElementsOfCombination[aElementsOfCombination.Length - 1]); //Add the last element
        }
        public void add(string keys, string sDeley = "")
        {
            lenght++;
            string processedKeys = keysParser(keys);
            int iDelay = 0;
            try { iDelay = Convert.ToInt32(sDeley); } 
            catch { }
            NodeOfCombination newNode = new NodeOfCombination(processedKeys, iDelay);
            if(headNode != null)
            {
                tailNode.nextNode = newNode;
                tailNode = newNode;
            }
            else
            {
                headNode = newNode;
                tailNode = newNode;
            }
            
        }
        public NodeOfCombination getCombination()
        {
            if(headNode == null)
            {
                return null;
            }
            lenght--;
            var result = headNode;
            headNode = headNode.nextNode;
            return result;
        }
        public int getLength() { return lenght; }
        private string keysParser(string unparsedKeys)
        {
            unparsedKeys = unparsedKeys.ToLower();//pointless
            string[] elementsOfUnparsedKeys = unparsedKeys.Split('|');
            string result = "";
            for(int i = 0; i < elementsOfUnparsedKeys.Length; i++)
            {
                var tempWord = searchingSpeshalWords(elementsOfUnparsedKeys[i]);
                result += tempWord;
            }
            return result;
        }
        //TODO: destroy part of code below
        private string searchingSpeshalWords(string word)
        {
            switch (word)
            {
                case "shift":
                    return "+";
                case "ctrl":
                    return "^";
                case "alt":
                    return "%";
                default:
                    return "{" + word + "}";
            }
            
        }
    }
}
