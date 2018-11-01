using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InteractiveCollages
{
    public class CollageMaker
    {
        private List<string> _collagesPaths;

        public string GetRandomCollage()
        {
            _collagesPaths = Directory.GetFiles("../../Resources/Collages", "*.*", SearchOption.TopDirectoryOnly)
                .ToList();

            var rand = new Random();
            var randomPath = _collagesPaths[rand.Next(0, _collagesPaths.Count)];

            return randomPath;
        }
    }
}