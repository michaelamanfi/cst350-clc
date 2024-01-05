using System;

namespace Minesweeper.Common
{   
    /// <summary>
    /// This class provides reusable methods for iterating 2D arrays.
    /// </summary>
    public class Iterator
    { /// <summary>
      /// A delegate to perform action for the specified button (r, and c).
      /// </summary>
      /// <param name="r"></param>
      /// <param name="c"></param>
        public delegate void GridAction(int r, int c);

        /// <summary>
        /// A delegate to determine if GridAction should be performed 
        /// for the specified button (r, and c).
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public delegate bool GridActionEval(int r, int c);

        /// <summary>
        /// Perform action for each button coordinate while optionally skipping some.
        /// </summary>
        /// <param name="size">The size of the 2D array</param>
        /// <param name="action">The action delegate</param>
        /// <param name="actionEval">The evalution delegate</param>
        public static void ForEachContinueOnFalse(int size,GridAction action, GridActionEval actionEval)
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (actionEval != null && !actionEval(row, col))
                        continue;

                    action?.Invoke(row, col);
                }
            }
        }

        /// <summary>
        /// Evaluate each button coordinate and terminate the iteration on a false return.
        /// </summary>
        /// <param name="size">The size of the 2D array</param>
        /// <param name="actionEval">The evalution delegate</param>
        /// <returns></returns>
        public static bool ForEachTermiateOnFalse(int size, GridAction action, GridActionEval actionEval)
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (actionEval != null && !actionEval(row, col))
                        return false;                    

                    action?.Invoke(row, col);
                }
            }
            return true;
        }
    }
}
