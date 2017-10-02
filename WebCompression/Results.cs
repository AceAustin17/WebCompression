/* Code adapted from
 * https://en.wikipedia.org/wiki/Data_compression_ratio
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompression
{
    class Results
    {
        private long original;
        private long compressed;
        public Results(long original, long compressed)
        {
            this.original = original;
            this.compressed = compressed;
        }

        public string ShowRatio()
        {
            double ratio = original / (double)compressed;

            return "Compression Ratio = " + Math.Round(ratio,1) + ":1";
        }
        public string ShowSavedData()
        {
            double saved = (1 - (compressed / (double)original))* 100;

            return "Saved Space = " + Math.Round(saved) + "%";
        }


    }
}
