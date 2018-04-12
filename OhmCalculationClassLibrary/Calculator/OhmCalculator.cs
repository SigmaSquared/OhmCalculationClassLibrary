using OhmCalculationClassLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;

namespace OhmCalculationClassLibrary
{
    public class OhmCalculator : IOhmValueCalculator
    {
        /// <summary>
           /// Calculates the Ohm value of a resistor based on the band colors.
           /// </summary>
           /// <param name="bandAColor">The color of the first figure of component value band.</param>
           /// <param name="bandBColor">The color of the second significant figure band.</param>
           /// <param name="bandCColor">The color of the decimal multiplier band.</param>
           /// <param name="bandDColor">The color of the tolerance value band.</param>

        public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            //TODO: perhaps multithreading?

            //TOO: ERROR HANDLING FOR DICTIONARY LOOKUP!!!!!!!!!!!!!!!!!!
            //TOO: IFormatterService - need to format these returned values!!!!!!!!!!!!!!!!!!

            int ohmCalculated = 0;

            if (ValuesAreEmptyOrNull(bandAColor, bandBColor, bandCColor, bandDColor))
            {
                throw new BandCantBeEmptyOrNullException();
                // get color values from dictionaries, turn them into strings, append bandA + bandB and turn the string into an int.
                // TODO: Create static class instead of a using singleton...but would it work better for multithreading               
            }
            else
            {
                String bandAString = "";
                String bandBString = "";
                try
                {
                    bandAString = string.Format("{0}", BandValueDictionary.Instance.bandsAB_SignifacantFigure[bandAColor]);
                    bandBString = string.Format("{0}", BandValueDictionary.Instance.bandsAB_SignifacantFigure[bandBColor]);
                }
                catch (KeyNotFoundException e)
                {
                    throw new KeyNotFoundException("Sorry, you cant get in with no key...quandary...we're not suppose to be here");
                }
                if (Int32.TryParse((bandAString + bandBString), out int n))
                {
                    int ohmResistanceValue = Convert.ToInt32(bandAString + bandBString);
                    int bandCMultiplier = BandValueDictionary.Instance.bandC_DecimalMultiplier[bandCColor];
                    ohmCalculated = (ohmResistanceValue * (int)Reducer((Math.Pow(10, bandCMultiplier))));// gets absolute value and removes 3 zeros for trillions and billions 
                }
            }
            return ohmCalculated;
        }
   	    /// <summary>
        /// UI Shouldnt allow it...but never know.
        /// </summary>
        private bool ValuesAreEmptyOrNull(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            if (string.IsNullOrWhiteSpace(bandAColor))
                return true;
            else if (string.IsNullOrWhiteSpace(bandBColor))
                return true;
            else if (string.IsNullOrWhiteSpace(bandCColor))
                return true;
            else if (string.IsNullOrWhiteSpace(bandDColor))
                return true;
            else return false;
        }
        /// <summary>
        /// Reduces because ints dont go as high as the calculations and we are retruning an int.
        /// </summary>
        private double Reducer(double result)
        {
            if (result >= 1000000000)
                return result / 1000;
            else if (result >= 1000000)
                return result / 1000;
            else
                return result;
        }
    }
}
