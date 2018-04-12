using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmCalculationClassLibrary
{
    class BandValueDictionary
    {
        public Dictionary<string, int> bandsAB_SignifacantFigure;
        public Dictionary<string, int> bandC_DecimalMultiplier;
        //public Dictionary<string, double> bandD_Tolerance;
        private static BandValueDictionary instance;
        private static readonly object padlock = new object();

        /// <summary>
        /// BandValueHolder is a singleton that contains dictionaries of
        /// Bands A/B signifacants, Band C decimal multiplier and 
        /// Band D tolerance.
        /// </summary>
        private BandValueDictionary() {
            // Bands A and B Signifacant Figures; repeating color/values for both.
            bandsAB_SignifacantFigure = new Dictionary<string, int> {

                {"black", 0},
                {"brown", 1},
                {"red", 2},
                {"orange", 3},
                {"yellow", 4},
                {"green", 5},
                {"blue", 6},
                {"violet", 7},
                {"gray", 8},
                {"white", 9}
            };

            // Bands C Signifacant Figures.
            bandC_DecimalMultiplier = new Dictionary<string, int> {
                {"pink", -3},
                {"silver", -2},
                {"gold", -1},
                {"black", 0},
                {"brown", 1},
                {"red", 2},
                {"orange",3},
                {"yellow", 4},
                {"green", 5},
                {"blue", 6},
                {"violet", 7},
                {"gray", 8},
                {"white", 9}
            };

            // Bands D Tolerance Values.
            /*bandD_Tolerance = new Dictionary<string, double> {
                {"silver", 0.10},
                {"gold", 0.05},
                {"brown", 0.01},
                {"red", 0.02},
                {"yellow", 0.05},
                {"green", 0.005},
                {"blue", 0.0025},
                {"violet", 0.001},
                //TO-DO: I need to see why grey can be both .0005 and .10 per wiki documentation
                {"gray", 0.0005},
            };*/
        }

        public static BandValueDictionary Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BandValueDictionary();
                    }
                    return instance;
                }
            }
        }       
    }
}