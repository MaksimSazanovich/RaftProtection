using System;

namespace Internal.Scripts.LocalStorage
{
    [Serializable]
    public class Progress
    {
        public int index;

        public Progress(int index)
        {
            this.index = index;
        }
    }
}