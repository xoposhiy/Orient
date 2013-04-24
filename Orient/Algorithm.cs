using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orient
{
    class Algorithm
    {
        public Algorithm(string file) {
            var state = new MainFormState(file);
            if (!state.Criteria90()) {
                state.Dispose();
                state = new MainFormState(state.Rotate());
            }
            var skew = state.FilteredLines.Average(line => line.LinearRegression().Skew());
            state.Dispose();
            state = new MainFormState(state.Rotate(skew));
        }
    }
}
