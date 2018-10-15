using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveCollages
{
    static class CollageMaker
    {
        private static List<string> _collagesPaths;
        public static string GetRandomCollage()
        {
            _collagesPaths = Directory.GetFiles("../../Resources/Collages", "*.*", SearchOption.TopDirectoryOnly).ToList();

            Random rand = new Random();
            string randomPath = _collagesPaths[rand.Next(0, _collagesPaths.Count)];

            return randomPath;
        }

    }
}
