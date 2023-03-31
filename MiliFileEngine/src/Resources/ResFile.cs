using System.Collections.Generic;

namespace MiliFileEngine.src.Resources
{
    internal class ResFile
    {
        private string name;
        private string hash;

        internal ResFile(string name,string hash) {
            this.name = name;
            this.hash = hash;
        }

        internal string GetName() {
            return name;
        }

        internal string Gethash() {
            return hash;
        }

        public static implicit operator KeyValuePair<string,string>(ResFile resFile) {
            return new KeyValuePair<string, string> (resFile.name,resFile.hash);
        }
    }
}
